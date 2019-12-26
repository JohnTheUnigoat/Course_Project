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

namespace CourseProject
{
    public partial class CircuitDesignerForm : Form
    {
        private Circuit circuit;

        private int gridSize = 20;

        private int pointerRadius = 4;

        private Point gridPointerPosition;

        private Brush pointerBrush;

        private enum Elements { Edit, AND, OR, NOT, NAND, NOR, XOR, XNOR, Wire };

        private Elements selectedElement;

        public CircuitDesignerForm()
        {
            InitializeComponent();
        }

        private void CircuitDesignerForm_Load(object sender, EventArgs e)
        {
            circuit = new Circuit(6, 1);

            pointerBrush = Brushes.GreenYellow;
            selectedElement = Elements.Edit;
            numInputs.Value = 2;
        }

        private void InvalidatePointer(Point gridPosition)
        {
            int offset = pointerRadius + 1;

            Rectangle invalidateRect = new Rectangle(
                gridPosition.X * gridSize - offset,
                gridPosition.Y * gridSize - offset,
                offset * 2,
                offset * 2);

            canvas.Invalidate(invalidateRect);
        }

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics gfx = e.Graphics;

            gfx.SmoothingMode = SmoothingMode.AntiAlias;

            gfx.Clear(canvas.BackColor);

            if (wire != null)
                wire.Draw(gfx, Pens.Wheat, Pens.GreenYellow, new SolidBrush(canvas.BackColor), gridSize);


            circuit.Draw(gfx, Pens.Wheat, Pens.GreenYellow, new SolidBrush(canvas.BackColor), gridSize);

        }

        private Wire wire;

        private Element element;

        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            Element connectedElement = null;

            if (selectedElement == Elements.Wire)
            {
                foreach(var element in circuit.AllElements)
                {
                    if (element.InputPositions.Contains(gridPointerPosition) && !(element is Wire))
                        return;

                    if (element.OutputPositions.Contains(gridPointerPosition))
                        connectedElement = element;
                }

                if (connectedElement == null)
                    wire = new Wire();
                else
                    wire = new Wire(new Connection(connectedElement));

                wire.Position = gridPointerPosition;
            }
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            Point oldGridPointerPosition = gridPointerPosition;

            gridPointerPosition.X = e.X / gridSize;
            gridPointerPosition.Y = e.Y / gridSize;

            if (e.X - gridPointerPosition.X * gridSize > gridSize / 2)
                gridPointerPosition.X++;

            if (e.Y - gridPointerPosition.Y * gridSize > gridSize / 2)
                gridPointerPosition.Y++;

