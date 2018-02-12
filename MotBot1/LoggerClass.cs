using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace MotBot1
{
    public class LoggerClass
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public static void LogError(string msg)
        {
            logger.Error("Error message", msg);
        }

        public static void LogInfo(string msg)
        {
            logger.Info("Info", msg);
        }
    }
}
