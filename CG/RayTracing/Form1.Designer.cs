namespace RayTracing
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.pixelsCount = new System.Windows.Forms.Label();
            this.sizeHeight = new System.Windows.Forms.TextBox();
            this.sizeWidth = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.renderBtn = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) (this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pixelsCount);
            this.panel1.Controls.Add(this.sizeHeight);
            this.panel1.Controls.Add(this.sizeWidth);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.renderBtn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 45);
            this.panel1.TabIndex = 1;
            // 
            // pixelsCount
            // 
            this.pixelsCount.Location = new System.Drawing.Point(225, 10);
            this.pixelsCount.Name = "pixelsCount";
            this.pixelsCount.Size = new System.Drawing.Size(37, 23);
            this.pixelsCount.TabIndex = 4;
            this.pixelsCount.Text = "Count";
            this.pixelsCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // sizeHeight
            // 
            this.sizeHeight.Location = new System.Drawing.Point(185, 12);
            this.sizeHeight.Name = "sizeHeight";
            this.sizeHeight.Size = new System.Drawing.Size(34, 20);
            this.sizeHeight.TabIndex = 3;
            this.sizeHeight.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.size_KeyPress);
            // 
            // sizeWidth
            // 
            this.sizeWidth.Location = new System.Drawing.Point(147, 12);
            this.sizeWidth.Name = "sizeWidth";
            this.sizeWidth.Size = new System.Drawing.Size(32, 20);
            this.sizeWidth.TabIndex = 2;
            this.sizeWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.sizeWidth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.size_KeyPress);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(104, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "Size";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // renderBtn
            // 
            this.renderBtn.Location = new System.Drawing.Point(12, 12);
            this.renderBtn.Name = "renderBtn";
            this.renderBtn.Size = new System.Drawing.Size(75, 23);
            this.renderBtn.TabIndex = 0;
            this.renderBtn.Text = "Render";
            this.renderBtn.UseVisualStyleBackColor = true;
            this.renderBtn.Click += new System.EventHandler(this.renderBtn_Click);
            this.renderBtn.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.renderBtn_KeyPress);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pictureBox);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 45);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 616);
            this.panel2.TabIndex = 2;
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(346, 287);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            this.pictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox_Paint);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(800, 661);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Location = new System.Drawing.Point(15, 15);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize) (this.pictureBox)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label pixelsCount;

        private System.Windows.Forms.Label label2;

        private System.Windows.Forms.TextBox sizeWidth;
        private System.Windows.Forms.TextBox sizeHeight;

        private System.Windows.Forms.Label label1;

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Button renderBtn;

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;

        #endregion
    }
}