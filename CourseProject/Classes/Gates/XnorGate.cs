using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject
{
    public class XnorGate : Gate
    {
        public override bool Output
        {
            get { return Inputs.Where(x => x.Value).Count() % 2 == 0; }
        }

        public XnorGate(int numberOfInputs = 2) : base(numberOfInputs) { }
    }
}
