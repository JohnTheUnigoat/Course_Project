using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject
{
    class XnorGate : Element
    {
        public override Input[] Inputs { get; }

        public override bool[] Outputs
        {
            get
            {
                bool res = Inputs.Where(x => x.Value).Count() % 2 == 0;

                return new bool[] { res };
            }
        }

        public XnorGate(int numberOfInputs = 2)
        {
            Inputs = new Input[numberOfInputs];
        }

        public void SetInput(int inputIndex, Input input)
        {
            Inputs[inputIndex] = input;
        }
    }
}
