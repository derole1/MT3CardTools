using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

using MT3CardTools.Src.CardTools;
using MT3CardTools.Src.Forms;
using MT3CardTools.Src.Interface;
using MT3CardTools.Src.Helpers;
using MT3CardTools.Src.Logging;
using System.Runtime.InteropServices;

namespace MT3CardTools.Src
{
    static class Program
    {
        [DllImport("kernel32.dll")]
        static extern bool AttachConsole(int dwProcessId);
        private const int ATTACH_PARENT_PROCESS = -1;

        static string[] args = Environment.GetCommandLineArgs();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (!Debugger.IsAttached)
            {
                Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Log.Info($"Main: argc:{args.Length},argv:{string.Join(" ", args)}");
            // TODO: Improve the below
            if (args.Length < 2 || (args.Length == 2 && (args[1] == "-log" || args[1] == "-console")))
            {
                if (args.Length > 1)
                {
                    if (args[1] == "-log")
                        Log.LogToFile = true;
                    if (args[1] == "-console" && !Debugger.IsAttached)
                        Log.Warn("Main: -console unimplemented!");  // TODO
                }
                Application.Run(new frmMain());
            }
            else
            {
                List<Form> forms = new List<Form>();
                for (int i=1; i<args.Length; i++)
                {
                    if (args[i].StartsWith("-"))
                    {
                        if (args[i] == "-reader")
                            forms.Add(new frmCardReaderInterface());
                        if (args[i] == "-generator")
                            forms.Add(new frmCardGenerator());
                        if (args[i] == "-keyextract")
                            forms.Add(new frmKeyExtractor());
                    }
                    else if (File.Exists(args[i]))
                        forms.Add(CardWindows.CreateCardWindow(args[i]));
                }
                Application.Run(new MultiFormContext(forms.ToArray()));
            }
        }

        static void HandleException(Exception e)
        {
            Log.Error($"HandleException: {e.Message}");
            if (Msg.Error("An error occured and the application will have to close.\nWould you like to see more details?"
                , MessageBoxButtons.YesNo, "An error occured!") == DialogResult.Yes)
                Msg.Exception(e, "Error report");
            if (Msg.Question("Would you like to attempt to continue?\nProgram stability is not guarenteed beyond this point. You should save and restart the program as soon as possible."
                , MessageBoxButtons.YesNo, "Continue execution?") == DialogResult.No)
                Environment.Exit(-1);
        }

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs args) => HandleException(args.Exception);
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs args) => HandleException((Exception)args.ExceptionObject);
    }
}
