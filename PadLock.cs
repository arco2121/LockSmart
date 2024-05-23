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
        public SerialPort motore;
        private string code;
        public string nome;
        public PadLock(bool initialstate, string code, string nome, string port)
        {
            this.locked = initialstate;
            this.nome = nome;
            this.code = code;
            this.motore = new SerialPort(port, 9600);
            this.motore.Open();
            try
            {
                if (this.locked)
                {
                    this.motore.Write("0");
                }
                else if (!this.locked)
                {
                    this.motore.Write("1");
                }
            }
            catch
            {

            }
            this.motore.DataReceived += this.OutUnLock;
        }

        public PadLock(bool initialstate, string nome)
        {
            this.nome = nome;
            this.locked = initialstate;
        }

        public bool IsCode
        {
            get
            {
                if (this.code == "")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public string Code
        {
            get => this.code;
        }

        public string Locked
        {
            get
            {
                if (this.locked == true)
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
            try
            {
                this.motore.Write("2");
                if (!this.locked)
                {
                    MessageBox.Show("Il lucchetto è gia sbloccato", "Kiwi Lock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.motore.Write("4");
                }
                else
                {
                    string value = "";
                    InputBox user = new InputBox("Chiave", true);
                    user.ShowDialog();
                    if (user.DialogResult == DialogResult.OK)
                    {
                        value = user.TextResult;
                        user = null;
                        try
                        {
                            if (value == this.code)
                            {
                                this.motore.Write("1");
                                this.locked = false;
                                string all = "";
                                string[] param = File.ReadAllLines("Memory.PadLock");
                                for (int i = 0; i < param.Length; i++)
                                {
                                    if (i != 1)
                                    {
                                        all += param[i] + "\n";
                                    }
                                    else
                                    {
                                        all += Criptografia.Cripta(this.locked + "", param[3], param[4]) + "\n";
                                    }
                                }
                                File.WriteAllText("Memory.PadLock", all + Criptografia.Cripta(DateTime.Now + " Lucchetto Sbloccato", param[3], param[4]) + "\n");
                            }
                            else
                            {
                                MessageBox.Show("Chiave Errata", "Kiwi Lock", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                this.motore.Write("4");
                            }
                        }
                        catch { MessageBox.Show("Impossibile comunicare con il lucchetto", "Kiwi Lock", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    }
                    else
                    {
                        this.motore.Write("4");
                    }
                }
            }
            catch
            {
                MessageBox.Show("Impossibile comunicare con il lucchetto", "Kiwi Lock", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Lock()
        {
            if (this.locked)
            {
                MessageBox.Show("Il lucchetto è gia bloccato", "Kiwi Lock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                try
                {
                    this.motore.Write("4");
                }
                catch { }
            }

            else
            {
                try
                {
                    this.motore.Write("0");
                    this.locked = true;
                    string all = "";
                    string[] param = File.ReadAllLines("Memory.PadLock");
                    for (int i = 0; i < param.Length; i++)
                    {
                        if (i != 1)
                        {
                            all += param[i] + "\n";
                        }
                        else
                        {
                            all += Criptografia.Cripta(this.locked + "", param[3], param[4]) + "\n";
                        }
                    }
                    File.WriteAllText("Memory.PadLock", all + Criptografia.Cripta(DateTime.Now + " Lucchetto Bloccato", param[3], param[4]) + "\n");
                }
                catch { MessageBox.Show("Impossibile comunicare con il lucchetto", "Kiwi Lock", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void OutUnLock(object sender, SerialDataReceivedEventArgs e)
        {
            string res = this.motore.ReadExisting();
            if (res == "s")
            {
                if (!this.locked)
                {
                    MessageBox.Show("Il lucchetto è gia sbloccato");
                    this.motore.Write("4");
                }
                else
                {
                    string value = "";
                    InputBox usera = new InputBox("Chiave", true);
                    usera.ShowDialog();
                    if (usera.DialogResult == DialogResult.OK)
                    {
                        value = usera.TextResult;
                        usera = null;
                        try
                        {
                            if (value == this.code)
                            {
                                this.motore.Write("1");
                                this.locked = false;
                                string all = "";
                                string[] param = File.ReadAllLines("Memory.PadLock");
                                for (int i = 0; i < param.Length; i++)
                                {
                                    if (i != 1)
                                    {
                                        all += param[i] + "\n";
                                    }
                                    else
                                    {
                                        all += Criptografia.Cripta(this.locked + "", param[3], param[4]) + "\n";
                                    }
                                }
                                File.WriteAllText("Memory.PadLock", all + Criptografia.Cripta(DateTime.Now + " Lucchetto Sbloccato", param[3], param[4]) + "\n");
                            }
                            else
                            {
                                MessageBox.Show("Chiave Errata", "Kiwi Lock", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                this.motore.Write("4");
                            }
                        }
                        catch { MessageBox.Show("Impossibile comunicare con il lucchetto", "Kiwi Lock", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    }
                    else
                    {
                        try { this.motore.Write("4"); }
                        catch { MessageBox.Show("Impossibile comunicare con il lucchetto", "Kiwi Lock", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    }
                }
            }
        }

        static public string NewPassword(string initialnome)
        {
            PadLock FirstSetupPassword = new PadLock(true, initialnome);
            return FirstSetupPassword.AskPassword(true);
        }

        private string AskPassword(bool nuovo)
        {
            string h;
            try
            {
                string[] encoded = File.ReadAllLines("Memory.PadLock");
                h = Criptografia.DeCripta(encoded[2], encoded[3], encoded[4]);
            }
            catch { h = ""; }


            if (h != "")
            {
                if (!nuovo)
                {
                    InputBox box = new InputBox("Vecchia Chiave", true);
                    box.ShowDialog();
                    if (box.DialogResult == DialogResult.OK)
                    {
                        string code = box.TextResult;
                        box = null;
                        if (code == h)
                        {
                            InputBox lox = new InputBox("Nuova Chiave", true);
                            lox.ShowDialog();
                            if (lox.DialogResult == DialogResult.OK)
                            {
                                string newcode = lox.TextResult;
                                lox = null;
                                try
                                {
                                    this.code = newcode;
                                    string all = "";
                                    string[] param = Criptografia.GeneraParametri();
                                    for (int i = 0; i < param.Length; i++)
                                    {
                                        if (i != 1)
                                        {
                                            all += param[i] + "\n";
                                        }
                                        else
                                        {
                                            all += Criptografia.Cripta(this.locked + "", param[3], param[4]) + "\n";
                                        }
                                    }
                                    File.WriteAllText("Memory.PadLock", all + Criptografia.Cripta(DateTime.Now + " Password Cambiata", param[3], param[4]) + "\n");
                                }
                                catch { MessageBox.Show("Impossibile comunicare con il lucchetto", "Kiwi Lock", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                                return newcode;
                            }
                            else
                            {
                                return h;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Chiave Errata", "Kiwi Lock", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                while (true)
                {
                    InputBox box = new InputBox("Prima Chiave", true);
                    box.ShowDialog();
                    if (box.DialogResult == DialogResult.OK)
                    {
                        string newcode = box.TextResult;
                        box = null;
                        string[] param = Criptografia.GeneraParametri();
                        string encoded = Criptografia.Cripta(this.nome, param[0], param[1]) + "\n" + Criptografia.Cripta("" + this.locked, param[0], param[1]) + "\n" + Criptografia.Cripta(newcode, param[0], param[1]) + "\n" + param[0] + "\n" + param[1];
                        File.WriteAllText("Memory.PadLock", encoded);
                        File.WriteAllText("FirstSetup", "true");
                        File.WriteAllText("Reloading", "true");
                        Application.Restart();
                        return newcode;
                    }
                    else
                    {

                    }
                }
            }
        }

        public void ChangeCode()
        {
            try
            {
                this.motore.Write("2");
                this.code = AskPassword(false);
                this.motore.Write("4");
            }
            catch
            {
                MessageBox.Show("Impossibile comunicare con il lucchetto", "Kiwi Lock", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void GenerateLog()
        {
            try
            {
                string[] content = File.ReadAllLines("Memory.PadLock");
                string text = "";
                for (int i = 5; i < content.Length; i++)
                {
                    text += Criptografia.DeCripta(content[i], content[3], content[4]) + "\n";
                }
                if (text != "")
                {
                    File.WriteAllText("Log-PadLock.txt", text);
                    MessageBox.Show("Log Generato", "Kiwi Lock", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("Nessun dato di Log", "Kiwi Lock", MessageBoxButtons.OK);
                }
            }
            catch
            {
                MessageBox.Show("Impossibile generare il log", "Kiwi Lock", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
