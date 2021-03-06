﻿using System;
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


		private bool output;

		public override bool[] Outputs
		{
			get
			{
				if (!IsTraced)
				{
					IsTraced = true;
					output = Inputs[0].Value;
				}

				return new bool[] { output };
			}
		}


		public enum Direction { Undefined, Up, Down, Left, Right};

		public Direction WireDirection { get; set; }

		public int OutputCounter
		{
			get
			{
				return elementsOnOutputs.Count;
			}
		}

		private int length;

		public int Length {
			get { return length; }
			set
			{
				length = value;
				if(length < 0)
				{
					length = -length;

					switch (WireDirection)
					{
						case Direction.Up:
							WireDirection = Direction.Down;
							break;
						case Direction.Down:
							WireDirection = Direction.Up;
							break;
						case Direction.Left:
							WireDirection = Direction.Right;
							break;
						case Direction.Right:
							WireDirection = Direction.Left;
							break;
					}
				}
			}
		}

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
						position = new Point(Position.X, Position.Y);
						size = new Size(0, Length);
						break;
					case Direction.Left:
						position = new Point(Position.X - Length, Position.Y);
						size = new Size(Length, 0);
						break;
					case Direction.Right:
						position = new Point(Position.X, Position.Y);
						size = new Size(Length, 0);
						break;
					default:
						position = Position;
						size = new Size(0, 0);
						break;
				}

				return new Rectangle(position, size);
			}
		}


		public Wire()
		{
			Inputs = new Connection[1];
		}


		public void SetInput(Connection input) 
		{
			SetInput(0, input);
		}


		public override void Draw(Graphics gfx, Pen pen, Pen activePen, int gridSize)
		{
			SolidBrush brush = new SolidBrush(pen.Color);
			SolidBrush activeBrush = new SolidBrush(activePen.Color);

			Point from = new Point(Position.X * gridSize, Position.Y * gridSize);
			Point to = new Point(OutputPositions[0].X * gridSize, OutputPositions[0].Y * gridSize);

			Point[] arrow = new Point[3];
			arrow[0] = new Point(
				(Position.X + OutputPositions[0].X) * gridSize / 2,
				(Position.Y + OutputPositions[0].Y) * gridSize / 2);


			int arrowLength = gridSize / 2;
			int arrowWidth = gridSize / 4;

			switch (WireDirection)
			{
				case Direction.Up:
					arrow[0].Y -= gridSize / 4;
					arrow[1] = new Point(arrow[0].X - arrowWidth, arrow[0].Y + arrowLength);
					arrow[2] = new Point(arrow[0].X + arrowWidth, arrow[0].Y + arrowLength);
					break;
				case Direction.Down:
					arrow[0].Y += gridSize / 4;
					arrow[1] = new Point(arrow[0].X - arrowWidth, arrow[0].Y - arrowLength);
					arrow[2] = new Point(arrow[0].X + arrowWidth, arrow[0].Y - arrowLength);
					break;
				case Direction.Left:
					arrow[0].X -= gridSize / 4;
					arrow[1] = new Point(arrow[0].X + arrowLength, arrow[0].Y - arrowWidth);
					arrow[2] = new Point(arrow[0].X + arrowLength, arrow[0].Y + arrowWidth);
					break;
				case Direction.Right:
					arrow[0].X += gridSize / 4;
					arrow[1] = new Point(arrow[0].X - arrowLength, arrow[0].Y - arrowWidth);
					arrow[2] = new Point(arrow[0].X - arrowLength, arrow[0].Y + arrowWidth);
					break;
			}

			int nodeRadius = gridSize / 5;

			Point nodePosition = new Point(OutputPositions[0].X * gridSize - nodeRadius, OutputPositions[0].Y * gridSize - nodeRadius);
			Size nodeSize = new Size(nodeRadius * 2, nodeRadius * 2);
			Rectangle nodeRect = new Rectangle(
				nodePosition,
				new Size(nodeRadius * 2, nodeRadius * 2));

			if (Outputs[0] == true && !IsSelected)
			{
				gfx.DrawLine(activePen, from, to);
				gfx.FillPolygon(activeBrush, arrow);

				if (OutputCounter > 1)
					gfx.FillRectangle(activeBrush, nodeRect);
			}
			else
			{
				gfx.DrawLine(pen, from, to);
				gfx.FillPolygon(brush, arrow);

				if (OutputCounter > 1)
					gfx.FillRectangle(brush, nodeRect);
			}
		}

		public override Rectangle GetInvalidateRect(int gridSize)
		{
			Rectangle rect = base.GetInvalidateRect(gridSize);

			if(rect.Width > rect.Height)
			{
				rect.Y -= gridSize / 2;
				rect.Height += gridSize;
			}
			else
			{
				rect.X -= gridSize / 2;
				rect.Width += gridSize;
			}

			return rect;
		}
	}
}
