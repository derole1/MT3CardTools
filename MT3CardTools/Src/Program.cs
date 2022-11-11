using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;

using MT3CardTools.Src.CardTools;
using MT3CardTools.Src.Forms;
using MT3CardTools.Src.Interface;
using MT3CardTools.Src.Helpers;
using MT3CardTools.Src.Logging;

namespace MT3CardTools.Src
{
    static class Program
    {
        const string message = "I like pancakes";

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
            if (args.Length < 2)
                Application.Run(new frmMain());
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
                    else
                        forms.Add(CardWindows.CreateCardWindow(args[i]));
                }
                Application.Run(new MultiFormContext(forms.ToArray()));
            }
        }

        static void HandleException(Exception e)
        {
            Log.Error($"HandleException: {e.Message}");
            if (Msg.Error("An error occured and the application will have to close.\nWould you like to see more details?"
                , MessageBoxButtons.YesNo, "Im in a pinch!") == DialogResult.Yes)
                Msg.Exception(e, "Im in a double pinch!");
            if (Msg.Question("Would you like to attempt to continue?\nProgram stability is not guarenteed beyond this point. You should save and restart the program as soon as possible."
                , MessageBoxButtons.YesNo, "Im in a triple pinch!") == DialogResult.No)
                Environment.Exit(-1);
        }

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs args) => HandleException(args.Exception);
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs args) => HandleException((Exception)args.ExceptionObject);
    }
}
