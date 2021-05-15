using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using TreeView;
using TreeView.Class;

namespace TreeView
{
    public partial class Form1_New : Form
    {
        ContextMenuStrip nodeMenu;
        //XDocument xDocSingleFhirSchema;
        XNamespace ns = "http://www.w3.org/2001/XMLSchema";
        string profileName = "MedicationRequest";
        Font nodeFont;
       //  string Version;
        Global objGlobal = new Global();
        public Form1_New()
        {
            InitializeComponent();
        }
        public Form1_New(string _version)
        {
            objGlobal.Version = _version;
            //this.Text = Version;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //CreateResourceTree(cmbBoxResource.Text);
        }
        private void Form1_Load(object sender, EventArgs e)
        {           
            SetFormText();

            LoadSchema(objGlobal.Version);
            LoadSerivceMethod();
            LoadSerivceMediaType();
            nodeMenu = new ContextMenuStrip();
            ToolStripMenuItem editLabel = new ToolStripMenuItem();
            editLabel.Text = "EditValue";
            nodeMenu.Items.Add(editLabel);
            editLabel.Click += new EventHandler(ContextMenuEditItemClick);

            ToolStripMenuItem openLabel = new ToolStripMenuItem();
            openLabel.Text = "Property";
            nodeMenu.Items.Add(openLabel);
            openLabel.Click += new EventHandler(ContextMenuPropertyItemClick);

            nodeFont = new Font(FontFamily.GenericSansSerif, 8, FontStyle.Bold);
            string str = ReadResource("TreeView.fhir_single_V3_0_1");           
        }

        private void LoadSchema(string version)
        {
            var assembly = Assembly.GetExecutingAssembly();
            cmbBoxResource.Items.Clear();
            try
            {
                switch (version)
                {
                    case "3.0.1":
                        {
                            string fileName = "TreeView.Schema.fhir-single_V3_0_1.xsd";
                            //xDocSingleFhirSchema = XDocument.Load(@"C:\MyData\Projects\TreeView\TreeView\fhir-v3_0_1\fhir-single.xsd");
                            objGlobal.MainSchemaDoc = XDocument.Load(assembly.GetManifestResourceStream(fileName));
                            XElement ele =
                           objGlobal.MainSchemaDoc.Descendants(ns + "complexType")
                           .Where(x => x.Attribute("name").Value.Equals("ResourceContainer")    //x.Name.LocalName.Equals("Activity") &&
                           ).FirstOrDefault();
                            foreach (XElement el in ele.Descendants(ns + "element"))
                            {
                                cmbBoxResource.Items.Add(el.Attribute("ref").Value);
                            }
                            break;
                        }
                    case "4.0.1":
                        {
                            string fileName = "TreeView.Schema.fhir-single_V4_0_1.xsd";
                            //xDocSingleFhirSchema = XDocument.Load(@"C:\MyData\Projects\TreeView\TreeView\fhir-v4_0_1\fhir-single.xsd");
                            objGlobal.MainSchemaDoc = XDocument.Load(assembly.GetManifestResourceStream(fileName));
                            XElement ele =
                          objGlobal.MainSchemaDoc.Descendants(ns + "complexType")
                          .Where(x => x.Attribute("name").Value.Equals("ResourceContainer")    //x.Name.LocalName.Equals("Activity") &&
                          ).FirstOrDefault();
                            foreach (XElement el in ele.Descendants(ns + "element"))
                            {
                                cmbBoxResource.Items.Add(el.Attribute("name").Value);
                            }
                            break;
                        }
                }
            }
            catch(Exception ex)
            {

            }
        }

