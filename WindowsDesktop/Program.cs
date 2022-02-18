using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WindowsDesktop
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Controller.Group group = Controller.Group.Instance;
            Controller.Clipboard clipboard = Controller.Clipboard.Instance;

            Application.Run(new View.Main());
        }
    }
}
