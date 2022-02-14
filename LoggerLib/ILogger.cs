using System;
using System.Collections.Generic;
using System.Text;

namespace wpfLog
{
    interface ILogger
    {
        void ProcessLogMessage(string logMessage);
    }
}
