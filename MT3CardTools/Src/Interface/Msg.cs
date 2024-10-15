using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MT3CardTools.Src.Logging;

namespace MT3CardTools.Src.Interface
{
    class Msg
    {
        public static DialogResult Default(object message, MessageBoxButtons buttons = MessageBoxButtons.OK, string title = "")
        {
            Log.Info($"Msg::Default: {title}: {message}");
            return MessageBox.Show(message.ToString(), title, buttons, MessageBoxIcon.None);
        }
        public static DialogResult Info(object message, MessageBoxButtons buttons = MessageBoxButtons.OK, string title = "")
        {
            Log.Info($"Msg::Info: {title}: {message}");
            return MessageBox.Show(message.ToString(), title, buttons, MessageBoxIcon.Information);
        }
        public static DialogResult Warning(object message, MessageBoxButtons buttons = MessageBoxButtons.OK, string title = "")
        {
            Log.Warn($"Msg::Warning: {title}: {message}");
            return MessageBox.Show(message.ToString(), title, buttons, MessageBoxIcon.Warning);
        }
        public static DialogResult Error(object message, MessageBoxButtons buttons = MessageBoxButtons.OK, string title = "")
        {
            Log.Error($"Msg::Error: {title}: {message}");
            return MessageBox.Show(message.ToString(), title, buttons, MessageBoxIcon.Error);
        }
        public static DialogResult Question(object message, MessageBoxButtons buttons = MessageBoxButtons.OK, string title = "")
        {
            Log.Info($"Msg::Question: {title}: {message}");
            return MessageBox.Show(message.ToString(), title, buttons, MessageBoxIcon.Question);
        }

        public static DialogResult Exception(Exception e, string title = "Error report")
        {
            var frm = new Form
            {
                ClientSize = new System.Drawing.Size(600, 300),
                Text = title,
                MaximizeBox = false,
                MinimizeBox = false,
                StartPosition = FormStartPosition.CenterScreen
            };
            frm.Controls.Add(new TextBox
            {
                Multiline = true,
                ReadOnly = true,
                WordWrap = false,
                ScrollBars = ScrollBars.Both,
                BorderStyle = BorderStyle.Fixed3D,
                Dock = DockStyle.Fill,
                Text = $"{DateTime.UtcNow.ToString("dd/MM/yyyy HH:mm:ss")} UTC\r\n{e.ToString()}",
                SelectionStart = 0,
                SelectionLength = 0
            });
            return frm.ShowDialog();
        }
    }
}
