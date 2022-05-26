using System;
using System.Windows.Forms;
using System.IO;

namespace Password_Book
{
    internal static class Program
    {
        public static MainForm mf;
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Helpers misc = new Helpers();
            string folder = $"{Path.GetTempPath()}//{misc.GenerateHash($"{Environment.UserName}", $"{misc.getHwid()}")}";
            if (File.Exists($"{folder}//{misc.GenerateHash(Environment.UserName, misc.getHwid())}"))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(mf = new MainForm());
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new SetPassword());
            }
        }
    }
}
