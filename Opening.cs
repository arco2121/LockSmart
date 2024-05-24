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
            }
        }

        private void Opening_Load(object sender, EventArgs e)
        {
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
                MessageBox.Show("Spegnere il Bluethooth per il corretto funzionamento", "Kiwi Lock", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            Informazioni Info = new Informazioni();
            Info.ShowDialog();
        }
    }
}
