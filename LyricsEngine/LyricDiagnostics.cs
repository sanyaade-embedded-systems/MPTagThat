using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace LyricsEngine
{
    public static class LyricDiagnostics
    {
        private static TraceSource ts;
        private static Stopwatch stopWatch;
        private static string logFileName = "";
        private static FileStream objStream;
        private static TextWriterTraceListener objTraceListener;


        public static void OpenLog(string url)
        {
            logFileName = url;

            if (ts == null)
            {
                if (File.Exists(logFileName))
                {
                    FileInfo file = new FileInfo(logFileName);
                    try
                    {
                        file.Delete();
                    }
                    catch { };
                }

                ts = new TraceSource("MyLyrics");
                ts.Switch = new SourceSwitch("sw1", "All");
                objStream = new FileStream(logFileName, FileMode.OpenOrCreate);
                objTraceListener = new TextWriterTraceListener(objStream);
                objTraceListener.Filter = new EventTypeFilter(SourceLevels.All);
                ts.Listeners.Add(objTraceListener);
                StartTimer();
            }
        }

        public static void Dispose()
        {
            if (ts != null)
            {
                ts.Flush();
                ts.Close();
                StopTimer();

                objStream.Close();
                objStream.Dispose();
                try
                {
                    objTraceListener.Close();
                    objTraceListener.Dispose();
                }
                catch { }

                if (System.IO.File.Exists(logFileName))
                {
                    FileStream file = new FileStream(logFileName, FileMode.OpenOrCreate, FileAccess.Write);
                    file.Close();
                }
            }
        }

        public static TraceSource TraceSource
        {
            get {
                if (ts != null)
                {
                    ts.Flush();
                    return ts;
                }
                else return null;
            }
        }

        private static void StartTimer()
        {
            stopWatch = new Stopwatch();
            stopWatch.Start();
        }

        private static void StopTimer()
        {
            stopWatch.Stop();
        }

        public static string ElapsedTimeString()
        {
            long time = stopWatch.ElapsedMilliseconds;
            long sec = time / 1000;
            long ms = (time / 100) - (sec * 10);
            string str = "";
            str += (sec < 100) ? "0" : "";
            str += (sec < 10) ? "0" : "";
            str += sec.ToString() + "." + ms.ToString();
            return str + ": ";
        }
    }
}