using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LockSmart
{
    public partial class Informazioni : Form
    {
        static PersonalFont font = new PersonalFont();
        static PrivateFontCollection QuickSand = font.QuickSand;
        public Informazioni()
        {
            InitializeComponent();
            this.label4.LinkClicked += Label4_LinkClicked;
            this.label3.Font = new System.Drawing.Font(QuickSand.Families[0],20F, System.Drawing.FontStyle.Bold);
            this.label4.Font = new System.Drawing.Font(QuickSand.Families[0], 15F, System.Drawing.FontStyle.Bold);
            this.label5.Font = new System.Drawing.Font(QuickSand.Families[0], 15F, System.Drawing.FontStyle.Bold);
        }

        private void Label4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/arco2121/LockSmart");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://docs.google.com/document/d/1iKKaZuwRTrkhoRk9N2YKxfXCy-Y4JO15MOydd83s0bQ/edit?usp=sharing");
        }
    }
}
