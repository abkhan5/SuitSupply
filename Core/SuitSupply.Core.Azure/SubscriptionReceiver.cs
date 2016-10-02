using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using SuitSupply.Core.Messaging;

namespace SuitSupply.Core.Azure
{
   public  class SubscriptionReceiver: IMessageReceiver
    {
        private readonly SubscriptionClient _client;
        private readonly string _subscription;
        private readonly object _lockObject = new object();
        private readonly TokenProvider _tokenProvider;
        private readonly string _topic;
        private readonly CancellationTokenSource _cancellationSource;
        private ICommandHandlerRegistery _handler;
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
            _cancellationSource= new CancellationTokenSource();
        }


        /// <summary>
        ///  Starts the listener.
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

        public void Start()
        {
            Start(MessageHandler);
        }

        private void MessageHandler(BrokeredMessage message)
        {
            JsonTextSerializer serializer = new JsonTextSerializer();

            using (var stream = message.GetBody<Stream>())
            {
                using (var reader = new StreamReader(stream))
                {
                    try
                    {
                        ICommand payLoad = serializer.Deserialize(reader) as ICommand;
                        _handler.ProcessCommand(payLoad);

                    }
                    catch (SerializationException e)
                    {
                    }
                }
            }
        }

        public void Stop()
        {
        }
    }
}
