using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Reflection;

namespace CourseProject
{
	public partial class CircuitDesignerForm : Form
	{
		private Circuit circuit;

		private int gridSize = 20;

		private Point gridPointerPosition;

		private readonly Pen mainPen = Pens.Wheat;
		private readonly Pen selectedPen = Pens.White;
		private readonly Pen activePen = Pens.YellowGreen;

		private enum Tools { Edit, AddElement, Wire };
	
		private Tools selectedTool;

		private enum Elements { AND, OR, NOT, NAND, NOR, XOR, XNOR };

		private Elements selectedAddElement;

		public CircuitDesignerForm()
		{
			InitializeComponent();
		}

		private void CircuitDesignerForm_Load(object sender, EventArgs e)
		{
			typeof(Panel).InvokeMember("DoubleBuffered",
				BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
				null, canvas, new object[] { true });

			OpenCircuitForm openForm = new OpenCircuitForm();
			if (openForm.ShowDialog() == DialogResult.OK)
				circuit = new Circuit(openForm.NumberOfInputs, openForm.NumberOfOutputs);
			else
				Application.Exit();

			selectedTool = Tools.Edit;
			numInputs.Value = 2;
		}

		private void Canvas_Paint(object sender, PaintEventArgs e)
		{
			Graphics gfx = e.Graphics;

			gfx.Clear(canvas.BackColor);

			circuit.Draw(gfx, mainPen, selectedPen, activePen, new SolidBrush(canvas.BackColor), gridSize);

			if (createdWire != null)
				createdWire.Draw(gfx, Pens.Wheat, Pens.GreenYellow, gridSize);

			if (movingElement != null)
				movingElement.Draw(gfx, mainPen, activePen, gridSize);

			Point pos = new Point(gridPointerPosition.X * gridSize - 4, gridPointerPosition.Y * gridSize - 4);
			Size size = new Size(8, 8);
		}


		private Wire createdWire;
		private Element createdElement;
	
		private Element _selectedElement;
		private Element selectedElement
		{
			get { return _selectedElement; }
			set
			{
				if (_selectedElement != null)
					_selectedElement.IsSelected = false;

				_selectedElement = value;

				if (_selectedElement != null)
					_selectedElement.IsSelected = true;
			}
		}

		private Element mouseDownElement;
		private Element mouseUpElement;

		private Point previousElementPosition;

		private Element movingElement;

		private Point positionDisplacement;

		private void CreateNewWire()
		{
			bool isOnInput = false;

			foreach (var element in circuit.AllElements)
			{
				if (!(element is Wire) && element.InputPositions.Contains(gridPointerPosition))
				{
					isOnInput = true;
				}
			}

			if (isOnInput)
				return;

			createdWire = new Wire();

			createdWire.Position = gridPointerPosition;
		}

		Point startDragGridPos;

		private void Canvas_MouseDown(object sender, MouseEventArgs e)
		{
			SetPointerPosition(e.Location);

			switch (selectedTool)
			{
				case Tools.Edit:
					foreach (var element in circuit.AllElements)
					{
						if (element.GetInvalidateRect(gridSize).Contains(e.Location))
						{
							mouseDownElement = element;

							startDragGridPos = gridPointerPosition;
							positionDisplacement.X = element.Position.X - gridPointerPosition.X;
							positionDisplacement.Y = element.Position.Y - gridPointerPosition.Y;
							break;
						}
					}

					canvas.Refresh();
					break;
				case Tools.Wire:
					if (createdWire == null)
						CreateNewWire();

					break;
			}
		}

		private void SetPointerPosition(Point mousePosition)
		{
			Point oldGridPointerPosition = gridPointerPosition;

			gridPointerPosition.X = mousePosition.X / gridSize;
			gridPointerPosition.Y = mousePosition.Y / gridSize;

			if (mousePosition.X - gridPointerPosition.X * gridSize > gridSize / 2)
				gridPointerPosition.X++;

			if (mousePosition.Y - gridPointerPosition.Y * gridSize > gridSize / 2)
				gridPointerPosition.Y++;
		}

