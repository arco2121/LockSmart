using LockSmart.Properties;
using System.Drawing;
using System.Drawing.Text;
using System.IO;

namespace LockSmart
{
    partial class Settings
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.RaccoltaPorte = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.State = new System.Windows.Forms.Label();
            this.Log = new System.Windows.Forms.Button();
            this.ChangeCode = new System.Windows.Forms.Button();
            this.UnLock = new System.Windows.Forms.Button();
            this.Lock = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // RaccoltaPorte
            // 
            this.RaccoltaPorte.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(246)))), ((int)(((byte)(232)))));
            this.RaccoltaPorte.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RaccoltaPorte.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RaccoltaPorte.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RaccoltaPorte.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(22)))), ((int)(((byte)(8)))));
            this.RaccoltaPorte.FormattingEnabled = true;
            this.RaccoltaPorte.Location = new System.Drawing.Point(872, 36);
            this.RaccoltaPorte.Margin = new System.Windows.Forms.Padding(0);
            this.RaccoltaPorte.Name = "RaccoltaPorte";
            this.RaccoltaPorte.Size = new System.Drawing.Size(151, 24);
            this.RaccoltaPorte.TabIndex = 3;
            this.RaccoltaPorte.SelectedIndexChanged += new System.EventHandler(this.RaccoltaPorte_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(22)))), ((int)(((byte)(8)))));
            this.label2.Location = new System.Drawing.Point(663, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Porta Seriale:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(22)))), ((int)(((byte)(8)))));
            this.label3.Location = new System.Drawing.Point(31, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "Stato:";
            // 
            // State
            // 
            this.State.AutoSize = true;
            this.State.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(22)))), ((int)(((byte)(8)))));
            this.State.Location = new System.Drawing.Point(135, 36);
            this.State.Name = "State";
            this.State.Size = new System.Drawing.Size(0, 16);
            this.State.TabIndex = 8;
            // 
            // Log
            // 
            this.Log.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Log.FlatAppearance.BorderSize = 0;
            this.Log.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Log.Image = ((System.Drawing.Image)(resources.GetObject("Log.Image")));
            this.Log.Location = new System.Drawing.Point(12, 551);
            this.Log.Name = "Log";
            this.Log.Padding = new System.Windows.Forms.Padding(20);
            this.Log.Size = new System.Drawing.Size(80, 78);
            this.Log.TabIndex = 9;
            this.Log.UseVisualStyleBackColor = true;
            this.Log.Click += new System.EventHandler(this.Log_Click);
            // 
            // ChangeCode
            // 
            this.ChangeCode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ChangeCode.FlatAppearance.BorderSize = 0;
            this.ChangeCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ChangeCode.Image = ((System.Drawing.Image)(resources.GetObject("ChangeCode.Image")));
            this.ChangeCode.Location = new System.Drawing.Point(967, 551);
            this.ChangeCode.Name = "ChangeCode";
            this.ChangeCode.Padding = new System.Windows.Forms.Padding(10);
            this.ChangeCode.Size = new System.Drawing.Size(85, 78);
            this.ChangeCode.TabIndex = 6;
            this.ChangeCode.UseVisualStyleBackColor = true;
            this.ChangeCode.Click += new System.EventHandler(this.ChangeCode_Click);
            // 
            // UnLock
            // 
            this.UnLock.Cursor = System.Windows.Forms.Cursors.Hand;
            this.UnLock.FlatAppearance.BorderSize = 0;
            this.UnLock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UnLock.Image = ((System.Drawing.Image)(resources.GetObject("UnLock.Image")));
            this.UnLock.Location = new System.Drawing.Point(579, 183);
            this.UnLock.Name = "UnLock";
            this.UnLock.Padding = new System.Windows.Forms.Padding(10);
            this.UnLock.Size = new System.Drawing.Size(341, 330);
            this.UnLock.TabIndex = 2;
            this.UnLock.UseVisualStyleBackColor = true;
            this.UnLock.Click += new System.EventHandler(this.UnLock_Click);
            // 
            // Lock
            // 
            this.Lock.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Lock.FlatAppearance.BorderSize = 0;
            this.Lock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Lock.Image = ((System.Drawing.Image)(resources.GetObject("Lock.Image")));
            this.Lock.Location = new System.Drawing.Point(138, 183);
            this.Lock.Name = "Lock";
            this.Lock.Padding = new System.Windows.Forms.Padding(10);
            this.Lock.Size = new System.Drawing.Size(341, 330);
            this.Lock.TabIndex = 1;
            this.Lock.UseVisualStyleBackColor = true;
            this.Lock.Click += new System.EventHandler(this.Lock_Click);
            // 
            // LockApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(246)))), ((int)(((byte)(232)))));
            this.ClientSize = new System.Drawing.Size(1064, 641);
            this.Controls.Add(this.Log);
            this.Controls.Add(this.State);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ChangeCode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.RaccoltaPorte);
            this.Controls.Add(this.UnLock);
            this.Controls.Add(this.Lock);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.MaximizeBox = false;
            this.Name = "LockApp";
            this.Text = "Kiwi Lock";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LockApp_FormClosing);
            this.Load += new System.EventHandler(this.LockApp_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button Lock;
        private System.Windows.Forms.Button UnLock;
        private System.Windows.Forms.ComboBox RaccoltaPorte;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button ChangeCode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label State;
        private System.Windows.Forms.Button Log;
    }
}

