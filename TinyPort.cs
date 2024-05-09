using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Windows.Forms;

namespace LockSmart
{
    internal class TinyPort : SerialPort
    {
        public SerialPort objecta;

        public TinyPort(string port)
        {
            this.objecta = new SerialPort(port, 9600);
            //this.objecta.Open();
        }
        public void ModifyPort(string porta)
        {
            this.objecta.PortName = porta;
        }
        public void CheckIfHavetoClose(bool haveto)
        {
            if (haveto)
            {
                if (this.objecta != null && this.objecta.IsOpen)
                {
                    this.objecta.Close();
                }
            }
        }

        public void WriteToPort(string message, bool havetoclose)
        {
            this.objecta.WriteLine(message);
            this.CheckIfHavetoClose(havetoclose);
        }

        public string ReadFromPort(bool havetoclose)
        {
            if(this.objecta.IsOpen)
            {
                string res = this.objecta.ReadLine();
                this.CheckIfHavetoClose(havetoclose);
                return res;
            }
            else
            {
                return null;
            }
        }

        public string WaitAndRead()
        {
            while (true)
            {
                string rep = this.ReadFromPort(false);
                if (rep.Contains("Ok"))
                {
                    string[] repa = rep.Split('>');
                    return repa[1];
                }
            }
        }

        public string Port
        {
            get => this.objecta.PortName;
        }
    }
}
