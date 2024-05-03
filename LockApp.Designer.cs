using System.Drawing;
using System.Drawing.Text;

namespace LockSmart
{
    partial class LockApp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LockApp));
            this.label1 = new System.Windows.Forms.Label();
            this.Lock = new System.Windows.Forms.Button();
            this.UnLock = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Consolas", 40F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(173)))), ((int)(((byte)(132)))));
            this.label1.Location = new System.Drawing.Point(289, 104);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(10);
            this.label1.Size = new System.Drawing.Size(424, 99);
            this.label1.TabIndex = 0;
            this.label1.Text = "Lock Smart";
            // 
            // Lock
            // 
            this.Lock.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Lock.BackgroundImage")));
            this.Lock.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Lock.FlatAppearance.BorderSize = 0;
            this.Lock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Lock.Location = new System.Drawing.Point(194, 259);
            this.Lock.Name = "Lock";
            this.Lock.Size = new System.Drawing.Size(147, 147);
            this.Lock.TabIndex = 1;
            this.Lock.UseVisualStyleBackColor = true;
            this.Lock.Click += new System.EventHandler(this.Lock_Click);
            // 
            // UnLock
            // 
            this.UnLock.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("UnLock.BackgroundImage")));
            this.UnLock.Cursor = System.Windows.Forms.Cursors.Hand;
            this.UnLock.FlatAppearance.BorderSize = 0;
            this.UnLock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UnLock.Location = new System.Drawing.Point(625, 259);
            this.UnLock.Name = "UnLock";
            this.UnLock.Size = new System.Drawing.Size(147, 147);
            this.UnLock.TabIndex = 2;
            this.UnLock.UseVisualStyleBackColor = true;
            this.UnLock.Click += new System.EventHandler(this.UnLock_Click);
            // 
            // LockApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(227)))), ((int)(((byte)(130)))));
            this.ClientSize = new System.Drawing.Size(982, 553);
            this.Controls.Add(this.UnLock);
            this.Controls.Add(this.Lock);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Consolas", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.MaximizeBox = false;
            this.Name = "LockApp";
            this.Text = "LockSmart";
            this.Load += new System.EventHandler(this.LockApp_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button Lock;
        private System.Windows.Forms.Button UnLock;
        private System.Windows.Forms.Label label1;
    }
}

