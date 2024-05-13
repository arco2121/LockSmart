using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using System.Threading;
using System.IO;

namespace LockSmart
{
    internal class PadLock
    {
        private bool locked;
        public TinyPort motore;
        private string code;
        public PadLock(bool initialstate, string port)
        {
            this.locked = initialstate;
            this.code = AskPassword(true);
            this.motore = new TinyPort(port);
            this.motore.objecta.DataReceived += this.OutUnLock;
        }

        public bool IsCode
        {
            get
            {
                if(this.code == "")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
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
                InputBox user = new InputBox("Inserisci Chiave",true);
                user.ShowDialog();
                if (user.DialogResult == DialogResult.OK)
                {
                    value = user.TextResult;
                    user = null;
                    try 
                    { 
                        if (value == this.code)
                        {
                            this.motore.WriteToPort("OpenPort", false);
                            this.locked = false;
                            string all = File.ReadAllText("Memory.PadLock");
                            string[] param = File.ReadAllLines("Memory.PadLock");
                            File.WriteAllText("Memory.PadLock", all + Criptografia.Cripta(DateTime.Now + " Lucchetto Sbloccato", param[1], param[2]) + "\n");
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
                    this.motore.WriteToPort("ClosePort",false);
                    this.locked = true;
                    string all = File.ReadAllText("Memory.PadLock");
                    string[] param = File.ReadAllLines("Memory.PadLock");
                    File.WriteAllText("Memory.PadLock", all + Criptografia.Cripta(DateTime.Now + " Lucchetto Bloccato", param[1], param[2]) + "\n");
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
                    InputBox usera = new InputBox("Inserisci Chiave",true);
                    usera.ShowDialog();
                    if (usera.DialogResult == DialogResult.OK)
                    {
                        value = usera.TextResult;
                        usera = null;
                        try
                        {
                            if (value == this.code)
                            {
                                this.motore.WriteToPort("OpenPort", false);
                                this.locked = false;
                                string all = File.ReadAllText("Memory.PadLock");
                                string[] param = File.ReadAllLines("Memory.PadLock");
                                File.WriteAllText("Memory.PadLock", all + Criptografia.Cripta(DateTime.Now + " Lucchetto Bloccato", param[1], param[2]) + "\n");
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
                        try { this.motore.WriteToPort("ClosePort", false); }
                        catch { MessageBox.Show("Impossibile comunicare con il lucchetto", "LockSmart", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    }
                }
            }
        }

        private string AskPassword(bool nuovo)
        {
            string h;
            try
            {
                string[] encoded = File.ReadAllLines("Memory.LockSmart");
                h = Criptografia.DeCripta(encoded[0], encoded[1], encoded[2]);
            }
            catch { h = ""; }

            if (h != "")
            {
                if (!nuovo)
                {
                    InputBox box = new InputBox("Inserisci Vecchia Chiave", true);
                    box.ShowDialog();
                    if (box.DialogResult == DialogResult.OK)
                    {
                        string code = box.TextResult;
                        box = null;
                        if (code == h)
                        {
                            InputBox lox = new InputBox("Inserisci Nuova Chiave",true);
                            lox.ShowDialog();
                            if (lox.DialogResult == DialogResult.OK)
                            {
                                string newcode = lox.TextResult;
                                lox = null;
                                try
                                {
                                    this.code = newcode;
                                    string[] param = Criptografia.GeneraParametri();
                                    string encoded = Criptografia.Cripta(newcode, param[0], param[1]) + "\n" + param[0] + "\n" + param[1] + "\n" + Criptografia.Cripta(DateTime.Now + " Password Cambiata",param[0],param[1]) + "\n";
                                    File.WriteAllText("Memory.LockSmart", encoded);
                                }
                                catch { MessageBox.Show("Impossibile comunicare con il lucchetto", "LockSmart", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                                return newcode;
                            }
                            else
                            {
                                return h;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Chiave Errata", "LockSmart", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return h;
                        }
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
                InputBox box = new InputBox("Inserisci Prima Chiave", true);
                box.ShowDialog();
                if (box.DialogResult == DialogResult.OK)
                {
                    string newcode = box.TextResult;
                    box = null;
                    string[] param = Criptografia.GeneraParametri();
                    string encoded = Criptografia.Cripta(newcode, param[0], param[1]) + "\n" + param[0] + "\n" + param[1];
                    File.WriteAllText("Memory.LockSmart", encoded);
                    return newcode;
                }
                else
                {
                    Application.Exit();
                    return h;
                }
            }
        }

        public void ChangeCode()
        {
            this.code = AskPassword(false);
        }

        public void GenerateLog()
        {
            try
            {
                string[] content = File.ReadAllLines("Memory.PadLock");
                string text = "";
                for(int i = 3; i<content.Length;  i++)
                {
                    text += Criptografia.DeCripta(content[i], content[1], content[2]) + "\n";
                }
                File.WriteAllText("Log.PadLock",text);
            }
            catch
            {
                MessageBox.Show("Impossibile generare il log", "LockSmart", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
    }
}
