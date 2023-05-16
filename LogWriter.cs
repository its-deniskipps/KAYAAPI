using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Reflection;

namespace KAYAAPI
{
    public class LogWriter
    {
        private string m_exePath = string.Empty;
        //public LogWriter(string logMessage)
        //{
        //    LogWrite(logMessage);
        //}
        public void LogWrite(string logKey, string logMessage)
        {
            try
            {
                string filename = @"C:\Dennis\kayalog\" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + "log.txt"; 
                using (StreamWriter w = File.AppendText(filename))
                {
                    Log(logKey, logMessage, w);
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void Log(string logKey, string logMessage, TextWriter txtWriter)
        {
            try
            {
                txtWriter.Write("{0} {1}", DateTime.Now.ToLongDateString(), DateTime.Now.ToLongTimeString());
                txtWriter.Write("  >>");
                txtWriter.WriteLine(" {0} : {1}", logKey, logMessage);
            }
            catch (Exception ex)
            {
            }
        }
    }
}