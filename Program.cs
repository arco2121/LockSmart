using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LockSmart
{
    internal static class Program
    {
        static private Mutex cosino = null;
        /// <summary>
        /// Punto di ingresso principale dell'applicazione.
        /// </summary>
        [STAThread]
        static void Main()
        {
            const string appname = "Kiwi Lock";
            bool newone;
            cosino = new Mutex(true, appname, out newone);

            if(!newone)
            {
                MessageBox.Show("App già in esecuzione", "Kiwi Lock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Application.Exit();
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Opening());
        }
    }
}
