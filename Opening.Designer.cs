namespace LockSmart
{
    partial class Opening
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Opening));
            this.label1 = new System.Windows.Forms.Label();
            this.Lock = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(121)))), ((int)(((byte)(116)))), ((int)(((byte)(18)))));
            this.label1.Location = new System.Drawing.Point(225, 66);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(10);
            this.label1.Size = new System.Drawing.Size(82, 36);
            this.label1.TabIndex = 1;
            this.label1.Text = "Kiwi Lock";
            // 
            // Lock
            // 
            this.Lock.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Lock.FlatAppearance.BorderSize = 0;
            this.Lock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Lock.Image = global::LockSmart.Properties.Resources.NewLock;
            this.Lock.Location = new System.Drawing.Point(292, 201);
            this.Lock.Name = "Lock";
            this.Lock.Size = new System.Drawing.Size(196, 190);
            this.Lock.TabIndex = 2;
            this.Lock.UseVisualStyleBackColor = true;
            this.Lock.Click += new System.EventHandler(this.Lock_Click);
            // 
            // Opening
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(246)))), ((int)(((byte)(232)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Lock);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Opening";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kiwi Lock";
            this.Load += new System.EventHandler(this.Opening_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Lock;
    }
}