using System;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace Password_Book
{
    public partial class ConfirmForm : Form
    {
        public Helpers misc = new Helpers();
        public ConfirmForm()
        {
            InitializeComponent();
        }

        public void Confirm()
        {
            try
            {
                string dataFile = null;
                Invoke(new Action(() =>
                {
                    label2.Text = "Validating data";
                }));
                string folder = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\{misc.GenerateHash($"{Environment.UserName}", $"{misc.getHwid()}")}";
                string hwid = File.ReadLines($"{folder}\\{misc.GenerateHash(Environment.UserName, misc.getHwid())}").Skip(0).First();
                string password = File.ReadLines($"{folder}\\{misc.GenerateHash(Environment.UserName, misc.getHwid())}").Skip(1).First();
                Invoke(new Action(() =>
                {
                    dataFile = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}//{misc.GenerateHash($"{Environment.UserName}", $"{misc.getHwid()}")}//{Program.mf.guna2ComboBox1.Text}.pw";
                }));
                string login = misc.Decrypt(File.ReadLines(dataFile).Skip(0).First());
                string pass = misc.Decrypt(File.ReadLines(dataFile).Skip(1).First());
                string dataHwid = File.ReadLines(dataFile).Skip(2).First();
                if (dataHwid != misc.GenerateHash(misc.getHwid(), Environment.UserName))
                {
                    Invoke(new Action(() =>
                    {
                        label2.Text = "Invalid HWID";
                    }));
                    return;
                }
                if (misc.GenerateHash(misc.getHwid(), $"{Environment.UserName}") != hwid)
                {
                    Invoke(new Action(() =>
                    {
                        label2.Text = "Invalid HWID";
                    }));
                    return;
                }
                if (misc.GenerateHash(guna2TextBox1.Text, $"{misc.getHwid()}") != password)
                {
                    Invoke(new Action(() =>
                    {
                        label2.Text = "Invalid Password";
                    }));
                    return;
                }
                Invoke(new Action(() =>
                {
                    label2.Text = "Validated! Starting uncrypt";
                    Clipboard.SetText($"{login}:{pass}");
                    Program.mf.label7.Text = "Copied";
                    this.Close();
                }));
            }
            catch (ThreadAbortException) { this.Close(); }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            new Thread(() => Confirm()).Start();
        }

        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
