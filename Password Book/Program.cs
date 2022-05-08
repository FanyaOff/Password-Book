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
            if (File.Exists($"{Environment.UserName}.pw"))
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
