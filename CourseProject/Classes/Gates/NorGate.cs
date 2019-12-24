using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject
{
    public class NorGate : Gate
    {
        public override bool Output
        {
            get { return Inputs.All(x => x.Value == false); }
        }

        public NorGate(int numberOfInputs = 2) : base(numberOfInputs) { }
    }
}
