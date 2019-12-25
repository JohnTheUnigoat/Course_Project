using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject
{
    public class Circuit
    {
        private List<CircuitInput> inputs;
        public ReadOnlyCollection<CircuitInput> Inputs
        {
            get { return new ReadOnlyCollection<CircuitInput>(inputs); }
        }

        private List<CircuitOutput> outputs;
        public ReadOnlyCollection<CircuitOutput> Outputs
        {
            get { return new ReadOnlyCollection<CircuitOutput>(outputs); }
        }

        private List<Element> elements;
        public ReadOnlyCollection<Element> Elements
        {
            get { return new ReadOnlyCollection<Element>(elements); }
        }

        public Circuit(int numberOfInputs = 0, int numberOfOutputs = 0)
        {
            inputs = new List<CircuitInput>(numberOfInputs);
            
            for(int i = 0; i < numberOfInputs; i++)
            {
                inputs.Add(new CircuitInput());
            }

            outputs = new List<CircuitOutput>(numberOfOutputs);

            for(int i = 0; i < numberOfOutputs; i++)
            {
                outputs.Add(new CircuitOutput());
            }

            elements = new List<Element>();
        }

        public void AddInput() { inputs.Add(new CircuitInput()); }

        public void AddOutput() { outputs.Add(new CircuitOutput()); }

        public void AddElement(Element element)
        {
            elements.Add(element);
        }
    }
}
