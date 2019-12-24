using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject
{
    public class NandGate : Gate
    {
        public override bool Output
        {
            get { return !Inputs.All(x => x.Value); }
        }

        public NandGate(int numberOfInputs = 2) : base(numberOfInputs) { }
    }
}
