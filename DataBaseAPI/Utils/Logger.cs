using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace DataBaseAPI.Utils
{
	public class Logger
	{

		public static void GetLog( Exception ex, string logPath )
		{

            using (StreamWriter wr = new StreamWriter(logPath, true))
            {
                wr.Write("Data:");
                wr.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                wr.Write("Mensagem:");
                wr.WriteLine(ex.Message);
                wr.Write("StackTrace:");
                wr.WriteLine(ex.StackTrace);
            }
        }
	}
}