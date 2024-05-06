using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using LockSmart.Properties;
using System.Drawing.Text;
using System.IO;

namespace LockSmart
{
    internal class InputBox:Form
    {
        private TextBox InBox;
        private Button OK;
        
        public InputBox(string title)
        {
            PersonalFont font = new PersonalFont();
            PrivateFontCollection QuickSand = font.QuickSand;
            InBox = new TextBox();
            OK = new Button();
            OK.Click += OK_Click;
            this.Text = title;
            this.Controls.Add(InBox);
            this.Controls.Add(OK);
            InBox.Size = new Size(150, 30);
            InBox.Font = this.Font = new System.Drawing.Font("Consolas", 12, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            InBox.Location = new Point((this.ClientSize.Width - InBox.Width) / 2, (this.ClientSize.Height - InBox.Height) / 2);
            OK.Location = new Point((this.ClientSize.Width - OK.Width) / 2, InBox.Bottom + 50);
            OK.Text = "Invia";
            this.MaximizeBox = false;
            OK.BackColor = Color.FromArgb(255, 173, 132);
            this.BackColor = Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(227)))), ((int)(((byte)(130)))));
            OK.ForeColor = Color.Black;
            OK.FlatAppearance.BorderSize = 0;
            InBox.BackColor = Color.FromArgb(255, 173, 132);
            OK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            OK.AutoSize = true;
            InBox.BorderStyle = BorderStyle.None;
            InBox.ForeColor = Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            InBox.Padding = new System.Windows.Forms.Padding(30,30,30,30);
            this.Font = new System.Drawing.Font(QuickSand.Families[0], 12, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
