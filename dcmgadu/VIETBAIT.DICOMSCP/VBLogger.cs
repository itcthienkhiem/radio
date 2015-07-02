using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using NLog;

namespace VIETBAIT.DICOMSCP
{
     
    public struct LogInfo
    {
        public LogLevel Level;
        public string Message;
        public int ThreadId;
        public DateTime Time;
    }

    public enum LogLevel
    {
        /// <summary>
        /// Debug log level.
        /// </summary>
        Debug,
        /// <summary>
        /// Info log level.
        /// </summary>
        Info,
        /// <summary>
        /// Warning log level.
        /// </summary>
        Warn,
        /// <summary>
        /// Error log level.
        /// </summary>
        Error,
        /// <summary>
        /// Fatal log level.
        /// </summary>
        Fatal
    }

    public static class VBLogger
    {
        //private static Logger _vbLogger = LogManager.GetCurrentClassLogger();
        private static Logger _vbLogger;
        private static TextBox _tb;
        private static NetworkStream _netStream;
        private static string _logip;
        private static string _logPort;

        public static void InitLogger(string plogIp, string plogPort)
        {
            //try
            //{
            //    if (_logip != plogIp) _logip = plogIp;
            //    if (_logPort != plogPort) _logPort = plogPort; 

            //    var client = new TcpClient();
            //    client.Connect(IPAddress.Parse(_logip), Convert.ToInt32(_logPort));
            //    _netStream = client.GetStream();
            //}
            //catch (Exception)
            //{
            //}

            //New log init using NLog
            try
            {
                _vbLogger = LogManager.GetCurrentClassLogger();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            
           
        }

        public static void Log(LogInfo info)
        {
            var sb = new StringBuilder();
            sb.AppendLine();
            sb.AppendFormat("({0})|{1}|{2}|({3})|{4}", info.ThreadId,
                            info.Time.ToString("dd/MM/yyyy"), info.Time.ToString("H:mm:ss"),
                            info.Level, info.Message);

            _vbLogger.Debug(sb.ToString);

        }

        private static void AppendToTextBox(string info)
        {
            if (_tb == null)
                return;

            if (!_tb.InvokeRequired)
            {
                _tb.AppendText(info);
            }
            else
            {
                _tb.BeginInvoke(new Action<string>(AppendToTextBox), new object[] {info});
            }
        }

        private static void Log(LogLevel level, Exception e, string message, params object[] formatArgs)
        {
            if (String.IsNullOrEmpty(message))
                return;

            var builder = new StringBuilder();
            builder.AppendFormat(message, formatArgs);
            if (e != null)
            {
                builder.AppendLine();
                builder.Append(e);
            }

            var info = new LogInfo
                           {
                               Level = level,
                               Message = builder.ToString(),
                               ThreadId = Thread.CurrentThread.ManagedThreadId,
                               Time = DateTime.Now
                           };

            Log(info);
        }

        public static void RegisterLogHandler(TextBox tb)
        {
            _tb = tb;
        }

        //public static void RegisterLogHandler(NetworkStream ns)
        //{
        //    _netStream = ns;
        //}

        public static void LogError(string message, params object[] formatArgs)
        {
            Log(LogLevel.Error, null, message, formatArgs);
        }

        public static void LogErrorException(Exception e, string message, params object[] formatArgs)
        {
            Log(LogLevel.Error, e, message, formatArgs);
        }

        public static void LogInfo(string message, params object[] formatArgs)
        {
            Log(LogLevel.Info, null, message, formatArgs);
        }

        public static void LogWarn(string message, params object[] formatArgs)
        {
            Log(LogLevel.Warn, null, message, formatArgs);
        }

        public static void LogDebug(string message, params object[] formatArgs)
        {
            Log(LogLevel.Debug, null, message, formatArgs);
        }
    }
}