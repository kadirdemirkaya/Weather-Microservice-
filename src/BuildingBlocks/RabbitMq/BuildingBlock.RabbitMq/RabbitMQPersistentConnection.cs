﻿using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System.Net.Sockets;

namespace BuildingBlock.RabbitMq
{
    public class RabbitMQPersistentConnection : IDisposable
    {
        private IConnection connection;
        private readonly IConnectionFactory ConnectionFactory;
        private object lock_object = new object();
        private readonly int RetryCount;
        private bool _disposed;
        private ConnectionFactory connectionConfig;

        public RabbitMQPersistentConnection(IConnectionFactory connectionFactory, int retryCount = 5)
        {
            ConnectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
            RetryCount = retryCount;
        }

        public RabbitMQPersistentConnection(IConnectionFactory connectionFactory, ConnectionFactory connectionConfig, int retryCount = 5)
        {
            ConnectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
            RetryCount = retryCount;
            connection = connectionConfig.CreateConnection();
        }

        public IConnection GetConnection
        {
            get
            {
                lock (lock_object)
                {
                    return connection;
                }
            }
        }

        public bool IsConnected => connection != null && connection.IsOpen;

        public IModel CreateModel() => connection.CreateModel();

        public void Dispose()
        {
            _disposed = true;
            connection?.Dispose();
        }

        public bool TryConnect()
        {
            lock (lock_object)
            {
                var policy = Policy.Handle<SocketException>()
                    .Or<BrokerUnreachableException>()
                    .WaitAndRetry(RetryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                    {

                    }
                );

                policy.Execute(() =>
                {
                    connection = ConnectionFactory.CreateConnection();
                    connection.CallbackException += Connection_CallbackException;
                    connection.ConnectionBlocked += Connection_ConnectionBlocked;
                });

                if (IsConnected)
                {
                    connection.ConnectionShutdown += Connection_ConnectionShutdown;
                    return true;
                }

                return false;
            }
        }

        private void Connection_ConnectionBlocked(object sender, global::RabbitMQ.Client.Events.ConnectionBlockedEventArgs e)
        {
            if (_disposed) return;

            TryConnect();
        }

        private void Connection_CallbackException(object sender, global::RabbitMQ.Client.Events.CallbackExceptionEventArgs e)
        {
            if (_disposed) return;

            TryConnect();
        }

        private void Connection_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            if (_disposed) return;

            TryConnect();
        }
    }
}
