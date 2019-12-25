using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject
{
    public class CircuitInput : Element
    {
        public bool Value { get; set; }

        private static Connection[] inputs = new Connection[0];
        public override Connection[] Inputs
        {
            get { return inputs; }
        }

        public override bool[] Outputs
        {
            get { return new bool[] { Value }; }
        }


        public override Point[] InputPositions
        {
            get { return new Point[0]; }
        }

        public override Point[] OutputPositions
        {
            get
            {
                Point outputPosition = new Point(Position.X + 2, Position.Y + 1);
                return new Point[] { outputPosition };
            }
        }


        public CircuitInput(bool value = false)
        {
            Value = value;
        }


        public override void Draw(Graphics gfx, Pen pen, Pen activePen, int gridSize)
        {
            Size size = new Size(gridSize, gridSize);
            Point position = new Point(Position.X * gridSize + gridSize / 2, Position.Y * gridSize + gridSize / 2);

            Rectangle rect = new Rectangle(position, size);

            gfx.DrawRectangle(pen, rect);

            Point from = new Point(OutputPositions[0].X * gridSize, OutputPositions[0].Y * gridSize);
            Point to = new Point(from.X - gridSize / 2, from.Y);

            string displayValue;

            if (Value == true)
            {
                gfx.DrawLine(activePen, from, to);
                displayValue = "1";
            }
            else
            {
                gfx.DrawLine(pen, from, to);
                displayValue = "0";
            }

            Font font = new Font("Arial", (float)(gridSize * 0.6));
            SolidBrush brush = new SolidBrush(pen.Color);

            var format = new StringFormat();
            format.Alignment = StringAlignment.Center;

            gfx.DrawString(displayValue, font, Brushes.Wheat, rect, format);
        }
    }
}
