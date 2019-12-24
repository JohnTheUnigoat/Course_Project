using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject
{
    public class NotGate : Gate
    {
        public override bool Output
        {
            get { return !Inputs[0].Value; }
        }

        public NotGate() : base(1) { }
    }
}
