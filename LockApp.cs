using LockSmart.Properties;
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
    public partial class LockApp : Form
    {
        static PersonalFont font = new PersonalFont();
        static PrivateFontCollection QuickSand = font.QuickSand;

        internal PadLock Lucchetto;
        private Timer Texto;
        public LockApp()
        {
            InitializeComponent();
            InitializeTimer();
            this.label1.Font = new System.Drawing.Font(QuickSand.Families[0], 40F, System.Drawing.FontStyle.Bold);
            this.RaccoltaPorte.Font = new System.Drawing.Font(QuickSand.Families[0], 13F, System.Drawing.FontStyle.Bold);
            this.label2.Font = new System.Drawing.Font(QuickSand.Families[0], 16F, System.Drawing.FontStyle.Bold);
            this.Font = new System.Drawing.Font(QuickSand.Families[0], 16F, System.Drawing.FontStyle.Bold);
        }

        private void LockApp_Load(object sender, EventArgs e)
        {
            string[] Ports = TinyPort.GetPortNames();
            foreach (string i in Ports)
            {
                RaccoltaPorte.Items.Add(i);
            }
            
            string Code = AskPassword(true);
            try 
            {
                Lucchetto = new PadLock(true, Code , Ports[0]);
                RaccoltaPorte.SelectedItem = Ports[0];
            }
            catch {
                { MessageBox.Show("Impossibile comunicare con il lucchetto", "LockSmart", MessageBoxButtons.OK, MessageBoxIcon.Error); }
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
            Lucchetto.motore.ModifyPort(porta);
        }

        private string AskPassword(bool nuovo)
        {
            string h;
            try { h = File.ReadAllText("Memory.LockSmart"); }
            catch { h = ""; }

            if(h != "")
            {
                if (!nuovo)
                {
                    InputBox box = new InputBox("Cambia Chiave Esistente");
                    if (box.ShowDialog() == DialogResult.OK)
                    {
                        string newcode = box.InText;
                        try 
                        { 
                            Lucchetto.motore.WriteToPort("/changecode>" + newcode, false);
                            Lucchetto.code = newcode;
                            File.WriteAllText("Memory.LockSmart", newcode);
                        }
                        catch { MessageBox.Show("Impossibile comunicare con il lucchetto", "LockSmart", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                        return newcode;
                    }
                    else
                        return h;
                }
                else
                {
                    return h;
                }
            }
            else
            {
                InputBox box = new InputBox("Inserisci Nuova Chiave");
                if (box.ShowDialog() == DialogResult.OK)
                {
                    string newcode = box.InText;
                    File.WriteAllText("Memory.LockSmart", newcode);
                    return newcode;
                }
                else
                {
                    Application.Exit();
                    return h;
                }
            }
        }

        private void ChangeCode_Click(object sender, EventArgs e)
        {
            AskPassword(false);
        }

        private void LockApp_FormClosing(object sender, FormClosingEventArgs e)
        {
            Lucchetto.motore.CheckIfHavetoClose(true);
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
    }
}
