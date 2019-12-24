using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject
{
    public class OrGate : Gate
    {
        public override bool Output
        {
            get { return Inputs.Any(x => x.Value); }
        }

        public OrGate(int numberOfInputs = 2) : base(numberOfInputs) { }
    }
}
