using System.Drawing.Drawing2D;

namespace Frank.Utils
{
    // Pour afficher un texte personalisé dans une picturebox
    public struct TextStyle
    {
        public string text;
        public FontFamily fontFamily;
        public int fontStyle;
        public float fontSize;
        public Point position;
        public StringFormat stringFormat;

        public TextStyle(string text, FontFamily fontFamily, int fontStyle, float fontSize, Point position, StringFormat stringFormat)
        {
            this.text = text;
            this.fontFamily = fontFamily;
            this.fontStyle = fontStyle;
            this.fontSize = fontSize;
            this.position = position;
            this.stringFormat = stringFormat;
        }
    }

    internal class Utils
    {
        public static StringFormat GetCenterAlignment()
        {
            return new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
        }

        public static void DrawText(TextStyle message, PaintEventArgs e)
        {

            // Ajoute le texte voulu
            using GraphicsPath path = new GraphicsPath();
            path.AddString(message.text, message.fontFamily, message.fontStyle, message.fontSize, message.position, message.stringFormat);

            // Crée un contour et un remplissage
            using Pen outline = new Pen(Color.Black, 4);
            using Brush fill = new SolidBrush(Color.White);

            // Dessine le texte avec un contour d'une autre couleur
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.DrawPath(outline, path);
            e.Graphics.FillPath(fill, path);
        }
    }
}
