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
    public partial class Opening : Form
    {
        static PersonalFont font = new PersonalFont();
        static PrivateFontCollection QuickSand = font.QuickSand;
        public Opening()
        {
            InitializeComponent();
            this.label1.Font = new System.Drawing.Font(QuickSand.Families[0], 40F, System.Drawing.FontStyle.Bold);
        }

        private void Lock_Click(object sender, EventArgs e)
        {
            InputBox Nome = new InputBox("Nome",false);
            Nome.ShowDialog();
            if(Nome.DialogResult == DialogResult.OK)
            {
                string h = Nome.TextResult;
                File.WriteAllText("Memory.PadLock", h + "\n" + "true" + "\n");
                Settings Lucchetteria = new Settings();
                Lucchetteria.ShowDialog();
                this.Close();
            }
        }

        private void Opening_Load(object sender, EventArgs e)
        {
            string h;
            try
            {
                string[] k = File.ReadAllLines("Memory.PadLock");
                h = k[2];
            }
            catch
            {
                h = "";
            }
            if (h != "")
            {
                Settings Lucchetteria = new Settings();
                Lucchetteria.ShowDialog();
                this.Close();
            }
            else
            {

            }
        }
    }
}
