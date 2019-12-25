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
