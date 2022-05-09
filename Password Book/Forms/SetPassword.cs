using System;
using System.Windows.Forms;
using System.IO;

namespace Password_Book
{
    public partial class SetPassword : Form
    {
        public Helpers misc = new Helpers();
        public SetPassword()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            FileStream fs = File.Create($"{Environment.UserName}.pw");
            fs.Close();
            StreamWriter sw = new StreamWriter($"{Environment.UserName}.pw");
            sw.WriteLine(misc.GenerateHash(misc.getHwid(), $"{Environment.UserName}"), "\n"); // crypted hwid
            sw.WriteLine(misc.GenerateHash(guna2TextBox1.Text, $"{misc.getHwid()}"), "\n"); // crypted pass
            sw.WriteLine("?");
            sw.Close();
            Application.Restart();
        }

        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