		private void SetCreatedWireDirection()
		{
			int xOffset = gridPointerPosition.X - createdWire.Position.X;
			int yOffset = gridPointerPosition.Y - createdWire.Position.Y;

			if (!(xOffset == 0 && yOffset == 0))
			{
				if (Math.Abs(xOffset) > Math.Abs(yOffset))
				{
					if (xOffset > 0)
						createdWire.WireDirection = Wire.Direction.Right;
					else
						createdWire.WireDirection = Wire.Direction.Left;
				}
				else
				{
					if (yOffset > 0)
						createdWire.WireDirection = Wire.Direction.Down;
					else
						createdWire.WireDirection = Wire.Direction.Up;
				}
			}
		}

		private void WireResize(Wire wire, bool fromStart)
		{
			if (fromStart)
			{
				canvas.Invalidate(createdWire.GetInvalidateRect(gridSize));

				switch (createdWire.WireDirection)
				{
					case Wire.Direction.Up:
						wire.Length = createdWire.Position.Y - gridPointerPosition.Y;
						break;
					case Wire.Direction.Down:
						wire.Length = gridPointerPosition.Y - createdWire.Position.Y;
						break;
					case Wire.Direction.Left:
						wire.Length = createdWire.Position.X - gridPointerPosition.X;
						break;
					case Wire.Direction.Right:
						wire.Length = gridPointerPosition.X - createdWire.Position.X;
						break;
				}

				canvas.Invalidate(createdWire.GetInvalidateRect(gridSize));
			}
		}

		private void Canvas_MouseMove(object sender, MouseEventArgs e)
		{
			SetPointerPosition(e.Location);

			switch (selectedTool)
			{
				case Tools.Edit:
					if (movingElement == null && mouseDownElement != null && !(mouseDownElement is Wire))
					{
						previousElementPosition = mouseDownElement.Position;
						movingElement = mouseDownElement;
					}

					if (movingElement != null)
					{
						if (gridPointerPosition != startDragGridPos)
							circuit.RemoveElement(movingElement);

						Point newPosition = new Point(
							gridPointerPosition.X + positionDisplacement.X,
							gridPointerPosition.Y + positionDisplacement.Y);

						movingElement.Position = newPosition;
						canvas.Refresh();
					}
					break;
				case Tools.Wire:
					if (createdWire != null)
					{
						if (createdWire.WireDirection == Wire.Direction.Undefined)
							SetCreatedWireDirection();

						WireResize(createdWire, true);
					}
					break;
				default:
					break;
			}

			canvas.Refresh();
		}

		private void Canvas_MouseUp(object sender, MouseEventArgs e)
		{
			SetPointerPosition(e.Location);

			switch (selectedTool)
			{
				case Tools.Edit:
					{
						foreach(var element in circuit.AllElements)
						{
							if (element.GetInvalidateRect(gridSize).Contains(e.Location))
							{
								mouseUpElement = element;
								break;
							}
						}

						if (movingElement != null)
						{
							if(!circuit.AddElement(movingElement))
							{
								movingElement.Position = previousElementPosition;
								circuit.AddElement(movingElement);
							}
						}
						else if (mouseUpElement == mouseDownElement)
						{
							if (mouseUpElement != null)
								selectedElement = mouseUpElement;
							else
								selectedElement = null;
						}

						canvas.Refresh();
						positionDisplacement = new Point(0, 0);

						mouseDownElement = null;
						movingElement = null;
						mouseUpElement = null;

						break;
					}
				case Tools.AddElement:
					{
						switch (selectedAddElement)
						{
							case Elements.AND:
								createdElement = new AndGate((int)numInputs.Value);
								break;
							case Elements.OR:
								createdElement = new OrGate((int)numInputs.Value);
								break;
							case Elements.NOT:
								createdElement = new NotGate();
								break;
							case Elements.NAND:
								createdElement = new NandGate((int)numInputs.Value);
								break;
							case Elements.NOR:
								createdElement = new NorGate((int)numInputs.Value);
								break;
							case Elements.XOR:
								createdElement = new XorGate((int)numInputs.Value);
								break;
							case Elements.XNOR:
								createdElement = new XnorGate((int)numInputs.Value);
								break;
							default:
								break;
						}

						createdElement.Position = gridPointerPosition;

						circuit.AddElement(createdElement);

						Rectangle rect = createdElement.GetInvalidateRect(gridSize);
						canvas.Invalidate(rect);
						break;
					}
				case Tools.Wire:
					{
						circuit.AddElement(createdWire);

						createdWire = null;
						canvas.Refresh();
						break;
					}
			}
		}

