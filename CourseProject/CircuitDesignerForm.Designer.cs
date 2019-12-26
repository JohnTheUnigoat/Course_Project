namespace CourseProject
{
    partial class CircuitDesignerForm
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
            this.canvas = new System.Windows.Forms.Panel();
            this.numInputs = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.btWire = new System.Windows.Forms.Button();
            this.btXnor = new System.Windows.Forms.Button();
            this.btXor = new System.Windows.Forms.Button();
            this.btNor = new System.Windows.Forms.Button();
            this.btNand = new System.Windows.Forms.Button();
            this.btNot = new System.Windows.Forms.Button();
            this.btOr = new System.Windows.Forms.Button();
            this.btAnd = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btEdit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numInputs)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // canvas
            // 
            this.canvas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.canvas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.canvas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.canvas.Cursor = System.Windows.Forms.Cursors.Default;
            this.canvas.Location = new System.Drawing.Point(126, 12);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(669, 463);
            this.canvas.TabIndex = 0;
            this.canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.Canvas_Paint);
            this.canvas.DoubleClick += new System.EventHandler(this.canvas_DoubleClick);
            this.canvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseDown);
            this.canvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseMove);
            this.canvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseUp);
            // 
            // numInputs
            // 
            this.numInputs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.numInputs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.numInputs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.numInputs.Location = new System.Drawing.Point(14, 422);
            this.numInputs.Name = "numInputs";
            this.numInputs.Size = new System.Drawing.Size(75, 16);
            this.numInputs.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 406);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Number of inputs";
            // 
            // btWire
            // 
            this.btWire.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.btWire.Cursor = System.Windows.Forms.Cursors.Default;
            this.btWire.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.btWire.FlatAppearance.BorderSize = 0;
            this.btWire.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btWire.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.btWire.Location = new System.Drawing.Point(10, 355);
            this.btWire.Name = "btWire";
            this.btWire.Size = new System.Drawing.Size(87, 37);
            this.btWire.TabIndex = 7;
            this.btWire.Text = "Wire";
            this.btWire.UseVisualStyleBackColor = false;
            this.btWire.Click += new System.EventHandler(this.btWire_Click);
            // 
            // btXnor
            // 
            this.btXnor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.btXnor.Cursor = System.Windows.Forms.Cursors.Default;
            this.btXnor.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.btXnor.FlatAppearance.BorderSize = 0;
            this.btXnor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btXnor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.btXnor.Location = new System.Drawing.Point(10, 312);
            this.btXnor.Name = "btXnor";
            this.btXnor.Size = new System.Drawing.Size(87, 37);
            this.btXnor.TabIndex = 6;
            this.btXnor.Text = "XNOR";
            this.btXnor.UseVisualStyleBackColor = false;
            this.btXnor.Click += new System.EventHandler(this.btXnor_Click);
            // 
            // btXor
            // 
            this.btXor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.btXor.Cursor = System.Windows.Forms.Cursors.Default;
            this.btXor.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.btXor.FlatAppearance.BorderSize = 0;
            this.btXor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btXor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.btXor.Location = new System.Drawing.Point(10, 269);
            this.btXor.Name = "btXor";
            this.btXor.Size = new System.Drawing.Size(87, 37);
            this.btXor.TabIndex = 5;
            this.btXor.Text = "XOR";
            this.btXor.UseVisualStyleBackColor = false;
            this.btXor.Click += new System.EventHandler(this.btXor_Click);
            // 
            // btNor
            // 
            this.btNor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.btNor.Cursor = System.Windows.Forms.Cursors.Default;
            this.btNor.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.btNor.FlatAppearance.BorderSize = 0;
            this.btNor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btNor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.btNor.Location = new System.Drawing.Point(10, 226);
            this.btNor.Name = "btNor";
            this.btNor.Size = new System.Drawing.Size(87, 37);
            this.btNor.TabIndex = 4;
            this.btNor.Text = "NOR";
            this.btNor.UseVisualStyleBackColor = false;
            this.btNor.Click += new System.EventHandler(this.btNor_Click);
            // 
            // btNand
            // 
            this.btNand.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.btNand.Cursor = System.Windows.Forms.Cursors.Default;
            this.btNand.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.btNand.FlatAppearance.BorderSize = 0;
            this.btNand.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btNand.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.btNand.Location = new System.Drawing.Point(10, 183);
            this.btNand.Name = "btNand";
            this.btNand.Size = new System.Drawing.Size(87, 37);
            this.btNand.TabIndex = 3;
            this.btNand.Text = "NAND";
            this.btNand.UseVisualStyleBackColor = false;
            this.btNand.Click += new System.EventHandler(this.btNand_Click);
            // 
            // btNot
            // 
            this.btNot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.btNot.Cursor = System.Windows.Forms.Cursors.Default;
            this.btNot.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.btNot.FlatAppearance.BorderSize = 0;
            this.btNot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btNot.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.btNot.Location = new System.Drawing.Point(10, 140);
            this.btNot.Name = "btNot";
            this.btNot.Size = new System.Drawing.Size(87, 37);
            this.btNot.TabIndex = 2;
            this.btNot.Text = "NOT";
            this.btNot.UseVisualStyleBackColor = false;
            this.btNot.Click += new System.EventHandler(this.btNot_Click);
            // 
            // btOr
            // 
            this.btOr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.btOr.Cursor = System.Windows.Forms.Cursors.Default;
            this.btOr.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.btOr.FlatAppearance.BorderSize = 0;
            this.btOr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btOr.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.btOr.Location = new System.Drawing.Point(10, 97);
            this.btOr.Name = "btOr";
            this.btOr.Size = new System.Drawing.Size(87, 37);
            this.btOr.TabIndex = 1;
            this.btOr.Text = "OR";
            this.btOr.UseVisualStyleBackColor = false;
            this.btOr.Click += new System.EventHandler(this.btOr_Click);
            // 
            // btAnd
            // 
            this.btAnd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.btAnd.Cursor = System.Windows.Forms.Cursors.Default;
            this.btAnd.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.btAnd.FlatAppearance.BorderSize = 0;
            this.btAnd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btAnd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.btAnd.Location = new System.Drawing.Point(10, 54);
            this.btAnd.Name = "btAnd";
            this.btAnd.Size = new System.Drawing.Size(87, 37);
            this.btAnd.TabIndex = 0;
            this.btAnd.Text = "AND";
            this.btAnd.UseVisualStyleBackColor = false;
            this.btAnd.Click += new System.EventHandler(this.btAnd_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btEdit);
            this.panel2.Controls.Add(this.numInputs);
            this.panel2.Controls.Add(this.btAnd);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.btOr);
            this.panel2.Controls.Add(this.btWire);
            this.panel2.Controls.Add(this.btNot);
            this.panel2.Controls.Add(this.btXnor);
            this.panel2.Controls.Add(this.btNand);
            this.panel2.Controls.Add(this.btXor);
            this.panel2.Controls.Add(this.btNor);
            this.panel2.Location = new System.Drawing.Point(11, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(109, 463);
            this.panel2.TabIndex = 2;
            // 
            // btEdit
            // 
            this.btEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.btEdit.Cursor = System.Windows.Forms.Cursors.Default;
            this.btEdit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.btEdit.FlatAppearance.BorderSize = 2;
            this.btEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btEdit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.btEdit.Location = new System.Drawing.Point(10, 11);
            this.btEdit.Name = "btEdit";
            this.btEdit.Size = new System.Drawing.Size(87, 37);
            this.btEdit.TabIndex = 10;
            this.btEdit.Text = "Edit";
            this.btEdit.UseVisualStyleBackColor = false;
            this.btEdit.Click += new System.EventHandler(this.btEdit_Click);
            // 
            // CircuitDesignerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.ClientSize = new System.Drawing.Size(807, 486);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.canvas);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.Name = "CircuitDesignerForm";
            this.Text = "Circuit Designer";
            this.Load += new System.EventHandler(this.CircuitDesignerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numInputs)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel canvas;
        private System.Windows.Forms.NumericUpDown numInputs;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btWire;
        private System.Windows.Forms.Button btXnor;
        private System.Windows.Forms.Button btXor;
        private System.Windows.Forms.Button btNor;
        private System.Windows.Forms.Button btNand;
        private System.Windows.Forms.Button btNot;
        private System.Windows.Forms.Button btOr;
        private System.Windows.Forms.Button btAnd;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btEdit;
    }
}

