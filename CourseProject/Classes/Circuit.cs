using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CourseProject
{
	public class Circuit
	{
		private List<CircuitInput> inputs;
		public ReadOnlyCollection<CircuitInput> Inputs
		{
			get { return new ReadOnlyCollection<CircuitInput>(inputs); }
		}

		private List<CircuitOutput> outputs;
		public ReadOnlyCollection<CircuitOutput> Outputs
		{
			get { return new ReadOnlyCollection<CircuitOutput>(outputs); }
		}

		private List<Element> elements;
		public ReadOnlyCollection<Element> Elements
		{
			get { return new ReadOnlyCollection<Element>(elements); }
		}

		public ReadOnlyCollection<Element> AllElements
		{
			get
			{
				return new ReadOnlyCollection<Element>(inputs.Concat(elements).Concat(outputs).ToList());
			}
		}

		public ReadOnlyCollection<Wire> Wires
		{
			get
			{
				return new ReadOnlyCollection<Wire>(elements.Where(x => x is Wire).Select(x => x as Wire).ToList());
			}
		}

		public Circuit(int numberOfInputs = 0, int numberOfOutputs = 0)
		{
			inputs = new List<CircuitInput>(numberOfInputs);

			for (int i = 0; i < numberOfInputs; i++)
			{
				CircuitInput input = new CircuitInput(true);
				input.Position = new Point(1, i * 2 + 1);
				inputs.Add(input);
			}

			outputs = new List<CircuitOutput>(numberOfOutputs);

			for (int i = 0; i < numberOfOutputs; i++)
			{
				CircuitOutput output = new CircuitOutput();
				output.Position = new Point(25, i * 2 + 1);
				outputs.Add(output);
			}

			elements = new List<Element>();
		}

		public void AddInput()
		{
			inputs.Add(new CircuitInput());
		}

		public void AddOutput()
		{
			outputs.Add(new CircuitOutput());
		}

		private void ConnectElement(Element connectingElement)
		{
			for (int i = 0; i < connectingElement.InputPositions.Length; i++)
			{
				var inputPos = connectingElement.InputPositions[i];
				foreach (var element in AllElements)
				{
					for (int j = 0; j < element.OutputPositions.Length; j++)
					{
						var outputPos = element.OutputPositions[j];
						if (inputPos == outputPos)
							connectingElement.SetInput(i, new Connection(element));
					}
				}
			}

			foreach (var outputPos in connectingElement.OutputPositions)
			{
				foreach (var element in AllElements)
				{
					for (int i = 0; i < element.InputPositions.Length; i++)
					{
						var inputPos = element.InputPositions[i];
						if (inputPos == outputPos)
							element.SetInput(i, new Connection(connectingElement));
					}
				}
			}
		}

		private static bool PointInWire(Point point, Wire wire)
		{
			switch (wire.WireDirection)
			{
				case Wire.Direction.Up:
					{
						if (point.X != wire.Position.X)
							return false;

						return point.Y > wire.OutputPositions[0].Y && point.Y < wire.Position.Y;
					}
				case Wire.Direction.Down:
					{
						if (point.X != wire.Position.X)
							return false;

						return point.Y > wire.Position.Y && point.Y < wire.OutputPositions[0].Y;
					}
				case Wire.Direction.Left:
					{
						if (point.Y != wire.Position.Y)
							return false;

						return point.X > wire.OutputPositions[0].X && point.X < wire.Position.X;
					}
				case Wire.Direction.Right:
					{
						if (point.Y != wire.Position.Y)
							return false;

						return point.X > wire.Position.X && point.X < wire.OutputPositions[0].X;
					}
				default:
					return false;
			}
		}

		private bool WirePlacementConflict(Wire wire)
		{
			foreach (var element in AllElements)
			{
				// check if wire intersects with non-wire element
				if (!(element is Wire) && element.Rect.IntersectsWith(wire.Rect))
					return true;

				// check is wire output is in other element's output
				if (element.OutputPositions.Contains(wire.OutputPositions[0]))
					return true;

				// check if wire output inside another wire
				if (element is Wire && PointInWire(wire.OutputPositions[0], element as Wire))
					return true;

				// check if wire covers any ports on the element
				if (element.OutputPositions.Concat(element.InputPositions).
					Any(pos => PointInWire(pos, wire)))
					return true;
			}

			return false;
		}

		public void SplitWire(Wire wireToSplit, Point splitPoint)
		{
			Wire firstHalf = new Wire();

			firstHalf.Position = wireToSplit.Position;
			firstHalf.WireDirection = wireToSplit.WireDirection;
			firstHalf.Length =
				Math.Abs(wireToSplit.Position.X - splitPoint.X) +
				Math.Abs(wireToSplit.Position.Y - splitPoint.Y);

			if(wireToSplit.Inputs[0].Source != null)
				wireToSplit.Inputs[0].Source.RemoveOutputElement(wireToSplit);

			firstHalf.SetInput(wireToSplit.Inputs[0]);

			wireToSplit.Position = splitPoint;
			wireToSplit.Length -= firstHalf.Length;

			wireToSplit.SetInput(new Connection(firstHalf));

			elements.Add(firstHalf);
		}

		public bool AddElement(Element element)
		{
			if (!(element is Wire))
			{
				if (AllElements.Any(x => x.Rect.IntersectsWith(element.Rect)))
					return false;

				ConnectElement(element);
			}
			else
			{
				Wire createdWire = element as Wire;

				if (createdWire.Length == 0)
					return false;

				if (WirePlacementConflict(createdWire))
					return false;

				Wire adjacentInput = null;
				Wire adjacentOutput = null;

				foreach (var wire in Wires)
				{
					if (PointInWire(createdWire.Position, wire))
					{
						SplitWire(wire, createdWire.Position);
						break;
					}

					if (createdWire.WireDirection == wire.WireDirection)
					{
						if (createdWire.Position == wire.OutputPositions[0] && wire.OutputCounter == 0)
							adjacentInput = wire;

						if (createdWire.OutputPositions[0] == wire.Position)
							adjacentOutput = wire;

						if (adjacentInput != null && adjacentOutput != null)
							break;
					}
				}

				if (adjacentInput != null)
				{
					createdWire.Position = adjacentInput.Position;
					createdWire.Length += adjacentInput.Length;

					RemoveElement(adjacentInput);
				}

				if (adjacentOutput != null && Wires.Where(w => w.Position == createdWire.OutputPositions[0]).Count() == 1)
				{
					createdWire.Length += adjacentOutput.Length;
					RemoveElement(adjacentOutput);
				}

				ConnectElement(createdWire);
			}

			if (element is CircuitInput)
			{
				inputs.Add(element as CircuitInput);
				return true;
			}

			if (element is CircuitOutput)
			{
				outputs.Add(element as CircuitOutput);
				return true;
			}

			elements.Add(element);
			return true;
		}

		public void RemoveElement(Element element)
		{
			element.Disconnect();

			if (element is CircuitInput)
			{
				inputs.Remove(element as CircuitInput);
				return;
			}

			if (element is CircuitOutput)
			{
				outputs.Remove(element as CircuitOutput);
				return;
			}

			elements.Remove(element);
		}

		public void Draw(Graphics gfx, Pen defaultPen, Pen selectedPen, Pen activePen, Brush fillBrush, int gridSize)
		{
			Retrace();

			foreach (var element in AllElements)
			{
				if(element.IsSelected)
					element.Draw(gfx, selectedPen, activePen, gridSize);
				else
					element.Draw(gfx, defaultPen, activePen, gridSize);
			}
		}

		public void Retrace()
		{
			foreach (var element in Elements)
				element.IsTraced = false;
		}
	}
}
