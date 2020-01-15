namespace CourseProject
{
	partial class OpenCircuitForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.numInputs = new System.Windows.Forms.NumericUpDown();
			this.numOutputs = new System.Windows.Forms.NumericUpDown();
			this.btCreateCircuit = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.numInputs)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numOutputs)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(13, 9);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(130, 20);
			this.label1.TabIndex = 0;
			this.label1.Text = "Number of inputs";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(13, 65);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(130, 20);
			this.label2.TabIndex = 1;
			this.label2.Text = "Number of inputs";
			// 
			// numInputs
			// 
			this.numInputs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
			this.numInputs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.numInputs.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.numInputs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
			this.numInputs.Location = new System.Drawing.Point(13, 34);
			this.numInputs.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.numInputs.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
			this.numInputs.Name = "numInputs";
			this.numInputs.Size = new System.Drawing.Size(165, 26);
			this.numInputs.TabIndex = 2;
			this.numInputs.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
			// 
			// numOutputs
			// 
			this.numOutputs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
			this.numOutputs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.numOutputs.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.numOutputs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
			this.numOutputs.Location = new System.Drawing.Point(12, 90);
			this.numOutputs.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.numOutputs.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
			this.numOutputs.Name = "numOutputs";
			this.numOutputs.Size = new System.Drawing.Size(166, 26);
			this.numOutputs.TabIndex = 3;
			this.numOutputs.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// btCreateCircuit
			// 
			this.btCreateCircuit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
			this.btCreateCircuit.FlatAppearance.BorderSize = 0;
			this.btCreateCircuit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btCreateCircuit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btCreateCircuit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
			this.btCreateCircuit.Location = new System.Drawing.Point(12, 124);
			this.btCreateCircuit.Name = "btCreateCircuit";
			this.btCreateCircuit.Size = new System.Drawing.Size(166, 54);
			this.btCreateCircuit.TabIndex = 4;
			this.btCreateCircuit.Text = "Create circuit";
			this.btCreateCircuit.UseVisualStyleBackColor = false;
			this.btCreateCircuit.Click += new System.EventHandler(this.btCreateClick);
			// 
			// OpenCircuitForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
			this.ClientSize = new System.Drawing.Size(190, 191);
			this.Controls.Add(this.btCreateCircuit);
			this.Controls.Add(this.numOutputs);
			this.Controls.Add(this.numInputs);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(206, 230);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(206, 230);
			this.Name = "OpenCircuitForm";
			this.Text = "OpenCircuitForm";
			((System.ComponentModel.ISupportInitialize)(this.numInputs)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numOutputs)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown numInputs;
		private System.Windows.Forms.NumericUpDown numOutputs;
		private System.Windows.Forms.Button btCreateCircuit;
	}
}