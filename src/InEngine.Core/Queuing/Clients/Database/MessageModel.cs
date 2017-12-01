﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace InEngine.Core.Queuing.Clients.Database
{
    public class MessageModel : Message
    {
        public string Status { get; set; }
        public string QueueName { get; set; }
    }
}