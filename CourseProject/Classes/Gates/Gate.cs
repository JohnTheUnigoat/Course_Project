using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject
{
	public abstract class Gate : Element
	{
		public override Connection[] Inputs { get; }

		protected bool output;

		public override bool[] Outputs {
			get
			{
				if (!IsTraced)
				{
					IsTraced = true;
					CalculateOutput();
				}

				return new bool[] { output };
			}
		}


		public override Point[] InputPositions
		{
			get
			{
				List<Point> inputPositions = new List<Point>(Inputs.Length);
				Point currPos = new Point(Position.X - 1, Position.Y + 1);

				for (int i = 0; i < Inputs.Length; i++)
				{
					inputPositions.Add(currPos);
					currPos.Y++;
				}

				return inputPositions.ToArray();
			}
		}

		public override Point[] OutputPositions
		{
			get
			{
				Point outputPosition = new Point(Position.X + 3, Position.Y + 1);
				return new Point[] { outputPosition };
			}
		}

		public override Rectangle Rect
		{
			get
			{
				Point position = new Point(Position.X - 1, Position.Y);
				Size size = new Size(4, Inputs.Length + 1);

				return new Rectangle(position, size);
			}
		}


		public Gate(int numberOfInputs)
		{
			Inputs = new Connection[numberOfInputs];
			IsTraced = false;
		}

		public override void Draw(Graphics gfx, Pen pen, Pen activePen, int gridSize)
		{
			base.Draw(gfx, pen, activePen, gridSize);

			Size size = new Size(gridSize * 2, gridSize * (Inputs.Length + 1));
			Point position = new Point(Position.X * gridSize, Position.Y * gridSize);

			Rectangle rect = new Rectangle(position, size);

			gfx.DrawRectangle(pen, rect);
		}

		//Sets the output field, returns true if output value changed
		abstract protected void CalculateOutput();
	}
}
