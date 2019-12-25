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
        private int gridSize = 25;

        private int pointerRadius = 8;

        private Point gridPointerPosition;

        private Brush pointerBrush = Brushes.Green;

        public CircuitDesignerForm()
        {
            InitializeComponent();
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

            InvalidatePointer(oldGridPointerPosition);
            InvalidatePointer(gridPointerPosition);
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
            Size pointerSize = new Size(pointerRadius, pointerRadius);

            Point pointerPosition = new Point(
                gridPointerPosition.X * gridSize,
                gridPointerPosition.Y * gridSize);

            Rectangle rect = new Rectangle(pointerPosition, pointerSize);

            gfx.FillEllipse(pointerBrush, rect);
        }

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics gfx = e.Graphics;

            gfx.SmoothingMode = SmoothingMode.AntiAlias;

            DrawPointer(gfx);
        }
    }
}
