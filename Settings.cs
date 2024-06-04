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
            /*Eseguito quando è richiamata la schermata di gestione del PadLock, prima da il nome alla finestra con in annesso il nome del PadLock dell'utente
             e successivamente inizializza l'effetivo PadLock da gestire e la lista di porte seriale disponibili*/
            this.Text = "Kiwi Lock - " + this.nome;
            string[] Ports = SerialPort.GetPortNames();
            for (int i = 0; i < Ports.Length; i++)
            {
                RaccoltaPorte.Items.Add(Ports[i]);
            }
            Lucchetto = new PadLock(this.instate, this.pass, this.nome, this.porta, false, true);
            Lucchetto.AcivateCheck();
            RaccoltaPorte.SelectedItem = Lucchetto.motore.PortName;
            RaccoltaPorte.SelectedIndexChanged += RaccoltaPorte_SelectedIndexChanged;
        }

        private void Lock_Click(object sender, EventArgs e)
        {
            Lucchetto.Lock();
        }

        private void UnLock_Click(object sender, EventArgs e)
        {
            Lucchetto.UnLock();
        }

        private  void RaccoltaPorte_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*Funzione per cambiare la porta seriale del PadLock attuale
            if(!ComponentiAggiuntivi.FinestraAperta)
            {
                try
                {
                    ComboBox Ogg = (ComboBox)sender;
                    string porta = Ogg.SelectedItem.ToString();
                    try
                    {
                        Lucchetto.motore.Write("2");
                        Lucchetto.Eliminando = true;
                        Lucchetto.motore.Close();
                    }
                    catch
                    {

                    }
                    PadLock NewLock = new PadLock(this.instate, this.pass, this.nome, porta, true, true);
                    NewLock.AcivateCheck();
                    Lucchetto = NewLock;
                    RaccoltaPorte.SelectedIndexChanged += null;
                    RaccoltaPorte.SelectedItem = Lucchetto.motore.PortName;
                    RaccoltaPorte.SelectedIndexChanged += RaccoltaPorte_SelectedIndexChanged;
                }
                catch
                {
                    MessageBox.Show("Impossibile comunicare con il Kiwi PadLock", "Kiwi Lock", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                RaccoltaPorte.SelectedIndex = RaccoltaPorte.SelectedIndex;
            }*/
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

        private async Task UpdateAll()
        {
            /*Funzione che controlla le porte seiali connesse se aggiungerle o toglierle dalla lista*/
            while(true)
            {
                State.Text = Lucchetto.Locked;
               try
                {
                   if(!ComponentiAggiuntivi.FinestraAperta)
                    {
                        string[] Ports = SerialPort.GetPortNames();
                        if (Selected != null && Selected.Length != Ports.Length)
                        {
                            foreach (string port in Ports)
                            {
                                if (RaccoltaPorte.Items.IndexOf(port) == -1)
                                {
                                    try
                                    {

                                        PadLock Momentum = new PadLock(this.instate, this.pass, this.nome, port, true, false);
                                        Momentum = null;
                                        RaccoltaPorte.Items.Add(port);
                                    }
                                    catch
                                    {

                                    }
                                }
                            }

                            for (int i = RaccoltaPorte.Items.Count - 1; i >= 0; i--)
                            {
                                if (Array.IndexOf(Ports, RaccoltaPorte.Items[i].ToString()) == -1)
                                {
                                    RaccoltaPorte.Items.RemoveAt(i);
                                }
                            }
                            Selected = Ports;
                        }
                    }
                }
                catch
                {

                }
                await Task.Delay(10);
            }
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
                    Lucchetto.Eliminando = true;
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
                MessageBox.Show("Impossibile eliminare il Kiwi PadLock", "Kiwi Lock", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Info_Click(object sender, EventArgs e)
        {
            Informazioni Info = new Informazioni();
            Info.ShowDialog();
        }

        private async void Settings_Shown(object sender, EventArgs e)
        {
            await UpdateAll();
        }
    }
}
