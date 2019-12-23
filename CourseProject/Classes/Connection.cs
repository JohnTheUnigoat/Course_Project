using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject
{
    class Connection : Element
    {
        private Input[] inputs;
        public override Input[] Inputs
        {
            get { return inputs; }
        }

        public override bool[] Outputs
        {
            get { return new bool[] { inputs[0].Value }; }
        }

        public Connection()
        {
            inputs = new Input[1];
        }

        public Connection(Input input)
        {
            inputs = new Input[] { input };
        }

        public void SetInput(Input input)
        {
            inputs[0] = input;
        }
    }
}
