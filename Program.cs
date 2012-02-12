// Copyright 2006 Blue Onion Software, All rights reserved
//
using System;
using System.Windows.Forms;

namespace BlueOnion
{
    class Program
    {
        [STAThread]
        static void Main(string[] commandLineArguments)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Calendar(commandLineArguments));
        }
    }
}
