using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LockSmart
{
    internal class PadLock
    {
        private bool locked;
        private TinyPort motore;
        private string code;
        public PadLock(bool initialstate,string code,string port)
        {
            this.locked = initialstate;
            this.code = code;
            this.motore = new TinyPort(port);
        }

        public bool Locked
        {
            get => this.locked;
        }
        public void UnLock()
        {
            if (!this.locked) 
            {
                MessageBox.Show("Il lucchetto è gia sbloccato");
            }
            else
            {
                string value = "";
                InputBox user = new InputBox();
                if (user.ShowDialog() == DialogResult.OK)
                {
                    value = user.InText;
                    if (value == this.code)
                    {
                        this.locked = false;
                        this.motore.objecta.Write("false");
                    }
                    else
                    {
                        MessageBox.Show("Chiave Errata");
                    }
                }
            }
        }
        public void Lock()
        {
            if(this.locked)
            {
                MessageBox.Show("Il lucchetto è gia bloccato");
            }
            else
            {
                try
                {
                    this.motore.objecta.Write("true");
                    this.locked = true;
                }
                catch { MessageBox.Show("Impossibile bloccare il lucchetto"); }
            }
        }
    }
}
