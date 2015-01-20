﻿using System;
using IntegrationEngine.Model;
using Quartz;

namespace IntegrationEngine.Scheduler
{
    public interface IEngineScheduler
    {
        IScheduler Scheduler { get; set; }
        void Start();
        void ScheduleJobWithCronTrigger(CronTrigger triggerDefinition);
        void ScheduleJobWithSimpleTrigger(SimpleTrigger triggerDefinition);
        bool IsJobTypeRegistered(string jobTypeName);
        Type GetRegisteredJobTypeByName(string jobTypeName);
        void Shutdown();
    }
}
