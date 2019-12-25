using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject
{
    public class CircuitOutput : Element
    {
        public Connection Connection { get; set; }

        public override Connection[] Inputs
        {
            get { return new Connection[] { Connection }; }
        }

        private bool[] outputs = new bool[0];
        public override bool[] Outputs
        {
            get { return outputs; }
        }

        public bool Value
        {
            get { return Connection.Value; }
        }

        public override Point[] InputPositions => throw new NotImplementedException();

        public override Point[] OutputPositions => throw new NotImplementedException();
    }
}
