using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject
{
    public class AndGate : Gate
    {
        public AndGate(int numberOfInputs = 2) : base(numberOfInputs) { }

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

            gfx.DrawString("&", font, Brushes.Wheat, rect, format);
        }

        protected override void CalculateOutput()
        {
            output = Inputs.All(x => x.Value);
        }
    }
}
