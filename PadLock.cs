using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public void UnLock(string value)
        {
            if (this.locked) { }
            else
            {
                if(value == this.code)
                {
                    this.locked = false;
                    this.motore.objecta.Write("true");
                }
            }
        }
        public void Lock()
        {
            this.locked = true;
            this.motore.objecta.Write("true");
        }
    }
}
