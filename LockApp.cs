using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LockSmart
{
    public partial class LockApp : Form
    {
        internal PadLock Lucchetto;
        public LockApp()
        {
            InitializeComponent();
        }

        private void LockApp_Load(object sender, EventArgs e)
        {
            try { Lucchetto = new PadLock(true, /*prima chederemo la chiave*/"HH", "COM7"); }
            catch { 
                MessageBox.Show("Errore nel comunicare con il lucchetto."); 
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
    }
}
