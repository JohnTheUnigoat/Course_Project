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
				output.Position = new Point(25, 1);
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

		public void AddElement(Element element)
		{
			if (!(element is Wire))
			{
				if (AllElements.Any(x => x.Rect.IntersectsWith(element.Rect)))
					return;

				ConnectElement(element);
			}

			if (element is CircuitInput)
			{
				inputs.Add(element as CircuitInput);
				return;
			}

			if (element is CircuitOutput)
			{
				outputs.Add(element as CircuitOutput);
				return;
			}

			elements.Add(element);
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
