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
        Assembly assembly = Assembly.GetExecutingAssembly();
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
            txtServiceEndpoint.Text = ConfigurationManager.AppSettings["ApiBaseUrl"].ToString();
            LoadSchema(objGlobal.Version);
            LoadSerivceMethod();
            LoadSerivceMediaType();
            nodeMenu = new ContextMenuStrip();

            ToolStripMenuItem editLabel = new ToolStripMenuItem();
            editLabel.Name = "EditValue";
            editLabel.Text = "EditValue";
            nodeMenu.Items.Add(editLabel);
            editLabel.Click += new EventHandler(ContextMenuEditItemClick);

            ToolStripMenuItem openLabel = new ToolStripMenuItem();
            openLabel.Name = "OpenProperty";
            openLabel.Text = "Property";
            nodeMenu.Items.Add(openLabel);
            openLabel.Click += new EventHandler(ContextMenuPropertyItemClick);

            ToolStripMenuItem extLabel = new ToolStripMenuItem();
            extLabel.Name = "AddExtnType";
            extLabel.Text = "Add Extn Type";
            nodeMenu.Items.Add(extLabel);
            extLabel.Click += new EventHandler(ContextMenuExtItemClick);

            ToolStripMenuItem addExtLabel = new ToolStripMenuItem();
            //extLabel.Enabled = false;
            addExtLabel.Name = "AddExtn";
            addExtLabel.Text = "Add Extn";
            nodeMenu.Items.Add(addExtLabel);
            addExtLabel.Click += new EventHandler(ContextMenuAddExtItemClick);

            ToolStripMenuItem removeExtLabel = new ToolStripMenuItem();
            removeExtLabel.Name = "RemoveExtn";
            removeExtLabel.Text = "Remove Extn";
            nodeMenu.Items.Add(removeExtLabel);
            removeExtLabel.Click += new EventHandler(ContextMenuRemoveExtItemClick);

            nodeFont = new Font(FontFamily.GenericSansSerif, 8, FontStyle.Bold);
            string str = ReadResource("TreeView.fhir_single_V3_0_1");           
        }

        private void LoadSchema(string version)
        {
            //var assembly = Assembly.GetExecutingAssembly();
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
                                if (el.Attribute("name") != null)
                                {
                                    cmbBoxResource.Items.Add(el.Attribute("name").Value);
                                }
                                else if (el.Attribute("ref") != null)
                                {
                                    cmbBoxResource.Items.Add(el.Attribute("ref").Value);
                                }
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
            XElement ele = GetBranchChild("Resource");
            AddTreeChildNode(tv.Nodes[profileName], ele);
            XElement ele1 = GetBranchChild("DomainResource");
            AddTreeChildNode(tv.Nodes[profileName], ele1);
            XElement ele2 = GetBranchChild(profileName);
            nodeContext.NodeDescription = GetResourceDescription(profileName);
            nodeContext.NodeTypeDescription = ele2.Value;
            nodeContext.TypeName = profileName;
            AddTreeChildNode(tv.Nodes[profileName], ele2);
        }
        private string GetResourceDescription(string resourceName)
        {
            XElement resourceElem =    //ns + "element"
               objGlobal.MainSchemaDoc.Root.Elements(ns + "element")
               .Where(x => x.Attribute("name").Value.Equals(resourceName)    //x.Name.LocalName.Equals("Activity") &&
               ).FirstOrDefault();
            return resourceElem.Value;
        }
        private void ContextMenuPropertyItemClick(object sender, EventArgs e)
        {           
            TreeNode node = tv.SelectedNode;
            //if(node.Text=="extension")
            //{
            //    NodeValueEditor nodeEdit = new NodeValueEditor(null, this);
            //    nodeEdit.ExtElement = GetBranchExtn("Extension");
            //    nodeEdit.SelectedNode = node;
            //    nodeEdit.StartPosition = FormStartPosition.CenterParent;
            //    nodeEdit.ShowDialog();
            //}
            //else
            //{
                NodeProperty nodeProperty = new NodeProperty((NodeContext)node.Tag);
                nodeProperty.StartPosition = FormStartPosition.CenterParent;
                nodeProperty.ShowDialog();
            //}
           
        }
        private void ContextMenuExtItemClick(object sender, EventArgs e)
        {
            TreeNode node = tv.SelectedNode;
            if (node.Text == "extension" || node.Text == "modifierExtension")
            {
                NodeValueEditor nodeEdit = new NodeValueEditor(null, this);
                nodeEdit.ExtElement = GetBranchExtn("Extension");
                nodeEdit.SelectedNode = node;
                nodeEdit.StartPosition = FormStartPosition.CenterParent;
                nodeEdit.ShowDialog();
            }
           
        }
        private void ContextMenuRemoveExtItemClick(object sender, EventArgs e)
        {
            TreeNode node = tv.SelectedNode;
            if (node.Text == profileName)
            {
                return;
            }
            if (node.Nodes["extension"] !=null)
            {
                node.Nodes["extension"].Remove();
            }
            if (node.Parent.Nodes["_" + node.Text] != null )
            {
                node.Parent.Nodes["_" + node.Text].Remove();
            }
                
        }
        private void ContextMenuAddExtItemClick(object sender, EventArgs e)
        {
            
            TreeNode node = tv.SelectedNode;
            if(node.Text ==profileName)
            {
                return;
            }
            XElement extnElem = GetBranchExtn("Extension");

            

            NodeContext cntxt = (NodeContext)tv.SelectedNode.Tag;
            if (node.Text != "extension" && node.Text != "modifierExtension" && !cntxt.IsLastNode)
            {
                TreeNode cNode = new TreeNode();
                NodeContext context = new NodeContext();
                context.NodeId = Guid.NewGuid().ToString();
                context.TypeName = "Extesnion";
                context.NodeDescription = extnElem.Value;                
                context.MinOccur = "0";
                context.MaxOccur = "unbounded";
                cNode.Text = "extension";
                cNode.Name = "extension";
                cNode.Tag = context;
                cNode.ContextMenuStrip = nodeMenu;
                node.Nodes.Add(cNode);
                AddExtenionElements(cNode);
            }
            else
            {
                TreeNode cNode = new TreeNode();
                NodeContext context = new NodeContext();
                context.NodeId = Guid.NewGuid().ToString();
                context.MinOccur = "0";
                context.MaxOccur = "1";
                cNode.Text = "_" + node.Text;
                cNode.Name = "_" + node.Text;
                cNode.Tag = context;
                cNode.ContextMenuStrip = nodeMenu;
                //node.Nodes.Add(cNode);
                //tv.Nodes[profileName].Nodes.Insert(node.Index+1, cNode);
                node.Parent.Nodes.Insert(node.Index + 1, cNode);

                TreeNode cNode1 = new TreeNode();
                NodeContext context1 = new NodeContext();
                context1.NodeId = Guid.NewGuid().ToString();
                context1.TypeName = "Extesnion";
                context1.NodeDescription = extnElem.Value;               
                context1.MinOccur = "0";
                context1.MaxOccur = "unbounded";
                cNode1.Text = "extension";
                cNode1.Name = "extension";
                cNode1.Tag = context1;
                cNode1.ContextMenuStrip = nodeMenu;
                cNode.Nodes.Add(cNode1);
                AddExtenionElements(cNode1);
            }
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

            if (nodeName == "value[x]" || nodeName == profileName || nodeName == "xhtml:div") //nodeName == "id" || nodeName == "id" || nodeName == "value" |||| nodeName == "url"
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
                    nodeContext.IsPrimitiveType = true;
                    nodeContext.IsLastNode = true;
                    GetNodeValueOptionList(nodeContext);
                }
                else
                {
                    nodeContext.IsPrimitiveType = false;
                    nodeContext.IsLastNode = false;
                    /*
                    if (e.Node.FirstNode == null)
                    {
                        AddRepeatChildNode(e.Node);
                        AddTreeChildNode(e.Node.FirstNode, branchTypeElem);
                        return;
                        // AddTreeChildNode(e.Node, branchTypeElem);
                        //nodeContext.IsPrimitiveType = false;
                        //nodeContext.IsLastNode = false;
                    }
                    */
                }
                AddTreeChildNode(e.Node, branchTypeElem);
            }         
        }

        private void AddRepeatChildNode(TreeNode parentNode)
        {
            NodeContext parentNodeContext = (NodeContext)parentNode.Tag;
            string nodeText = parentNode.Text;
            parentNode.Text = nodeText + "[1]";
            parentNode.Nodes.Add(nodeText + Guid.NewGuid().ToString(), nodeText);

            NodeContext nodeContext = new NodeContext();
            nodeContext.IsChoiceElement = parentNodeContext.IsChoiceElement;
            nodeContext.NodeId = Guid.NewGuid().ToString();
            nodeContext.MinOccur = parentNodeContext.MinOccur;
            nodeContext.MaxOccur = parentNodeContext.MaxOccur;
            nodeContext.TypeName = parentNodeContext.TypeName;
            nodeContext.NodeDescription = parentNodeContext.NodeDescription;
            nodeContext.NodeTypeDescription = parentNodeContext.NodeTypeDescription;
            parentNode.Nodes[0].Tag = nodeContext;
            parentNode.Nodes[0].ContextMenuStrip = nodeMenu;
        }

        private void tv_AfterSelect(TreeNode node)
        {
            string attrUse = string.Empty;
            string attrType = string.Empty;
            string nodeName = node.Text;

            //if (nodeName == "id" || nodeName == "value" || nodeName == "value[x]" || nodeName == profileName || nodeName == "url") //nodeName == "id" || nodeName == "value" ||
            //{
            //    return;
            //}
            NodeContext nodeContext = (NodeContext)node.Tag;

            if (node.FirstNode == null && !nodeContext.IsLastNode)
            {
                XElement branchTypeElem = GetBranchChild(nodeContext.TypeName);
                nodeContext.NodeTypeDescription = branchTypeElem.Value;
                if (branchTypeElem.Descendants(ns + "element").Count() == 0)
                {
                    nodeContext.IsPrimitiveType = true;
                    nodeContext.IsLastNode = true;
                    GetNodeValueOptionList(nodeContext);
                }
                else
                {
                    // AddTreeChildNode(e.Node, branchTypeElem);
                    nodeContext.IsPrimitiveType = false;
                    nodeContext.IsLastNode = false;
                }
                AddTreeChildNode(node, branchTypeElem);
            }
        }


        private void GetNodeValueOptionList(NodeContext nodeContext)
        {
            if(nodeContext.TypeName=="boolean")
            {
                nodeContext.ValueOptions.Add("true");
                nodeContext.ValueOptions.Add("false");
                return;
            }
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
            NodeContext parentNodeContext= (NodeContext) parentBranch.Tag;
            if (parentBranch.Name=="extension" || parentBranch.Name == "modifierExtension")
            {
                AddExtenionElements(parentBranch);
                return;
            }
            //foreach (XElement element in branchTypeElem.Descendants(ns + "element"))
            foreach (XElement element in branchTypeElem.Descendants().Where (c=> (c.Name.Equals(ns + "element") && !c.Parent.Name.LocalName.Equals("choice")) || c.Name.Equals(ns+ "choice")))
            {
                string nodeKey = string.Empty;
                string typeName = string.Empty;
                //NodeContext nodeContext = 
                // add choice and the option elements
                if (element.Name.LocalName == "choice")
                {
                    nodeKey = "choice_" + Guid.NewGuid().ToString();
                    string nodeText = "Choice";
                    parentBranch.Nodes.Add(nodeKey, nodeText);
                    NodeContext nodeContextChoice = new NodeContext();
                    nodeContextChoice.NodeId = Guid.NewGuid().ToString();
                    //nodeContextChoice.MinOccur = "1";
                    //nodeContextChoice.MaxOccur = "1";
                    //typeName = //element.Attribute("type").Value;
                   // nodeContextChoice.TypeName = typeName;// element.Attribute("type").Value;
                    if (element.Attribute("minOccurs") != null)
                    {
                        nodeContextChoice.MinOccur = element.Attribute("minOccurs").Value;
                    }
                    if (element.Attribute("maxOccurs") != null)
                    {
                        nodeContextChoice.MaxOccur = element.Attribute("maxOccurs").Value;
                    }

                    nodeContextChoice.NodeDescription = element.Value;
                    //nodeContext.NodeTypeDescription = element.Value;
                    parentBranch.Nodes[nodeKey].Tag = nodeContextChoice;
                    parentBranch.Nodes[nodeKey].ContextMenuStrip = nodeMenu;

                    foreach (XElement el in element.Descendants(ns + "element"))
                    {
                        string choiceNodeType = string.Empty;
                        string choiceNodeKey = string.Empty;
                        if (el.Attribute("name") != null)
                        {
                            choiceNodeKey = el.Attribute("name").Value;
                            choiceNodeType = el.Attribute("type").Value;
                        }
                        else if (el.Attribute("ref") != null)
                        {
                            choiceNodeKey = el.Attribute("ref").Value;
                            choiceNodeType = choiceNodeKey;
                        }
                        else
                        {
                            continue;
                        }
                        parentBranch.Nodes[nodeKey].Nodes.Add(choiceNodeKey, choiceNodeKey);
                        NodeContext nodeContextChoiceChild = new NodeContext();
                        nodeContextChoiceChild.IsChoiceElement = true;
                        nodeContextChoiceChild.NodeId = Guid.NewGuid().ToString();
                        nodeContextChoiceChild.MinOccur = "1";
                        nodeContextChoiceChild.MaxOccur = "1";
                        nodeContextChoiceChild.TypeName = choiceNodeType;// element.Attribute("type").Value;
                        if (el.Attribute("minOccurs") != null)
                        {
                            nodeContextChoiceChild.MinOccur = el.Attribute("minOccurs").Value;
                        }
                        if (el.Attribute("maxOccurs") != null)
                        {
                            nodeContextChoiceChild.MaxOccur = el.Attribute("maxOccurs").Value;
                        }

                        nodeContextChoiceChild.NodeDescription = el.Value;
                        //nodeContext.NodeTypeDescription = element.Value;
                        parentBranch.Nodes[nodeKey].Nodes[choiceNodeKey].Tag = nodeContextChoiceChild;
                        parentBranch.Nodes[nodeKey].Nodes[choiceNodeKey].ContextMenuStrip = nodeMenu;
                    }
                    continue;
                }   // add elements other than choice             
                if (element.Attribute("name") != null)
                {
                    nodeKey = element.Attribute("name").Value;
                    typeName = element.Attribute("type").Value;
                }
                else if (element.Attribute("ref") != null)
                {
                    nodeKey = element.Attribute("ref").Value;
                    typeName = nodeKey;
                }
                //else if (element.Name.LocalName=="choice")
                //{
                //    nodeKey = "choice";
                //    typeName = "choice"; // element.Value;
                //}
                else
                {
                    continue;
                }

                //if (CyclicReferenceExits(parentBranch, typeName) || string.IsNullOrEmpty(nodeKey))// || nodeKey == "extension")
                //{
                //    continue;
                //}

                if (string.IsNullOrEmpty(nodeKey))// || nodeKey == "extension")
                {
                    continue;
                }
                parentBranch.Nodes.Add(nodeKey, nodeKey);
                NodeContext nodeContext = new NodeContext();
                nodeContext.IsChoiceElement = parentNodeContext.IsChoiceElement;
                nodeContext.NodeId = Guid.NewGuid().ToString();
                nodeContext.MinOccur = "1";
                nodeContext.MaxOccur = "1";
                nodeContext.TypeName = typeName;// element.Attribute("type").Value;
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

        private void AddExtenionElements(TreeNode node)
        {
            string nodeKey = "url";
            node.Nodes.Add(nodeKey, nodeKey);
            NodeContext nodeContext = new NodeContext();
            //nodeContext.IsExtensionElement = true;
            nodeContext.NodeId = Guid.NewGuid().ToString();
            nodeContext.IsLastNode = true;
            nodeContext.MinOccur = "1";
            nodeContext.MaxOccur = "1";
            nodeContext.TypeName = "uri-primitive";// element.Attribute("type").Value;
            nodeContext.IsAttribute = true;
            nodeContext.NodeDescription = "uri-primitive";
            nodeContext.NodeTypeDescription = "uri-primitive";
            node.Nodes[nodeKey].Tag = nodeContext;
            node.Nodes[nodeKey].ContextMenuStrip = nodeMenu;

            nodeKey = "value[x]";
            node.Nodes.Add(nodeKey, nodeKey);
            NodeContext nodeContextForValue = new NodeContext();
            nodeContextForValue.IsExtensionValElement = true;
            nodeContextForValue.NodeId = Guid.NewGuid().ToString();
            nodeContextForValue.MinOccur = "1";
            nodeContextForValue.MaxOccur = "1";
            //nodeContextForValue.IsLastNode = true;
            nodeContextForValue.TypeName = "";// element.Attribute("type").Value;                                    
            node.Nodes[nodeKey].Tag = nodeContextForValue;
            node.Nodes[nodeKey].ContextMenuStrip = nodeMenu;
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
                objGlobal.MainSchemaDoc.Descendants(ns + "complexType")
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
            if (nodeContext.IsChoiceElement)
            {
                RemoveChoiceValues(nodeContext.NodeId);
            }

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
            WriteXML(tv);
            //WriteJsonToTextBox();
        }

        private void RemoveChoiceValues(string currentNodeId)
        {
           NodeContext nodeContext = (NodeContext)tv.SelectedNode.Tag;
            TreeNode choiceNode = GetChoiceNode(tv.SelectedNode);
            TreeNode choiceChildCurrentParentNode = GetPrentForSelectedChoiceNode(tv.SelectedNode);
            if (choiceNode ==null)
            {
                return;
            }
            IEnumerable<TreeNode> choiceNodesForRemoval = choiceNode.Nodes.Cast<TreeNode>().Where(c =>  c.Text!= choiceChildCurrentParentNode.Text); //!(((NodeContext)c.Tag).ChildNodeIdList.Contains(currentNodeId))

            foreach (TreeNode node in choiceNodesForRemoval)
            {
                NodeContext innerNodeContext = (NodeContext)node.Tag;
                //NodeContext parentNodeContext = (NodeContext)node.Parent.Tag;
                //if (innerNodeContext.NodeId != currentNodeId  node.Parent.Text!="Choice")
                //{
                    innerNodeContext.ChildNodeIdList.Clear();
                    innerNodeContext.ValueAttr = null;
                    node.NodeFont = null;
                RemoveChildIdFromParentNode(node, innerNodeContext.NodeId);
                    if (node.FirstNode != null)
                    {
                        RemoveChoiceValues(node.Nodes);
                    }
                //}
              //  else 
            }           
        }

        private TreeNode GetChoiceNode(TreeNode currentNode)
        {
            if (currentNode.Parent != null && currentNode.Parent.Text=="Choice")
            {
                return currentNode.Parent;                
            }           
            return  GetChoiceNode(currentNode.Parent);           
        }

        // get selected node's parent which is immediate child of choice
        private TreeNode GetPrentForSelectedChoiceNode(TreeNode currentNode)
        {
            if (currentNode.Parent != null && currentNode.Parent.Text == "Choice")
            {
                return currentNode;
            }
            return GetPrentForSelectedChoiceNode(currentNode.Parent);
        }

        private void RemoveChoiceValues(TreeNodeCollection nodes)
        {
            // IEnumerable<TreeNode> childNodes = tv.Nodes.Cast<TreeNode>().Where(c => c.Name.Equals("extension") && ((NodeContext)c.Tag).ChildNodeIdList.Contains(nodeId));
            foreach (TreeNode childNode in nodes)
            {
                NodeContext nodeContext = (NodeContext)childNode.Tag;
                //NodeContext parentNodeContext = (NodeContext)childNode.Parent.Tag;
                //if (nodeContext.NodeId != nodeId && !parentNodeContext.ChildNodeIdList.Contains(nodeId))
               // {
                    nodeContext.ChildNodeIdList.Clear();
                    nodeContext.ValueAttr = null;
                    childNode.NodeFont = null;
                RemoveChildIdFromParentNode(childNode, nodeContext.NodeId);
                //  }
                if (childNode.Nodes.Count > 0)
                {
                    RemoveChoiceValues(childNode.Nodes);
                }
            }
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
            //NodeContext nodeContext = (NodeContext)treeNode.Tag;
            if (treeNode.Parent != null)// && !nodeContext.IsChoiceElement)
            {
                NodeContext nodeContextParent = (NodeContext)treeNode.Parent.Tag;
                nodeContextParent.ChildNodeIdList.Remove(nodeId);
                if(nodeContextParent.ChildNodeIdList.Count()==0)
                {
                    treeNode.Parent.NodeFont = null;
                }
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

        //private bool CyclicReferenceExits(TreeNode treeNode,string newNode)
        //{
        //    if (treeNode.Parent != null)
        //    {               
        //        if (treeNode.Parent.Name == newNode)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            CyclicReferenceExits(treeNode.Parent, newNode);
        //        }
        //    }
        //    return false;
        //}

        private bool CyclicReferenceExits(TreeNode treeNode, string newNode)
        {
            if (treeNode.Parent != null)
            {
                NodeContext nodeContext = (NodeContext)treeNode.Parent.Tag;
                if (nodeContext.TypeName == newNode)
                {
                    return true;
                }
                else
                {
                    CyclicReferenceExits(treeNode.Parent, newNode);
                }               
            }
            return false;
        }


        public void WriteXML(System.Windows.Forms.TreeView tree)
        {
              MemoryStream memory_stream = new MemoryStream();
            XmlTextWriter xmlTextWriter = new XmlTextWriter(memory_stream, System.Text.Encoding.UTF8);
           // XmlTextWriter xmlTextWriter = new XmlTextWriter(filename, System.Text.Encoding.UTF8);
            xmlTextWriter.WriteStartDocument();
            xmlTextWriter.WriteStartElement(tree.Nodes[0].Text);
            foreach (TreeNode node in tree.Nodes[0].Nodes)
            {
                TreeNode innerNode = node;
                if (node.Text == "Choice")
                {
                    innerNode = node.Nodes.Cast<TreeNode>().Where(c => !string.IsNullOrEmpty(((NodeContext)c.Tag).ValueAttr) || ((NodeContext)c.Tag).ChildNodeIdList.Count > 0).FirstOrDefault();
                }
                if (innerNode == null)
                {
                    continue;
                }

                if (innerNode.Nodes.Count > 0)
                {
                    xmlTextWriter.WriteStartElement(innerNode.Text);
                    SaveToXML(innerNode, xmlTextWriter);
                    xmlTextWriter.WriteEndElement();
                }
                else
                {
                    NodeContext nodeContext = (NodeContext)innerNode.Tag;
                    if (nodeContext!=null && nodeContext.IsLastNode)
                    {
                        xmlTextWriter.WriteStartElement(innerNode.Text);
                        xmlTextWriter.WriteAttributeString("value", nodeContext.ValueAttr);
                        xmlTextWriter.WriteEndElement();
                    }
                }
              
            }
            xmlTextWriter.WriteEndElement();
            xmlTextWriter.Flush();
            
            StreamReader stream_reader = new StreamReader(memory_stream);
            memory_stream.Seek(0, SeekOrigin.Begin);
            //rchTxtServiceResponse.Text = stream_reader.ReadToEnd();
            XDocument xDoc = XDocument.Parse(stream_reader.ReadToEnd());
            rchTxtXmlRequest.Text = xDoc.ToString();
            xmlTextWriter.Close();
        }

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
                TreeNode innerNode = childNode;
                if (childNode.Text == "Choice")
                {
                    innerNode = childNode.Nodes.Cast<TreeNode>().Where(c => !string.IsNullOrEmpty(((NodeContext)c.Tag).ValueAttr) || ((NodeContext)c.Tag).ChildNodeIdList.Count > 0).FirstOrDefault();
                }
                if (innerNode == null)
                {
                    continue;
                }
                NodeContext nodeContext = (NodeContext)innerNode.Tag;
                if (innerNode.Nodes.Count > 0 && nodeContext.ChildNodeIdList.Count() > 0)
                {
                    xmlTextWriter.WriteStartElement(innerNode.Text);
                    SaveToXML(innerNode, xmlTextWriter);
                    xmlTextWriter.WriteEndElement();
                }
                else
                {                    
                    if (nodeContext.IsLastNode && !string.IsNullOrEmpty(nodeContext.ValueAttr))
                    {
                        xmlTextWriter.WriteStartElement(innerNode.Text);
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
                if (childNode.Text == "xhtml:div")
                {
                    continue;
                }
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
                if (childNode.Nodes.Count > 0 && (nodeContext.ChildNodeIdList.Count() > 0 || childNode.Text=="Choice"))
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
            tw.WriteLine("\"resourceType\": \"" + tv.Nodes[0].Text + "\",");
            WriteJson(tw, tv.Nodes[0]);           
            tw.WriteLine("}");
            tw.Flush();
            StreamReader stream_reader = new StreamReader(memory_stream);

            memory_stream.Seek(0, SeekOrigin.Begin);
            string json = stream_reader.ReadToEnd();
            try
            {                
                JToken jToken = JToken.Parse(json);
                rchTxtJson.Text = jToken.ToString();//stream_reader.ReadToEnd();
            }
            catch
            {
                rchTxtJson.Text = json;
                MessageBox.Show("There are some invalid chars in the current input value.","Invalid Char");
            }
            tw.Close();
        }
        private void WriteJson(TextWriter tw, TreeNode parentNode)
        {
            //foreach (XmlNode node1 in node.ChildNodes)
            //IEnumerable<TreeNode> childNodes = parentNode.Nodes.Cast<TreeNode>().Where(c => (c.Tag !=null) && (!string.IsNullOrEmpty(((NodeContext)c.Tag).ValueAttr) || ((NodeContext)c.Tag).ChildNodeIdList.Count > 0));
            IEnumerable<TreeNode> childNodes = parentNode.Nodes.Cast<TreeNode>().Where(c => (c.Tag!=null) && (!string.IsNullOrEmpty(((NodeContext)c.Tag).ValueAttr) || ((NodeContext)c.Tag).ChildNodeIdList.Count > 0));
            int counter = 0;
            foreach (TreeNode node in childNodes)
            {
                TreeNode innerNode = node;
                if (node.Text == "Choice")
                {
                    innerNode = node.Nodes.Cast<TreeNode>().Where(c => !string.IsNullOrEmpty(((NodeContext)c.Tag).ValueAttr) || ((NodeContext)c.Tag).ChildNodeIdList.Count > 0).FirstOrDefault();
                }
                if(innerNode ==null)
                {
                    continue;
                }
                counter = counter + 1;
                NodeContext nodeContext = (NodeContext)innerNode.Tag;
                if (!nodeContext.IsLastNode && nodeContext.ChildNodeIdList.Count > 0)
                {                   
                    if (nodeContext.MaxOccur == "unbounded")
                    {
                        tw.WriteLine("\"" + innerNode.Text + "\": [");
                        tw.WriteLine("{");
                    }
                    else
                    {
                        tw.WriteLine("\"" + innerNode.Text + "\": {");
                    }
                    WriteJson(tw, innerNode);                   
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
                        if (nodeContext.TypeName == "boolean" || nodeContext.TypeName == "integer" || nodeContext.TypeName == "decimal")
                        {
                            if (nodeContext.MaxOccur == "unbounded")
                            {
                                tw.WriteLine("\"" + innerNode.Text + "\":[" + nodeContext.ValueAttr + "]");
                            }
                            else
                            {
                                tw.WriteLine("\"" + innerNode.Text + "\":" + nodeContext.ValueAttr);
                            }
                        }
                        else
                        {
                            if (nodeContext.MaxOccur == "unbounded")
                            {
                                tw.WriteLine("\"" + innerNode.Text + "\": [\"" + nodeContext.ValueAttr + "\"]");
                            }
                            else
                            {
                                tw.WriteLine("\"" + innerNode.Text + "\": \"" + nodeContext.ValueAttr + "\"");
                            }
                           
                        }
                    }
                    else
                    {
                        if (nodeContext.TypeName == "boolean" || nodeContext.TypeName == "integer" || nodeContext.TypeName == "decimal")
                        {
                            if (nodeContext.MaxOccur == "unbounded")
                            {
                                tw.WriteLine("\"" + innerNode.Text + "\": [" + nodeContext.ValueAttr + "],");
                            }
                            else
                            {
                                tw.WriteLine("\"" + innerNode.Text + "\": " + nodeContext.ValueAttr + ",");
                            }
                           
                        }
                        else
                        {
                            if (nodeContext.MaxOccur == "unbounded")
                            {
                                tw.WriteLine("\"" + innerNode.Text + "\": [\"" + nodeContext.ValueAttr + "\"],");
                            }
                            else
                            {
                                tw.WriteLine("\"" + innerNode.Text + "\": \"" + nodeContext.ValueAttr + "\",");
                            }
                           
                        }
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
                    return;
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
                tv.Nodes[profileName].ContextMenuStrip = nodeMenu;
                XElement ele = xDoc.Root.Element(profileName);
                //AddTreeChildNode(tv.Nodes[profileName], ele);
                LoadTreeChildNodeFromXml(tv.Nodes[profileName], ele);
                WriteJson();
                WriteXML(tv);
               
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
                    string nodeText = nodeKey;
                    //string nodeKey = element.Name.ToString();
                    if (element.Name == "Choice")
                    {
                        nodeKey = nodeKey = "choice_" + Guid.NewGuid().ToString();
                        nodeText = "Choice";
                    }
                    //if (parentBranch.Text=="Choice")
                    //{
                    //    NodeContext parentNodeContext = (NodeContext)parentBranch.Tag;
                    //    //nodeKey= 
                    //}
                    parentBranch.Nodes.Add(nodeKey, nodeText);
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
                        nodeContext.IsLastNode = true;
                        parentBranch.Nodes[nodeKey].NodeFont = nodeFont;
                        AddChildIdToParentNode(parentBranch.Nodes[nodeKey], nodeContext.NodeId);
                        HighlightParentNode(parentBranch.Nodes[nodeKey], nodeContext.NodeId);
                    }
                    parentBranch.Nodes[nodeKey].ContextMenuStrip = nodeMenu;
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
           // PopulateSearchParam(cmbBoxResource.Text);
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
            ContextMenuStrip menu = e.Node.ContextMenuStrip;

            if (e.Node.Text == "Choice")
            {
                menu.Items["OpenProperty"].Enabled = true;
                menu.Items["EditValue"].Enabled = false;
                menu.Items["AddExtn"].Enabled = false;
                menu.Items["RemoveExtn"].Enabled = false;
                menu.Items["AddExtnType"].Enabled = false;
            }
            else if (e.Node.Text == "value[x]")
            {
                menu.Items["OpenProperty"].Enabled = false;
                menu.Items["EditValue"].Enabled = false;
                menu.Items["AddExtn"].Enabled = false;
                menu.Items["RemoveExtn"].Enabled = false;
                menu.Items["AddExtnType"].Enabled = false;
            }
            else if (e.Node.Text=="extension" || e.Node.Text == "modifierExtension")
            {
                menu.Items["OpenProperty"].Enabled = true;
                menu.Items["EditValue"].Enabled = false;
                menu.Items["AddExtn"].Enabled = false;
                menu.Items["RemoveExtn"].Enabled = false;
                menu.Items["AddExtnType"].Enabled = true;
            }
            else
            {
                menu.Items["OpenProperty"].Enabled = true;
                menu.Items["EditValue"].Enabled = true;
                menu.Items["AddExtn"].Enabled = true;
                menu.Items["RemoveExtn"].Enabled = true;
                menu.Items["AddExtnType"].Enabled = false;
            }
        }

        private void tv_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            OpenNodeValueEditor();
        }

        private void OpenNodeValueEditor()
        {
            TreeNode node = tv.SelectedNode;
            NodeContext context = (NodeContext)node.Tag;
            //if (context.IsExtensionValElement)
            //{
            //    NodeValueEditor nodeEdit = new NodeValueEditor(context, this);
            //    nodeEdit.ExtElement = GetBranchExtn("Extension");
            //    nodeEdit.SelectedNode = node;
            //    nodeEdit.StartPosition = FormStartPosition.CenterParent;
            //    nodeEdit.ShowDialog();
            //}
            //else 
            if (context.IsLastNode)
            {
                NodeValueEditor nodeEdit = new NodeValueEditor(context, this);
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
            cmbServiceMethod.Items.Add("GET");
            cmbServiceMethod.Items.Add("POST");            
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
            rchTxtServiceResponse.Text = apiRes;
            try
            {
                // JToken jToken = JToken.Parse(apiRes);
                // rchTxtServiceResponse.Text = jToken.ToString();
                // XNode node = JsonConvert.DeserializeXNode(rchTxtServiceResponse.Text, "Root");
                rchTxtServiceResponseXml.Text = apiRes;// node.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            if(string.IsNullOrEmpty(rchTxtServiceResponse.Text))
            {
                rchTxtServiceResponse.Text = "No response received from service.";
            }
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

        public void SetExtensionValDataTypeNode(TreeNode node,NodeContext newNodeContext)
        {
            foreach(TreeNode c in node.Nodes)
            {
                if(c.Text=="url")
                {
                    continue;
                }
                node.Nodes.Remove(c);
            }
            node.Nodes.Add(newNodeContext.ExtensionValElementName, newNodeContext.ExtensionValElementName);
            newNodeContext.IsLastNode = false;          
            node.Nodes[newNodeContext.ExtensionValElementName].Tag = newNodeContext;
            node.Nodes[newNodeContext.ExtensionValElementName].ContextMenuStrip = nodeMenu;
            //context.IsLastNode = false;
            //node.Text = context.ExtensionValElementName;            
            tv_AfterSelect(node.Nodes[newNodeContext.ExtensionValElementName]);
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void lnkCopyJsonReq_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Windows.Forms.Clipboard.SetText(rchTxtJson.Text);
        }

        private void lnkCopyXmlReq_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Windows.Forms.Clipboard.SetText(rchTxtXmlRequest.Text);
        }

        private void lnkParseJsonReq_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                JToken jToken = JToken.Parse(rchTxtJson.Text);
                rchTxtJson.Text = jToken.ToString();
                MessageBox.Show("Valid JSON","Valid");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid JSON","Invalid");
            }
        }


        private void WriteJsonToTextBox()
        {
            rt.Text = string.Empty;         
            rt.AppendText("{" + Environment.NewLine);
            rt.AppendText("\"resourceType\": \"" + tv.Nodes[0].Text + "\"," + Environment.NewLine);
            WriteJsonToTextBox(tv.Nodes[0]);
            rt.AppendText("}" + Environment.NewLine);
            // tw.Flush();
            //StreamReader stream_reader = new StreamReader(memory_stream);

            //memory_stream.Seek(0, SeekOrigin.Begin);
            //string json = stream_reader.ReadToEnd();
          //  JToken jToken = JToken.Parse(json);
          //  rchTxtJson.Text = jToken.ToString();//stream_reader.ReadToEnd();
            // tw.Close();
            rt.ScrollToCaret();
        }
        private void WriteJsonToTextBox(TreeNode parentNode)
        {
            //foreach (XmlNode node1 in node.ChildNodes)
            //IEnumerable<TreeNode> childNodes = parentNode.Nodes.Cast<TreeNode>().Where(c => (c.Tag !=null) && (!string.IsNullOrEmpty(((NodeContext)c.Tag).ValueAttr) || ((NodeContext)c.Tag).ChildNodeIdList.Count > 0));
            IEnumerable<TreeNode> childNodes = parentNode.Nodes.Cast<TreeNode>().Where(c => (c.Tag != null) && (!string.IsNullOrEmpty(((NodeContext)c.Tag).ValueAttr) || ((NodeContext)c.Tag).ChildNodeIdList.Count > 0));
            int counter = 0;
            foreach (TreeNode node in childNodes)
            {
                TreeNode innerNode = node;
                if (node.Text == "Choice")
                {
                    innerNode = node.Nodes.Cast<TreeNode>().Where(c => !string.IsNullOrEmpty(((NodeContext)c.Tag).ValueAttr) || ((NodeContext)c.Tag).ChildNodeIdList.Count > 0).FirstOrDefault();
                }
                if (innerNode == null)
                {
                    continue;
                }
                counter = counter + 1;
                NodeContext nodeContext = (NodeContext)innerNode.Tag;
                if (!nodeContext.IsLastNode && nodeContext.ChildNodeIdList.Count > 0)
                {
                    if (nodeContext.MaxOccur == "unbounded")
                    {
                        rt.AppendText("\"" + innerNode.Text + "\": [" + Environment.NewLine);
                        rt.AppendText("{" + Environment.NewLine);
                    }
                    else
                    {
                        rt.AppendText("\"" + innerNode.Text + "\": {" + Environment.NewLine);
                    }
                    WriteJsonToTextBox( innerNode);
                    if (nodeContext.MaxOccur == "unbounded")
                    {
                        if (counter == childNodes.Count())
                        {
                            rt.AppendText("}" + Environment.NewLine);
                            rt.AppendText("]" + Environment.NewLine);
                        }
                        else
                        {
                            rt.AppendText("}" + Environment.NewLine);
                            rt.AppendText("]," + Environment.NewLine);
                        }
                    }
                    else
                    {
                        if (counter == childNodes.Count())
                        {
                            rt.AppendText("}" + Environment.NewLine);
                        }
                        else
                        {
                            rt.AppendText("}," + Environment.NewLine);
                        }
                    }
                }
                else //if(!string.IsNullOrEmpty( nodeContext.ValueAttr))
                {
                    if (counter == childNodes.Count())
                    {
                        if (nodeContext.TypeName == "boolean" || nodeContext.TypeName == "integer" || nodeContext.TypeName == "decimal")
                        {
                            if (nodeContext.MaxOccur == "unbounded")
                            {
                                rt.AppendText("\"" + innerNode.Text + "\": ");
                                rt.AppendText("[" + nodeContext.ValueAttr + "]" + Environment.NewLine, Color.DarkGray);
                            }
                            else
                            {
                                rt.AppendText("\"" + innerNode.Text + "\": ");
                                rt.AppendText(nodeContext.ValueAttr + Environment.NewLine, Color.DarkGray);
                            }
                        }
                        else
                        {
                            if (nodeContext.MaxOccur == "unbounded")
                            {
                                rt.AppendText("\"" + innerNode.Text + "\": ");
                                rt.AppendText("[\"" + nodeContext.ValueAttr + "\"]" + Environment.NewLine, Color.DarkGray);
                            }
                            else
                            {
                                rt.AppendText("\"" + innerNode.Text + "\": ");
                                rt.AppendText("\"" + nodeContext.ValueAttr + "\"" + Environment.NewLine, Color.DarkGray);
                            }

                        }
                    }
                    else
                    {
                        if (nodeContext.TypeName == "boolean" || nodeContext.TypeName == "integer" || nodeContext.TypeName == "decimal")
                        {
                            if (nodeContext.MaxOccur == "unbounded")
                            {
                                rt.AppendText("\"" + innerNode.Text + "\": ");
                                rt.AppendText("[" + nodeContext.ValueAttr + "]," + Environment.NewLine, Color.DarkGray);
                            }
                            else
                            {
                                rt.AppendText("\"" + innerNode.Text + "\": ");
                                rt.AppendText(nodeContext.ValueAttr + "," + Environment.NewLine, Color.DarkGray);
                            }

                        }
                        else
                        {
                            if (nodeContext.MaxOccur == "unbounded")
                            {
                                rt.AppendText("\"" + innerNode.Text + "\": ");
                                rt.AppendText("[\"" + nodeContext.ValueAttr + "\"]," + Environment.NewLine, Color.DarkGray);
                            }
                            else
                            {
                                rt.AppendText("\"" + innerNode.Text + "\": ");
                                rt.AppendText("\"" + nodeContext.ValueAttr + "\"," + Environment.NewLine, Color.DarkGray);
                            }

                        }
                    }
                }
            }
        }

        private void PopulateSearchParam(string resourceName)
        {
            try
            {
                XNamespace ns = "http://hl7.org/fhir";
                string fileName = "TreeView.Schema.search-parameters.xml";
                XDocument xDoc = XDocument.Load(assembly.GetManifestResourceStream(fileName));
                IEnumerable<XElement> list = xDoc.Root
                    .Descendants(ns + "SearchParameter")
                    .Where(c => (c.Elements(ns + "base").Where(a => a.Attribute("value").Value.Equals(resourceName)).Count() > 0));

                //.Attribute("value").Value.Equals("Person"));
                DataTable dt = new DataTable();
                dt.Columns.Add("Name");
                dt.Columns.Add("Desc");
                foreach (XElement el in list)
                {
                    DataRow dRow = dt.NewRow();
                    dRow["Name"] = el.Element(ns + "name").Attribute("value").Value;
                    dRow["Desc"] = el.Element(ns + "description").Attribute("value").Value;
                    dt.Rows.Add(dRow);
                }
                dgParams.DataSource = dt;
            }
            catch
            {

            }
        }       
    }

    public static class RichTextBoxExtensions
    {
        public static void AppendText(this RichTextBox box, string text, Color color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = Color.DarkRed;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }
    }
}
