
namespace Joy2Key
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Represents the direction of the gear shift.
        /// </summary>
        public enum GearDirection
        {
            Up,
            Down
        }

        /// <summary>
        /// Sets the gear position based on the direction.
        /// </summary>
        /// <param name="direction">The direction to shift the gear.</param>
        public void SetGear(GearDirection direction)
        {
            if (direction == GearDirection.Up)
            {
                if (++GearPosition > 4)
                {
                    GearPosition = 4; // clamp
                }

            }
            else if (direction == GearDirection.Down)
            {
                if (--GearPosition < 1)
                {
                    GearPosition = 1; // clamp
                }
            }
            DebugPrintLine($"Gear position : {GearPosition.ToString()}");

            string key = string.Empty;

            switch (GearPosition)
            {
                case 1:
                    key = "I";
                    break;
                case 2:
                    key = "K";
                    break;
                case 3:
                    key = "O";
                    break;
                case 4:
                    key = "L";
                    break;
            }

            HandleVirtualKeystroke(key);
        }
    }
}
