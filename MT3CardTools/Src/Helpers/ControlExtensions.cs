using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

using MT3CardTools.Src.Logging;

namespace MT3CardTools.Src.Helpers
{
    static class ControlExtensions
    {
        public static void FillValues<T>(this Control parent, T c)
        {
            foreach (var property in c.GetType().GetProperties())
            {
                try
                {
                    if (!property.CanRead || (property.GetIndexParameters().Length > 0))
                        continue;

                    if (property.PropertyType == typeof(bool))
                    {
                        var chk = (CheckBox)parent.FindControl($"chk{property.Name}");
                        if (chk != null)
                            chk.Checked = (bool)property.GetValue(c, null);
                    }
                    else if (property.PropertyType == typeof(string))
                    {
                        var txt = (TextBox)parent.FindControl($"txt{property.Name}");
                        if (txt != null)
                            txt.Text = (string)property.GetValue(c, null);
                    }
                    else if (property.PropertyType.IsArray)
                    {
                        //Unimplemented
                    }
                    else
                    {
                        var num = (NumericUpDown)parent.FindControl($"num{property.Name}");
                        if (num != null)
                            num.Value = Convert.ToDecimal(property.GetValue(c, null));
                        else
                        {
                            var cmb = (ComboBox)parent.FindControl($"cmb{property.Name}");
                            if (cmb != null)
                                cmb.SelectedIndex = Convert.ToInt32(property.GetValue(c, null));
                            else
                            {
                                var cmd = (ComboBox)parent.FindControl($"cmd{property.Name}");
                                if (cmd != null)
                                    cmd.SelectedIndex = cmd.Items.OfType<ComboBoxItem>().ToList().FindIndex(x => x.Id == Convert.ToInt32(property.GetValue(c, null)));
                            }
                        }
                    }
                }
                catch
                {
                    Log.Error($"FillValues: Failed to assign {property.Name} to a control");
                }
            }
        }
        public static void GetValues<T>(this Control parent, T c)
        {
            foreach (var property in c.GetType().GetProperties())
            {
                try
                {
                    if (!property.CanRead || (property.GetIndexParameters().Length > 0))
                        continue;

                    if (property.PropertyType == typeof(bool))
                    {
                        var chk = (CheckBox)parent.FindControl($"chk{property.Name}");
                        if (chk != null)
                            property.SetValue(c, chk.Checked, null);
                    }
                    else if (property.PropertyType == typeof(string))
                    {
                        var txt = (TextBox)parent.FindControl($"txt{property.Name}");
                        if (txt != null)
                            property.SetValue(c, txt.Text, null);
                    }
                    else if (property.PropertyType.IsArray)
                    {
                        //Unimplemented
                    }
                    else
                    {
                        var num = (NumericUpDown)parent.FindControl($"num{property.Name}");
                        if (num != null)
                            property.SetValue(c, Convert.ChangeType(num.Value, property.PropertyType), null);
                        else
                        {
                            var cmb = (ComboBox)parent.FindControl($"cmb{property.Name}");
                            if (cmb != null)
                                property.SetValue(c, Convert.ChangeType(cmb.SelectedIndex, property.PropertyType), null);
                            else
                            {
                                var cmd = (ComboBox)parent.FindControl($"cmd{property.Name}");
                                if (cmd != null)
                                    property.SetValue(c, Convert.ChangeType(((ComboBoxItem)cmd.SelectedItem).Id, property.PropertyType), null);
                            }
                        }
                    }
                }
                catch
                {
                    Log.Error($"GetValues: Failed to get {property.Name} from a control");
                }
            }
        }

        public static Control FindControl(this Control ParentCntl, string NameToSearch)
        {
            if (ParentCntl.Name == NameToSearch)
                return ParentCntl;

            foreach (Control ChildCntl in ParentCntl.Controls)
            {
                Control ResultCntl = FindControl(ChildCntl, NameToSearch);
                if (ResultCntl != null)
                    return ResultCntl;
            }
            return null;
        }

