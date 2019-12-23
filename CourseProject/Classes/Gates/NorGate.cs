using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject
{
    public class NorGate : Element
    {
        public override Input[] Inputs { get; }

        public override bool[] Outputs
        {
            get
            {
                bool res = Inputs.All(x => x.Value == false);

                return new bool[] { res };
            }
        }

        public NorGate(int numberOfInputs = 2)
        {
            Inputs = new Input[numberOfInputs];
        }

        public void SetInput(int inputIndex, Input input)
        {
            Inputs[inputIndex] = input;
        }
    }
}
