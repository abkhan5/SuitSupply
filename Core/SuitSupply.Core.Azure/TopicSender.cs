using SuitSupply.Core.Messaging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

namespace SuitSupply.Core.Azure
{
    public class TopicSender : IMessageSender
    {
        private readonly TokenProvider tokenProvider;
        private readonly Uri serviceUri;
        private readonly string topic;
        private readonly RetryPolicy retryPolicy;
        private readonly TopicClient topicClient;

       

        /// <summary>
        /// Initializes a new instance of the <see cref="TopicSender"/> class, 
        /// automatically creating the given topic if it does not exist.
        /// </summary>
        protected TopicSender(string topic,
            string tokenIssuer,
            string tokenAccessKey,
            string serviceUriScheme,
            string serviceNamespace,
            string servicePath)
        {
            this.topic = topic;

            this.tokenProvider = TokenProvider.
                CreateSharedSecretTokenProvider(tokenIssuer,tokenAccessKey);
            this.serviceUri = ServiceBusEnvironment.
                CreateServiceUri(serviceUriScheme, 
                serviceNamespace, servicePath);

            var factory = MessagingFactory.Create(this.serviceUri, this.tokenProvider);
            this.topicClient = factory.CreateTopicClient(this.topic);
        }

        /// <summary>
        /// Notifies that the sender is retrying due to a transient fault.
        /// </summary>

        /// <summary>
        /// Asynchronously sends the specified message.
        /// </summary>
        public void SendAsync(Func<BrokeredMessage> messageFactory)
        {
            // TODO: SendAsync is not currently being used by the app or infrastructure.
            // Consider removing or have a callback notifying the result.
            // Always send async.
            this.SendAsync(messageFactory, () => { }, ex => { });
        }
        

        public void SendAsync(Func<BrokeredMessage> messageFactory, Action successCallback, Action<Exception> exceptionCallback)
        {
            this.retryPolicy.ExecuteAction(
                ac => this.DoBeginSendMessage(messageFactory(), ac),
                this.DoEndSendMessage,
                successCallback,
                ex =>
                {
                    Trace.TraceError("An unrecoverable error occurred while trying to send a message:\r\n{0}", ex);
                    exceptionCallback(ex);
                });
        }

        public void Send(Func<BrokeredMessage> messageFactory)
        {
            var resetEvent = new ManualResetEvent(false);
            Exception exception = null;

            this.SendAsync(
                messageFactory,
                () => resetEvent.Set(),
                ex =>
                {
                    exception = ex;
                    resetEvent.Set();
                });

            resetEvent.WaitOne();
            if (exception != null)
            {
                throw exception;
            }
        }

        protected virtual void DoBeginSendMessage(BrokeredMessage message, AsyncCallback ac)
        {
            try
            {
                this.topicClient.BeginSend(message, ac, message);
            }
            catch
            {
                message.Dispose();
                throw;
            }
        }

        protected virtual void DoEndSendMessage(IAsyncResult ar)
        {
            try
            {
                this.topicClient.EndSend(ar);
            }
            finally
            {
                using (ar.AsyncState as IDisposable) { }
            }
        }

        public void Send(Func<IMessage> messageFactory)
        {
            throw new NotImplementedException();
        }
    }
}
