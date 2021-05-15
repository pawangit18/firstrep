namespace TreeView
{
    partial class NodeProperty
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
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNodeTypeDes = new System.Windows.Forms.TextBox();
            this.txtNodeDes = new System.Windows.Forms.TextBox();
            this.txtType = new System.Windows.Forms.TextBox();
            this.txtCardialityMax = new System.Windows.Forms.TextBox();
            this.txtCardialityMin = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 246);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(115, 17);
            this.label9.TabIndex = 20;
            this.label9.Text = "Type Description";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(43, 114);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(79, 17);
            this.label8.TabIndex = 19;
            this.label8.Text = "Description";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(43, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 17);
            this.label3.TabIndex = 16;
            this.label3.Text = "Type";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(229, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 17);
            this.label2.TabIndex = 17;
            this.label2.Text = "Max";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 17);
            this.label1.TabIndex = 18;
            this.label1.Text = "Min";
            // 
            // txtNodeTypeDes
            // 
            this.txtNodeTypeDes.Location = new System.Drawing.Point(119, 223);
            this.txtNodeTypeDes.Multiline = true;
            this.txtNodeTypeDes.Name = "txtNodeTypeDes";
            this.txtNodeTypeDes.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtNodeTypeDes.Size = new System.Drawing.Size(523, 205);
            this.txtNodeTypeDes.TabIndex = 11;
            // 
            // txtNodeDes
            // 
            this.txtNodeDes.Location = new System.Drawing.Point(119, 91);
            this.txtNodeDes.Multiline = true;
            this.txtNodeDes.Name = "txtNodeDes";
            this.txtNodeDes.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtNodeDes.Size = new System.Drawing.Size(523, 116);
            this.txtNodeDes.TabIndex = 12;
            // 
            // txtType
            // 
            this.txtType.Location = new System.Drawing.Point(119, 47);
            this.txtType.Name = "txtType";
            this.txtType.Size = new System.Drawing.Size(302, 22);
            this.txtType.TabIndex = 13;
            // 
            // txtCardialityMax
            // 
            this.txtCardialityMax.Location = new System.Drawing.Point(277, 16);
            this.txtCardialityMax.Name = "txtCardialityMax";
            this.txtCardialityMax.Size = new System.Drawing.Size(144, 22);
            this.txtCardialityMax.TabIndex = 14;
            // 
            // txtCardialityMin
            // 
            this.txtCardialityMin.Location = new System.Drawing.Point(119, 16);
            this.txtCardialityMin.Name = "txtCardialityMin";
            this.txtCardialityMin.Size = new System.Drawing.Size(79, 22);
            this.txtCardialityMin.TabIndex = 15;
            // 
            // NodeProperty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 454);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNodeTypeDes);
            this.Controls.Add(this.txtNodeDes);
            this.Controls.Add(this.txtType);
            this.Controls.Add(this.txtCardialityMax);
            this.Controls.Add(this.txtCardialityMin);
            this.Name = "NodeProperty";
            this.Text = "NodeProperty";
            this.Load += new System.EventHandler(this.NodeProperty_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNodeTypeDes;
        private System.Windows.Forms.TextBox txtNodeDes;
        private System.Windows.Forms.TextBox txtType;
        private System.Windows.Forms.TextBox txtCardialityMax;
        private System.Windows.Forms.TextBox txtCardialityMin;
    }
}