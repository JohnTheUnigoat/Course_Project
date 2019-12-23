using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject
{
    public struct Input
    {
        public Element Source { get; set; }

        public int OutputIndex { get; set; }

        public bool IsSet
        {
            get { return Source != null; }
        }

        public bool Value
        {
            get
            {
                if (!IsSet)
                    return false;

                return Source.Outputs[OutputIndex];
            }
        }

        public Input(Element source, int outputIndex = 0)
        {
            Source = source;
            OutputIndex = outputIndex;
        }
    }

    abstract public class Element
    {
        abstract public Input[] Inputs { get; }

        abstract public bool[] Outputs { get; }
    }
}
