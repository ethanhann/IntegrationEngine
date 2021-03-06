﻿using System;
using System.Collections.Generic;
using System.Threading;
using Common.Logging;
using InEngine.Core.IO;
using InEngine.Core.Queuing.Message;

namespace InEngine.Core.Queuing.Clients
{
    public class SyncClient : IQueueClient
    {
        public MailSettings MailSettings { get; set; }

        public ILog Log { get; set; } = LogManager.GetLogger<SyncClient>();
        public int Id { get; set; } = 0;
        public string QueueBaseName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string QueueName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool UseCompression { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Publish(AbstractCommand command)
        {
            command.Run();
        }

        public void Recover()
        {}

        public void Consume(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public ICommandEnvelope Consume()
        {
            throw new NotImplementedException();
        }

        public bool ClearFailedQueue()
        {
            throw new NotImplementedException();
        }

        public bool ClearInProgressQueue()
        {
            throw new NotImplementedException();
        }

        public bool ClearPendingQueue()
        {
            throw new NotImplementedException();
        }

        public List<ICommandEnvelope> PeekFailedMessages(long from, long to)
        {
            throw new NotImplementedException();
        }

        public List<ICommandEnvelope> PeekInProgressMessages(long from, long to)
        {
            throw new NotImplementedException();
        }

        public List<ICommandEnvelope> PeekPendingMessages(long from, long to)
        {
            throw new NotImplementedException();
        }

        public void RepublishFailedMessages()
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, long> GetQueueLengths()
        {
            return new Dictionary<string, long>();
        }
    }
}
