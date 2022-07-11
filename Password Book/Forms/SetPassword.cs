using System;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace Password_Book
{
    public partial class SetPassword : Form
    {
        public Helpers misc = new Helpers();
        public SetPassword()
        {
            InitializeComponent();
        }



        public void Add()
        {
            Invoke(new Action(() =>
            {
                string folder = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\{misc.GenerateHash($"{Environment.UserName}", $"{misc.getHwid()}")}";
                start:
                try
                {
                    label2.Text = $"Crypting your data";
                    FileStream fs = File.Create($"{folder}\\{misc.GenerateHash(Environment.UserName, misc.getHwid())}");
                    fs.Close();
                    StreamWriter sw = new StreamWriter($"{folder}\\{misc.GenerateHash(Environment.UserName, misc.getHwid())}");
                    label2.Text = $"Writing crypted data to db";
                    sw.WriteLine(misc.GenerateHash(misc.getHwid(), $"{Environment.UserName}"), "\n"); // crypted hwid
                    sw.WriteLine(misc.GenerateHash(guna2TextBox1.Text, $"{misc.getHwid()}"), "\n"); // crypted pass
                    label2.Text = $"Restarting";
                    sw.Close();
                    Thread.CurrentThread.Abort();
                }
                catch (DirectoryNotFoundException)
                {
                    Directory.CreateDirectory(folder);
                    goto start;
                }
                catch (ThreadAbortException) { Application.Restart(); }
            }));
                
        }
        
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            new Thread(() => Add()).Start();
        }

        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }
    }
}
