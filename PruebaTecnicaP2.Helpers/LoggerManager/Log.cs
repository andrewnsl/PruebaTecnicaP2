using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaP2.Helpers.LoggerManager
{
    public class Log: ILog
    {
        public Log()
        {
            LogManager.LoadConfiguration(string.Concat(AppDomain.CurrentDomain.BaseDirectory, "nlog.config"));
        }

        /// <summary>
        /// Instance of NLog
        /// </summary>
        private static ILogger logger = LogManager.GetCurrentClassLogger();
        private string _class { get; set; } = null!;
        private string _method { get; set; } = null!;

        /// <summary>
        /// Error type message
        /// </summary>
        /// <param name="ex"></param>
        public void LogError(Exception ex)
        {
            MethodBase methodBase = new StackTrace().GetFrame(1)!.GetMethod()!;
            GetConfiguration(methodBase);

            string mensaje = ex.Message;
            Exception innerException = ex.InnerException!;
            while (innerException != null)
            {
                mensaje = $"{mensaje}; {innerException.Message}";
                innerException = innerException.InnerException!;
            }

            logger.Error($"{_class}.{_method}:\t{mensaje}\n{innerException}");
        }


        private void GetConfiguration(MethodBase methodBase)
        {

            _class = methodBase.ReflectedType!.Name.Split('<')[1].Split('>').FirstOrDefault()!;
            _method = methodBase.ReflectedType!.ReflectedType!.Name;
        }
    }
}
