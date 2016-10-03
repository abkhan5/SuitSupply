using System;
using System.IO;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using SuitSupply.Core.Messaging;

namespace SuitSupply.Core.Azure
{
    public class SubscriptionReceiver : IMessageReceiver
    {
        private readonly CancellationTokenSource _cancellationSource;
        private readonly SubscriptionClient _client;
        private readonly object _lockObject = new object();
        private readonly string _subscription;
        private readonly TokenProvider _tokenProvider;
        private readonly string _topic;
        private readonly ICommandHandlerRegistery _handler;

        public SubscriptionReceiver(string topic, string subscription, string tokenIssuer, string tokenAccessKey,
            string serviceUriScheme, string serviceNamespace, string servicePath,
            ICommandHandlerRegistery handler)
        {
            _topic = topic;
            _subscription = subscription;
            _handler = handler;
            //_instrumentation = instrumentation;

            _tokenProvider = TokenProvider.CreateSharedAccessSignatureTokenProvider(tokenIssuer,
                tokenAccessKey);
            var serviceUri = ServiceBusEnvironment.CreateServiceUri(serviceUriScheme, serviceNamespace,
                servicePath);

            var messagingFactory = MessagingFactory.Create(serviceUri, _tokenProvider);
            _client = messagingFactory.CreateSubscriptionClient(topic, subscription, ReceiveMode.ReceiveAndDelete);
            _client.PrefetchCount = 18;
            _cancellationSource = new CancellationTokenSource();
        }

        public void Start()
        {
            Start(MessageHandler);
        }

        public void Stop()
        {
        }


        /// <summary>
        ///     Starts the listener.
        /// </summary>
        public void Start(Action<BrokeredMessage> messageHandler)
        {
            lock (_lockObject)
            {
                Task.Factory.StartNew(() =>
                            ReceiveMessages(messageHandler),
                    _cancellationSource.Token);
            }
        }

        /// <summary>
        ///     Receives the messages in an endless asynchronous loop.
        /// </summary>
        private void ReceiveMessages(Action<BrokeredMessage> messageHandler)
        {
            var options = new OnMessageOptions();
            options.AutoComplete = false;
            options.AutoRenewTimeout = TimeSpan.FromMinutes(1);
            _client.OnMessage(msg => { messageHandler(msg); }, options);
        }

        private void MessageHandler(BrokeredMessage message)
        {
            var serializer = new JsonTextSerializer();

            using (var stream = message.GetBody<Stream>())
            {
                using (var reader = new StreamReader(stream))
                {
                    try
                    {
                        var payLoad = serializer.Deserialize(reader) as ICommand;
                        _handler.ProcessCommand(payLoad);
                    }
                    catch (SerializationException e)
                    {
                    }
                }
            }
        }
    }
}