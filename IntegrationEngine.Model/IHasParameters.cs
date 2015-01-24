﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationEngine.Model
{
    public interface IHasParameters
    {
        IDictionary<string, string> Parameters { get; set; }
    }
}
