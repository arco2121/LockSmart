using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace LockSmart
{
    internal class TinyPort : SerialPort
    {
        private string port;
        public SerialPort objecta;

        public TinyPort(string port)
        {
            this.port = port;
            this.objecta = new SerialPort(port, 9600);
            //this.objecta.Open();
        }
        public void ModifyPort(string porta)
        {
            SerialPort port = new SerialPort(porta, 9600);
            this.objecta = port;
        }
        public void CheckIfHavetoClose(bool haveto)
        {
            if (haveto)
            {
                if (this.objecta.IsOpen && this.objecta != null)
                {
                    this.objecta.Close();
                }
            }
        }
        public string Port
        {
            get => this.port;
        }
    }
}