        public static void SetCoverImage(this MdiClient pbox, Image img) => SetCoverImage(pbox, (Bitmap)img);
        public static void SetCoverImage(this MdiClient pbox, Bitmap bmp)
        {
            if (pbox.Width > 0 && pbox.Height > 0)
            {
                //pbox.SizeMode = PictureBoxSizeMode.Normal;
                bool source_is_wider = (float)bmp.Width / bmp.Height > (float)pbox.Width / pbox.Height;

                var resized = new Bitmap(pbox.Width, pbox.Height);
                using (var g = Graphics.FromImage(resized))
                {
                    var dest_rect = new Rectangle(0, 0, pbox.Width, pbox.Height);
                    Rectangle src_rect;

                    if (source_is_wider)
                    {
                        float size_ratio = (float)pbox.Height / bmp.Height;
                        int sample_width = (int)(pbox.Width / size_ratio);
                        src_rect = new Rectangle((bmp.Width - sample_width) / 2, 0, sample_width, bmp.Height);
                    }
                    else
                    {
                        float size_ratio = (float)pbox.Width / bmp.Width;
                        int sample_height = (int)(pbox.Height / size_ratio);
                        src_rect = new Rectangle(0, (bmp.Height - sample_height) / 2, bmp.Width, sample_height);
                    }

                    g.DrawImage(bmp, dest_rect, src_rect, GraphicsUnit.Pixel);

                    pbox.BackgroundImage = resized;
                }
            }
        }

        public static MdiClient GetMdiClient(this Form frm)
        {
            foreach (Control c in frm.Controls)
            {
                MdiClient client = c as MdiClient;
                if (client != null)
                    return client;
            }
            return null;
        }

        [DllImport("user32.dll")]
        private static extern int ShowScrollBar(IntPtr hWnd, int wBar, int bShow);

        private const int SB_BOTH = 3;
        private const int WM_NCCALCSIZE = 0x83;

        public static void HideScrollBars(this MdiClient client)
        {
            try
            {
                if (client != null)
                    ShowScrollBar(client.Handle, SB_BOTH, 0);
            }
            catch { }
        }

        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", ExactSpelling = true)]
        private static extern int SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_CLIENTEDGE = 0x200;
        private const uint SWP_NOSIZE = 0x0001;
        private const uint SWP_NOMOVE = 0x0002;
        private const uint SWP_NOZORDER = 0x0004;
        private const uint SWP_NOREDRAW = 0x0008;
        private const uint SWP_NOACTIVATE = 0x0010;
        private const uint SWP_FRAMECHANGED = 0x0020;
        private const uint SWP_SHOWWINDOW = 0x0040;
        private const uint SWP_HIDEWINDOW = 0x0080;
        private const uint SWP_NOCOPYBITS = 0x0100;
        private const uint SWP_NOOWNERZORDER = 0x0200;
        private const uint SWP_NOSENDCHANGING = 0x0400;

        public static void SetBevel(this MdiClient client, bool show)
        {
            int windowLong = GetWindowLong(client.Handle, GWL_EXSTYLE);

            if (show)
            {
                windowLong |= WS_EX_CLIENTEDGE;
            }
            else
            {
                windowLong &= ~WS_EX_CLIENTEDGE;
            }

            SetWindowLong(client.Handle, GWL_EXSTYLE, windowLong);

            // Update the non-client area.
            SetWindowPos(client.FindForm().Handle, IntPtr.Zero, 0, 0, 0, 0,
                SWP_NOACTIVATE | SWP_NOMOVE | SWP_NOSIZE | SWP_NOZORDER |
                SWP_NOOWNERZORDER | SWP_FRAMECHANGED);
        }
    }

    public class ComboBoxItem
    {
        public int Id { get; }
        public string Text { get; }

        public ComboBoxItem(int id, string text)
        {
            Id = id;
            Text = text;
        }

        public override string ToString() => Text;
        //public override bool Equals(object obj) => Id == ((ComboBoxItem)obj).Id;
        //public override int GetHashCode() => Id;
    }

    public class MultiFormContext : ApplicationContext
    {
        private int openForms;
        public MultiFormContext(params Form[] forms)
        {
            openForms = forms.Length;

            foreach (var form in forms)
            {
                if (form != null)
                {
                    form.FormClosed += (s, args) =>
                    {
                        if (Interlocked.Decrement(ref openForms) == 0)
                            ExitThread();
                    };
                    form.Show();
                }
            }
        }
    }
}
