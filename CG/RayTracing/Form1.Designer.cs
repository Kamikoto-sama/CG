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
            this.rotationZ = new System.Windows.Forms.TextBox();
            this.rotationY = new System.Windows.Forms.TextBox();
            this.rotationX = new System.Windows.Forms.TextBox();
            this.positionZ = new System.Windows.Forms.TextBox();
            this.positionY = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.positionX = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.saveBtn = new System.Windows.Forms.Button();
            this.tracingDepth = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pixelsCount = new System.Windows.Forms.Label();
            this.sizeHeight = new System.Windows.Forms.TextBox();
            this.sizeWidth = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.renderBtn = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) (this.pictureBox)).BeginInit();
            this.SuspendLayout();
            //
            // panel1
            //
            this.panel1.Controls.Add(this.rotationZ);
            this.panel1.Controls.Add(this.rotationY);
            this.panel1.Controls.Add(this.rotationX);
            this.panel1.Controls.Add(this.positionZ);
            this.panel1.Controls.Add(this.positionY);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.positionX);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.saveBtn);
            this.panel1.Controls.Add(this.tracingDepth);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.pixelsCount);
            this.panel1.Controls.Add(this.sizeHeight);
            this.panel1.Controls.Add(this.sizeWidth);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.renderBtn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 67);
            this.panel1.TabIndex = 1;
            //
            // rotationZ
            //
            this.rotationZ.Location = new System.Drawing.Point(380, 40);
            this.rotationZ.Name = "rotationZ";
            this.rotationZ.Size = new System.Drawing.Size(22, 20);
            this.rotationZ.TabIndex = 17;
            this.rotationZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.rotationZ.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ApplyTransform);
            //
            // rotationY
            //
            this.rotationY.Location = new System.Drawing.Point(352, 40);
            this.rotationY.Name = "rotationY";
            this.rotationY.Size = new System.Drawing.Size(22, 20);
            this.rotationY.TabIndex = 16;
            this.rotationY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.rotationY.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ApplyTransform);
            //
            // rotationX
            //
            this.rotationX.Location = new System.Drawing.Point(324, 40);
            this.rotationX.Name = "rotationX";
            this.rotationX.Size = new System.Drawing.Size(22, 20);
            this.rotationX.TabIndex = 15;
            this.rotationX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.rotationX.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ApplyTransform);
            //
            // positionZ
            //
            this.positionZ.Location = new System.Drawing.Point(245, 40);
            this.positionZ.Name = "positionZ";
            this.positionZ.Size = new System.Drawing.Size(22, 20);
            this.positionZ.TabIndex = 14;
            this.positionZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.positionZ.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ApplyTransform);
            //
            // positionY
            //
            this.positionY.Location = new System.Drawing.Point(217, 40);
            this.positionY.Name = "positionY";
            this.positionY.Size = new System.Drawing.Size(22, 20);
            this.positionY.TabIndex = 13;
            this.positionY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.positionY.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ApplyTransform);
            //
            // label4
            //
            this.label4.Location = new System.Drawing.Point(273, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 23);
            this.label4.TabIndex = 12;
            this.label4.Text = "Rotation";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // positionX
            //
            this.positionX.Location = new System.Drawing.Point(189, 40);
            this.positionX.Name = "positionX";
            this.positionX.Size = new System.Drawing.Size(22, 20);
            this.positionX.TabIndex = 9;
            this.positionX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.positionX.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ApplyTransform);
            //
            // label3
            //
            this.label3.Location = new System.Drawing.Point(93, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 23);
            this.label3.TabIndex = 8;
            this.label3.Text = "Camera Position";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // saveBtn
            //
            this.saveBtn.Enabled = false;
            this.saveBtn.Location = new System.Drawing.Point(12, 38);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(75, 23);
            this.saveBtn.TabIndex = 7;
            this.saveBtn.Text = "Save";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            //
            // tracingDepth
            //
            this.tracingDepth.Location = new System.Drawing.Point(177, 14);
            this.tracingDepth.Name = "tracingDepth";
            this.tracingDepth.Size = new System.Drawing.Size(34, 20);
            this.tracingDepth.TabIndex = 6;
            this.tracingDepth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tracingDepth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tracingDepth_KeyPress);
            //
            // label2
            //
            this.label2.Location = new System.Drawing.Point(93, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 23);
            this.label2.TabIndex = 5;
            this.label2.Text = "Tracing depth";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // pixelsCount
            //
            this.pixelsCount.Location = new System.Drawing.Point(338, 12);
            this.pixelsCount.Name = "pixelsCount";
            this.pixelsCount.Size = new System.Drawing.Size(37, 23);
            this.pixelsCount.TabIndex = 4;
            this.pixelsCount.Text = "Count";
            this.pixelsCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // sizeHeight
            //
            this.sizeHeight.Location = new System.Drawing.Point(298, 14);
            this.sizeHeight.Name = "sizeHeight";
            this.sizeHeight.Size = new System.Drawing.Size(34, 20);
            this.sizeHeight.TabIndex = 3;
            this.sizeHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.sizeHeight.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.size_KeyPress);
            //
            // sizeWidth
            //
            this.sizeWidth.Location = new System.Drawing.Point(260, 14);
            this.sizeWidth.Name = "sizeWidth";
            this.sizeWidth.Size = new System.Drawing.Size(32, 20);
            this.sizeWidth.TabIndex = 2;
            this.sizeWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.sizeWidth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.size_KeyPress);
            //
            // label1
            //
            this.label1.Location = new System.Drawing.Point(217, 12);
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
            this.panel2.Location = new System.Drawing.Point(0, 67);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 594);
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

        private System.Windows.Forms.TextBox rotationZ;

        private System.Windows.Forms.TextBox rotationY;

        private System.Windows.Forms.TextBox positionZ;

        private System.Windows.Forms.TextBox rotationX;

        private System.Windows.Forms.TextBox positionY;

        private System.Windows.Forms.TextBox positionX;


        private System.Windows.Forms.Label label4;

        private System.Windows.Forms.Label label3;

        private System.Windows.Forms.SaveFileDialog saveFileDialog;

        private System.Windows.Forms.Button saveBtn;

        private System.Windows.Forms.TextBox tracingDepth;

        private System.Windows.Forms.Label label2;

        private System.Windows.Forms.Label pixelsCount;

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