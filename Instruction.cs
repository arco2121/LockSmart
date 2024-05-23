using InTheHand.Net.Bluetooth;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace LockSmart
{
    public partial class Instruction : Form
    {
        static PersonalFont font = new PersonalFont();
        static PrivateFontCollection QuickSand = font.QuickSand;
        private bool instate;
        private string pass;
        private string nome;
        public Instruction()
        {
            InitializeComponent();
            this.label3.Font = new System.Drawing.Font(QuickSand.Families[0], 20F, System.Drawing.FontStyle.Bold);
        }

        private void Instruction_Load(object sender, EventArgs e)
        {
            try
            {
                string[] prama = File.ReadAllLines("Memory.PadLock");
                string firstsetupif = "";
                try
                {
                    firstsetupif = File.ReadAllText("FirstSetup");
                }
                catch
                {
                    firstsetupif = "";
                }
                bool instate = Convert.ToBoolean(Criptografia.DeCripta(prama[1], prama[3], prama[4]));
                this.nome = Criptografia.DeCripta(prama[0], prama[3], prama[4]);
                if (firstsetupif != "true")
                {
                    InputBox Pass = new InputBox("Chiave", true);
                    Pass.ShowDialog();
                    if (Pass.DialogResult == DialogResult.OK)
                    {
                        if (Pass.TextResult == Criptografia.DeCripta(prama[2], prama[3], prama[4]))
                        {
                            this.pass = Criptografia.DeCripta(prama[2], prama[3], prama[4]);
                            this.instate = instate;
                        }
                        else
                        {
                            MessageBox.Show("Chiave Errata", "Kiwi Lock", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            Environment.Exit(1);
                        }
                    }
                    else
                    {
                        Environment.Exit(1);
                    }
                }
                else
                {
                    File.Delete("FirstSetup");
                    this.pass = Criptografia.DeCripta(prama[2], prama[3], prama[4]);
                    this.instate = instate;
                }
            }
            catch
            {
                MessageBox.Show("Impossibile leggere i dati. PadLock resettato", "Kiwi Lock", MessageBoxButtons.OK, MessageBoxIcon.Error);
                File.Delete("Memory.PadLock");
                Environment.Exit(1);
            }
            RadioMode State;
            BluetoothRadio radio = null;
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
                radio.Dispose();
                Environment.Exit(1);
            }
        }

        private void Instruction_Shown(object sender, EventArgs e)
        {
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = 100;
            timer.Tick += new EventHandler(InitializeAll);
            timer.Start();
        }

        private void InitializeAll(object sender, EventArgs e)
        {
            ((System.Windows.Forms.Timer)sender).Stop();
            PadLock Lucchetto;
            string[] Ports = SerialPort.GetPortNames();
            bool o = false;
            while (!o)
            {
                for (int i = 0; i < Ports.Length; i++)
                {
                    try
                    {
                        Lucchetto = new PadLock(this.instate, this.pass, this.nome, Ports[i]);
                        if (!Lucchetto.IsCode)
                        {
                            File.WriteAllText("Reloading", "true");
                            Application.Restart();
                        }
                        o = true;
                        Settings Impostazioni = new Settings(Lucchetto);
                        this.Hide();
                        Impostazioni.ShowDialog();
                        break;
                    }
                    catch
                    {
                        o = false;
                    }
                }
            }
        }
    }
}
