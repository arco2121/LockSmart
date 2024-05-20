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
using static System.Windows.Forms.AxHost;

namespace LockSmart
{
    public partial class Settings : Form
    {
        static PersonalFont font = new PersonalFont();
        static PrivateFontCollection QuickSand = font.QuickSand;

        internal PadLock Lucchetto;
        private string nome;
        private Timer Texto;
        public Settings()
        {
            InitializeComponent();
            this.RaccoltaPorte.Font = new System.Drawing.Font(QuickSand.Families[0], 13F, System.Drawing.FontStyle.Bold);
            this.label2.Font = new System.Drawing.Font(QuickSand.Families[0], 16F, System.Drawing.FontStyle.Bold);
            this.Font = new System.Drawing.Font(QuickSand.Families[0], 16F, System.Drawing.FontStyle.Bold);
        }

        private void LockApp_Load(object sender, EventArgs e)
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
                this.nome = Criptografia.DeCripta(prama[0], prama[3], prama[4]);
                bool instate = Convert.ToBoolean(Criptografia.DeCripta(prama[1], prama[3], prama[4]));
                this.Text = "Kiwi Lock - " + this.nome;
                string passw = Criptografia.DeCripta(prama[2], prama[3], prama[4]);
                if (firstsetupif != "true")
                {
                    InputBox Pass = new InputBox("Chiave", true);
                    Pass.ShowDialog();
                    if (Pass.DialogResult == DialogResult.OK)
                    {
                        if (Pass.TextResult == Criptografia.DeCripta(prama[2], prama[3], prama[4]))
                        {
                            InitializeAll(instate, passw);
                        }
                        else
                        {
                            MessageBox.Show("Chiave Errata", "Kiwi Lock", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            Application.Exit();
                        }
                    }
                    else
                    {
                        Application.Exit();
                    }
                }
                else
                {
                    File.Delete("FirstSetup");
                    InitializeAll(instate, passw);
                }
            }
            catch
            {
                MessageBox.Show("Impossibile leggere i dati. PadLock resettato", "Kiwi Lock", MessageBoxButtons.OK, MessageBoxIcon.Error);
                File.Delete("Memory.PadLock");
                Application.Exit();
            }
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
            ComboBox Ogg = (ComboBox)sender;
            string porta = Ogg.SelectedItem.ToString();
            Lucchetto.motore.PortName = porta;
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
                    Lucchetto.motore.Write("3");
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
            }
            catch
            {
                MessageBox.Show("Impossibile eliminare il lucchetto", "Kiwi Lock", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeAll(bool instate,string passw)
        {
            string[] Ports = SerialPort.GetPortNames();
            bool o = true;
            foreach (string i in Ports)
            {
                try
                {
                    RaccoltaPorte.Items.Add(i);
                    Lucchetto = new PadLock(instate, passw, this.nome, i);
                    if (!Lucchetto.IsCode)
                    {
                        File.WriteAllText("Reloading", "true");
                        Application.Restart();
                    }
                    InitializeTimer();
                    RaccoltaPorte.SelectedItem = i;
                    o = true;
                    break;
                }
                catch
                {
                    o = false;
                }
            }
            if (o == false)
            {
                MessageBox.Show("Impossibile comunicare con il lucchetto", "Kiwi Lock", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            RaccoltaPorte.SelectedIndexChanged += new System.EventHandler(this.RaccoltaPorte_SelectedIndexChanged);
        }

        private void Info_Click(object sender, EventArgs e)
        {
            Informazioni Info = new Informazioni();
            Info.ShowDialog();
        }
    }
}
