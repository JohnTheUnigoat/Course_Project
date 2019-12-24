using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject
{
    public class AndGate : Gate
    {
        public override bool Output
        {
            get { return Inputs.All(x => x.Value); }
        }

        public AndGate(int numberOfInputs = 2) : base(numberOfInputs) { }
    }
}
