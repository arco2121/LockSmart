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
using System.Windows.Forms.VisualStyles;
using System.Resources;

namespace LockSmart
{
    internal class InputBox:Form
    {
        static PersonalFont font = new PersonalFont();
        static PrivateFontCollection QuickSand = font.QuickSand;

        private TextBox InBox;
        private bool forpassword;
        private Button OK;
        private Label label2;
        private string InText;
        
        /*Finestra utilizzata per richiedere informazioni e password con integrati sistemi di controllo interni tipo un minimo di 6 caratteri per le
         password e un minimo di uno per le informzaioni generali.*/

        public InputBox(string title,bool forpassword)
        {
            InitializeComponent();
            InBox = new TextBox();
            this.forpassword = forpassword;
            OK = new Button();
            this.Text = "Kiwi Lock - " + title;
            this.Controls.Add(InBox);
            this.Controls.Add(OK);
            InBox.Size = new Size(200, 30);
            if(this.forpassword)
            {
                InBox.PasswordChar = '•';
            }
            InBox.Location = new Point((this.ClientSize.Width - InBox.Width) / 2, ((this.ClientSize.Height - InBox.Height) / 2) + 20);
            OK.Location = new Point((this.ClientSize.Width - OK.Width) / 2, InBox.Bottom + 40);
            OK.Text = "Invio";
            InBox.KeyDown += InBox_KeyDown;
            OK.BackColor = Color.FromArgb(186, 178, 57);
            this.BackColor = Color.FromArgb(255, 246, 232);
            OK.ForeColor = Color.FromArgb(32, 22, 8);
            OK.FlatAppearance.BorderSize = 0;
            InBox.BackColor = Color.FromArgb(255, 246, 232);
            OK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            OK.AutoSize = true;
            InBox.ForeColor = Color.FromArgb(32, 22, 8);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            InBox.Font = new System.Drawing.Font(QuickSand.Families[0], 12, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Font = new System.Drawing.Font(QuickSand.Families[0], 12, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            OK.Click += OK_Click;
            InBox.Click += InBox_Click;
            ComponentiAggiuntivi.FinestraAperta = true;
        }

        private void InBox_Click(object sender, EventArgs e)
        {
            if(forpassword)
            {
                if (InBox.PasswordChar == '\0')
                {
                    InBox.PasswordChar = '•';
                }
                else
                {
                    InBox.PasswordChar = '\0';
                }
            }
        }

        public string TextResult
        {
            get
            {
                return this.InText;
            }
        }

        private void OK_Click(object sender, EventArgs e)
        {
            if(InBox.Text != "")
            {
                if (forpassword)
                {
                    if (InBox.Text.Length >= 6)
                    {
                        this.InText = InBox.Text;
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Inserisci almeno 6 caratteri", "Kiwi Lock", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    this.InText = InBox.Text;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Inserisci qualcosa", "Kiwi Lock", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InputBox));
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(121)))), ((int)(((byte)(116)))), ((int)(((byte)(18)))));
            this.label2.Image = global::LockSmart.Properties.Resources.TinyIcon;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point((this.ClientSize.Width - label2.Width) / 2 - 2, 10);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(5);
            this.label2.Size = new System.Drawing.Size(100, 100);
            this.label2.TabIndex = 4;
            // 
            // InputBox
            // 
            this.BackgroundImage = global::LockSmart.Properties.Resources.KiwiBackground;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Controls.Add(this.label2);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InputBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += Chiusura;
            this.ResumeLayout(false);

        }

        private void Chiusura(object e, FormClosingEventArgs es)
        {
            ComponentiAggiuntivi.FinestraAperta = false;
        }

        private void InBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                OK.PerformClick();
            }
        }
    }

    static class ComponentiAggiuntivi
    {
        static public bool FinestraAperta = false;
    }
}
