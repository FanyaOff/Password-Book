﻿using System;
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
            string folder = $"{Path.GetTempPath()}\\{misc.GenerateHash($"{Environment.UserName}", $"{misc.getHwid()}")}";
            FileStream fs = File.Create($"{folder}\\{misc.GenerateHash(Environment.UserName, misc.getHwid())}");
            fs.Close();
            StreamWriter sw = new StreamWriter($"{folder}\\{misc.GenerateHash(Environment.UserName, misc.getHwid())}");
            sw.WriteLine(misc.GenerateHash(misc.getHwid(), $"{Environment.UserName}"), "\n"); // crypted hwid
            sw.WriteLine(misc.GenerateHash(guna2TextBox1.Text, $"{misc.getHwid()}"), "\n"); // crypted pass
            sw.Close();
            Application.Restart();
        }

        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
