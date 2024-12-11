
namespace Joy2Key
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Clears the content of the rich text box.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void ClearButton_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        /// <summary>
        /// Finds the application window.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void FindApp_button_Click(object sender, EventArgs e)
        {
            FindRunningApp(AppName_textBox.Text, WindowSearchOptions.BringWindowToFront);
        }

        /// <summary>
        /// Handles the click event for the coin button.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void Coin_button_click(object sender, MouseEventArgs e)
        {
            HandleVirtualKeystroke("6");
        }

        /// <summary>
        /// Handles the click event for the gear up button.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void GearUp_button_click(object sender, MouseEventArgs e)
        {
            SetGear(GearDirection.Up);
        }

        /// <summary>
        /// Handles the click event for the gear down button.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void GearDown_button_Click(object sender, MouseEventArgs e)
        {
            SetGear(GearDirection.Down);
        }

        static int counter = 0;
        private void Switch_Button_Click(object sender, MouseEventArgs e)
        {
            counter++;
            if (counter > 3)
            {
                counter = 0;
            }

            switch (counter)
            {
                case 0:
                    AppName_textBox.Text = "Sega Rally Championship (Rev B)";
                    break;
                case 1:
                    AppName_textBox.Text = "Calculator";
                    break;
                case 2:
                    AppName_textBox.Text = "Model 2 Emulator";
                    break;
                case 3:
                    AppName_textBox.Text = "Notepad";
                    break;
            }
        }
    }
}
