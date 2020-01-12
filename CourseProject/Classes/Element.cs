using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CourseProject
{
	public struct Connection
	{
		public Element Source { get; set; }

		public uint SourceOutputIndex { get; set; }

		public bool IsSet
		{
			get { return Source != null; }
		}

		public bool Value
		{
			get
			{
				if (!IsSet)
					return false;

				return Source.Outputs[SourceOutputIndex];
			}
		}

		public Connection(Element source, uint sourceOutputIndex = 0)
		{
			if (sourceOutputIndex >= source.Outputs.Length)
				throw new IndexOutOfRangeException();

			Source = source;
			SourceOutputIndex = sourceOutputIndex;
		}
	}

	abstract public class Element
	{
		abstract public Connection[] Inputs { get; }

		abstract public bool[] Outputs { get; }

		public bool IsTraced { protected get; set; }

		public bool IsSelected { get; set; }

		public Point Position { get; set; }

		abstract public Rectangle Rect { get; }

		abstract public Point[] InputPositions { get; }

		abstract public Point[] OutputPositions { get; }

		protected List<Element> connectedToOutputs = new List<Element>();


		private void AddOutputElement(Element element)
		{
			connectedToOutputs.Add(element);
		}

		public void RemoveOutputElement(Element element)
		{
			connectedToOutputs.Remove(element);
		}

		public void Disconnect()
		{
			for (int i = 0; i < Inputs.Length; i++)
			{
				if (Inputs[i].Source == null)
					continue;

				Inputs[i].Source.RemoveOutputElement(this);
				Inputs[i].Source = null;
			}

			foreach (var element in connectedToOutputs)
			{
				for (int i = 0; i < element.Inputs.Length; i++)
				{
					if (element.Inputs[i].Source == this)
					{
						element.Inputs[i].Source = null;
						break;
					}
				}
			}

			connectedToOutputs.Clear();
		}

		public void SetInput(int inputIndex, Connection input)
		{
			Inputs[inputIndex] = input;

			if (input.Source != null)
				input.Source.AddOutputElement(this);
		}

		virtual public void Draw(Graphics gfx, Pen pen, Pen activePen, int gridSize)
		{
			for(int i = 0; i < InputPositions.Length; i++)
			{
				Point from = new Point(InputPositions[i].X * gridSize, InputPositions[i].Y * gridSize);
				Point to = new Point(from.X + gridSize, from.Y);

				if (Inputs[i].Value == true)
					gfx.DrawLine(activePen, from, to);
				else
					gfx.DrawLine(pen, from, to);
			}

			for (int i = 0; i < OutputPositions.Length; i++)
			{
				Point from = new Point(OutputPositions[i].X * gridSize, OutputPositions[i].Y * gridSize);
				Point to = new Point(from.X - gridSize, from.Y);

				if (Outputs[i] == true)
					gfx.DrawLine(activePen, from, to);
				else
					gfx.DrawLine(pen, from, to);
			}
		}

		virtual public Rectangle GetInvalidateRect(int gridSize)
		{
			Rectangle invalidateRect = new Rectangle(
				Rect.X * gridSize - 1, Rect.Y * gridSize - 1,
				Rect.Width * gridSize + 2, Rect.Height * gridSize + 2);

			return invalidateRect;
		}
	}
}
