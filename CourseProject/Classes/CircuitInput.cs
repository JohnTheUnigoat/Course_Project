using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject
{
    public class CircuitInput : Element
    {
        public bool Value { get; set; }

        private static Connection[] inputs = new Connection[0];
        public override Connection[] Inputs
        {
            get { return inputs; }
        }

        public override bool[] Outputs
        {
            get { return new bool[] { Value }; }
        }

        public override Point[] InputPositions => throw new NotImplementedException();

        public override Point[] OutputPositions => throw new NotImplementedException();

        public CircuitInput(bool value = false)
        {
            Value = value;
        }
    }
}
