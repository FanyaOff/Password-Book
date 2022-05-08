using System;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace Password_Book
{
    public partial class ConfirmForm : Form
    {
        public Helpers misc = new Helpers();
        public ConfirmForm()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string hwid = File.ReadLines($"{Environment.UserName}.pw").Skip(0).First();
            string password = File.ReadLines($"{Environment.UserName}.pw").Skip(1).First();

            if (misc.GenerateHash(misc.getHwid(), $"{Environment.UserName}") != hwid)
            {
                label2.Text = "Invalid HWID";
                return;
            }
            if (misc.GenerateHash(guna2TextBox1.Text, $"{misc.getHwid()}") != password)
            {
                label2.Text = "Invalid Password";
                return;
            }
            MainForm main = new MainForm();
            string dataFile = $"{Path.GetTempPath()}//{misc.GenerateHash($"{Environment.UserName}", $"{misc.getHwid()}")}//{Program.mf.guna2ComboBox1.Text}.pw";
            string login = misc.Decrypt(File.ReadLines(dataFile).Skip(0).First());
            string pass = misc.Decrypt(File.ReadLines(dataFile).Skip(1).First());
            Clipboard.SetText($"{login}:{pass}");
            Program.mf.label7.Text = "Copied";
            this.Close();
        }

        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
