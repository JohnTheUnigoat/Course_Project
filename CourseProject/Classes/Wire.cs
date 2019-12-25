using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject
{
    public class Wire : Element
    {
        public override Connection[] Inputs { get; }

        public override bool[] Outputs
        {
            get { return new bool[] { Inputs[0].Value }; }
        }


        public enum Direction { Up, Down, Left, Right};
        public Direction WireDirection { get; set; }

        public int Length { get; set; }

        public override Point[] InputPositions
        {
            get { return new Point[] { Position }; }
        }

        public override Point[] OutputPositions
        {
            get
            {
                Point outputPosition = Position;

                switch (WireDirection)
                {
                    case Direction.Up:
                        outputPosition.Y -= Length;
                        break;
                    case Direction.Down:
                        outputPosition.Y += Length;
                        break;
                    case Direction.Left:
                        outputPosition.X -= Length;
                        break;
                    case Direction.Right:
                        outputPosition.X += Length;
                        break;
                }

                return new Point[] { outputPosition };
            }
        }

        public override Rectangle Rect
        {
            get
            {
                Size size;
                Point position;

                switch (WireDirection)
                {
                    case Direction.Up:
                        position = new Point(Position.X, Position.Y - Length);
                        size = new Size(0, Length);
                        break;
                    case Direction.Down:
                        position = Position;
                        size = new Size(0, Length);
                        break;
                    case Direction.Left:
                        position = new Point(Position.X - Length, Position.Y);
                        size = new Size(Length, 0);
                        break;
                    case Direction.Right:
                        position = Position;
                        size = new Size(Length, 0);
                        break;
                    default:
                        position = new Point();
                        size = new Size();
                        break;
                }

                return new Rectangle(position, size);
            }
        }


        public Wire()
        {
            Inputs = new Connection[1];
        }

        public Wire(Connection input)
        {
            Inputs = new Connection[] { input };
        }

        public void SetInput(Connection input)
        {
            Inputs[0] = input;
        }


        public override void Draw(Graphics gfx, Pen pen, Pen activePen, int gridSize)
        {
            Point from = new Point(Position.X * gridSize, Position.Y * gridSize);
            Point to = new Point(OutputPositions[0].X * gridSize, OutputPositions[0].Y * gridSize);

            if (Outputs[0] == true)
                gfx.DrawLine(activePen, from, to);
            else
                gfx.DrawLine(pen, from, to);
        }
    }
}
