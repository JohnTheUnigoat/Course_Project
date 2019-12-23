using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject
{
    public class NotGate : Element
    {
        public override Input[] Inputs { get; }

        public override bool[] Outputs
        {
            get { return new bool[] { !Inputs[0].Value }; }
        }

        public NotGate()
        {
            Inputs = new Input[1];
        }

        public void SetInput(Input input)
        {
            Inputs[0] = input;
        }
    }
}
