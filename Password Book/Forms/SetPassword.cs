using System;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace Password_Book
{
    public partial class SetPassword : Form
    {
        private string dots = null;
        public Helpers misc = new Helpers();
        public SetPassword()
        {
            InitializeComponent();
        }



        public void Add()
        {
            new Thread(() => timer1_Tick()).Start();
            Invoke(new Action(() =>
            {
                string folder = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\{misc.GenerateHash($"{Environment.UserName}", $"{misc.getHwid()}")}";
                start:
                try
                {
                    label2.Text = $"Crypting your data{dots}";
                    FileStream fs = File.Create($"{folder}\\{misc.GenerateHash(Environment.UserName, misc.getHwid())}");
                    fs.Close();
                    StreamWriter sw = new StreamWriter($"{folder}\\{misc.GenerateHash(Environment.UserName, misc.getHwid())}");
                    label2.Text = $"Writing crypted data to db{dots}";
                    sw.WriteLine(misc.GenerateHash(misc.getHwid(), $"{Environment.UserName}"), "\n"); // crypted hwid
                    sw.WriteLine(misc.GenerateHash(guna2TextBox1.Text, $"{misc.getHwid()}"), "\n"); // crypted pass
                    label2.Text = $"Restarting{dots}";
                    sw.Close();
                    Application.Restart();
                }
                catch (DirectoryNotFoundException)
                {
                    Directory.CreateDirectory(folder);
                    goto start;
                }
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

        private void timer1_Tick()
        {
            while (true)
            {
                dots = ".";
                Thread.Sleep(10);
                dots = "..";
                Thread.Sleep(10);
                dots = "...";
                Thread.Sleep(10);
            }
        }
    }
}
