using SuitSupply.Core.Messaging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
                CreateSharedAccessSignatureTokenProvider(tokenIssuer, tokenAccessKey);
            this.serviceUri = ServiceBusEnvironment.
                CreateServiceUri(serviceUriScheme,
                    serviceNamespace, servicePath);

            var factory = MessagingFactory.Create(this.serviceUri, this.tokenProvider);
            this.topicClient = factory.CreateTopicClient(this.topic);
        }

        public void Send(
            Func<ICommand> messageFactory,
            Action callBackOnSuccessFull, 
            Action<Exception> callBackOnFail)
        {
            try
            {
                var command = messageFactory();
                var payload = BuildMessage(command);
                this.topicClient.Send(payload);
                callBackOnSuccessFull();
            }
            catch (Exception ex)
            {
                callBackOnFail(ex);
            }
        }

        private BrokeredMessage BuildMessage(ICommand command)
        {
            var stream = new MemoryStream();
            try
            {
                var writer = new StreamWriter(stream);
                JsonTextSerializer serializer= new JsonTextSerializer();
                serializer.Serialize(writer, command);
                stream.Position = 0;

                var message = new BrokeredMessage(stream, true);
                return message;
            }
            catch
            {
                stream.Dispose();
                throw;
            }
        }
    }
}
