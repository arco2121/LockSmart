using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LockSmart
{
    internal static class Program
    {
        /*Si definisce un oggetto Mutex (threading) che ha lo scopo di controllare se 
         è aperta piu di una istanza dell'applicazione. In tal caso viene lanciato un messaggio di avviso invece di aprire l'app.
        La presenza del File Reloading viene controllata per impedire che si verifichi un comportamento anomalo quando si richiama Application.Restart.
        Infatti il File viene creato ogni volta che richiamo la funzione Restart.*/
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
            if (!File.Exists("Reloading"))
            {
                if (!newone)
                {
                    MessageBox.Show("App già in esecuzione", "Kiwi Lock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Environment.Exit(1);
                    return;
                }
            }
            else
            {
                File.Delete("Reloading");
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Opening());
        }
    }
}
