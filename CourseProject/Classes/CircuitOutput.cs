﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject
{
    public class CircuitOutput : Element
    {
        public Connection Connection { get; set; }

        public override Connection[] Inputs
        {
            get { return new Connection[] { Connection }; }
        }

        private bool[] outputs = new bool[0];
        public override bool[] Outputs
        {
            get { return outputs; }
        }

        public bool Value
        {
            get { return Connection.Value; }
        }


        public override Point[] InputPositions
        {
            get
            {
                Point inputPosition = new Point(Position.X, Position.Y + 1);
                return new Point[] { inputPosition };
            }
        }

        public override Point[] OutputPositions
        {
            get { return new Point[0]; }
        }


        public override void Draw(Graphics gfx, Pen pen, Pen activePen, int gridSize)
        {
            Size size = new Size(gridSize, gridSize);
            Point position = new Point(Position.X * gridSize + gridSize / 2, Position.Y * gridSize + gridSize / 2);

            Rectangle rect = new Rectangle(position, size);

            gfx.DrawRectangle(pen, rect);

            Point from = new Point(InputPositions[0].X * gridSize, InputPositions[0].Y * gridSize);
            Point to = new Point(from.X + gridSize / 2, from.Y);

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