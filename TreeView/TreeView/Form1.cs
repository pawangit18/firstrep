using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using TreeView.Class;

namespace TreeView
{
    public partial class Form1 : Form
    {     
        XDocument xDocSingleFhirSchema;
        XNamespace ns = "http://www.w3.org/2001/XMLSchema";
        string profileName = "MedicationRequest";
        Font nodeFont;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //CreateResourceTree(cmbBoxResource.Text);
        }

        private void CreateResourceTree(string rootNode)
        {
            ContextMenuStrip docMenu = new ContextMenuStrip();
            ToolStripMenuItem openLabel = new ToolStripMenuItem();
            openLabel.Text = "Open";
            ToolStripMenuItem deleteLabel = new ToolStripMenuItem();
            deleteLabel.Text = "Delete";
            ToolStripMenuItem renameLabel = new ToolStripMenuItem();
            renameLabel.Text = "Rename";

            //Add the menu items to the menu.
            docMenu.Items.AddRange(new ToolStripMenuItem[]{openLabel,
        deleteLabel, renameLabel});

            profileName = rootNode; ;
            tv.Nodes.Clear();
            tv.Nodes.Add(profileName, profileName);
            NodeContext nodeContext = new NodeContext();
            tv.Nodes[profileName].Tag = nodeContext;
            tv.Nodes[profileName].ContextMenuStrip = docMenu;
            XElement ele = GetBranchChild(profileName);
            AddTreeChildNode(tv.Nodes[profileName], ele);
        }

        private void tv_AfterSelect(object sender, TreeViewEventArgs e)
        {            
            string attrUse = string.Empty;
            string attrType = string.Empty;
            txtId.Enabled = false;
            txtValue.Enabled = false;
            string nodeName = e.Node.Text;
            
            if (nodeName =="@id" || nodeName =="@value" || nodeName== profileName )
            {
                return;
            }
            NodeContext nodeContext =(NodeContext) e.Node.Tag;            
            txtCardialityMin.Text = string.Empty;
            txtCardialityMax.Text = string.Empty;

           
            if (e.Node.FirstNode ==null && !nodeContext.IsLastNode)
            {
                XElement branchTypeElem = GetBranchChild(nodeContext.TypeName);
                nodeContext.NodeTypeDescription = branchTypeElem.Value;
                if (branchTypeElem.Descendants(ns + "element").Count() == 0)
                {
                    // AddTreeAttributeNode(e.Node);                 
                    nodeContext.IsLastNode = true;
                }
                else
                {
                    AddTreeChildNode(e.Node, branchTypeElem);
                    nodeContext.IsLastNode = false;
                }
            }
            txtId.Text = nodeContext.IdAttr;
            txtValue.Text = nodeContext.ValueAttr;
            txtType.Text = nodeContext.TypeName;
            txtCardialityMin.Text = nodeContext.MinOccur;
            txtCardialityMax.Text = nodeContext.MaxOccur;
            txtNodeDes.Text = nodeContext.NodeDescription;
            txtNodeTypeDes.Text = nodeContext.NodeTypeDescription;

            if (nodeContext.IsLastNode)
            {
                txtId.Enabled = true;
                txtValue.Enabled = true;
            }
        }

        private void AddTreeAttributeNode(TreeNode parentBranch)
        {
            if (parentBranch.Nodes["@id"] ==null)
            {
                parentBranch.Nodes.Add("@id", "@id");
                parentBranch.Nodes.Add("@value", "@value");
            }     
        }

        private void AddTreeChildNode(TreeNode parentBranch, XElement branchTypeElem)
        {            
            foreach (XElement element in branchTypeElem.Descendants(ns + "element"))
            {
                string nodeKey = element.Attribute("name").Value;
                if (CyclicReferenceExits(parentBranch, nodeKey))
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
            }
        }
        private XElement GetBranchChild(string branchType)
        {
            XElement branchTypeElem =    //ns + "element"
                xDocSingleFhirSchema.Descendants(ns + "complexType")
                .Where(x => x.Attribute("name").Value.Equals(branchType)    //x.Name.LocalName.Equals("Activity") &&
                ).FirstOrDefault();
            return branchTypeElem;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            nodeFont = new Font(FontFamily.GenericSansSerif, 8, FontStyle.Bold);            
            txtCardialityMin.ReadOnly = true;
            txtCardialityMax.ReadOnly = true;
            txtValOptions.ReadOnly = true;
            txtType.ReadOnly = true;
            txtNodeDes.ReadOnly = true;
            xDocSingleFhirSchema = XDocument.Load(@"C:\Backup t450\MyData\Projects\TreeView\TreeView\fhir-codegen-xsd\fhir-single.xsd");
            XElement ele =
                      xDocSingleFhirSchema.Descendants(ns + "complexType")
                      .Where(x => x.Attribute("name").Value.Equals("ResourceContainer")    //x.Name.LocalName.Equals("Activity") &&
                      ).FirstOrDefault();
            foreach (XElement el in ele.Descendants(ns + "element"))
            {
                cmbBoxResource.Items.Add(el.Attribute("name").Value);
            }
        }
        private void txtValue_KeyUp(object sender, KeyEventArgs e)
        {
            //MessageBox.Show(txtValue.Text);           
            NodeContext nodeContext = (NodeContext) tv.SelectedNode.Tag;
            nodeContext.ValueAttr = txtValue.Text;
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
                    treeNode.Parent.NodeFont = nodeFont;
                }
                else
                {
                    treeNode.Parent.NodeFont = null ;
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
            ExportToXML(tv, @"C:\Backup t450\MyData\Projects\Test.xml");
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

        private void btnLoadResource_Click(object sender, EventArgs e)
        {
            //XmlDocument xmlDoc = new XmlDocument();
            XDocument xDoc = XDocument.Load(@"C:\Backup t450\MyData\Projects\test.xml");
          
            profileName = xDoc.Root.Name.ToString();
            tv.Nodes.Clear();
            tv.Nodes.Add(profileName, profileName);
            NodeContext nodeContext = new NodeContext();
            tv.Nodes[profileName].Tag = nodeContext;
            XElement ele = xDoc.Root;
            //AddTreeChildNode(tv.Nodes[profileName], ele);
            LoadTreeChildNodeFromXml(tv.Nodes[profileName], ele);
            WriteJson();
            cmbBoxResource.Text = string.Empty;           
        }

        private void LoadTreeChildNodeFromXml(TreeNode parentBranch, XElement parentElement)
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
                if(!string.IsNullOrEmpty(element.Attribute("value_attr").Value))
                {
                    parentBranch.Nodes[nodeKey].NodeFont = nodeFont;
                    AddChildIdToParentNode(parentBranch.Nodes[nodeKey], nodeContext.NodeId);
                    HighlightParentNode(parentBranch.Nodes[nodeKey], nodeContext.NodeId);
                }
                //nodeContext.NodeTypeDescription = element.Value;
                parentBranch.Nodes[nodeKey].Tag = nodeContext;
                if(element.HasElements)
                {
                    LoadTreeChildNodeFromXml(parentBranch.Nodes[nodeKey], element);
                }
            }
        }

        private void cmbBoxResource_SelectedIndexChanged(object sender, EventArgs e)
        {
            // MessageBox.Show(cmbBoxResource.Text);
            CreateResourceTree(cmbBoxResource.Text);
            rchTxtJson.Text = string.Empty;
            rchTxtBox.Text = string.Empty;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            XNode node = JsonConvert.DeserializeXNode(rchTxtJson.Text, "Root");
            rchtxtXml.Text = node.ToString();
        }
    }   
}
