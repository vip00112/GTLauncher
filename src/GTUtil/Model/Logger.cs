using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTUtil
{
    public static class Logger
    {
        private static readonly NLog.Logger _errorLogger;

        static Logger()
        {
            _errorLogger = LogManager.GetLogger("ErrorLog");
        }

        public static void Error(Exception e, string msg = null)
        {
            if (msg != null)
            {
                _errorLogger.Error(e, msg);
            }
            else
            {
                _errorLogger.Error(e);
            }
        }
    }
}
