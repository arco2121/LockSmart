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
            string[] k = File.ReadAllLines("Memory.PadLock");
            this.nome = k[0];
            bool instate = Convert.ToBoolean(k[1]);
            this.Text = "Kiwi Lock - " + this.nome;
            string[] Ports = SerialPort.GetPortNames();
            bool o = true;
            foreach (string i in Ports)
            {
                try
                {
                    RaccoltaPorte.Items.Add(i);
                    Lucchetto = new PadLock(instate, this.nome, i);
                    if (!Lucchetto.IsCode)
                    {
                        Settings Home = new Settings();
                        Home.Show();
                        this.Close();
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
            if(o == false)
            {
                MessageBox.Show("Impossibile comunicare con il lucchetto", "Kiwi Lock", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
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
    }
}
