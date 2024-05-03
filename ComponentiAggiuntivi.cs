using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace LockSmart
{
    internal class InputBox:Form
    {
        private TextBox InBox;
        private Button OK;
        
        public InputBox()
        {
            InBox = new TextBox();
            OK = new Button();
            OK.Click += OK_Click;
            this.Text = "Inserisci Chiave";
            this.Controls.Add(InBox);
            this.Controls.Add(OK);
            InBox.Size = new Size(150, 60);
            InBox.Location = new Point((this.ClientSize.Width - InBox.Width) / 2, (this.ClientSize.Height - InBox.Height) / 2);
            OK.Location = new Point((this.ClientSize.Width - OK.Width) / 2, InBox.Bottom + 30);
            OK.Text = "Invia";
            this.MaximizeBox = false;
            OK.BackColor = Color.FromArgb(255, 173, 132);
            this.BackColor = Color.FromArgb(255, 196, 126);
            OK.FlatAppearance.BorderSize = 0;
            InBox.BackColor = Color.FromArgb(255, 173, 132);
            OK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            OK.AutoSize = true;
            InBox.BorderStyle = BorderStyle.None;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            InBox.Padding = new System.Windows.Forms.Padding(30,30,30,30);
            this.Font = new System.Drawing.Font("Consolas", 12, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }

        public string InText
        {
            get
            {
                return InBox.Text;
            }
            set
            {
                InBox.Text = value;
            }
        }

        private void OK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
