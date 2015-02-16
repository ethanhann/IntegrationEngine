﻿using IntegrationEngine.ConsoleHost.IntegrationPoints;
using IntegrationEngine.Core.Jobs;
using IntegrationEngine.Core.Mail;
using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace IntegrationEngine.ConsoleHost.Car
{
    public class CarMailMessageJob : IMailJob, IParameterizedJob
    {
        public IMailClient MailClient { get; set; }
        public IDictionary<string, string> Parameters { get; set; }

        public CarMailMessageJob()
        {
        }

        public CarMailMessageJob(IFooMailClient mailClient)
            : this()
        {
            MailClient = mailClient;
        } 

        public void Run()
        {
            var mailMessage = new MailMessage();
            mailMessage.To.Add("ethanhann@gmail.com");
            mailMessage.Subject = "Your car report is ready.";
            mailMessage.From = new MailAddress("root@localhost");
            mailMessage.Body = "Body content about cars.";
            MailClient.Send(mailMessage);
        }
    }
}

