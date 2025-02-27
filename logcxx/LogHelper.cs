using System;

namespace log4cxx
{
    class LogHelper
    {
        private static readonly log4net.ILog loginfo = log4net.LogManager.GetLogger("LogInfo");
        public static void Info(string info)
        {
            if (loginfo.IsInfoEnabled)
            {
                loginfo.Info(info);
            }
        }
        public static void Info(string excptionType, string message, string stackTrace)
        {
            if (loginfo.IsInfoEnabled)
            {
                loginfo.ErrorFormat("{0}:{1}\n{2}", excptionType, message, stackTrace);
            }
        }
        public static void Warn(string info)
        {
            if (loginfo.IsWarnEnabled)
            {
                loginfo.Warn(info);
            }
        }
        public static void Debug(string info)
        {
            if (loginfo.IsDebugEnabled)
            {
                loginfo.Debug(info);
            }
        }
        public static void Error(Exception ex)
        {
            if (loginfo.IsErrorEnabled)
            {
                loginfo.Error(ex);
            }
        }
        public static void Error(object message)
        {
            if (loginfo.IsErrorEnabled)
            {
                loginfo.Error(message);
            }
        }
        public static void Error(string info, Exception ex)
        {
            if (loginfo.IsErrorEnabled)
            {
                loginfo.Error(info, ex);
            }
        }
    }
}
