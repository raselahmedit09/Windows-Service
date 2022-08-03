using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstWindowsService
{
    public class LoggerUtility
    {
        protected static log4net.ILog _Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public const string LogInfo = "info";
        public const string LogError = "error";

        public static void LogServiceMessage(string targetLog, string message)
        {
            if (targetLog.Equals(LogInfo))
            {
                _Logger.InfoFormat("{0}" + Environment.NewLine + "{1}", DateTime.Now, message);
            }
            else
            {
                _Logger.ErrorFormat("{0}" + Environment.NewLine + "{1}", DateTime.Now, message);
            }
        }
    }
}
