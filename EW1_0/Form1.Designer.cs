namespace EW1_0
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.Pic_view = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Status = new System.Windows.Forms.Label();
            this.ConnectPnl = new System.Windows.Forms.Panel();
            this.Connect_BTN = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.P_Minimize = new System.Windows.Forms.PictureBox();
            this.P_Maximize = new System.Windows.Forms.PictureBox();
            this.P_Close = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pic_view)).BeginInit();
            this.ConnectPnl.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.P_Minimize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.P_Maximize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.P_Close)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.Pic_view);
            this.panel1.Location = new System.Drawing.Point(751, 48);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(123, 30);
            this.panel1.TabIndex = 13;
            // 
            // Pic_view
            // 
            this.Pic_view.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Pic_view.Image = ((System.Drawing.Image)(resources.GetObject("Pic_view.Image")));
            this.Pic_view.Location = new System.Drawing.Point(0, 0);
            this.Pic_view.Name = "Pic_view";
            this.Pic_view.Size = new System.Drawing.Size(119, 26);
            this.Pic_view.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Pic_view.TabIndex = 2;
            this.Pic_view.TabStop = false;
            this.Pic_view.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Pic_view_MouseDown);
            this.Pic_view.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Pic_view_MouseMove_1);
            this.Pic_view.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Pic_view_MouseUp);
            this.Pic_view.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.Pic_View_MouseWheel);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 457);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 24);
            this.label2.TabIndex = 12;
            this.label2.Text = "Status : ";
            // 
            // Status
            // 
            this.Status.AutoSize = true;
            this.Status.Location = new System.Drawing.Point(109, 458);
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(0, 24);
            this.Status.TabIndex = 17;
            // 
            // ConnectPnl
            // 
            this.ConnectPnl.Controls.Add(this.Connect_BTN);
            this.ConnectPnl.Controls.Add(this.textBox1);
            this.ConnectPnl.Controls.Add(this.label1);
            this.ConnectPnl.Location = new System.Drawing.Point(315, 165);
            this.ConnectPnl.Name = "ConnectPnl";
            this.ConnectPnl.Size = new System.Drawing.Size(262, 173);
            this.ConnectPnl.TabIndex = 21;
            // 
            // Connect_BTN
            // 
            this.Connect_BTN.Location = new System.Drawing.Point(80, 84);
            this.Connect_BTN.Name = "Connect_BTN";
            this.Connect_BTN.Size = new System.Drawing.Size(144, 39);
            this.Connect_BTN.TabIndex = 14;
            this.Connect_BTN.Text = "Connect !";
            this.Connect_BTN.UseVisualStyleBackColor = true;
            this.Connect_BTN.Click += new System.EventHandler(this.Connect_BTN_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(80, 49);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(144, 29);
            this.textBox1.TabIndex = 13;
            this.textBox1.Text = "127.0.0.1";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 24);
            this.label1.TabIndex = 12;
            this.label1.Text = "IP :";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(886, 41);
            this.panel2.TabIndex = 22;
            this.panel2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel2_MouseDown);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(279, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 24);
            this.label6.TabIndex = 27;
            this.label6.Text = "label6";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.P_Minimize);
            this.panel3.Controls.Add(this.P_Maximize);
            this.panel3.Controls.Add(this.P_Close);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(574, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(310, 39);
            this.panel3.TabIndex = 26;
            // 
            // P_Minimize
            // 
            this.P_Minimize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.P_Minimize.Image = ((System.Drawing.Image)(resources.GetObject("P_Minimize.Image")));
            this.P_Minimize.Location = new System.Drawing.Point(176, 2);
            this.P_Minimize.Name = "P_Minimize";
            this.P_Minimize.Size = new System.Drawing.Size(41, 36);
            this.P_Minimize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.P_Minimize.TabIndex = 25;
            this.P_Minimize.TabStop = false;
            this.P_Minimize.Click += new System.EventHandler(this.P_Minimize_Click);
            // 
            // P_Maximize
            // 
            this.P_Maximize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.P_Maximize.Image = ((System.Drawing.Image)(resources.GetObject("P_Maximize.Image")));
            this.P_Maximize.Location = new System.Drawing.Point(222, 2);
            this.P_Maximize.Name = "P_Maximize";
            this.P_Maximize.Size = new System.Drawing.Size(41, 36);
            this.P_Maximize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.P_Maximize.TabIndex = 24;
            this.P_Maximize.TabStop = false;
            this.P_Maximize.Click += new System.EventHandler(this.P_Maximize_Click);
            // 
            // P_Close
            // 
            this.P_Close.Cursor = System.Windows.Forms.Cursors.Hand;
            this.P_Close.Image = ((System.Drawing.Image)(resources.GetObject("P_Close.Image")));
            this.P_Close.Location = new System.Drawing.Point(268, 2);
            this.P_Close.Name = "P_Close";
            this.P_Close.Size = new System.Drawing.Size(41, 36);
            this.P_Close.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.P_Close.TabIndex = 23;
            this.P_Close.TabStop = false;
            this.P_Close.Click += new System.EventHandler(this.P_Close_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(188, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 24);
            this.label5.TabIndex = 25;
            this.label5.Text = "label5";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 24);
            this.label3.TabIndex = 23;
            this.label3.Text = "label3";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(67, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 24);
            this.label4.TabIndex = 24;
            this.label4.Text = "label4";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(886, 491);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.ConnectPnl);
            this.Controls.Add(this.Status);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Pic_view)).EndInit();
            this.ConnectPnl.ResumeLayout(false);
            this.ConnectPnl.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.P_Minimize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.P_Maximize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.P_Close)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox Pic_view;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label Status;
        private System.Windows.Forms.Panel ConnectPnl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button Connect_BTN;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox P_Minimize;
        private System.Windows.Forms.PictureBox P_Maximize;
        private System.Windows.Forms.PictureBox P_Close;
        private System.Windows.Forms.Label label6;
    }
}

