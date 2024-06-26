﻿namespace LockSmart
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Opening));
            this.Lock = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.Info = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Lock
            // 
            this.Lock.BackColor = System.Drawing.Color.Transparent;
            this.Lock.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Lock.FlatAppearance.BorderSize = 0;
            this.Lock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Lock.Image = global::LockSmart.Properties.Resources.NewLock;
            this.Lock.Location = new System.Drawing.Point(350, 311);
            this.Lock.Name = "Lock";
            this.Lock.Size = new System.Drawing.Size(160, 160);
            this.Lock.TabIndex = 2;
            this.Lock.UseVisualStyleBackColor = false;
            this.Lock.Click += new System.EventHandler(this.Lock_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(121)))), ((int)(((byte)(116)))), ((int)(((byte)(18)))));
            this.label1.Image = global::LockSmart.Properties.Resources.Logo;
            this.label1.Location = new System.Drawing.Point(100, 13);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(5);
            this.label1.Size = new System.Drawing.Size(660, 290);
            this.label1.TabIndex = 1;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // Info
            // 
            this.Info.BackColor = System.Drawing.Color.Transparent;
            this.Info.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Info.FlatAppearance.BorderSize = 0;
            this.Info.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Info.Image = global::LockSmart.Properties.Resources.InfoIcon;
            this.Info.Location = new System.Drawing.Point(790, 421);
            this.Info.Name = "Info";
            this.Info.Padding = new System.Windows.Forms.Padding(10);
            this.Info.Size = new System.Drawing.Size(60, 60);
            this.Info.TabIndex = 12;
            this.Info.UseVisualStyleBackColor = false;
            this.Info.Click += new System.EventHandler(this.Info_Click);
            // 
            // Opening
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(246)))), ((int)(((byte)(232)))));
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(862, 493);
            this.Controls.Add(this.Info);
            this.Controls.Add(this.Lock);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Opening";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kiwi Lock";
            this.Load += new System.EventHandler(this.Opening_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button Lock;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button Info;
    }
}