#region Namespace

using System;
using System.IO;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using SuitSupply.Core.Messaging;

#endregion

namespace SuitSupply.Core.Azure
{
    public class TopicSender : IMessageSender
    {
        private readonly RetryPolicy _retryPolicy;
        private readonly Uri _serviceUri;
        private readonly TokenProvider _tokenProvider;
        private readonly string _topic;
        private readonly TopicClient _topicClient;


        /// <summary>
        ///     Initializes a new instance of the <see cref="TopicSender" /> class,
        ///     automatically creating the given topic if it does not exist.
        /// </summary>
        public TopicSender(string topic,
            string tokenIssuer,
            string tokenAccessKey,
            string serviceUriScheme,
            string serviceNamespace,
            string servicePath)
        {
            _topic = topic;

            _tokenProvider = TokenProvider.
                CreateSharedAccessSignatureTokenProvider(tokenIssuer, tokenAccessKey);
            _serviceUri = ServiceBusEnvironment.
                CreateServiceUri(serviceUriScheme,
                    serviceNamespace, servicePath);

            var factory = MessagingFactory.Create(_serviceUri, _tokenProvider);
            _topicClient = factory.CreateTopicClient(_topic);
        }

        public void Send(ICommand command)
        {
            var payload = BuildMessage(command);
            _topicClient.Send(payload);
        }

        private BrokeredMessage BuildMessage(ICommand command)
        {
            var stream = new MemoryStream();
            try
            {
                var writer = new StreamWriter(stream);
                var serializer = new JsonTextSerializer();
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