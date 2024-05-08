using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using System.Threading;

namespace LockSmart
{
    internal class PadLock
    {
        private bool locked;
        public TinyPort motore;
        public string code;
        public PadLock(bool initialstate, string code, string port)
        {
            this.locked = initialstate;
            this.code = code;
            this.motore = new TinyPort(port);
            this.motore.objecta.DataReceived += this.OutUnLock;
            //this.motore.WriteToPort("/newcode>>>>" + this.code, false);
        }

        public string Locked
        {
            get 
            {
                if(this.locked == true)
                {
                    return "Bloccato";
                }
                else
                {
                    return "Sbloccato";
                }
            }
        }
        public bool LockedBool
        {
            get => this.locked;
        }

        public void UnLock()
        {
            if (!this.locked) 
            {
                MessageBox.Show("Il lucchetto è gia sbloccato", "LockSmart", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string value = "";
                InputBox user = new InputBox("Inserisci Chiave");
                if (user.ShowDialog() == DialogResult.OK)
                {
                    value = user.InText;
                    try 
                    { 
                        this.motore.WriteToPort("/code>>>>" + value,false);
                        Thread.Sleep(50);
                        string h = this.motore.ReadFromPort(false);
                        if (h == "Yes")
                        {
                            this.locked = false;
                        }
                        else
                        {
                            MessageBox.Show("Chiave Errata", "LockSmart", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    catch { MessageBox.Show("Impossibile comunicare con il lucchetto","LockSmart",MessageBoxButtons.OK,MessageBoxIcon.Error); }
                }
            }
        }

        public void Lock()
        {
            if (this.locked)
            {
                MessageBox.Show("Il lucchetto è gia bloccato", "LockSmart", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    this.motore.WriteToPort("true",false);
                    this.locked = true;
                }
                catch { MessageBox.Show("Impossibile comunicare con il lucchetto", "LockSmart", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void OutUnLock(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort port = (SerialPort)sender;
            string res = port.ReadLine();
            if (res == "request")
            {
                if (!this.locked)
                {
                    MessageBox.Show("Il lucchetto è gia sbloccato");
                }
                else
                {
                    string value = "";
                    InputBox user = new InputBox("Inserisci Chiave");
                    if (user.ShowDialog() == DialogResult.OK)
                    {
                        value = user.InText;
                        try
                        {
                            this.motore.WriteToPort("/code>>>>" + value, false);
                            Thread.Sleep(50);
                            string h = this.motore.ReadFromPort(false);
                            if(h == "Yes")
                            {
                                this.locked = false;
                            }
                            else
                            {
                                MessageBox.Show("Chiave Errata", "LockSmart", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                        }
                        catch { MessageBox.Show("Impossibile comunicare con il lucchetto", "LockSmart", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    }
                    else
                    {
                        try { this.motore.WriteToPort("delete", false); }
                        catch { MessageBox.Show("Impossibile comunicare con il lucchetto", "LockSmart", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    }
                }
            }
        }
    }
}
