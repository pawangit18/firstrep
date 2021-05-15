namespace TreeView
{
    partial class NodeValueEditor
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
            this.txtBoxValue = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.lbl = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.dtPicker = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.cmbExtElementDataTypes = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // txtBoxValue
            // 
            this.txtBoxValue.Location = new System.Drawing.Point(85, 14);
            this.txtBoxValue.Name = "txtBoxValue";
            this.txtBoxValue.Size = new System.Drawing.Size(255, 26);
            this.txtBoxValue.TabIndex = 0;
            this.txtBoxValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBoxValue_KeyPress);
            this.txtBoxValue.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtBoxValue_KeyUp);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(135, 66);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(70, 26);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lbl
            // 
            this.lbl.AutoSize = true;
            this.lbl.Location = new System.Drawing.Point(11, 15);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(43, 18);
            this.lbl.TabIndex = 2;
            this.lbl.Text = "Value";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(85, 14);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(255, 26);
            this.comboBox1.TabIndex = 3;
            // 
            // dtPicker
            // 
            this.dtPicker.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtPicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtPicker.Location = new System.Drawing.Point(85, 15);
            this.dtPicker.Name = "dtPicker";
            this.dtPicker.Size = new System.Drawing.Size(255, 26);
            this.dtPicker.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(228, 66);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(70, 26);
            this.button1.TabIndex = 1;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmbExtElementDataTypes
            // 
            this.cmbExtElementDataTypes.FormattingEnabled = true;
            this.cmbExtElementDataTypes.Location = new System.Drawing.Point(85, 15);
            this.cmbExtElementDataTypes.Name = "cmbExtElementDataTypes";
            this.cmbExtElementDataTypes.Size = new System.Drawing.Size(255, 26);
            this.cmbExtElementDataTypes.TabIndex = 5;
            // 
            // NodeValueEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(352, 106);
            this.Controls.Add(this.cmbExtElementDataTypes);
            this.Controls.Add(this.dtPicker);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.lbl);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtBoxValue);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NodeValueEditor";
            this.Text = "Field Value Editor";
            this.Load += new System.EventHandler(this.NodeValueEditor_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtBoxValue;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lbl;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.DateTimePicker dtPicker;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cmbExtElementDataTypes;
    }
}