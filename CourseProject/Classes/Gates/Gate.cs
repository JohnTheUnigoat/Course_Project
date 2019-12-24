﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject
{
    public abstract class Gate : Element
    {
        public override Input[] Inputs { get; }
        
        public abstract bool Output { get; }

        public override bool[] Outputs {
            get { return new bool[] { Output }; }
        }

        public Gate(int numberOfInputs)
        {
            Inputs = new Input[numberOfInputs];
        }

        public void SetInput(int inputIndex, Input input)
        {
            Inputs[inputIndex] = input;
        }
    }
}
