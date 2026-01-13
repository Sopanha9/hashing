using System.Drawing;

namespace StudentManagementSystem.Helpers
{
    public static class ModernTheme
    {
        // Colors
        public static Color BackColor = Color.FromArgb(18, 18, 18); // Deep Dark
        public static Color SurfaceColor = Color.FromArgb(30, 30, 30); // Slightly lighter
        public static Color PrimaryColor = Color.FromArgb(108, 92, 231); // Vibrant Purple
        public static Color HoverColor = Color.FromArgb(162, 155, 254);
        public static Color SecondaryColor = Color.FromArgb(0, 184, 148); // Vibrant Green
        public static Color ErrorColor = Color.FromArgb(214, 48, 49); // Red
        public static Color TextColor = Color.FromArgb(240, 240, 240); // Off-white
        public static Color SubTextColor = Color.FromArgb(178, 190, 195); // Gray text
        public static Color InputBackColor = Color.FromArgb(45, 52, 54); // Dark input
        public static Color InputForeColor = Color.White;

        // Fonts
        public static Font TitleFont = new Font("Segoe UI", 18, FontStyle.Bold);
        public static Font HeaderFont = new Font("Segoe UI", 12, FontStyle.Bold);
        public static Font BodyFont = new Font("Segoe UI", 10, FontStyle.Regular);
        public static Font SmallFont = new Font("Segoe UI", 8, FontStyle.Regular);
    }
}
