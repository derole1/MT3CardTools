using MT3CardTools.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT3CardTools.Src.Logging
{
    class Log
    {
        //Just in case we want that extra optimisation
        //const bool USE_STACK_FRAME = false;
        const string STACK_FRAME_OFF_MSG = "STACK FRAME OFF";

        public static bool LogToFile = false;

        public static void Info(string message, params object[] param) => DebugLog("I", Settings.Default.UseStackFramesInDebugLog ? new System.Diagnostics.StackFrame(1, true).GetMethod().Name : STACK_FRAME_OFF_MSG, message, param);
        public static void Warn(string message, params object[] param) => DebugLog("W", Settings.Default.UseStackFramesInDebugLog ? new System.Diagnostics.StackFrame(1, true).GetMethod().Name : STACK_FRAME_OFF_MSG, message, param);
        public static void Error(string message, params object[] param) => DebugLog("E", Settings.Default.UseStackFramesInDebugLog ? new System.Diagnostics.StackFrame(1, true).GetMethod().Name : STACK_FRAME_OFF_MSG, message, param);
        public static void Debug(string message, params object[] param) => DebugLog("D", Settings.Default.UseStackFramesInDebugLog ? new System.Diagnostics.StackFrame(1, true).GetMethod().Name : STACK_FRAME_OFF_MSG, message, param);

        static void DebugLog(string prefix, string function, string message, params object[] param)
        {
            var msg = $"{prefix}:{(function == STACK_FRAME_OFF_MSG ? "" : $"[{function}]:")} " +
                $"{string.Format(message, param)} ";
            System.Diagnostics.Debug.WriteLine(msg);
            if (LogToFile)
                File.AppendAllText("log.txt", $"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")} {msg}\r\n");
        }
    }
}
