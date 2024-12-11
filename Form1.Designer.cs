namespace JoyKey
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
            this.FindButton = new System.Windows.Forms.Button();
            this.Coin_button = new System.Windows.Forms.Button();
            this.SwitchButton = new System.Windows.Forms.Button();
            this.ClearButton = new System.Windows.Forms.Button();
            this.GearUp_button = new System.Windows.Forms.Button();
            this.GearDown_button = new System.Windows.Forms.Button();
            this.AppName_textBox = new System.Windows.Forms.TextBox();
            this.KeyHoldTime_TextBox = new System.Windows.Forms.TextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // FindButton
            // 
            this.FindButton.Location = new System.Drawing.Point(243, 33);
            this.FindButton.Name = "FindButton";
            this.FindButton.Size = new System.Drawing.Size(67, 20);
            this.FindButton.TabIndex = 0;
            this.FindButton.Text = "Find";
            this.FindButton.UseVisualStyleBackColor = true;
            this.FindButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FindApp_button_Click);
            // 
            // Coin_button
            // 
            this.Coin_button.Location = new System.Drawing.Point(15, 115);
            this.Coin_button.Name = "Coin_button";
            this.Coin_button.Size = new System.Drawing.Size(194, 48);
            this.Coin_button.TabIndex = 1;
            this.Coin_button.Text = "Insert Coin (6)";
            this.Coin_button.UseVisualStyleBackColor = true;
            this.Coin_button.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Coin_button_click);
            // 
            // SwitchButton
            // 
            this.SwitchButton.Location = new System.Drawing.Point(316, 33);
            this.SwitchButton.Name = "SwitchButton";
            this.SwitchButton.Size = new System.Drawing.Size(64, 20);
            this.SwitchButton.TabIndex = 2;
            this.SwitchButton.Text = "Switch";
            this.SwitchButton.UseVisualStyleBackColor = true;
            this.SwitchButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Switch_Button_Click);
            // 
            // ClearButton
            // 
            this.ClearButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ClearButton.Location = new System.Drawing.Point(15, 169);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(125, 30);
            this.ClearButton.TabIndex = 3;
            this.ClearButton.Text = "Clear debug";
            this.ClearButton.UseVisualStyleBackColor = true;
            this.ClearButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ClearButton_Click);
            // 
            // GearUp_button
            // 
            this.GearUp_button.Location = new System.Drawing.Point(115, 84);
            this.GearUp_button.Name = "GearUp_button";
            this.GearUp_button.Size = new System.Drawing.Size(94, 25);
            this.GearUp_button.TabIndex = 4;
            this.GearUp_button.Text = "Shift Up";
            this.GearUp_button.UseVisualStyleBackColor = true;
            this.GearUp_button.MouseClick += new System.Windows.Forms.MouseEventHandler(this.GearUp_button_click);
            // 
            // GearDown_button
            // 
            this.GearDown_button.Location = new System.Drawing.Point(13, 84);
            this.GearDown_button.Name = "GearDown_button";
            this.GearDown_button.Size = new System.Drawing.Size(96, 25);
            this.GearDown_button.TabIndex = 5;
            this.GearDown_button.Text = "Shift Down";
            this.GearDown_button.UseVisualStyleBackColor = true;
            this.GearDown_button.MouseClick += new System.Windows.Forms.MouseEventHandler(this.GearDown_button_Click);
            // 
            // AppName_textBox
            // 
            this.AppName_textBox.Location = new System.Drawing.Point(13, 33);
            this.AppName_textBox.Name = "AppName_textBox";
            this.AppName_textBox.Size = new System.Drawing.Size(224, 20);
            this.AppName_textBox.TabIndex = 6;
            this.AppName_textBox.Text = "Sega Rally Championship (Rev B)";
            // 
            // KeyHoldTime_TextBox
            // 
            this.KeyHoldTime_TextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.KeyHoldTime_TextBox.Location = new System.Drawing.Point(328, 179);
            this.KeyHoldTime_TextBox.Name = "KeyHoldTime_TextBox";
            this.KeyHoldTime_TextBox.Size = new System.Drawing.Size(82, 20);
            this.KeyHoldTime_TextBox.TabIndex = 7;
            this.KeyHoldTime_TextBox.Text = "100";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.Location = new System.Drawing.Point(3, 205);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(407, 75);
            this.richTextBox1.TabIndex = 8;
            this.richTextBox1.Text = "";
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(146, 177);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(86, 17);
            this.checkBox1.TabIndex = 9;
            this.checkBox1.Text = "Show Xinput";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(325, 163);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Key hold time mS";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Program Name";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(433, 285);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.KeyHoldTime_TextBox);
            this.Controls.Add(this.AppName_textBox);
            this.Controls.Add(this.GearDown_button);
            this.Controls.Add(this.GearUp_button);
            this.Controls.Add(this.ClearButton);
            this.Controls.Add(this.SwitchButton);
            this.Controls.Add(this.Coin_button);
            this.Controls.Add(this.FindButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button FindButton;
        private System.Windows.Forms.Button Coin_button;
        private System.Windows.Forms.Button SwitchButton;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.Button GearUp_button;
        private System.Windows.Forms.Button GearDown_button;
        private System.Windows.Forms.TextBox AppName_textBox;
        private System.Windows.Forms.TextBox KeyHoldTime_TextBox;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

