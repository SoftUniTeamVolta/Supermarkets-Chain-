namespace SupermarketChain.Common
{
    using System;
    using System.IO;
    using System.Text;

    using Interfaces;
    using Utils;

    public class Logger : ILogger
    {
        private static volatile Logger instance;
        private static object SyncLock = new object();
        private static readonly string defaultName = "Logger";
        private static readonly string DefaultLogPath;

        static Logger()
        {
            ValidateDirectory();
            string userDefinedName = ConfigUtils.GetAppSetting("LogName");
            if (!string.IsNullOrEmpty(userDefinedName))
            {
                Logger.DefaultLogPath = string.Format("./logs/{0}.{1}.log", DateTime.Now.ToShortDateString(), userDefinedName);
            }
            else
            {
                Logger.DefaultLogPath = string.Format("./logs/{0}.{1}.log", DateTime.Now.ToShortDateString(), Logger.defaultName);
            }
        }

        private Logger()
        { }

        public static Logger Instance
        {
            get
            {
                if (Logger.instance == null)
                {
                    lock (Logger.SyncLock)
                    {
                        if (Logger.instance == null)
                        {
                            Logger.instance = new Logger();
                        }
                    }
                }

                return Logger.instance;
            }
        }

        public void Information(string message)
        {
            lock (Logger.SyncLock)
            {
                string msg = this.GetLogMessage(LogType.Information, message);

                this.SaveLogMessage(msg);
            }
        }

        public void Warning(string message)
        {
            lock (Logger.SyncLock)
            {
                string msg = this.GetLogMessage(LogType.Warning, message);

                this.SaveLogMessage(msg);
            }
        }

        public void Error(string message)
        {
            lock (Logger.SyncLock)
            {
                string msg = this.GetLogMessage(LogType.Error, message);

                this.SaveLogMessage(msg);
            }
        }

        public void Error(Exception exception, string message = "")
        {

            lock (Logger.SyncLock)
            {
                string msg = this.GetLogMessage(LogType.Error, message, exception.ToString());

                this.SaveLogMessage(msg);
            }
        }

        private string GetLogMessage(LogType logType, params string[] messages)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("{0} - {1}", logType, DateTime.Now.ToShortTimeString());
            builder.AppendLine();
            builder.AppendLine(new string('-', 25));
            foreach (string message in messages)
            {
                builder.AppendLine(message);
            }
            builder.AppendLine();

            return builder.ToString();
        }

        private void SaveLogMessage(string msg)
        {
            File.AppendAllText(Logger.DefaultLogPath, msg);
        }

        private static void ValidateDirectory()
        {
            if (Directory.Exists("logs"))
            {
                Directory.CreateDirectory("logs");
            }
        }
    }
}
