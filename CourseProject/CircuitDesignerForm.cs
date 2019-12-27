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

        private enum Tools { Edit, AND, OR, NOT, NAND, NOR, XOR, XNOR, Wire };

        private Tools selectedTool;

        public CircuitDesignerForm()
        {
            InitializeComponent();
        }

        private void CircuitDesignerForm_Load(object sender, EventArgs e)
        {
            typeof(Panel).InvokeMember("DoubleBuffered",
                BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                null, canvas, new object[] { true });

            circuit = new Circuit(6, 1);

            selectedTool = Tools.Edit;
            numInputs.Value = 2;
        }

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics gfx = e.Graphics;

            gfx.SmoothingMode = SmoothingMode.AntiAlias;

            gfx.Clear(canvas.BackColor);

            if (createdWire != null)
                createdWire.Draw(gfx, Pens.Wheat, Pens.GreenYellow, gridSize);

            circuit.Draw(gfx, Pens.Wheat, Pens.GreenYellow, new SolidBrush(canvas.BackColor), gridSize);
        }


        private Wire createdWire;

        private Element createdElement;

        private bool PointInWire(Point point, Wire wire)
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

        private void CreateNewWire()
        {
            Element connectedElement = null;

            foreach (var element in circuit.AllElements)
            {
                // prevent wire from overlaping with element
                if (element.InputPositions.Contains(gridPointerPosition))
                    return;

                if (element.OutputPositions.Contains(gridPointerPosition))
                {
                    connectedElement = element;
                    break;
                }
            }

            createdWire = new Wire();

            if (connectedElement != null)
                createdWire.SetInput(new Connection(connectedElement));

            createdWire.Position = gridPointerPosition;
        }

        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (selectedTool == Tools.Wire && createdWire == null)
            {
                CreateNewWire();
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

            if (selectedTool == Tools.Wire && createdWire != null)
            {
                if (createdWire.WireDirection == Wire.Direction.Undefined)
                {
                    SetCreatedWireDirection();
                }

                WireResize(createdWire, true);
            }
        }


        private void AddCreatedWireToCircuit()
        {
            if (createdWire == null)
                return;

            // check if wire intersects with any elements
            if (circuit.AllElements.Any(x => x.Rect.IntersectsWith(createdWire.Rect) && !(x is Wire)))
            {
                canvas.Invalidate(createdWire.GetInvalidateRect(gridSize));
                createdWire = null;
                return;
            }

            // check if wire output tries to connect to other element's output
            if (circuit.AllElements.Any(x => x.OutputPositions.Contains(createdWire.OutputPositions[0])))
            {
                canvas.Invalidate(createdWire.GetInvalidateRect(gridSize));
                createdWire = null;
                return;
            }

            // prevent putting wire outputs inside other wires
            if (circuit.Wires.Any(x => PointInWire(createdWire.OutputPositions[0], x)))
            {
                canvas.Invalidate(createdWire.GetInvalidateRect(gridSize));
                createdWire = null;
                return;
            }

            // prevent creating wires that lay over other elements' ports
            if (circuit.AllElements.
                Any(x => 
                    x.OutputPositions.Any(xOut => PointInWire(xOut, createdWire)) ||
                    x.InputPositions.Any(xIn => PointInWire(xIn, createdWire))))
            {
                canvas.Invalidate(createdWire.GetInvalidateRect(gridSize));
                createdWire = null;
                return;
            }

            // check if wire output corresponds to any input
            foreach (var element in circuit.AllElements)
            {
                for (int i = 0; i < element.InputPositions.Length; i++)
                {
                    if (createdWire.OutputPositions[0] == element.InputPositions[i])
                        element.SetInput(i, new Connection(createdWire));
                }
            }

            Wire adjacentInput = null;
            Wire adjacentOutput = null;

            //merge wires if needed
            foreach (var wire in circuit.Wires)
            {
                if (createdWire.WireDirection == wire.WireDirection)
                {
                    if (createdWire.Position == wire.OutputPositions[0] && wire.OutputCounter == 1)
                        adjacentInput = wire;

                    if (createdWire.OutputPositions[0] == wire.Position)
                        adjacentOutput = wire;

                    if (adjacentInput != null && adjacentOutput != null)
                        break;
                }
            }

            if (adjacentInput != null)
            {
                createdWire.SetInput(adjacentInput.Inputs[0]);
                createdWire.Position = adjacentInput.Position;
                createdWire.Length += adjacentInput.Length;

                circuit.RemoveElement(adjacentInput);
            }

            if (adjacentOutput != null)
            {
                adjacentOutput.SetInput(createdWire.Inputs[0]);
                adjacentOutput.Position = createdWire.Position;
                adjacentOutput.Length += createdWire.Length;

                createdWire = null;
            }



            // add wire to circuit
            if (createdWire != null && createdWire.Length > 0)
                circuit.AddElement(createdWire);

            createdWire = null;
            canvas.Refresh();
        }

        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            SetPointerPosition(e.Location);

            if(selectedTool == Tools.Wire)
            {
                AddCreatedWireToCircuit();
            }

            switch (selectedTool)
            {
                case Tools.Edit:
                    return;
                case Tools.AND:
                    createdElement = new AndGate((int)numInputs.Value);
                    break;
                case Tools.OR:
                    createdElement = new OrGate((int)numInputs.Value);
                    break;
                case Tools.NOT:
                    createdElement = new NotGate();
                    break;
                case Tools.NAND:
                    createdElement = new NandGate((int)numInputs.Value);
                    break;
                case Tools.NOR:
                    createdElement = new NorGate((int)numInputs.Value);
                    break;
                case Tools.XOR:
                    createdElement = new XorGate((int)numInputs.Value);
                    break;
                case Tools.XNOR:
                    createdElement = new XnorGate((int)numInputs.Value);
                    break;
                default:
                    return;
            }

            createdElement.Position = gridPointerPosition;

            circuit.AddElement(createdElement);

            Rectangle rect = createdElement.GetInvalidateRect(gridSize);
            canvas.Invalidate(rect);
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
            selectedTool = Tools.AND;
            btAnd.FlatAppearance.BorderSize = 2;
            numInputs.Enabled = true;
        }

        private void btOr_Click(object sender, EventArgs e)
        {
            RemoveSelection();
            selectedTool = Tools.OR;
            btOr.FlatAppearance.BorderSize = 2;
            numInputs.Enabled = true;
        }

        private void btNot_Click(object sender, EventArgs e)
        {
            RemoveSelection();
            selectedTool = Tools.NOT;
            btNot.FlatAppearance.BorderSize = 2;
            numInputs.Enabled = false;
        }

        private void btNand_Click(object sender, EventArgs e)
        {
            RemoveSelection();
            selectedTool = Tools.NAND;
            btNand.FlatAppearance.BorderSize = 2;
            numInputs.Enabled = true;
        }

        private void btNor_Click(object sender, EventArgs e)
        {
            RemoveSelection();
            selectedTool = Tools.NOR;
            btNor.FlatAppearance.BorderSize = 2;
            numInputs.Enabled = true;
        }

        private void btXor_Click(object sender, EventArgs e)
        {
            RemoveSelection();
            selectedTool = Tools.XOR;
            btXor.FlatAppearance.BorderSize = 2;
            numInputs.Enabled = true;
        }

        private void btXnor_Click(object sender, EventArgs e)
        {
            RemoveSelection();
            selectedTool = Tools.XNOR;
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
                    (element as CircuitInput).Toggle();
                }
            }

            canvas.Refresh();

        }
    }
}