        public string ReadResource(string name)
        {
            // Determine path
            var assembly = Assembly.GetExecutingAssembly();
            string resourcePath = "TreeView.Schema.fhir-single_V3_0_1.xsd"; // name;
            using (Stream stream = assembly.GetManifestResourceStream(resourcePath))
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        private void CreateResourceTree(string rootNode)
        {            
            profileName = rootNode;
            tv.Nodes.Clear();
            tv.Nodes.Add(profileName, profileName);
            NodeContext nodeContext = new NodeContext();
            tv.Nodes[profileName].Tag = nodeContext;
            tv.Nodes[profileName].ContextMenuStrip = nodeMenu;
            //tv.Nodes[profileName].ContextMenu = menu;
            //XElement ele = GetBranchChild("Resource");
            //AddTreeChildNode(tv.Nodes[profileName], ele);
            //XElement ele1 = GetBranchChild("DomainResource");
            //AddTreeChildNode(tv.Nodes[profileName], ele1);
            XElement ele2 = GetBranchChild(profileName);
            AddTreeChildNode(tv.Nodes[profileName], ele2);
        }
        private void ContextMenuPropertyItemClick(object sender, EventArgs e)
        {           
            TreeNode node = tv.SelectedNode;
            NodeProperty nodeProperty = new NodeProperty((NodeContext)node.Tag);
            nodeProperty.StartPosition = FormStartPosition.CenterParent;
            nodeProperty.ShowDialog();
        }
        private void ContextMenuEditItemClick(object sender, EventArgs e)
        {
            OpenNodeValueEditor();
        }
        private void tv_AfterSelect(object sender, TreeViewEventArgs e)
        {            
            string attrUse = string.Empty;
            string attrType = string.Empty;          
            string nodeName = e.Node.Text;
            
            if (nodeName =="id" || nodeName =="value" || nodeName== profileName || nodeName == "url")
            {
                return;
            }
            NodeContext nodeContext =(NodeContext) e.Node.Tag;   
           
            if (e.Node.FirstNode ==null && !nodeContext.IsLastNode)
            {
                XElement branchTypeElem = GetBranchChild(nodeContext.TypeName);
                nodeContext.NodeTypeDescription = branchTypeElem.Value;
                if (branchTypeElem.Descendants(ns + "element").Count() == 0)
                {                   
                    nodeContext.IsLastNode = true;
                    GetNodeValueOptionList(nodeContext);
                }
                else
                {
                    //AddTreeChildNode(e.Node, branchTypeElem);
                    nodeContext.IsLastNode = false;
                }
                AddTreeChildNode(e.Node, branchTypeElem);
            }         
        }

        private void GetNodeValueOptionList(NodeContext nodeContext)
        {






            XElement branchTypeElem =    //ns + "element"
              objGlobal.MainSchemaDoc.Descendants(ns + "simpleType")
              .Where(x => x.Attribute("name") !=null && x.Attribute("name").Value.Equals(nodeContext.TypeName + "-list")    //x.Name.LocalName.Equals("Activity") &&
              ).FirstOrDefault();
            if (branchTypeElem == null)
            {
                return;
            }
            foreach(XElement el in branchTypeElem.Descendants(ns + "enumeration"))
            {
                nodeContext.ValueOptions.Add(el.Attribute("value").Value);
            }
        }
        private void AddTreeChildNode(TreeNode parentBranch, XElement branchTypeElem)
        {
            
            if (branchTypeElem.Descendants(ns + "extension").Count()>0)
            {
                string extnBase = branchTypeElem.Descendants(ns + "extension").First().Attribute("base").Value;

                

                if (branchTypeElem.Attribute("name") !=null && branchTypeElem.Attribute("name").Value == "Extension")
                {
                    NodeContext nodeContextForId = new NodeContext();
                    nodeContextForId.NodeId = Guid.NewGuid().ToString();
                    nodeContextForId.IsLastNode = true;
                    parentBranch.Nodes.Add("id", "id");
                    parentBranch.Nodes["id"].Tag = nodeContextForId;

                    NodeContext nodeContextForUrl = new NodeContext();
                    nodeContextForUrl.NodeId = Guid.NewGuid().ToString();
                    nodeContextForUrl.IsLastNode = true;
                    parentBranch.Nodes.Add("url","url");
                    parentBranch.Nodes["url"].Tag = nodeContextForUrl;
                }
                /*
                else if (extnBase == "Element")
                {
                    NodeContext nodeContextForId = new NodeContext();
                    nodeContextForId.NodeId = Guid.NewGuid().ToString();
                    nodeContextForId.IsLastNode = true;
                    parentBranch.Nodes.Add("id", "id");
                    parentBranch.Nodes["id"].Tag = nodeContextForId;

                    NodeContext nodeContextForValue = new NodeContext();
                    nodeContextForValue.NodeId = Guid.NewGuid().ToString();
                    nodeContextForValue.IsLastNode = true;

                    parentBranch.Nodes.Add("value","value");
                    parentBranch.Nodes["value"].Tag = nodeContextForValue;
                }
                */
                XElement extnElem = GetBranchChild(extnBase);
                AddTreeChildNode(parentBranch, extnElem);
            }
            foreach (XElement element in branchTypeElem.Descendants(ns + "element"))
            {
                string nodeKey = string.Empty;

                if (element.Attribute("name") != null)
                {
                    nodeKey = element.Attribute("name").Value;

                }
                //else if (element.Attribute("ref") != null)
                //{
                //    nodeKey = element.Attribute("ref").Value;
                //
                //}
                //else
                //{
                //    continue;
                //}
                   
                if (CyclicReferenceExits(parentBranch, nodeKey) || string.IsNullOrEmpty(nodeKey))
                {
                    continue;
                }                
                parentBranch.Nodes.Add(nodeKey, nodeKey);
                NodeContext nodeContext = new NodeContext();
                nodeContext.NodeId = Guid.NewGuid().ToString();
                nodeContext.MinOccur = "1";
                nodeContext.MaxOccur = "1";
                nodeContext.TypeName = element.Attribute("type").Value;
                if (element.Attribute("minOccurs") != null)
                {
                    nodeContext.MinOccur = element.Attribute("minOccurs").Value;
                }
                if (element.Attribute("maxOccurs") != null)
                {
                    nodeContext.MaxOccur = element.Attribute("maxOccurs").Value;
                }
                
                nodeContext.NodeDescription = element.Value;
                //nodeContext.NodeTypeDescription = element.Value;
                parentBranch.Nodes[nodeKey].Tag = nodeContext;
                parentBranch.Nodes[nodeKey].ContextMenuStrip = nodeMenu;               
            }
        }
        private XElement GetBranchChild(string branchType)
        {
            XElement branchTypeElem =    //ns + "element"
                objGlobal.MainSchemaDoc.Descendants(ns + "complexType")
                .Where(x => x.Attribute("name").Value.Equals(branchType)    //x.Name.LocalName.Equals("Activity") &&
                ).FirstOrDefault();
            return branchTypeElem;
        }

        private XElement GetBranchExtn(string branchType)
        {
            XElement branchTypeElem =    //ns + "element"
                objGlobal.MainSchemaDoc.Descendants(ns + "extension")
                .Where(x => x.Attribute("name").Value.Equals(branchType)    //x.Name.LocalName.Equals("Activity") &&
                ).FirstOrDefault();
            return branchTypeElem;
        }



        public void txtValue_KeyUp(object sender, KeyEventArgs e)
        {
            //MessageBox.Show(txtValue.Text);           
            NodeContext nodeContext = (NodeContext) tv.SelectedNode.Tag;
            //nodeContext.ValueAttr = txtValue.Text;
            //nodeContext.Id = txtId.Text;
            if (string.IsNullOrEmpty(nodeContext.ValueAttr) && string.IsNullOrEmpty( nodeContext.IdAttr))
            {
                tv.SelectedNode.NodeFont = null;
                RemoveChildIdFromParentNode(tv.SelectedNode, nodeContext.NodeId);
                HighlightParentNode(tv.SelectedNode, nodeContext.NodeId);
            }
            else
            {
                AddChildIdToParentNode(tv.SelectedNode, nodeContext.NodeId);
                tv.SelectedNode.NodeFont = nodeFont;
                HighlightParentNode(tv.SelectedNode,nodeContext.NodeId);
            }

           // ExportToXML(tv);
            WriteJson();
        }

        private void txtId_KeyUp(object sender, KeyEventArgs e)
        {
           /*
            NodeContext nodeContext = (NodeContext)tv.SelectedNode.Tag;
            //nodeContext.Value = txtValue.Text;
            nodeContext.IdAttr = txtId.Text;

            if (string.IsNullOrEmpty(nodeContext.ValueAttr) && string.IsNullOrEmpty(nodeContext.IdAttr))
            {
                tv.SelectedNode.NodeFont = null;
            }
            else
            {
                tv.SelectedNode.NodeFont = nodeFont;
                HighlightParentNode(tv.SelectedNode);
            }
            */
        }

        private void AddChildIdToParentNode(TreeNode treeNode, string nodeId)
        {
            if (treeNode.Parent != null)
            {
                NodeContext nodeContext = (NodeContext)treeNode.Parent.Tag;
                if (!nodeContext.ChildNodeIdList.Contains(nodeId))
                {
                    nodeContext.ChildNodeIdList.Add(nodeId);
                }
                AddChildIdToParentNode(treeNode.Parent, nodeId);
            }
        }

        private void RemoveChildIdFromParentNode(TreeNode treeNode, string nodeId)
        {
            if (treeNode.Parent != null)
            {
                NodeContext nodeContext = (NodeContext)treeNode.Parent.Tag;
                nodeContext.ChildNodeIdList.Remove(nodeId);
                RemoveChildIdFromParentNode(treeNode.Parent, nodeId);
            }
        }

        private void HighlightParentNode(TreeNode treeNode, string nodeId)
        {
            if(treeNode.Parent!=null)
            {
                NodeContext nodeContext = (NodeContext)treeNode.Parent.Tag;
                if (nodeContext.ChildNodeIdList.Count() > 0)
                {
                    nodeContext.IsLastNode = false;
                    treeNode.Parent.NodeFont = nodeFont;
                    btnExportXml.Enabled = true;
                }
                else
                {
                    nodeContext.IsLastNode = true;
                    treeNode.Parent.NodeFont = null ;
                    btnExportXml.Enabled = false;
                }
                HighlightParentNode(treeNode.Parent, nodeId);               
            }           
        }

        private bool CyclicReferenceExits(TreeNode treeNode,string newNode)
        {
            if (treeNode.Parent != null)
            {
                if(treeNode.Parent.Name == newNode)
                {
                    return true;
                }
                else
                {
                    CyclicReferenceExits(treeNode.Parent, newNode);
                }               
                // treeNode.Parent.Nodes
            }
            return false;
        }

        /*
        public void ExportToXML(System.Windows.Forms.TreeView tree, string filename)
        {
            XmlTextWriter xmlTextWriter = new XmlTextWriter(filename, System.Text.Encoding.UTF8);
            xmlTextWriter.WriteStartDocument();
            xmlTextWriter.WriteStartElement(tree.Nodes[0].Text);
            foreach (TreeNode node in tree.Nodes[0].Nodes)
            {
                
                if (node.Nodes.Count > 0)
                {
                    xmlTextWriter.WriteStartElement(node.Text);
                    SaveToXML(node.Nodes, xmlTextWriter);
                    xmlTextWriter.WriteEndElement();
                }
                else
                {
                    NodeContext nodeContext = (NodeContext)node.Tag;
                    if (nodeContext.IsLastNode)
                    {
                        xmlTextWriter.WriteStartElement(node.Text);
                        xmlTextWriter.WriteAttributeString("value", nodeContext.ValueAttr);
                        xmlTextWriter.WriteEndElement();
                    }
                }
              
            }
          //  xmlTextWriter.WriteEndElement();
            xmlTextWriter.Close();
        }
        */
        public void ExportToXML(System.Windows.Forms.TreeView tree,string fileName)
        {
            //string fileName = @"C:\Backup t450\MyData\Projects\test.xml";
            // MemoryStream memory_stream = new MemoryStream();
            //XmlTextWriter xmlTextWriter = new XmlTextWriter(memory_stream, System.Text.Encoding.UTF8);
            XmlTextWriter xmlTextWriter = new XmlTextWriter(fileName, System.Text.Encoding.UTF8);
            xmlTextWriter.Formatting = System.Xml.Formatting.Indented;
            xmlTextWriter.Indentation = 4;

            xmlTextWriter.WriteStartDocument(true);
            xmlTextWriter.WriteStartElement("FhirResourceProfile");
            xmlTextWriter.WriteAttributeString("version", objGlobal.Version);
            xmlTextWriter.WriteAttributeString("name", tree.Nodes[0].Text);
            xmlTextWriter.WriteStartElement(tree.Nodes[0].Text);
            //SaveToXML(tree.Nodes[0], xmlTextWriter);
            SaveToFullXML(tree.Nodes[0], xmlTextWriter);
            //xmlTextWriter.WriteAttributeString("min", nodeContext.ValueAttr);
            /*
            foreach (TreeNode node in tree.Nodes[0].Nodes)
            {
                NodeContext nodeContext = (NodeContext)node.Tag;
                if (node.Nodes.Count > 0 && nodeContext.ChildNodeIdList.Count()>0)
                {
                    xmlTextWriter.WriteStartElement(node.Text);
                    SaveToXML(node.Nodes, xmlTextWriter);
                    xmlTextWriter.WriteEndElement();
                }
                else
                {                    
                    if (nodeContext.IsLastNode && !string.IsNullOrEmpty(nodeContext.ValueAttr))
                    {
                        xmlTextWriter.WriteStartElement(node.Text);
                        xmlTextWriter.WriteAttributeString("value", nodeContext.ValueAttr);
                        xmlTextWriter.WriteEndElement();
                    }
                }

            }
            */
            xmlTextWriter.WriteEndElement();
            xmlTextWriter.WriteEndElement();
            xmlTextWriter.Flush();
           // StreamReader stream_reader = new StreamReader(memory_stream);

           // memory_stream.Seek(0, SeekOrigin.Begin);
          //  rchTxtBox.Text = stream_reader.ReadToEnd();
         //   rchTxtBox.Select(0, 0);
            xmlTextWriter.Close();
            
        }

        private void SaveToXML(TreeNode parentNode, XmlTextWriter xmlTextWriter)
        {
            IEnumerable<TreeNode> childNodes = parentNode.Nodes.Cast<TreeNode>().Where(c => !string.IsNullOrEmpty(((NodeContext)c.Tag).ValueAttr) || ((NodeContext)c.Tag).ChildNodeIdList.Count > 0);

            foreach (TreeNode childNode in childNodes)
            {
                NodeContext nodeContext = (NodeContext)childNode.Tag;
                if (childNode.Nodes.Count > 0 && nodeContext.ChildNodeIdList.Count() > 0)
                {
                    xmlTextWriter.WriteStartElement(childNode.Text);
                    SaveToXML(childNode, xmlTextWriter);
                    xmlTextWriter.WriteEndElement();
                }
                else
                {                    
                    if (nodeContext.IsLastNode && !string.IsNullOrEmpty(nodeContext.ValueAttr))
                    {
                        xmlTextWriter.WriteStartElement(childNode.Text);
                        xmlTextWriter.WriteAttributeString("value", nodeContext.ValueAttr);
                        xmlTextWriter.WriteEndElement();
                    }                  
                }
            }
        }
        private void SaveToFullXML(TreeNode parentNode, XmlTextWriter xmlTextWriter)
        {
            //IEnumerable<TreeNode> childNodes = parentNode.Nodes.Cast<TreeNode>().Where(c => !string.IsNullOrEmpty(((NodeContext)c.Tag).ValueAttr) || ((NodeContext)c.Tag).ChildNodeIdList.Count > 0);

            foreach (TreeNode childNode in parentNode.Nodes)
            {
                NodeContext nodeContext = (NodeContext)childNode.Tag;
                xmlTextWriter.WriteStartElement(childNode.Text);
                xmlTextWriter.WriteAttributeString("value_attr", nodeContext.ValueAttr);
                xmlTextWriter.WriteAttributeString("id_attr", nodeContext.IdAttr);
                xmlTextWriter.WriteAttributeString("minOccurs", nodeContext.MinOccur);
                xmlTextWriter.WriteAttributeString("maxOccurs", nodeContext.MaxOccur);
                xmlTextWriter.WriteAttributeString("type", nodeContext.TypeName);
                xmlTextWriter.WriteAttributeString("type_des", nodeContext.NodeTypeDescription);
                xmlTextWriter.WriteAttributeString("des", nodeContext.NodeDescription);
                xmlTextWriter.WriteAttributeString("node_id", nodeContext.NodeId);
                if (childNode.Nodes.Count > 0 && nodeContext.ChildNodeIdList.Count() > 0)
                {
                   
                    SaveToFullXML(childNode, xmlTextWriter);
                   // xmlTextWriter.WriteEndElement();
                }
               // else
               // {
                    //if (nodeContext.IsLastNode)
                    //{
                       // xmlTextWriter.WriteStartElement(childNode.Text);
                     //   xmlTextWriter.WriteAttributeString("value", nodeContext.ValueAttr);
                      //  xmlTextWriter.WriteEndElement();
                    //}
               // }
                xmlTextWriter.WriteEndElement();
            }
        }

        private void btnExportXml_Click(object sender, EventArgs e)
        {
           // if (tv.HasChildren && ((NodeContext)tv.Nodes[0].Tag).ChildNodeIdList.Count() > 0)
          //  {
                saveFileDialog1.Filter = "XML-File | *.xml";
                DialogResult res = saveFileDialog1.ShowDialog();
                if (res == DialogResult.Cancel)
                {
                    return;
                }
                string fileName = saveFileDialog1.FileName;
                fileName = fileName.Remove(fileName.LastIndexOf('.'));
                ExportToXML(tv, fileName + ".xml");
            //}
            //else
            //{
            //    MessageBox.Show("Data not ", "NoData");
            //}
        }
        private void WriteJson()
        {
            MemoryStream memory_stream = new MemoryStream();
            TextWriter tw = new StreamWriter(memory_stream);
           
            tw.WriteLine("{");
            tw.WriteLine("\"ResourceType\": \"" + tv.Nodes[0].Text + "\",");
            WriteJson(tw, tv.Nodes[0]);           
            tw.WriteLine("}");
            tw.Flush();
            StreamReader stream_reader = new StreamReader(memory_stream);

            memory_stream.Seek(0, SeekOrigin.Begin);
            JToken jToken = JToken.Parse(stream_reader.ReadToEnd());
            rchTxtJson.Text = jToken.ToString();//stream_reader.ReadToEnd();
            tw.Close();
        }
        private void WriteJson(TextWriter tw, TreeNode parentNode)
        {
            //foreach (XmlNode node1 in node.ChildNodes)
            IEnumerable<TreeNode> childNodes = parentNode.Nodes.Cast<TreeNode>().Where(c => !string.IsNullOrEmpty(((NodeContext)c.Tag).ValueAttr) || ((NodeContext)c.Tag).ChildNodeIdList.Count > 0);
            int counter = 0;
            foreach (TreeNode node in childNodes)
            {
                counter = counter + 1;
                NodeContext nodeContext = (NodeContext)node.Tag;
                if (!nodeContext.IsLastNode && nodeContext.ChildNodeIdList.Count > 0)
                {
                    if (nodeContext.MaxOccur == "unbounded")
                    {
                        tw.WriteLine("\"" + node.Text + "\": [");
                        tw.WriteLine("{");
                    }
                    else
                    {
                        tw.WriteLine("\"" + node.Text + "\": {");
                    }
                    WriteJson(tw, node);                   
                    if (nodeContext.MaxOccur == "unbounded")
                    {
                        if (counter == childNodes.Count())
                        {
                            tw.WriteLine("}");
                            tw.WriteLine("]");
                        }
                        else
                        {
                            tw.WriteLine("}");
                            tw.WriteLine("],");
                        }
                    }
                    else
                    {
                        if (counter == childNodes.Count())
                        {
                            tw.WriteLine("}");                         
                        }
                        else
                        {
                            tw.WriteLine("},");
                        }
                    }
                }
                else //if(!string.IsNullOrEmpty( nodeContext.ValueAttr))
                {
                    if (counter == childNodes.Count())
                    {
                        tw.WriteLine("\"" + node.Text + "\": \"" + nodeContext.ValueAttr + "\"");
                    }
                    else
                    {
                        tw.WriteLine("\"" + node.Text + "\": \"" + nodeContext.ValueAttr + "\",");
                    }
                }
            }
        }

        private void SetFormText()
        {
            this.Text = "FHIR Version-" +objGlobal.Version;
            lblFormHeader.Text= "FHIR Version-" + objGlobal.Version;
        }
        private void btnLoadResource_Click(object sender, EventArgs e)
        {
            try
            {
                XDocument xDoc=null;
                //XmlDocument xmlDoc = new XmlDocument();
                openFileDialog1.Filter = "XML-File | *.xml";
                DialogResult res= openFileDialog1.ShowDialog();
                if(res== DialogResult.Cancel)
                {
                    return;
                }
                string fileName = openFileDialog1.FileName;
                try
                {
                    xDoc = XDocument.Load(fileName);
                    if (xDoc.Root.Name != "FhirResourceProfile")
                    {
                        MessageBox.Show("Invalid resource profile", "Invalid");
                        return;
                    }
                }
               catch(Exception ex)
                {
                    MessageBox.Show("Invalid resource file." + ex.Message, "Invalid");
                }
                objGlobal.Version = xDoc.Root.Attribute("version").Value;
                profileName = xDoc.Root.Attribute("name").Value;
                LoadSchema(objGlobal.Version);
                cmbBoxResource.Text = profileName;                
                SetFormText();               
                tv.Nodes.Clear();
                tv.Nodes.Add(profileName, profileName);
                NodeContext nodeContext = new NodeContext();
                tv.Nodes[profileName].Tag = nodeContext;
                XElement ele = xDoc.Root.Element(profileName);
                //AddTreeChildNode(tv.Nodes[profileName], ele);
                LoadTreeChildNodeFromXml(tv.Nodes[profileName], ele);
                WriteJson();
               // cmbBoxResource.Text = string.Empty;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error loading profile." + ex.Message);
            }
        }

        private void LoadTreeChildNodeFromXml(TreeNode parentBranch, XElement parentElement)
        {
            try
            {
                foreach (XElement element in parentElement.Elements())
                {
                    string nodeKey = element.Name.ToString();
                    //string nodeKey = element.Name.ToString();
                    //if (CyclicReferenceExits(parentBranch, nodeKey))
                    //{
                    //    continue;
                    //}
                    parentBranch.Nodes.Add(nodeKey, nodeKey);
                    NodeContext nodeContext = new NodeContext();
                    nodeContext.NodeId = Guid.NewGuid().ToString();
                    nodeContext.MinOccur = "1";
                    nodeContext.MaxOccur = "1";
                    nodeContext.TypeName = element.Attribute("type").Value;
                    // if (element.Attribute("minOccurs") != null)
                    {
                        nodeContext.MinOccur = element.Attribute("minOccurs").Value;
                    }
                    //if (element.Attribute("maxOccurs") != null)
                    {
                        nodeContext.MaxOccur = element.Attribute("maxOccurs").Value;
                    }

                    nodeContext.NodeDescription = element.Attribute("des").Value;
                    nodeContext.ValueAttr = element.Attribute("value_attr").Value;
                    if (!string.IsNullOrEmpty(element.Attribute("value_attr").Value))
                    {
                        parentBranch.Nodes[nodeKey].NodeFont = nodeFont;
                        AddChildIdToParentNode(parentBranch.Nodes[nodeKey], nodeContext.NodeId);
                        HighlightParentNode(parentBranch.Nodes[nodeKey], nodeContext.NodeId);
                    }
                    //nodeContext.NodeTypeDescription = element.Value;
                    parentBranch.Nodes[nodeKey].Tag = nodeContext;
                    if (element.HasElements)
                    {
                        LoadTreeChildNodeFromXml(parentBranch.Nodes[nodeKey], element);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error loading profile." + ex.Message);
            }
        }

        private void cmbBoxResource_SelectedIndexChanged(object sender, EventArgs e)
        {
            // MessageBox.Show(cmbBoxResource.Text);
            CreateResourceTree(cmbBoxResource.Text);
            rchTxtJson.Text = string.Empty;
           // rchTxtBox.Text = string.Empty;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //XNode node = JsonConvert.DeserializeXNode(rchTxtJson.Text, "Root");
            //rchtxtXml.Text = node.ToString();
        }

        private void tv_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            tv.SelectedNode = e.Node;
        }

        private void tv_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            OpenNodeValueEditor();
        }

        private void OpenNodeValueEditor()
        {
            TreeNode node = tv.SelectedNode;
            NodeContext context = (NodeContext)node.Tag;
            if (context.IsLastNode)
            {
                NodeValueEditor nodeEdit = new NodeValueEditor(context,this);
                nodeEdit.StartPosition = FormStartPosition.CenterParent;
                nodeEdit.ShowDialog();
            }
            else
            {
                NodeProperty nodeProp = new NodeProperty(context);
                nodeProp.StartPosition = FormStartPosition.CenterParent;
                nodeProp.ShowDialog();
            }
        }

        private void LoadSerivceMethod()
        {
            cmbServiceMethod.Items.Add("POST");
            cmbServiceMethod.Items.Add("GET");
            cmbServiceMethod.Items.Add("PUT");
            cmbServiceMethod.Items.Add("DELETE");
            cmbServiceMethod.SelectedIndex = 0;
        }
        private void LoadSerivceMediaType()
        {
            cmbServiceMediaType.Items.Add("application/json");
            cmbServiceMediaType.Items.Add("application/xml");
            cmbServiceMediaType.Items.Add("text/xml");
            cmbServiceMediaType.Items.Add("text/json");
            cmbServiceMediaType.SelectedIndex = 0;
        }

        private void CreateServiceRequest()
        {
            ServiceRequest serviceReq = new ServiceRequest();
            serviceReq.Endpoint = txtServiceEndpoint.Text;
            serviceReq.Method = cmbServiceMethod.Text;
            serviceReq.MediaType = cmbServiceMediaType.Text;
            serviceReq.RequestData = rchTxtJson.Text;

            foreach (DataGridViewRow row in dgServiceHeader.Rows)
            {
                if (row.Cells["HeaderName"].Value!=null && row.Cells["HeaderValue"].Value !=null)
                {
                    serviceReq.ServiceHeaders.Add(new ServiceHeader { 
                        Name = row.Cells["HeaderName"].Value.ToString(), 
                        Value = row.Cells["HeaderValue"].Value.ToString() }
                    );
                }
            }
            APIWebRequest apiReq = new APIWebRequest(serviceReq);
            string apiRes = apiReq.CallAPI();
            try
            {
                JToken jToken = JToken.Parse(apiRes);
                apiRes = jToken.ToString();
            }
            catch (Exception ex)
            {
                
            }
            rchTxtServiceResponse.Text = apiRes;

        }
        private void btnSendServiceRequest_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtServiceEndpoint.Text))
            {
                MessageBox.Show("Service endpoint is blank.", "Endpoint");
                return;
            }
            CreateServiceRequest();
        }     

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