            if(selectedElement == Elements.Wire && wire != null)
            {
                if (wire.WireDirection == Wire.Direction.Undefined)
                {
                    int xOffset = gridPointerPosition.X - wire.Position.X;
                    int yOffset = gridPointerPosition.Y - wire.Position.Y;

                    if (!(xOffset == 0 && yOffset == 0))
                    {
                        if (Math.Abs(xOffset) > Math.Abs(yOffset))
                        {
                            if (xOffset > 0)
                                wire.WireDirection = Wire.Direction.Right;
                            else
                                wire.WireDirection = Wire.Direction.Left;
                        }
                        else
                        {
                            if (yOffset > 0)
                                wire.WireDirection = Wire.Direction.Down;
                            else
                                wire.WireDirection = Wire.Direction.Up;
                        }
                    }
                }

                canvas.Invalidate(wire.GetInvalidateRect(gridSize));

                switch (wire.WireDirection)
                {
                    case Wire.Direction.Up:
                        wire.Length = wire.Position.Y - gridPointerPosition.Y;
                        break;
                    case Wire.Direction.Down:
                        wire.Length = gridPointerPosition.Y - wire.Position.Y;
                        break;
                    case Wire.Direction.Left:
                        wire.Length = wire.Position.X - gridPointerPosition.X;
                        break;
                    case Wire.Direction.Right:
                        wire.Length = gridPointerPosition.X - wire.Position.X;
                        break;
                }

                canvas.Invalidate(wire.GetInvalidateRect(gridSize));
            }
        }

        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            if(selectedElement == Elements.Wire)
            {
                if (wire == null)
                    return;

                if (circuit.AllElements.Any(x => !(x is Wire) && x.Rect.IntersectsWith(wire.Rect)))
                {
                    canvas.Invalidate(wire.GetInvalidateRect(gridSize));
                    wire = null;
                    return;
                }

                // check if wire output corresponds to any input
                foreach (var element in circuit.AllElements)
                {
                    for(int i = 0; i < element.InputPositions.Length; i++)
                    {
                        if (gridPointerPosition == element.InputPositions[i])
                        {
                            element.SetInput(i, new Connection(wire));
                            canvas.Refresh();
                        }
                    }
                }

                circuit.AddElement(wire);

                wire = null;
                return;
            }

            switch (selectedElement)
            {
                case Elements.Edit:
                    return;
                case Elements.AND:
                    element = new AndGate((int)numInputs.Value);
                    break;
                case Elements.OR:
                    element = new OrGate((int)numInputs.Value);
                    break;
                case Elements.NOT:
                    element = new NotGate();
                    break;
                case Elements.NAND:
                    element = new NandGate((int)numInputs.Value);
                    break;
                case Elements.NOR:
                    element = new NorGate((int)numInputs.Value);
                    break;
                case Elements.XOR:
                    element = new XorGate((int)numInputs.Value);
                    break;
                case Elements.XNOR:
                    element = new XnorGate((int)numInputs.Value);
                    break;
                default:
                    return;
            }

            element.Position = gridPointerPosition;

            circuit.AddElement(element);

            Rectangle rect = element.GetInvalidateRect(gridSize);
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
            selectedElement = Elements.Edit;
            btEdit.FlatAppearance.BorderSize = 2;
            numInputs.Enabled = false;
        }

        private void btAnd_Click(object sender, EventArgs e)
        {
            RemoveSelection();
            selectedElement = Elements.AND;
            btAnd.FlatAppearance.BorderSize = 2;
            numInputs.Enabled = true;
        }

        private void btOr_Click(object sender, EventArgs e)
        {
            RemoveSelection();
            selectedElement = Elements.OR;
            btOr.FlatAppearance.BorderSize = 2;
            numInputs.Enabled = true;
        }

        private void btNot_Click(object sender, EventArgs e)
        {
            RemoveSelection();
            selectedElement = Elements.NOT;
            btNot.FlatAppearance.BorderSize = 2;
            numInputs.Enabled = false;
        }

        private void btNand_Click(object sender, EventArgs e)
        {
            RemoveSelection();
            selectedElement = Elements.NAND;
            btNand.FlatAppearance.BorderSize = 2;
            numInputs.Enabled = true;
        }

        private void btNor_Click(object sender, EventArgs e)
        {
            RemoveSelection();
            selectedElement = Elements.NOR;
            btNor.FlatAppearance.BorderSize = 2;
            numInputs.Enabled = true;
        }

        private void btXor_Click(object sender, EventArgs e)
        {
            RemoveSelection();
            selectedElement = Elements.XOR;
            btXor.FlatAppearance.BorderSize = 2;
            numInputs.Enabled = true;
        }

        private void btXnor_Click(object sender, EventArgs e)
        {
            RemoveSelection();
            selectedElement = Elements.XNOR;
            btXnor.FlatAppearance.BorderSize = 2;
            numInputs.Enabled = true;
        }

        private void btWire_Click(object sender, EventArgs e)
        {
            RemoveSelection();
            selectedElement = Elements.Wire;
            btWire.FlatAppearance.BorderSize = 2;
            numInputs.Enabled = false;
        }

        private void canvas_DoubleClick(object sender, EventArgs e)
        {
            if (selectedElement != Elements.Edit)
                return;

            foreach (var element in (circuit.AllElements.Where(x => x.Rect.Contains(gridPointerPosition))))
            {
                if (element is CircuitInput)
                {
                    (element as CircuitInput).Toggle();
                    canvas.Refresh();
                }
            }

        }
    }
}
