using InTheHand.Net.Sockets;
using LockSmart.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InTheHand.Net.Bluetooth;

namespace LockSmart
{
    partial class Settings : Form
    {
        static PersonalFont font = new PersonalFont();
        static PrivateFontCollection QuickSand = font.QuickSand;

        internal PadLock Lucchetto;
        private string nome;
        private Timer Texto;
        private bool instate;
        private string pass;
        private string porta;
        private string[] Selected;

        public Settings(bool instate,string pass,string nome,string porta)
        {
            InitializeComponent();
            this.instate = instate;
            this.pass = pass;
            this.porta = porta;
            this.nome = nome;
            this.RaccoltaPorte.Font = new System.Drawing.Font(QuickSand.Families[0], 13F, System.Drawing.FontStyle.Bold);
            this.label2.Font = new System.Drawing.Font(QuickSand.Families[0], 16F, System.Drawing.FontStyle.Bold);
            this.Font = new System.Drawing.Font(QuickSand.Families[0], 16F, System.Drawing.FontStyle.Bold);
        }

        private void LockApp_Load(object sender, EventArgs e)
        {
            this.Text = "Kiwi Lock - " + this.nome;
            string[] Ports = SerialPort.GetPortNames();
            for (int i = 0; i < Ports.Length; i++)
            {
                RaccoltaPorte.Items.Add(Ports[i]);
            }
            Lucchetto = new PadLock(this.instate, this.pass, this.nome, this.porta);
            RaccoltaPorte.SelectedItem = Lucchetto.motore.PortName;
            InitializeTimer();
            RaccoltaPorte.SelectedIndexChanged += new System.EventHandler(this.RaccoltaPorte_SelectedIndexChanged);
        }

        private void Lock_Click(object sender, EventArgs e)
        {
            Lucchetto.Lock();
        }

        private void UnLock_Click(object sender, EventArgs e)
        {
            Lucchetto.UnLock();
        }

        private void RaccoltaPorte_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox Ogg = (ComboBox)sender;
                string porta = Ogg.SelectedItem.ToString();
                Lucchetto.motore.Write("2");
                Lucchetto.motore.Close();
                Lucchetto = null;
                PadLock NewLock = new PadLock(this.instate, this.pass, this.nome, porta);
                Lucchetto = NewLock;
            }
            catch
            {
                MessageBox.Show("Impossibile eliminare il lucchetto", "Kiwi Lock", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void ChangeCode_Click(object sender, EventArgs e)
        { 
            Lucchetto.ChangeCode();
        }

        private void LockApp_FormClosing(object sender, FormClosingEventArgs e)
        {
            try 
            { 
                if(Lucchetto != null)
                {
                    Lucchetto.motore.Write("2");
                }
            }
            catch { }
        }

        private void InitializeTimer()
        {
            Texto = new Timer();
            Texto.Interval = 100;
            Texto.Tick += Texto_Tick;
            Texto.Start();
        }

        private void Texto_Tick(object sender, EventArgs e)
        {
            State.Text = Lucchetto.Locked;
            string[] Ports = SerialPort.GetPortNames();
            if (Selected != Ports)
            {
                for (int i = 0; i < Ports.Length; i++)
                {
                    if (RaccoltaPorte.Items.IndexOf(Ports[i]) == -1)
                    {
                        RaccoltaPorte.Items.Add(Ports[i]);
                    }
                }
                Selected = Ports;
            }
            Texto.Stop();
            Texto.Start();
        }

        private void Log_Click(object sender, EventArgs e)
        {
            Lucchetto.GenerateLog();
        }

        private void Settings_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (Lucchetto != null)
                {
                    Lucchetto.motore.Close();
                }
            }
            catch { }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            try
            {
                Lucchetto.motore.Write("2");
                InputBox Pass = new InputBox("Chiave",true);
                Pass.ShowDialog();
                if(Pass.DialogResult == DialogResult.OK)
                {
                    if(Pass.TextResult == Lucchetto.Code)
                    {
                        File.Delete("Memory.PadLock");
                        File.WriteAllText("Reloading", "true");
                        Application.Restart();
                    }
                    else
                    {
                        MessageBox.Show("Chiave Errata", "Kiwi Lock", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {

                }
                Lucchetto.motore.Write("4");
            }
            catch
            {
                MessageBox.Show("Impossibile eliminare il lucchetto", "Kiwi Lock", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Info_Click(object sender, EventArgs e)
        {
            Informazioni Info = new Informazioni();
            Info.ShowDialog();
        }
    }
}
