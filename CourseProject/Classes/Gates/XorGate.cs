using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject
{
    public class XorGate : Gate
    {
        public override bool Output
        {
            get { return Inputs.Where(x => x.Value).Count() % 2 == 1; }
        }

        public XorGate(int numberOfInputs = 2) : base(numberOfInputs) { }
    }
}
