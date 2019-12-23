using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject
{
    public class CircuitInput : Element
    {
        public bool Value { get; set; }

        private Input[] inputs = new Input[0];
        public override Input[] Inputs
        {
            get { return inputs; }
        }

        public override bool[] Outputs
        {
            get { return new bool[] { Value }; }
        }

        public CircuitInput(bool value = false)
        {
            Value = value;
        }
    }
}
