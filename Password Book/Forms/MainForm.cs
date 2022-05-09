﻿using System;
using System.IO;
using System.Management;
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
            Application.Exit();
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
            start:
            string folder = $"{Path.GetTempPath()}//{misc.GenerateHash($"{Environment.UserName}", $"{misc.getHwid()}")}";
            if (Directory.Exists(folder))
            {
                FileStream fs = File.Create($"{folder}//{nameBox.Text}.pw");
                fs.Close();
                StreamWriter sw = new StreamWriter($"{folder}//{nameBox.Text}.pw");
                sw.WriteLine($"{misc.Encrypt($"{loginBox.Text}")}", "\n");
                sw.WriteLine($"{misc.Encrypt($"{passBox.Text}")}", "\n");
                sw.WriteLine($"{misc.GenerateHash(misc.getHwid(), Environment.UserName)}");
                sw.Close();
                updateList();
                label7.Text = "Succeful added!";
                return;
            }
            Directory.CreateDirectory(folder);
            goto start;
        }

        public void updateList()
        {
            string folder = $"{Path.GetTempPath()}\\{misc.GenerateHash($"{Environment.UserName}", $"{misc.getHwid()}")}";
            foreach (string file in Directory.EnumerateFiles(folder, "*.pw", SearchOption.AllDirectories))
            {
                guna2ComboBox1.Items.Add(Path.GetFileNameWithoutExtension(file));
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            updateList();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (guna2ComboBox1.SelectedIndex == -1)
            {
                label7.Text = "Please, select a value";
                return;
            }
            string folder = $"{Path.GetTempPath()}//{misc.GenerateHash($"{Environment.UserName}", $"{misc.getHwid()}")}";
            File.Delete($"{folder}//{guna2ComboBox1.Text}.pw");
        }
    }
}