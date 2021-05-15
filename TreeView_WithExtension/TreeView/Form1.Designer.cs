namespace TreeView
{
    partial class Form1
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
            this.tv = new System.Windows.Forms.TreeView();
            this.txtCardialityMin = new System.Windows.Forms.TextBox();
            this.txtCardialityMax = new System.Windows.Forms.TextBox();
            this.txtType = new System.Windows.Forms.TextBox();
            this.txtNodeDes = new System.Windows.Forms.TextBox();
            this.txtValOptions = new System.Windows.Forms.TextBox();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.cmbBoxResource = new System.Windows.Forms.ComboBox();
            this.txtId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtNodeTypeDes = new System.Windows.Forms.TextBox();
            this.btnExportXml = new System.Windows.Forms.Button();
            this.rchTxtBox = new System.Windows.Forms.RichTextBox();
            this.rchTxtJson = new System.Windows.Forms.RichTextBox();
            this.btnLoadResource = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.rchtxtXml = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tv
            // 
            this.tv.Location = new System.Drawing.Point(29, 100);
            this.tv.Name = "tv";
            this.tv.Size = new System.Drawing.Size(339, 555);
            this.tv.TabIndex = 1;
            this.tv.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tv_AfterSelect);
            // 
            // txtCardialityMin
            // 
            this.txtCardialityMin.Location = new System.Drawing.Point(460, 60);
            this.txtCardialityMin.Name = "txtCardialityMin";
            this.txtCardialityMin.Size = new System.Drawing.Size(79, 22);
            this.txtCardialityMin.TabIndex = 2;
            // 
            // txtCardialityMax
            // 
            this.txtCardialityMax.Location = new System.Drawing.Point(618, 60);
            this.txtCardialityMax.Name = "txtCardialityMax";
            this.txtCardialityMax.Size = new System.Drawing.Size(144, 22);
            this.txtCardialityMax.TabIndex = 2;
            // 
            // txtType
            // 
            this.txtType.Location = new System.Drawing.Point(460, 91);
            this.txtType.Name = "txtType";
            this.txtType.Size = new System.Drawing.Size(302, 22);
            this.txtType.TabIndex = 2;
            // 
            // txtNodeDes
            // 
            this.txtNodeDes.Location = new System.Drawing.Point(457, 232);
            this.txtNodeDes.Multiline = true;
            this.txtNodeDes.Name = "txtNodeDes";
            this.txtNodeDes.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtNodeDes.Size = new System.Drawing.Size(302, 183);
            this.txtNodeDes.TabIndex = 2;
            // 
            // txtValOptions
            // 
            this.txtValOptions.Location = new System.Drawing.Point(460, 177);
            this.txtValOptions.Name = "txtValOptions";
            this.txtValOptions.Size = new System.Drawing.Size(302, 22);
            this.txtValOptions.TabIndex = 2;
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(460, 149);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(302, 22);
            this.txtValue.TabIndex = 2;
            this.txtValue.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtValue_KeyUp);
            // 
            // cmbBoxResource
            // 
            this.cmbBoxResource.FormattingEnabled = true;
            this.cmbBoxResource.Location = new System.Drawing.Point(29, 59);
            this.cmbBoxResource.Name = "cmbBoxResource";
            this.cmbBoxResource.Size = new System.Drawing.Size(339, 24);
            this.cmbBoxResource.TabIndex = 3;
            this.cmbBoxResource.SelectedIndexChanged += new System.EventHandler(this.cmbBoxResource_SelectedIndexChanged);
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(460, 119);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(302, 22);
            this.txtId.TabIndex = 2;
            this.txtId.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtId_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(384, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Min";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(570, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Max";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(384, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Type";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(384, 180);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 17);
            this.label5.TabIndex = 4;
            this.label5.Text = "Value List";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(384, 119);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(21, 17);
            this.label6.TabIndex = 4;
            this.label6.Text = "ID";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(384, 149);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 17);
            this.label7.TabIndex = 4;
            this.label7.Text = "Value";
            // 
            // txtNodeTypeDes
            // 
            this.txtNodeTypeDes.Location = new System.Drawing.Point(457, 450);
            this.txtNodeTypeDes.Multiline = true;
            this.txtNodeTypeDes.Name = "txtNodeTypeDes";
            this.txtNodeTypeDes.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtNodeTypeDes.Size = new System.Drawing.Size(302, 205);
            this.txtNodeTypeDes.TabIndex = 2;
            // 
            // btnExportXml
            // 
            this.btnExportXml.Location = new System.Drawing.Point(55, 680);
            this.btnExportXml.Name = "btnExportXml";
            this.btnExportXml.Size = new System.Drawing.Size(114, 23);
            this.btnExportXml.TabIndex = 5;
            this.btnExportXml.Text = "SaveProfile";
            this.btnExportXml.UseVisualStyleBackColor = true;
            this.btnExportXml.Click += new System.EventHandler(this.btnExportXml_Click);
            // 
            // rchTxtBox
            // 
            this.rchTxtBox.Location = new System.Drawing.Point(390, 309);
            this.rchTxtBox.Name = "rchTxtBox";
            this.rchTxtBox.Size = new System.Drawing.Size(35, 32);
            this.rchTxtBox.TabIndex = 6;
            this.rchTxtBox.Text = "";
            this.rchTxtBox.Visible = false;
            // 
            // rchTxtJson
            // 
            this.rchTxtJson.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.rchTxtJson.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rchTxtJson.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rchTxtJson.ForeColor = System.Drawing.Color.Blue;
            this.rchTxtJson.Location = new System.Drawing.Point(785, 56);
            this.rchTxtJson.Name = "rchTxtJson";
            this.rchTxtJson.Size = new System.Drawing.Size(326, 599);
            this.rchTxtJson.TabIndex = 7;
            this.rchTxtJson.Text = "";
            // 
            // btnLoadResource
            // 
            this.btnLoadResource.Location = new System.Drawing.Point(175, 680);
            this.btnLoadResource.Name = "btnLoadResource";
            this.btnLoadResource.Size = new System.Drawing.Size(114, 23);
            this.btnLoadResource.TabIndex = 5;
            this.btnLoadResource.Text = "Load Profile";
            this.btnLoadResource.UseVisualStyleBackColor = true;
            this.btnLoadResource.Click += new System.EventHandler(this.btnLoadResource_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "Select Resource";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(457, 212);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(79, 17);
            this.label8.TabIndex = 9;
            this.label8.Text = "Description";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(457, 430);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(115, 17);
            this.label9.TabIndex = 10;
            this.label9.Text = "Type Description";
            // 
            // rchtxtXml
            // 
            this.rchtxtXml.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.rchtxtXml.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rchtxtXml.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rchtxtXml.ForeColor = System.Drawing.Color.Blue;
            this.rchtxtXml.Location = new System.Drawing.Point(1130, 61);
            this.rchtxtXml.Name = "rchtxtXml";
            this.rchtxtXml.Size = new System.Drawing.Size(326, 222);
            this.rchtxtXml.TabIndex = 7;
            this.rchtxtXml.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1259, 341);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1496, 717);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.rchtxtXml);
            this.Controls.Add(this.rchTxtJson);
            this.Controls.Add(this.rchTxtBox);
            this.Controls.Add(this.btnLoadResource);
            this.Controls.Add(this.btnExportXml);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbBoxResource);
            this.Controls.Add(this.txtNodeTypeDes);
            this.Controls.Add(this.txtNodeDes);
            this.Controls.Add(this.txtValOptions);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.txtValue);
            this.Controls.Add(this.txtType);
            this.Controls.Add(this.txtCardialityMax);
            this.Controls.Add(this.txtCardialityMin);
            this.Controls.Add(this.tv);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TreeView tv;
        private System.Windows.Forms.TextBox txtCardialityMin;
        private System.Windows.Forms.TextBox txtCardialityMax;
        private System.Windows.Forms.TextBox txtType;
        private System.Windows.Forms.TextBox txtNodeDes;
        private System.Windows.Forms.TextBox txtValOptions;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.ComboBox cmbBoxResource;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtNodeTypeDes;
        private System.Windows.Forms.Button btnExportXml;
        private System.Windows.Forms.RichTextBox rchTxtBox;
        private System.Windows.Forms.RichTextBox rchTxtJson;
        private System.Windows.Forms.Button btnLoadResource;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.RichTextBox rchtxtXml;
        private System.Windows.Forms.Button button1;
    }
}

