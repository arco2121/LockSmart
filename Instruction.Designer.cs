namespace LockSmart
{
    partial class Instruction
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Instruction));
            this.Info = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Info
            // 
            this.Info.BackColor = System.Drawing.Color.Transparent;
            this.Info.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Info.FlatAppearance.BorderSize = 0;
            this.Info.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Info.Image = global::LockSmart.Properties.Resources.InfoIcon;
            this.Info.Location = new System.Drawing.Point(180, 31);
            this.Info.Name = "Info";
            this.Info.Padding = new System.Windows.Forms.Padding(10);
            this.Info.Size = new System.Drawing.Size(80, 80);
            this.Info.TabIndex = 12;
            this.Info.UseVisualStyleBackColor = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(22)))), ((int)(((byte)(8)))));
            this.label3.Location = new System.Drawing.Point(63, 149);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(146, 16);
            this.label3.TabIndex = 13;
            this.label3.Text = "Collega il Kiwi PadLock";
            // 
            // Instruction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackgroundImage = global::LockSmart.Properties.Resources.KiwiBackground;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(462, 273);
            this.ControlBox = false;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Info);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Instruction";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kiwi Lock - Aspettando";
            this.Load += new System.EventHandler(this.Instruction_Load);
            this.Shown += new System.EventHandler(this.Instruction_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Info;
        private System.Windows.Forms.Label label3;
    }
}