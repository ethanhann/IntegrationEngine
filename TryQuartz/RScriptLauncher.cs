﻿using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using Nest;
using TryQuartz.Reports;

namespace TryQuartz
{
    public class RScriptLauncher
    {
        public IElasticClient ElasticClient { get; set; }

        public RScriptLauncher()
        {
            ElasticClient = ContainerSingleton.GetContainer().Resolve<IElasticClient>();
        }

        public void Run<T>(IReport<T> report)
        {
            var startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.FileName = "/usr/bin/Rscript";
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.Arguments = string.Format("RScripts/{0}.R", report.GetType().Name);
            startInfo.RedirectStandardOutput = true;

            var scriptOutput = "";
            using (var process = System.Diagnostics.Process.Start(startInfo))
            {
                scriptOutput = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
            }
            report.Data = JsonConvert.DeserializeObject<IList<T>>(scriptOutput);
            ElasticClient.Index(report, x => x.Type(report.GetType().Name));
        }
    }
}

