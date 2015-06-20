using System;
using System.Windows.Forms;

namespace BlueOnion
{
    internal static class Program
    {
        [STAThread]
        private static void Main(string[] commandLineArguments)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Calendar(commandLineArguments));
        }
    }
}