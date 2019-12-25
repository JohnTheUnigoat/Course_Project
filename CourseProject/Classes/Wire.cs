using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject
{
    public class Wire : Element
    {
        public override Connection[] Inputs { get; }

        public override bool[] Outputs
        {
            get { return new bool[] { Inputs[0].Value }; }
        }

        public Wire()
        {
            Inputs = new Connection[1];
        }

        public Wire(Connection input)
        {
            Inputs = new Connection[] { input };
        }

        public void SetInput(Connection input)
        {
            Inputs[0] = input;
        }
    }
}
