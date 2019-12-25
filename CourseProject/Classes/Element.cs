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
        //Logic
        abstract public Connection[] Inputs { get; }

        abstract public bool[] Outputs { get; }

        //Visuals
        public Point Position { get; set; }

        abstract public Point[] InputPositions { get; }
        abstract public Point[] OutputPositions { get; }

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
    }
}
