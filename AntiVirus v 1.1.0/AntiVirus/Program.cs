using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AntiVirus
{
    static class Program
    {
        public static Form1 fm1;
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            fm1 = new Form1();
            //Application.Run(new Form1());
            Application.Run(fm1);
        }
    }
}
