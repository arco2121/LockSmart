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
            /*Eseguito quando si richiama il modulo. Innanzitutto controlla se se deve fare gia l'autenticazione o è appena stato fatto
             il riavvio in seguito all processo di creazione. Una volta superato questo passaggio, Viene eseguita il set di istruzione previsto per l'avvio della finestra (Shown*/
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
        }

        private async void Instruction_Shown(object sender, EventArgs e)
        {
            await InitializeAll();
        }

        private async Task InitializeAll()
        {
            /*Da qui iniza un ciclo continuo che controlla che sia stato collegato un Kiwi PadLock (andando a creare un oggetto PadLock con le dobute caratteristiche), e 
             * se risulta effettivamente collegato(cioè non entra nel catch), la seguente schermata verrà chiusa e si aprira la schermata effettiva di gestione del PadLock, interrompendo anche il ciclo.
            Il controllo è applicato alle porte individuate dal SerialPort.GetPortnames().*/
            bool o = false;
            while (!o)
            {
                string[] Ports = SerialPort.GetPortNames();
                for (int i = Ports.Length - 1; i >= 0; i--)
                {
                    try
                    {
                        PadLock Lucchetto = new PadLock(this.instate, this.pass, this.nome, Ports[i],true,false);
                        if (!Lucchetto.IsCode)
                        {
                            File.WriteAllText("Reloading", "true");
                            Application.Restart();
                        }
                        o = true;
                        Lucchetto.motore.Close();
                        Lucchetto = null;
                        Settings Impostazioni = new Settings(this.instate, this.pass, this.nome, Ports[i]);
                        this.Hide();
                        Impostazioni.ShowDialog();
                        return;
                    }
                    catch
                    {
                        o = false;
                    }
                }
                await Task.Delay(10);
            }
        }
    }
}
