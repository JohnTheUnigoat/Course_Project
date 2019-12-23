using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject
{
    public class OrGate : Element
    {
        public override Input[] Inputs { get; }

        public override bool[] Outputs
        {
            get
            {
                bool res = Inputs.Any(x => x.Value);

                return new bool[] { res };
            }
        }

        public OrGate(int numberOfInputs = 2)
        {
            Inputs = new Input[numberOfInputs];
        }

        public void SetInput(int inputIndex, Input input)
        {
            Inputs[inputIndex] = input;
        }
    }
}
