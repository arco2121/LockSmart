using InTheHand.Net.Bluetooth;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LockSmart
{
    public partial class Opening : Form
    {
        public Opening()
        {
            InitializeComponent();
        }

        private void Lock_Click(object sender, EventArgs e)
        {
            InputBox Nome = new InputBox("Nome",false);
            Nome.ShowDialog();
            if(Nome.DialogResult == DialogResult.OK)
            {
                string h = Nome.TextResult;
                string pass = PadLock.NewPassword(h);
                //Viene creato il PadLock e l'app è riavviata
            }
        }

        private void Opening_Load(object sender, EventArgs e)
        {
            /*Eseguito quando la app di avvia, innanzitutto controlla se il bluethooth è attivo.
             * Poi controlla se nel file di Memoria del PadLock è presente la password, se non è presente allora avviera la procedura di creazione
             del PadLock, se invece è presente allora il PadLock sarà gia creato e si passera alla schermata di richiesta di autenticazione e di 
             connessione con il PadLock*/

            RadioMode State;
            BluetoothRadio radio;
            try
            {
                radio = BluetoothRadio.Default;
                State = radio.Mode;
            }
            catch
            {
                State = RadioMode.PowerOff;
            }
            if (!(State == RadioMode.PowerOff))
            {
                MessageBox.Show("Spegnere il Bluethooth per il corretto funzionamento dell'applicazione ", "Kiwi Lock", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }
            try
            {
                string h;
                try
                {
                    string[] k = File.ReadAllLines("Memory.PadLock");
                    h = k[2];
                }
                catch
                {
                    h = "";
                }
                if (h != "")
                {
                    Instruction Lucchetteria = new Instruction();
                    Lucchetteria.ShowDialog();
                    this.Close();
                }
                else
                {

                }
            }
            catch
            {
                MessageBox.Show("Impossibile leggere i dati. PadLock resettato", "Kiwi Lock", MessageBoxButtons.OK, MessageBoxIcon.Error);
                File.Delete("Memory.PadLock");
                Environment.Exit(1);
            }
        }

        private void Info_Click(object sender, EventArgs e)
        {
            //Apre solo la schermata di Info dell'applicazione
            Informazioni Info = new Informazioni();
            Info.ShowDialog();
        }
    }
}