		private void RemoveSelection()
		{
			btEdit.FlatAppearance.BorderSize = 0;
			btAnd.FlatAppearance.BorderSize = 0;
			btOr.FlatAppearance.BorderSize = 0;
			btNot.FlatAppearance.BorderSize = 0;
			btNand.FlatAppearance.BorderSize = 0;
			btNor.FlatAppearance.BorderSize = 0;
			btXor.FlatAppearance.BorderSize = 0;
			btXnor.FlatAppearance.BorderSize = 0;
			btWire.FlatAppearance.BorderSize = 0;
		}

		private void btEdit_Click(object sender, EventArgs e)
		{
			RemoveSelection();
			selectedTool = Tools.Edit;
			btEdit.FlatAppearance.BorderSize = 2;
			numInputs.Enabled = false;
		}

		private void btAnd_Click(object sender, EventArgs e)
		{
			RemoveSelection();
			selectedTool = Tools.AddElement;
			selectedAddElement = Elements.AND;
			btAnd.FlatAppearance.BorderSize = 2;
			numInputs.Enabled = true;
		}

		private void btOr_Click(object sender, EventArgs e)
		{
			RemoveSelection();
			selectedTool = Tools.AddElement;
			selectedAddElement = Elements.OR;
			btOr.FlatAppearance.BorderSize = 2;
			numInputs.Enabled = true;
		}

		private void btNot_Click(object sender, EventArgs e)
		{
			RemoveSelection();
			selectedTool = Tools.AddElement;
			selectedAddElement = Elements.NOT;
			btNot.FlatAppearance.BorderSize = 2;
			numInputs.Enabled = false;
		}

		private void btNand_Click(object sender, EventArgs e)
		{
			RemoveSelection();
			selectedTool = Tools.AddElement;
			selectedAddElement = Elements.NAND;
			btNand.FlatAppearance.BorderSize = 2;
			numInputs.Enabled = true;
		}

		private void btNor_Click(object sender, EventArgs e)
		{
			RemoveSelection();
			selectedTool = Tools.AddElement;
			selectedAddElement = Elements.NOR;
			btNor.FlatAppearance.BorderSize = 2;
			numInputs.Enabled = true;
		}

		private void btXor_Click(object sender, EventArgs e)
		{
			RemoveSelection();
			selectedTool = Tools.AddElement;
			selectedAddElement = Elements.XOR;
			btXor.FlatAppearance.BorderSize = 2;
			numInputs.Enabled = true;
		}

		private void btXnor_Click(object sender, EventArgs e)
		{
			RemoveSelection();
			selectedTool = Tools.AddElement;
			selectedAddElement = Elements.XNOR;
			btXnor.FlatAppearance.BorderSize = 2;
			numInputs.Enabled = true;
		}

		private void btWire_Click(object sender, EventArgs e)
		{
			RemoveSelection();
			selectedTool = Tools.Wire;
			btWire.FlatAppearance.BorderSize = 2;
			numInputs.Enabled = false;
		}

		private void canvas_DoubleClick(object sender, EventArgs e)
		{
			if (selectedTool != Tools.Edit)
				return;

			foreach (var element in (circuit.AllElements.Where(x => x.Rect.Contains(gridPointerPosition))))
			{
				if (element is CircuitInput)
				{
					if (selectedElement != null)
					{
						selectedElement.IsSelected = false;
						selectedElement = null;
					}
					(element as CircuitInput).Toggle();
				}
			}

			canvas.Refresh();

		}

		private void CircuitDesignerForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (selectedElement == null)
				return;

			if (e.KeyCode == Keys.Delete)
			{
				circuit.RemoveElement(selectedElement);
				movingElement = null;
				selectedElement = null;
			}

			canvas.Refresh();
		}
	}
}
