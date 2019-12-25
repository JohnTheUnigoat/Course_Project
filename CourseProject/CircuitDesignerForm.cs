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
        private int gridSize = 20;

        private int pointerRadius = 4;

        private Point gridPointerPosition;

        private Brush pointerBrush;

        private enum Elements { None, AND, OR, NOT, NAND, NOR, XOR, XNOR, Wire };

        private Elements selectedElement;

        public CircuitDesignerForm()
        {
            InitializeComponent();
        }

        private void CircuitDesignerForm_Load(object sender, EventArgs e)
        {
            pointerBrush = Brushes.GreenYellow;
            selectedElement = Elements.None;
            numInputs.Value = 2;
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

            if (oldGridPointerPosition != gridPointerPosition)
            {
                InvalidatePointer(oldGridPointerPosition);
                InvalidatePointer(gridPointerPosition);
            }
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

        private void DrawPointer(Graphics gfx)
        {
            Size pointerSize = new Size(pointerRadius * 2, pointerRadius * 2);

            Point pointerPosition = new Point(
                gridPointerPosition.X * gridSize - pointerRadius,
                gridPointerPosition.Y * gridSize - pointerRadius);

            Rectangle rect = new Rectangle(pointerPosition, pointerSize);

            gfx.FillEllipse(pointerBrush, rect);
        }

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics gfx = e.Graphics;

            gfx.SmoothingMode = SmoothingMode.AntiAlias;

            DrawPointer(gfx);
        }

        private void Canvas_MouseEnter(object sender, EventArgs e)
        {
            Cursor.Hide();
        }

        private void Canvas_MouseLeave(object sender, EventArgs e)
        {
            Cursor.Show();
        }

        private void RemoveSelection()
        {
            selectedElement = Elements.None;

            btAnd.FlatAppearance.BorderSize = 0;
            btOr.FlatAppearance.BorderSize = 0;
            btNot.FlatAppearance.BorderSize = 0;
            btNand.FlatAppearance.BorderSize = 0;
            btNor.FlatAppearance.BorderSize = 0;
            btXor.FlatAppearance.BorderSize = 0;
            btXnor.FlatAppearance.BorderSize = 0;
            btWire.FlatAppearance.BorderSize = 0;
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
    }
}
