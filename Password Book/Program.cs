using System;
using System.Windows.Forms;
using System.IO;
using System.Threading;
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
            string folder = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}//{misc.GenerateHash($"{Environment.UserName}", $"{misc.getHwid()}")}";
            if (File.Exists($"{folder}//{misc.GenerateHash(Environment.UserName, misc.getHwid())}"))
            {
                try
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(mf = new MainForm());
                } catch (ThreadAbortException) { }
            }
            else
            {
                try
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new SetPassword());
                }
                catch (ThreadAbortException) { }
            }
        }
    }
}
