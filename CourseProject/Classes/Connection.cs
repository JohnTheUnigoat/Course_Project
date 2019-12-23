﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject
{
    public class Connection : Element
    {
        public override Input[] Inputs { get; }

        public override bool[] Outputs
        {
            get { return new bool[] { Inputs[0].Value }; }
        }

        public Connection()
        {
            Inputs = new Input[1];
        }

        public Connection(Input input)
        {
            Inputs = new Input[] { input };
        }

        public void SetInput(Input input)
        {
            Inputs[0] = input;
        }
    }
}
