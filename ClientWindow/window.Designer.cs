namespace ClientWindow
{
    partial class window
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.IP_textBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Port_textBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Connect_button1 = new System.Windows.Forms.Button();
            this.Message_richTextBox = new System.Windows.Forms.RichTextBox();
            this.SendMessage_textBox = new System.Windows.Forms.TextBox();
            this.SendMessage_button = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // IP_textBox
            // 
            this.IP_textBox.Location = new System.Drawing.Point(70, 12);
            this.IP_textBox.Name = "IP_textBox";
            this.IP_textBox.Size = new System.Drawing.Size(91, 25);
            this.IP_textBox.TabIndex = 0;
            this.IP_textBox.Text = "127.0.0.1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(13, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "IP：";
            // 
            // Port_textBox
            // 
            this.Port_textBox.Location = new System.Drawing.Point(251, 12);
            this.Port_textBox.Name = "Port_textBox";
            this.Port_textBox.Size = new System.Drawing.Size(59, 25);
            this.Port_textBox.TabIndex = 0;
            this.Port_textBox.Text = "8888";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(171, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Port：";
            // 
            // Connect_button1
            // 
            this.Connect_button1.Font = new System.Drawing.Font("微軟正黑體", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Connect_button1.Location = new System.Drawing.Point(460, 12);
            this.Connect_button1.Name = "Connect_button1";
            this.Connect_button1.Size = new System.Drawing.Size(118, 25);
            this.Connect_button1.TabIndex = 2;
            this.Connect_button1.Text = "連接";
            this.Connect_button1.UseVisualStyleBackColor = true;
            this.Connect_button1.Click += new System.EventHandler(this.Connect_button1_Click);
            // 
            // Message_richTextBox
            // 
            this.Message_richTextBox.Location = new System.Drawing.Point(18, 54);
            this.Message_richTextBox.Name = "Message_richTextBox";
            this.Message_richTextBox.Size = new System.Drawing.Size(560, 379);
            this.Message_richTextBox.TabIndex = 3;
            this.Message_richTextBox.Text = "";
            this.Message_richTextBox.TextChanged += new System.EventHandler(this.Message_richTextBox_TextChanged);
            // 
            // SendMessage_textBox
            // 
            this.SendMessage_textBox.Location = new System.Drawing.Point(18, 439);
            this.SendMessage_textBox.Multiline = true;
            this.SendMessage_textBox.Name = "SendMessage_textBox";
            this.SendMessage_textBox.Size = new System.Drawing.Size(456, 85);
            this.SendMessage_textBox.TabIndex = 4;
            this.SendMessage_textBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SendMessage_textBox_KeyDown);
            // 
            // SendMessage_button
            // 
            this.SendMessage_button.Location = new System.Drawing.Point(480, 439);
            this.SendMessage_button.Name = "SendMessage_button";
            this.SendMessage_button.Size = new System.Drawing.Size(98, 85);
            this.SendMessage_button.TabIndex = 5;
            this.SendMessage_button.Text = "發送";
            this.SendMessage_button.UseVisualStyleBackColor = true;
            this.SendMessage_button.Click += new System.EventHandler(this.SendMessage_button_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(396, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(59, 25);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "呵呵";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(316, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 25);
            this.label3.TabIndex = 1;
            this.label3.Text = "匿名：";
            // 
            // window
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 536);
            this.Controls.Add(this.SendMessage_button);
            this.Controls.Add(this.SendMessage_textBox);
            this.Controls.Add(this.Message_richTextBox);
            this.Controls.Add(this.Connect_button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Port_textBox);
            this.Controls.Add(this.IP_textBox);
            this.Name = "window";
            this.Text = "客戶端視窗";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox IP_textBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Port_textBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Connect_button1;
        public System.Windows.Forms.RichTextBox Message_richTextBox;
        private System.Windows.Forms.TextBox SendMessage_textBox;
        private System.Windows.Forms.Button SendMessage_button;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;
    }
}

