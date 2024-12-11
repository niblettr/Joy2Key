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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // FindButton
            // 
            this.FindButton.Location = new System.Drawing.Point(238, 25);
            this.FindButton.Name = "FindButton";
            this.FindButton.Size = new System.Drawing.Size(37, 20);
            this.FindButton.TabIndex = 0;
            this.FindButton.Text = "Find";
            this.FindButton.UseVisualStyleBackColor = true;
            this.FindButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FindApp_button_Click);
            // 
            // Coin_button
            // 
            this.Coin_button.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.Coin_button.Location = new System.Drawing.Point(6, 50);
            this.Coin_button.Name = "Coin_button";
            this.Coin_button.Size = new System.Drawing.Size(162, 40);
            this.Coin_button.TabIndex = 1;
            this.Coin_button.Text = "Insert Coin (6)";
            this.Coin_button.UseVisualStyleBackColor = false;
            this.Coin_button.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Coin_button_click);
            // 
            // SwitchButton
            // 
            this.SwitchButton.Location = new System.Drawing.Point(281, 25);
            this.SwitchButton.Name = "SwitchButton";
            this.SwitchButton.Size = new System.Drawing.Size(64, 20);
            this.SwitchButton.TabIndex = 2;
            this.SwitchButton.Text = "Switch";
            this.SwitchButton.UseVisualStyleBackColor = true;
            this.SwitchButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Switch_Button_Click);
            // 
            // ClearButton
            // 
            this.ClearButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ClearButton.Location = new System.Drawing.Point(15, 176);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(86, 22);
            this.ClearButton.TabIndex = 3;
            this.ClearButton.Text = "Clear debug";
            this.ClearButton.UseVisualStyleBackColor = true;
            this.ClearButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ClearButton_Click);
            // 
            // GearUp_button
            // 
            this.GearUp_button.BackColor = System.Drawing.SystemColors.Info;
            this.GearUp_button.Location = new System.Drawing.Point(90, 19);
            this.GearUp_button.Name = "GearUp_button";
            this.GearUp_button.Size = new System.Drawing.Size(78, 25);
            this.GearUp_button.TabIndex = 4;
            this.GearUp_button.Text = "Shift Up";
            this.GearUp_button.UseVisualStyleBackColor = false;
            this.GearUp_button.MouseClick += new System.Windows.Forms.MouseEventHandler(this.GearUp_button_click);
            // 
            // GearDown_button
            // 
            this.GearDown_button.BackColor = System.Drawing.SystemColors.Info;
            this.GearDown_button.Location = new System.Drawing.Point(6, 19);
            this.GearDown_button.Name = "GearDown_button";
            this.GearDown_button.Size = new System.Drawing.Size(78, 25);
            this.GearDown_button.TabIndex = 5;
            this.GearDown_button.Text = "Shift Down";
            this.GearDown_button.UseVisualStyleBackColor = false;
            this.GearDown_button.MouseClick += new System.Windows.Forms.MouseEventHandler(this.GearDown_button_Click);
            // 
            // AppName_textBox
            // 
            this.AppName_textBox.Location = new System.Drawing.Point(15, 25);
            this.AppName_textBox.Name = "AppName_textBox";
            this.AppName_textBox.Size = new System.Drawing.Size(217, 20);
            this.AppName_textBox.TabIndex = 6;
            this.AppName_textBox.Text = "Sega Rally Championship (Rev B)";
            // 
            // KeyHoldTime_TextBox
            // 
            this.KeyHoldTime_TextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.KeyHoldTime_TextBox.Location = new System.Drawing.Point(174, 68);
            this.KeyHoldTime_TextBox.Name = "KeyHoldTime_TextBox";
            this.KeyHoldTime_TextBox.Size = new System.Drawing.Size(36, 20);
            this.KeyHoldTime_TextBox.TabIndex = 7;
            this.KeyHoldTime_TextBox.Text = "100";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.Location = new System.Drawing.Point(12, 204);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(359, 75);
            this.richTextBox1.TabIndex = 8;
            this.richTextBox1.Text = "";
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(109, 180);
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
            this.label1.Location = new System.Drawing.Point(174, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Key hold";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Program Name";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Menu;
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.GearDown_button);
            this.groupBox1.Controls.Add(this.GearUp_button);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.Coin_button);
            this.groupBox1.Controls.Add(this.KeyHoldTime_TextBox);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.groupBox1.Location = new System.Drawing.Point(15, 59);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(269, 101);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Key sim send";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(213, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "mS";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 285);
            this.Controls.Add(this.FindButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.AppName_textBox);
            this.Controls.Add(this.ClearButton);
            this.Controls.Add(this.SwitchButton);
            this.Name = "Form1";
            this.Text = "Joy2Key v0.2";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
    }
}

