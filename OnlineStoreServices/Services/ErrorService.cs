using System;
using System.IO;
using System.Text;

namespace OnlineStoreServices.Services
{
    class ErrorService
    {
        // Log an Exception 
        internal static void LogException(Exception ex)
        {
            string logFile = String.Format(@"D:\Microsoft Visual Studio 2015\Projects\OnlineStore\OnlineStoreServices\ErrorLogs{0}.txt", "\\" + DateTime.Now.ToString("M-d-yyyy_h.mm.sstt.FFFFFFF"));

            // Open the log file for append and write the log
            using (FileStream sw = File.Create(logFile))
            {
                Write(sw, String.Format("Timestamp: {0}", DateTime.Now));

                if (ex.InnerException != null)
                {
                    Write(sw, "Inner Exception Type: ");
                    Write(sw, "\r\n" + ex.InnerException.GetType().ToString());
                    Write(sw, "Inner Exception: ");
                    Write(sw, "\r\n" + ex.InnerException.Message);
                    Write(sw, "Inner Source: ");
                    Write(sw, "\r\n" + ex.InnerException.Source);
                    if (ex.InnerException.StackTrace != null)
                    {
                        Write(sw, "\r\n" + "Inner Stack Trace: ");
                        Write(sw, "\r\n" + ex.InnerException.StackTrace);
                    }
                }

                Write(sw, "Exception Type: ");
                Write(sw, "\r\n" + ex.GetType().ToString());
                Write(sw, "\r\n" + "Exception: " + ex.Message);
                Write(sw, "\r\n" + "Stack Trace: ");

                if (ex.StackTrace != null)
                {
                    Write(sw, "\r\n" + ex.StackTrace);
                    Write(sw, "\r\n");
                }
                sw.Close();
            }
        }

        private static void Write(FileStream fw, string text)
        {
            Byte[] info = new UTF8Encoding(true).GetBytes(text);
            fw.Write(info, 0, info.Length);
        }
    }
}
