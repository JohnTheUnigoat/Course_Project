using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseProject
{
	public partial class OpenCircuitForm : Form
	{
		public int NumberOfInputs { get; set; }
		public int NumberOfOutputs { get; set; } 

		public OpenCircuitForm()
		{
			InitializeComponent();
		}

		private void btCreateClick(object sender, EventArgs e)
		{
			NumberOfInputs = (int)numInputs.Value;
			NumberOfOutputs = (int)numOutputs.Value;

			DialogResult = DialogResult.OK;
			Close();
		}
	}
}
