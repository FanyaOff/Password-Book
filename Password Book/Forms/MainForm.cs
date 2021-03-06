using System;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Threading;
using System.Windows.Forms;

namespace Password_Book
{
    public partial class MainForm : Form
    {
        public Helpers misc = new Helpers();
        public MainForm()
        {
            InitializeComponent();
        }

        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (guna2ComboBox1.SelectedIndex == -1)
            {
                label7.Text = "Please, select a value";
                return;
            }
            ConfirmForm confirm = new ConfirmForm();
            confirm.Show();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            new Thread(() => addThread()).Start();
        }

        public void addThread()
        {
            try
            {
                if (nameBox.Text == "" || loginBox.Text == "" || passBox.Text == "")
                {
                    Invoke(new Action(() =>
                    {
                        label7.Text = "Invalid data";
                    }));
                    return;
                }
            start:
                string folder = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}//{misc.GenerateHash($"{Environment.UserName}", $"{misc.getHwid()}")}";
                if (Directory.Exists(folder))
                {
                    Invoke(new Action(() =>
                    {
                        label7.Text = "Crypting data";
                    }));
                    FileStream fs = File.Create($"{folder}//{nameBox.Text}.pw");
                    fs.Close();
                    StreamWriter sw = new StreamWriter($"{folder}//{nameBox.Text}.pw");
                    Invoke(new Action(() =>
                    {
                        label7.Text = "Writing data";
                    }));
                    sw.WriteLine($"{misc.Encrypt($"{loginBox.Text}")}", "\n");
                    sw.WriteLine($"{misc.Encrypt($"{passBox.Text}")}", "\n");
                    sw.WriteLine($"{misc.GenerateHash(misc.getHwid(), Environment.UserName)}");
                    sw.Close();
                    Invoke(new Action(() =>
                    {
                        label7.Text = "Updating";
                        updateList();
                        label7.Text = "Succeful added!";
                        nameBox.Text = null;
                        loginBox.Text = null;
                        passBox.Text = null;
                    }));
                    return;
                }
                Directory.CreateDirectory(folder);
                goto start;
            }
            catch (ThreadAbortException) { }
        }

        public void updateList()
        {
            try
            {
                guna2ComboBox1.Items.Clear();
                string folder = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\{misc.GenerateHash($"{Environment.UserName}", $"{misc.getHwid()}")}";
                foreach (string file in Directory.EnumerateFiles(folder, "*.pw", SearchOption.AllDirectories))
                {
                     guna2ComboBox1.Items.Add(Path.GetFileNameWithoutExtension(file));
                }
            }
            catch (DirectoryNotFoundException) {}
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            updateList();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            new Thread(() => reset()).Start();
        }

        public void reset()
        {

            string folder = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\{misc.GenerateHash($"{Environment.UserName}", $"{misc.getHwid()}")}";
            Directory.Delete(folder, recursive: true);
            Invoke(new Action(() =>
            {
                Application.Restart();
            }));
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            new Thread(() => remove()).Start();
        }
        public void remove()
        {
            Invoke(new Action(() =>
            {
                if (guna2ComboBox1.SelectedIndex == -1)
                {
                    label7.Text = "Please, select a value";
                    return;
                }
            }));
            string folder = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}//{misc.GenerateHash($"{Environment.UserName}", $"{misc.getHwid()}")}";
            Invoke(new Action(() =>
            {
                File.Delete($"{folder}//{guna2ComboBox1.Text}.pw");
                updateList();
                label7.Text = $"Removed {guna2ComboBox1.Text}";
            }));
        }
    }
}