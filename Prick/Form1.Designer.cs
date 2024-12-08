namespace Prick
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            richTextBox1 = new RichTextBox();
            AppName_textBox = new TextBox();
            label1 = new Label();
            button1 = new Button();
            button2 = new Button();
            checkBox1 = new CheckBox();
            SuspendLayout();
            // 
            // richTextBox1
            // 
            richTextBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            richTextBox1.Location = new Point(3, 219);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(1224, 256);
            richTextBox1.TabIndex = 0;
            richTextBox1.Text = "";
            // 
            // AppName_textBox
            // 
            AppName_textBox.Location = new Point(104, 17);
            AppName_textBox.Name = "AppName_textBox";
            AppName_textBox.Size = new Size(184, 23);
            AppName_textBox.TabIndex = 1;
            AppName_textBox.Text = "Sega Rally Championship (Rev B)";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 20);
            label1.Name = "label1";
            label1.Size = new Size(86, 15);
            label1.TabIndex = 2;
            label1.Text = "Program name";
            // 
            // button1
            // 
            button1.Location = new Point(3, 181);
            button1.Name = "button1";
            button1.Size = new Size(88, 32);
            button1.TabIndex = 3;
            button1.Text = "Clear";
            button1.UseVisualStyleBackColor = true;
            button1.Click += ClearButton_Click;
            // 
            // button2
            // 
            button2.Location = new Point(294, 16);
            button2.Name = "button2";
            button2.Size = new Size(88, 24);
            button2.TabIndex = 4;
            button2.Text = "find";
            button2.UseVisualStyleBackColor = true;
            button2.Click += FindApp_button_Click;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Checked = true;
            checkBox1.CheckState = CheckState.Checked;
            checkBox1.Location = new Point(12, 46);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(108, 19);
            checkBox1.TabIndex = 5;
            checkBox1.Text = "Show all Xinput";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1266, 477);
            Controls.Add(checkBox1);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label1);
            Controls.Add(AppName_textBox);
            Controls.Add(richTextBox1);
            Name = "Form1";
            Text = "Joy2Key v0.2 2024";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox richTextBox1;
        private TextBox AppName_textBox;
        private Label label1;
        private Button button1;
        private Button button2;
        private CheckBox checkBox1;
    }
}
