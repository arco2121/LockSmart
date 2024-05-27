﻿using System;
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
        private string InText;
        
        public InputBox(string title,bool forpassword)
        {
            InitializeComponent();
            InBox = new TextBox();
            this.forpassword = forpassword;
            OK = new Button();
            this.Text = "Kiwi Lock - " + title;
            this.Controls.Add(InBox);
            this.Controls.Add(OK);
            InBox.Size = new Size(180, 30);
            InBox.Font = this.Font = new System.Drawing.Font("Microsoft YaHei UI", 12, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            if(this.forpassword)
            {
                InBox.PasswordChar = '•';
            }
            InBox.Location = new Point((this.ClientSize.Width - InBox.Width) / 2, (this.ClientSize.Height - InBox.Height) / 2);
            OK.Location = new Point((this.ClientSize.Width - OK.Width) / 2, InBox.Bottom + 50);
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
            this.SuspendLayout();
            // 
            // InputBox
            // 
            this.BackgroundImage = global::LockSmart.Properties.Resources.KiwiBackground;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InputBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);

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
}
