namespace TreeView
{
    partial class Form1_New
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
            this.btnExportXml = new System.Windows.Forms.Button();
            this.rchTxtJson = new System.Windows.Forms.RichTextBox();
            this.btnLoadResource = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.rt = new System.Windows.Forms.RichTextBox();
            this.lnkParseJsonReq = new System.Windows.Forms.LinkLabel();
            this.lnkCopyJsonReq = new System.Windows.Forms.LinkLabel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lnkCopyXmlReq = new System.Windows.Forms.LinkLabel();
            this.rchTxtXmlRequest = new System.Windows.Forms.RichTextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgServiceHeader = new System.Windows.Forms.DataGridView();
            this.HeaderName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HeaderValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmbServiceMediaType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSendServiceRequest = new System.Windows.Forms.Button();
            this.cmbServiceMethod = new System.Windows.Forms.ComboBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabResponseJson = new System.Windows.Forms.TabPage();
            this.rchTxtServiceResponse = new System.Windows.Forms.RichTextBox();
            this.tabXml = new System.Windows.Forms.TabPage();
            this.rchTxtServiceResponseXml = new System.Windows.Forms.RichTextBox();
            this.tabHeaders = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtServiceEndpoint = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tabControl3 = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tv = new System.Windows.Forms.TreeView();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.dgParams = new System.Windows.Forms.DataGridView();
            this.cmbBoxResource = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lblFormHeader = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgServiceHeader)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabResponseJson.SuspendLayout();
            this.tabXml.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabControl3.SuspendLayout();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgParams)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnExportXml
            // 
            this.btnExportXml.BackColor = System.Drawing.SystemColors.Control;
            this.btnExportXml.Enabled = false;
            this.btnExportXml.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnExportXml.Location = new System.Drawing.Point(400, 208);
            this.btnExportXml.Name = "btnExportXml";
            this.btnExportXml.Size = new System.Drawing.Size(110, 30);
            this.btnExportXml.TabIndex = 5;
            this.btnExportXml.Text = "Save Instance";
            this.btnExportXml.UseVisualStyleBackColor = false;
            this.btnExportXml.Click += new System.EventHandler(this.btnExportXml_Click);
            // 
            // rchTxtJson
            // 
            this.rchTxtJson.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.rchTxtJson.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rchTxtJson.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rchTxtJson.ForeColor = System.Drawing.Color.Blue;
            this.rchTxtJson.Location = new System.Drawing.Point(3, 22);
            this.rchTxtJson.Name = "rchTxtJson";
            this.rchTxtJson.Size = new System.Drawing.Size(351, 359);
            this.rchTxtJson.TabIndex = 7;
            this.rchTxtJson.Text = "";
            // 
            // btnLoadResource
            // 
            this.btnLoadResource.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnLoadResource.Location = new System.Drawing.Point(400, 244);
            this.btnLoadResource.Name = "btnLoadResource";
            this.btnLoadResource.Size = new System.Drawing.Size(110, 30);
            this.btnLoadResource.TabIndex = 5;
            this.btnLoadResource.Text = "Load Instance";
            this.btnLoadResource.UseVisualStyleBackColor = true;
            this.btnLoadResource.Click += new System.EventHandler(this.btnLoadResource_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.groupBox1.Controls.Add(this.tabControl2);
            this.groupBox1.Controls.Add(this.btnSendServiceRequest);
            this.groupBox1.Controls.Add(this.cmbServiceMethod);
            this.groupBox1.Controls.Add(this.tabControl1);
            this.groupBox1.Controls.Add(this.btnLoadResource);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.btnExportXml);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtServiceEndpoint);
            this.groupBox1.Font = new System.Drawing.Font("Book Antiqua", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(318, 40);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(903, 539);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Service Client";
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage1);
            this.tabControl2.Controls.Add(this.tabPage2);
            this.tabControl2.Controls.Add(this.tabPage3);
            this.tabControl2.Font = new System.Drawing.Font("Book Antiqua", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl2.Location = new System.Drawing.Point(11, 109);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(368, 420);
            this.tabControl2.TabIndex = 15;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.rt);
            this.tabPage1.Controls.Add(this.lnkParseJsonReq);
            this.tabPage1.Controls.Add(this.lnkCopyJsonReq);
            this.tabPage1.Controls.Add(this.rchTxtJson);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(360, 387);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Json";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // rt
            // 
            this.rt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rt.ForeColor = System.Drawing.Color.BlueViolet;
            this.rt.Location = new System.Drawing.Point(3, 0);
            this.rt.Name = "rt";
            this.rt.Size = new System.Drawing.Size(40, 16);
            this.rt.TabIndex = 9;
            this.rt.Text = "";
            // 
            // lnkParseJsonReq
            // 
            this.lnkParseJsonReq.AutoSize = true;
            this.lnkParseJsonReq.Location = new System.Drawing.Point(293, 0);
            this.lnkParseJsonReq.Name = "lnkParseJsonReq";
            this.lnkParseJsonReq.Size = new System.Drawing.Size(45, 20);
            this.lnkParseJsonReq.TabIndex = 8;
            this.lnkParseJsonReq.TabStop = true;
            this.lnkParseJsonReq.Text = "Parse";
            this.lnkParseJsonReq.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkParseJsonReq_LinkClicked);
            // 
            // lnkCopyJsonReq
            // 
            this.lnkCopyJsonReq.AutoSize = true;
            this.lnkCopyJsonReq.Location = new System.Drawing.Point(246, 0);
            this.lnkCopyJsonReq.Name = "lnkCopyJsonReq";
            this.lnkCopyJsonReq.Size = new System.Drawing.Size(45, 20);
            this.lnkCopyJsonReq.TabIndex = 8;
            this.lnkCopyJsonReq.TabStop = true;
            this.lnkCopyJsonReq.Text = "Copy";
            this.lnkCopyJsonReq.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkCopyJsonReq_LinkClicked);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lnkCopyXmlReq);
            this.tabPage2.Controls.Add(this.rchTxtXmlRequest);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(360, 387);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Xml";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lnkCopyXmlReq
            // 
            this.lnkCopyXmlReq.AutoSize = true;
            this.lnkCopyXmlReq.Location = new System.Drawing.Point(312, 1);
            this.lnkCopyXmlReq.Name = "lnkCopyXmlReq";
            this.lnkCopyXmlReq.Size = new System.Drawing.Size(45, 20);
            this.lnkCopyXmlReq.TabIndex = 9;
            this.lnkCopyXmlReq.TabStop = true;
            this.lnkCopyXmlReq.Text = "Copy";
            this.lnkCopyXmlReq.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkCopyXmlReq_LinkClicked);
            // 
            // rchTxtXmlRequest
            // 
            this.rchTxtXmlRequest.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.rchTxtXmlRequest.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rchTxtXmlRequest.Font = new System.Drawing.Font("Book Antiqua", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rchTxtXmlRequest.ForeColor = System.Drawing.Color.Blue;
            this.rchTxtXmlRequest.Location = new System.Drawing.Point(6, 21);
            this.rchTxtXmlRequest.Name = "rchTxtXmlRequest";
            this.rchTxtXmlRequest.Size = new System.Drawing.Size(347, 366);
            this.rchTxtXmlRequest.TabIndex = 8;
            this.rchTxtXmlRequest.Text = "";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox3);
            this.tabPage3.Location = new System.Drawing.Point(4, 29);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(360, 387);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Properties";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dgServiceHeader);
            this.groupBox3.Controls.Add(this.cmbServiceMediaType);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox3.Location = new System.Drawing.Point(13, 14);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(344, 355);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Service Properties";
            // 
            // dgServiceHeader
            // 
            this.dgServiceHeader.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgServiceHeader.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.HeaderName,
            this.HeaderValue});
            this.dgServiceHeader.Location = new System.Drawing.Point(6, 91);
            this.dgServiceHeader.Name = "dgServiceHeader";
            this.dgServiceHeader.RowHeadersWidth = 51;
            this.dgServiceHeader.RowTemplate.Height = 24;
            this.dgServiceHeader.Size = new System.Drawing.Size(331, 141);
            this.dgServiceHeader.TabIndex = 12;
            // 
            // HeaderName
            // 
            this.HeaderName.HeaderText = "Name";
            this.HeaderName.MinimumWidth = 6;
            this.HeaderName.Name = "HeaderName";
            this.HeaderName.Width = 150;
            // 
            // HeaderValue
            // 
            this.HeaderValue.HeaderText = "Value";
            this.HeaderValue.MinimumWidth = 6;
            this.HeaderValue.Name = "HeaderValue";
            this.HeaderValue.Width = 200;
            // 
            // cmbServiceMediaType
            // 
            this.cmbServiceMediaType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbServiceMediaType.FormattingEnabled = true;
            this.cmbServiceMediaType.Location = new System.Drawing.Point(106, 34);
            this.cmbServiceMediaType.Name = "cmbServiceMediaType";
            this.cmbServiceMediaType.Size = new System.Drawing.Size(122, 26);
            this.cmbServiceMediaType.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "Headers";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 39);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 17);
            this.label5.TabIndex = 8;
            this.label5.Text = "Media Type";
            // 
            // btnSendServiceRequest
            // 
            this.btnSendServiceRequest.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSendServiceRequest.Location = new System.Drawing.Point(400, 280);
            this.btnSendServiceRequest.Name = "btnSendServiceRequest";
            this.btnSendServiceRequest.Size = new System.Drawing.Size(110, 30);
            this.btnSendServiceRequest.TabIndex = 11;
            this.btnSendServiceRequest.Text = "Send Request";
            this.btnSendServiceRequest.UseVisualStyleBackColor = true;
            this.btnSendServiceRequest.Click += new System.EventHandler(this.btnSendServiceRequest_Click);
            // 
            // cmbServiceMethod
            // 
            this.cmbServiceMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbServiceMethod.Font = new System.Drawing.Font("Book Antiqua", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbServiceMethod.FormattingEnabled = true;
            this.cmbServiceMethod.Location = new System.Drawing.Point(837, 49);
            this.cmbServiceMethod.Name = "cmbServiceMethod";
            this.cmbServiceMethod.Size = new System.Drawing.Size(56, 28);
            this.cmbServiceMethod.TabIndex = 11;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabResponseJson);
            this.tabControl1.Controls.Add(this.tabXml);
            this.tabControl1.Controls.Add(this.tabHeaders);
            this.tabControl1.Font = new System.Drawing.Font("Book Antiqua", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(531, 109);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(366, 424);
            this.tabControl1.TabIndex = 8;
            // 
            // tabResponseJson
            // 
            this.tabResponseJson.Controls.Add(this.rchTxtServiceResponse);
            this.tabResponseJson.Location = new System.Drawing.Point(4, 29);
            this.tabResponseJson.Name = "tabResponseJson";
            this.tabResponseJson.Padding = new System.Windows.Forms.Padding(3);
            this.tabResponseJson.Size = new System.Drawing.Size(358, 391);
            this.tabResponseJson.TabIndex = 0;
            this.tabResponseJson.Text = "Json";
            this.tabResponseJson.UseVisualStyleBackColor = true;
            // 
            // rchTxtServiceResponse
            // 
            this.rchTxtServiceResponse.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.rchTxtServiceResponse.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rchTxtServiceResponse.Font = new System.Drawing.Font("Book Antiqua", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rchTxtServiceResponse.ForeColor = System.Drawing.Color.Blue;
            this.rchTxtServiceResponse.Location = new System.Drawing.Point(6, 7);
            this.rchTxtServiceResponse.Name = "rchTxtServiceResponse";
            this.rchTxtServiceResponse.Size = new System.Drawing.Size(349, 380);
            this.rchTxtServiceResponse.TabIndex = 7;
            this.rchTxtServiceResponse.Text = "";
            // 
            // tabXml
            // 
            this.tabXml.Controls.Add(this.rchTxtServiceResponseXml);
            this.tabXml.Location = new System.Drawing.Point(4, 29);
            this.tabXml.Name = "tabXml";
            this.tabXml.Padding = new System.Windows.Forms.Padding(3);
            this.tabXml.Size = new System.Drawing.Size(358, 391);
            this.tabXml.TabIndex = 1;
            this.tabXml.Text = "Xml";
            this.tabXml.UseVisualStyleBackColor = true;
            // 
            // rchTxtServiceResponseXml
            // 
            this.rchTxtServiceResponseXml.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.rchTxtServiceResponseXml.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rchTxtServiceResponseXml.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rchTxtServiceResponseXml.ForeColor = System.Drawing.Color.Blue;
            this.rchTxtServiceResponseXml.Location = new System.Drawing.Point(8, 6);
            this.rchTxtServiceResponseXml.Name = "rchTxtServiceResponseXml";
            this.rchTxtServiceResponseXml.Size = new System.Drawing.Size(344, 372);
            this.rchTxtServiceResponseXml.TabIndex = 8;
            this.rchTxtServiceResponseXml.Text = "";
            // 
            // tabHeaders
            // 
            this.tabHeaders.Location = new System.Drawing.Point(4, 29);
            this.tabHeaders.Name = "tabHeaders";
            this.tabHeaders.Size = new System.Drawing.Size(358, 391);
            this.tabHeaders.TabIndex = 2;
            this.tabHeaders.Text = "Headers";
            this.tabHeaders.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Book Antiqua", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label7.Location = new System.Drawing.Point(8, 89);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(107, 18);
            this.label7.TabIndex = 8;
            this.label7.Text = "Service Request";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Book Antiqua", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label6.Location = new System.Drawing.Point(528, 89);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(116, 18);
            this.label6.TabIndex = 8;
            this.label6.Text = "Service Response";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Book Antiqua", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(8, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 18);
            this.label1.TabIndex = 8;
            this.label1.Text = "Service Endpoint";
            // 
            // txtServiceEndpoint
            // 
            this.txtServiceEndpoint.Font = new System.Drawing.Font("Book Antiqua", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtServiceEndpoint.Location = new System.Drawing.Point(11, 49);
            this.txtServiceEndpoint.Name = "txtServiceEndpoint";
            this.txtServiceEndpoint.Size = new System.Drawing.Size(809, 26);
            this.txtServiceEndpoint.TabIndex = 9;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.groupBox2.Controls.Add(this.tv);
            this.groupBox2.Controls.Add(this.cmbBoxResource);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Font = new System.Drawing.Font("Book Antiqua", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox2.Location = new System.Drawing.Point(5, 40);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(307, 539);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Resource";
            // 
            // tabControl3
            // 
            this.tabControl3.Controls.Add(this.tabPage4);
            this.tabControl3.Controls.Add(this.tabPage5);
            this.tabControl3.Location = new System.Drawing.Point(144, 595);
            this.tabControl3.Name = "tabControl3";
            this.tabControl3.SelectedIndex = 0;
            this.tabControl3.Size = new System.Drawing.Size(81, 55);
            this.tabControl3.TabIndex = 9;
            this.tabControl3.Visible = false;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(73, 26);
            this.tabPage4.TabIndex = 0;
            this.tabPage4.Text = "Elements";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tv
            // 
            this.tv.Font = new System.Drawing.Font("Book Antiqua", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tv.Location = new System.Drawing.Point(6, 83);
            this.tv.Name = "tv";
            this.tv.Size = new System.Drawing.Size(295, 450);
            this.tv.TabIndex = 1;
            this.tv.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tv_AfterSelect);
            this.tv.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tv_NodeMouseClick);
            this.tv.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tv_NodeMouseDoubleClick);
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.dgParams);
            this.tabPage5.Location = new System.Drawing.Point(4, 26);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(290, 420);
            this.tabPage5.TabIndex = 1;
            this.tabPage5.Text = "Search Param";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // dgParams
            // 
            this.dgParams.AllowUserToAddRows = false;
            this.dgParams.AllowUserToDeleteRows = false;
            this.dgParams.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgParams.Location = new System.Drawing.Point(3, 6);
            this.dgParams.MultiSelect = false;
            this.dgParams.Name = "dgParams";
            this.dgParams.ReadOnly = true;
            this.dgParams.RowHeadersWidth = 51;
            this.dgParams.RowTemplate.Height = 24;
            this.dgParams.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgParams.ShowEditingIcon = false;
            this.dgParams.Size = new System.Drawing.Size(281, 404);
            this.dgParams.TabIndex = 0;
            // 
            // cmbBoxResource
            // 
            this.cmbBoxResource.Font = new System.Drawing.Font("Book Antiqua", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBoxResource.FormattingEnabled = true;
            this.cmbBoxResource.Location = new System.Drawing.Point(6, 49);
            this.cmbBoxResource.Name = "cmbBoxResource";
            this.cmbBoxResource.Size = new System.Drawing.Size(295, 28);
            this.cmbBoxResource.TabIndex = 3;
            this.cmbBoxResource.SelectedIndexChanged += new System.EventHandler(this.cmbBoxResource_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(6, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 18);
            this.label2.TabIndex = 8;
            this.label2.Text = "Select Resource";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox5.Controls.Add(this.lblFormHeader);
            this.groupBox5.Controls.Add(this.btnClose);
            this.groupBox5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox5.Location = new System.Drawing.Point(-7, -11);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(1243, 45);
            this.groupBox5.TabIndex = 14;
            this.groupBox5.TabStop = false;
            // 
            // lblFormHeader
            // 
            this.lblFormHeader.AutoSize = true;
            this.lblFormHeader.Font = new System.Drawing.Font("Book Antiqua", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormHeader.Location = new System.Drawing.Point(537, 20);
            this.lblFormHeader.Name = "lblFormHeader";
            this.lblFormHeader.Size = new System.Drawing.Size(59, 22);
            this.lblFormHeader.TabIndex = 1;
            this.lblFormHeader.Text = "label6";
            // 
            // btnClose
            // 
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(1184, 15);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(38, 26);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "X";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // Form1_New
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1230, 675);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.tabControl3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "Form1_New";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgServiceHeader)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabResponseJson.ResumeLayout(false);
            this.tabXml.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabControl3.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgParams)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnExportXml;
        private System.Windows.Forms.RichTextBox rchTxtJson;
        private System.Windows.Forms.Button btnLoadResource;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbServiceMethod;
        private System.Windows.Forms.TextBox txtServiceEndpoint;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabResponseJson;
        private System.Windows.Forms.RichTextBox rchTxtServiceResponse;
        private System.Windows.Forms.TabPage tabXml;
        private System.Windows.Forms.Button btnSendServiceRequest;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dgServiceHeader;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbServiceMediaType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label lblFormHeader;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridViewTextBoxColumn HeaderName;
        private System.Windows.Forms.DataGridViewTextBoxColumn HeaderValue;
        private System.Windows.Forms.TabPage tabHeaders;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RichTextBox rchTxtServiceResponseXml;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.RichTextBox rchTxtXmlRequest;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.LinkLabel lnkCopyJsonReq;
        private System.Windows.Forms.LinkLabel lnkCopyXmlReq;
        private System.Windows.Forms.LinkLabel lnkParseJsonReq;
        private System.Windows.Forms.RichTextBox rt;
        private System.Windows.Forms.TreeView tv;
        private System.Windows.Forms.ComboBox cmbBoxResource;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabControl tabControl3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.DataGridView dgParams;
    }
}

