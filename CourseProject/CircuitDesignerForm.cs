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

            circuit.Draw(gfx, Pens.Gray, Pens.White, Pens.GreenYellow, new SolidBrush(canvas.BackColor), gridSize);

            Point pos = new Point(gridPointerPosition.X * gridSize - 4, gridPointerPosition.Y * gridSize - 4);
            Size size = new Size(8, 8);
            gfx.DrawRectangle(Pens.Red, new Rectangle(pos, size));
        }


        private Wire createdWire;

        private Element createdWireInput;

        private Element createdElement;


        private Element movingElement;

        private Point positionDisplacement;

    
        private Element selectedElement;


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
            createdWireInput = null;

            bool isOnInput = false;

            foreach (var element in circuit.AllElements)
            {
                if (element.InputPositions.Contains(gridPointerPosition))
                {
                    isOnInput = true;
                }

                if (element.OutputPositions.Contains(gridPointerPosition))
                {
                    createdWireInput = element;
                    break;
                }
            }

            if (isOnInput && createdWireInput == null)
                return;

            createdWire = new Wire();

            createdWire.Position = gridPointerPosition;
        }

        // TODO proper element selection (pen = activePen is great and all, but for real.)
        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            SetPointerPosition(e.Location);

            switch (selectedTool)
            {
                case Tools.Edit:
                    bool elementClick = false;

                    foreach (var element in circuit.AllElements)
                    {
                        if (element.Rect.Contains(gridPointerPosition))
                        {
                            movingElement = element;
                            movingElement.Disconnect();
                            positionDisplacement.X = element.Position.X - gridPointerPosition.X;
                            positionDisplacement.Y = element.Position.Y - gridPointerPosition.Y;
                        }
                    }

                    canvas.Refresh();
                    break;
                case Tools.AddElement:
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
                    if(movingElement != null)
                    {
                        Point newPosition = new Point(
                            gridPointerPosition.X + positionDisplacement.X,
                            gridPointerPosition.Y + positionDisplacement.Y);

                        movingElement.Position = newPosition;
                        canvas.Refresh();
                    }
                    break;
                case Tools.AddElement:
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


        private void AddCreatedWireToCircuit()
        {
            if (createdWire == null)
                return;

            // check if wire intersects with any elements
            if (circuit.AllElements.Any(x =>
            {
                if(x.Rect.IntersectsWith(createdWire.Rect) && !(x is Wire))
                {
                    MessageBox.Show(x.ToString());
                    return true;
                }
                return false;
            }))
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

            Wire wireToSplit = null;

            //merge wires if needed
            foreach (var wire in circuit.Wires)
            {
                if (createdWire.WireDirection == wire.WireDirection)
                {
                    if (createdWire.Position == wire.OutputPositions[0] && wire.OutputCounter == 0)
                        adjacentInput = wire;

                    if (createdWire.OutputPositions[0] == wire.Position)
                        adjacentOutput = wire;

                    if (adjacentInput != null && adjacentOutput != null)
                        break;
                }
                else if (PointInWire(createdWire.Position, wire))
                {
                    wireToSplit = wire;
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
                if(createdWireInput != null)
                    adjacentOutput.SetInput(new Connection(createdWireInput));

                adjacentOutput.Position = createdWire.Position;
                adjacentOutput.Length += createdWire.Length;

                createdWire = null;
            }

            if (wireToSplit != null)
            {
                Wire firstHalf = new Wire();
                firstHalf.Position = wireToSplit.Position;
                firstHalf.WireDirection = wireToSplit.WireDirection;
                firstHalf.Length = 
                    Math.Abs(wireToSplit.Position.X - createdWire.Position.X) + 
                    Math.Abs(wireToSplit.Position.Y - createdWire.Position.Y);

                //if (createdWireInput != null)
                    firstHalf.SetInput(wireToSplit.Inputs[0]);

                wireToSplit.Position = createdWire.Position;
                wireToSplit.Length -= firstHalf.Length;

                wireToSplit.SetInput(new Connection(firstHalf));
                createdWire.SetInput(new Connection(firstHalf));

                circuit.AddElement(firstHalf);
            }

            // add wire to circuit
            if (createdWire != null && createdWire.Length > 0)
            {
                if(createdWireInput != null)
                    createdWire.SetInput(new Connection(createdWireInput));

                circuit.AddElement(createdWire);
            }

            createdWire = null;
            createdWireInput = null;
            canvas.Refresh();
        }

        // Needs to be reimplemented, by removing element from cirquit entirely while you move it.
        // Let the circuit handle the connecting/disconnecting.
        private void ConnectElement(Element connectingElement)
        {
            for (int i = 0; i < connectingElement.InputPositions.Length; i++)
            {
                var inputPos = connectingElement.InputPositions[i];
                foreach (var element in circuit.AllElements)
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
                foreach (var element in circuit.AllElements)
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

        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            SetPointerPosition(e.Location);

            switch (selectedTool)
            {
                case Tools.Edit:
                    bool elementClick = false;

                    foreach (var element in circuit.Elements)
                    {
                        if (element.Rect.Contains(gridPointerPosition))
                        {
                            if (selectedElement != null)
                                selectedElement.IsSelected = false;

                            selectedElement = element;
                            selectedElement.IsSelected = true;
                            elementClick = true;
                            break;
                        }
                    }

                    if (!elementClick && selectedElement != null)
                    {
                        selectedElement.IsSelected = false;
                        selectedElement = null;
                    }

                    if (movingElement == null)
                        return;

                    if (selectedElement == null)
                        movingElement.IsSelected = false;

                    ConnectElement(movingElement);

                    canvas.Refresh();
                    positionDisplacement = new Point(0, 0);
                    movingElement = null;
                    break;
                case Tools.AddElement:
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

                    ConnectElement(createdElement);

                    circuit.AddElement(createdElement);

                    Rectangle rect = createdElement.GetInvalidateRect(gridSize);
                    canvas.Invalidate(rect);
                    break;
                case Tools.Wire:
                    AddCreatedWireToCircuit();
                    break;
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
                circuit.RemoveElement(selectedElement);

            canvas.Refresh();
        }
    }
}
