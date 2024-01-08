using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLog;

namespace PushService_Umeko
{
    public class NLogHelper
    {
        public static NLogHelper logger = new NLogHelper("normal");
        public static NLogHelper failLogger = new NLogHelper("fail");

		protected Logger _Log;
		public NLogHelper(string logName)
		{
            _Log = LogManager.GetLogger(logName);
		}

		public void Info(string msg)
		{
			_Log.Info(msg);
		}
		public void Warn(string msg)
		{
			_Log.Warn(msg);
		}
		public void Warn(Exception ex, string msg)
		{
			_Log.Warn(ex, msg);
		}
		public void Debug(string msg)
		{
			_Log.Debug(msg);
		}
		public void Error(string msg)
		{
			_Log.Error(msg);
		}
		public void Error(Exception ex, string msg)
		{
			_Log.Error(ex, msg);
		}
		public void Trace(string msg)
		{
			_Log.Trace(msg);
		}
		public void Trace(Exception ex, string msg)
		{
			_Log.Trace(ex, msg);
		}
    }
}