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
        public bool Eliminando;

        public PadLock(bool initialstate, string code, string nome, string port,bool Official,bool SendFirstSignal)
        {
            /*Si inizializza il PadLock e se Official è su true si controlla anche se si tratta vwramente di un Kiwi PadLock, utilizato
             * più che altro per la fase di connessione e di riconnessione nel caso di selezione di un'alta porta seriale o si disconnette 
             * il PadLock. Quansdo si va a inizializzare un PadLock per la prima volta si verra a creare un File di memoria che serve al programa per identificare 
             e registrare il PadLock associato a quel file di memoria. Tuute le informazioni salvate nel file sono salvate in maniera da essere criptate*/
            this.locked = initialstate;
            this.nome = nome;
            this.code = code;
            this.Eliminando = false;
            this.motore = new SerialPort(port, 9600);
            this.motore.Open();
            if(Official)
            {
                string exe = CheckOfficial();
                if (exe != "OK")
                {
                    this.motore.Close();
                    throw new Exception("Non è un Kiwi PadLock");
                }
            }
            if(SendFirstSignal)
            {
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
            }
            this.motore.DataReceived += this.OutUnLock;

        }

        private PadLock(bool initialstate, string nome)
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


        /*Nel caso in cui FinestraAperta (globale) sia su true allora le richieste della porta seriale vengono ignorate, per evitare interruzioni nelle
         azioni dell'utente. Lo stsso vale nel caso in cui il'Utente cerchi di buggare il programma aprendo piu finestre contemporaneamente e quindi inviando piu richieste
        al PadLock*/
        public void UnLock()
        {
            if(!ComponentiAggiuntivi.FinestraAperta)
            {
                try
                {
                    this.motore.Write("2");
                    if (!this.locked)
                    {
                        MessageBox.Show("Il Kiwi PadLock è gia sbloccato", "Kiwi Lock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                                    File.WriteAllText("Memory.PadLock", all + Criptografia.Cripta(DateTime.Now + " Kiwi PadLock Sbloccato", param[3], param[4]) + "\n");
                                }
                                else
                                {
                                    MessageBox.Show("Chiave Errata", "Kiwi Lock", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    this.motore.Write("4");
                                }
                            }
                            catch { MessageBox.Show("Impossibile comunicare con il Kiwi PadLock", "Kiwi Lock", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                        }
                        else
                        {
                            this.motore.Write("4");
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Impossibile comunicare con il Kiwi PadLock", "Kiwi Lock", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void Lock()
        {
            if (this.locked)
            {
                MessageBox.Show("Il Kiwi PadLock è gia bloccato", "Kiwi Lock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    File.WriteAllText("Memory.PadLock", all + Criptografia.Cripta(DateTime.Now + " Kiwi PadLock Bloccato", param[3], param[4]) + "\n");
                }
                catch { MessageBox.Show("Impossibile comunicare con il Kiwi PadLock", "Kiwi Lock", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void OutUnLock(object sender, SerialDataReceivedEventArgs e)
        {
           string res = "";
           try
            {
                res = this.motore.ReadExisting();
            }
            catch
            {

            }
            if (res == "s")
            {
                if(!ComponentiAggiuntivi.FinestraAperta)
                {
                    if (!this.locked)
                    {
                        MessageBox.Show("Il Kiwi PadLock è gia sbloccato", "Kiwi Lock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                                    File.WriteAllText("Memory.PadLock", all + Criptografia.Cripta(DateTime.Now + " Kiwi PadLock Sbloccato", param[3], param[4]) + "\n");
                                }
                                else
                                {
                                    MessageBox.Show("Chiave Errata", "Kiwi Lock", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    this.motore.Write("4");
                                }
                            }
                            catch { MessageBox.Show("Impossibile comunicare con il Kiwi PadLock", "Kiwi Lock", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                        }
                        else
                        {
                            try { this.motore.Write("4"); }
                            catch { MessageBox.Show("Impossibile comunicare con il Kiwi PadLock", "Kiwi Lock", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                        }
                    }
                }
                else
                {
                    try { this.motore.Write("4"); }
                    catch { MessageBox.Show("Impossibile comunicare con il Kiwi PadLock", "Kiwi Lock", MessageBoxButtons.OK, MessageBoxIcon.Error); }
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
                        if (code == h)
                        {
                            InputBox lox = new InputBox("Nuova Chiave", true);
                            lox.ShowDialog();
                            if (lox.DialogResult == DialogResult.OK)
                            {
                                string newcode = lox.TextResult;
                                try
                                {
                                    this.code = newcode;
                                    string all = "";
                                    string[] text = File.ReadAllLines("Memory.PadLock");
                                    for (int i = 0; i < text.Length; i++)
                                    {
                                        if(i==2)
                                        {
                                            all += Criptografia.Cripta(newcode, text[3], text[4]) + "\n";
                                        }
                                        else if(i == 1)
                                        {
                                            all += Criptografia.Cripta(this.locked + "", text[3], text[4]) + "\n";
                                        }
                                        else
                                        {
                                            all += text[i] + "\n";
                                        }
                                    }
                                    File.WriteAllText("Memory.PadLock", all + Criptografia.Cripta(DateTime.Now + " Password Cambiata", text[3], text[4]));
                                }
                                catch { MessageBox.Show("Impossibile comunicare con il Kiwi PadLock", "Kiwi Lock", MessageBoxButtons.OK, MessageBoxIcon.Error); }
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
                InputBox box = new InputBox("Prima Chiave", true);
                box.ShowDialog();
                if (box.DialogResult == DialogResult.OK)
                {
                    string newcode = box.TextResult;
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
                    return h;
                }
            }
        }

        public void ChangeCode()
        {
           if(!ComponentiAggiuntivi.FinestraAperta)
            {
                try
                {
                    this.motore.Write("2");
                    this.code = AskPassword(false);
                    this.motore.Write("4");
                }
                catch
                {
                    MessageBox.Show("Impossibile comunicare con il Kiwi PadLock", "Kiwi Lock", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
                    string download = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
                    string filePath = Path.Combine(download, "Log-PadLock.txt");
                    File.WriteAllText(filePath, text);
                    MessageBox.Show("Log Generato", "Kiwi Lock", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("Nessun dato di Log", "Kiwi Lock", MessageBoxButtons.OK);
                }
            }
            catch
            {
                MessageBox.Show("Impossibile generare il Log", "Kiwi Lock", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string CheckOfficial()
        {
            /*Il programma tramite 'C' richiede al PadLock di reinviare indietro 'H' per determinare se è connesso un effettivo PadLock*/
            const int timeout = 500;
            this.motore.Write("C");
            DateTime start = DateTime.Now;
            while (true)
            {
                if ((DateTime.Now - start).TotalMilliseconds > timeout)
                {
                    return "NO";
                }
                string rec = this.motore.ReadExisting();
                if (rec == "H")
                {
                    return "OK";
                }
            }
        }

        public async Task AcivateCheck()
        {
            /*Questo serve per determinare continuamente se il PadLock è ancora connesso, in caso contriario il programma si riavvia in maniera
             tale da tornare nella schermata di connessione del PadLock*/
            while(true)
            {
                if(!this.Eliminando && !this.motore.IsOpen)
                {
                    File.WriteAllText("FirstSetup", "true");
                    File.WriteAllText("Reloading", "true");
                    Application.Restart();
                    return;
                }
                await Task.Delay(10);
            }
        }
    }
}
