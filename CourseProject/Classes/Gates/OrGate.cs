using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CourseProject
{
    public class OrGate : Gate
    {
        public override bool Output
        {
            get { return Inputs.Any(x => x.Value); }
        }

        public OrGate(int numberOfInputs = 2) : base(numberOfInputs) { }

        public override void Draw(Graphics gfx, Pen pen, Pen activePen, Brush fillBrush, int gridSize)
        {
            base.Draw(gfx, pen, activePen, fillBrush, gridSize);

            Font font = new Font("Arial", (float)(gridSize * 0.6));
            var brush = new SolidBrush(pen.Color);

            var format = new StringFormat();
            format.Alignment = StringAlignment.Center;

            Point position = new Point(Position.X * gridSize, Position.Y * gridSize + gridSize / 3);
            Size size = new Size(gridSize * 2, gridSize * 2);

            Rectangle rect = new Rectangle(position, size);

            gfx.DrawString("1", font, Brushes.Wheat, rect, format);
        }
    }
}
