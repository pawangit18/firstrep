using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Authentication.ExtendedProtection.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using TreeView.Class;


namespace TreeView
{
    public partial class NodeValueEditor : Form
    {
        XNamespace ns = "http://www.w3.org/2001/XMLSchema";
        NodeContext nodeContext;
        public TreeNode SelectedNode;
        Form1_New parentForm;
        public XElement ExtElement;
       // string elementValue;
        public NodeValueEditor()
        {
            InitializeComponent();
        }

        public NodeValueEditor(NodeContext _context,Form1_New _parentForm)
        {
            nodeContext = _context;
            parentForm = _parentForm;
            InitializeComponent();
        }

        private void NodeValueEditor_Load(object sender, EventArgs e)
        {
            lbl.Text = "Value";
            if (ExtElement !=null)
            {
                lbl.Text = "Type";
                cmbExtElementDataTypes.ValueMember = "Value";
                cmbExtElementDataTypes.DisplayMember = "Text";
                foreach (XElement element in ExtElement.Descendants(ns + "element"))
                {
                    DropDownItem item = new DropDownItem();
                    item.Text = element.Attribute("name").Value;
                    item.Value = element.Attribute("type").Value;
                    cmbExtElementDataTypes.Items.Add(item);                    
                }
                cmbExtElementDataTypes.Visible = true;
                comboBox1.Visible = false;
                dtPicker.Visible = false;
                txtBoxValue.Visible = false;
            }
            else if (nodeContext.ValueOptions.Count()==0 && nodeContext.TypeName!= "dateTime" && nodeContext.TypeName != "date")
            {                
                txtBoxValue.Text = nodeContext.ValueAttr;
                comboBox1.Visible = false;
                dtPicker.Visible = false;
                txtBoxValue.Visible = true;
                cmbExtElementDataTypes.Visible = false;
            }
            else if (nodeContext.ValueOptions.Count() == 0 && nodeContext.TypeName == "dateTime")
            {
                dtPicker.CustomFormat = "yyyy-MM-dd HH:mm:ss";
                dtPicker.Text = nodeContext.ValueAttr;
                comboBox1.Visible = false;
                dtPicker.Visible = true;
                txtBoxValue.Visible = false;
                cmbExtElementDataTypes.Visible = false;
            }
            else if (nodeContext.ValueOptions.Count() == 0 && nodeContext.TypeName == "date")
            {
                dtPicker.CustomFormat = "yyyy-MM-dd";
                dtPicker.Text = nodeContext.ValueAttr;
                comboBox1.Visible = false;
                dtPicker.Visible = true;
                txtBoxValue.Visible = false;
                cmbExtElementDataTypes.Visible = false;
            }
            else
            {
                foreach (string str in nodeContext.ValueOptions)
                {
                    comboBox1.Items.Add(str);
                }
                comboBox1.Text = nodeContext.ValueAttr;
                comboBox1.Visible = true;
                dtPicker.Visible = false;
                txtBoxValue.Visible = false;
                cmbExtElementDataTypes.Visible = false;
            }          
        }

        //private void textBox2_KeyUp(object sender, KeyEventArgs e)
        //{
        //    nodeContext.ValueAttr = textBox2.Text;
        //   // parentForm.txtValue_KeyUp(null,null);
        //}

        //private void comboBox1_TextChanged(object sender, EventArgs e)
        //{
        //    nodeContext.ValueAttr = comboBox1.Text;
        //  //  parentForm.txtValue_KeyUp(null, null);
        //}

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (cmbExtElementDataTypes.Visible == true)
            {
                NodeContext newNodeContext = new NodeContext();
                newNodeContext.NodeId = Guid.NewGuid().ToString();
                newNodeContext.TypeName = ((DropDownItem)cmbExtElementDataTypes.SelectedItem).Value;
                newNodeContext.ExtensionValElementType = ((DropDownItem)cmbExtElementDataTypes.SelectedItem).Value;
                newNodeContext.ExtensionValElementName = ((DropDownItem)cmbExtElementDataTypes.SelectedItem).Text;

                XElement extnElem =    //ns + "element"
                 ExtElement.Descendants(ns + "element")
                 .Where(x => x.Attribute("name").Value.Equals(newNodeContext.ExtensionValElementName)    //x.Name.LocalName.Equals("Activity") &&
                 ).FirstOrDefault();

                newNodeContext.NodeDescription = extnElem.Value;
                if (extnElem.Attribute("minOccurs") != null)
                {
                    newNodeContext.MinOccur = extnElem.Attribute("minOccurs").Value;
                }
                if (extnElem.Attribute("maxOccurs") != null)
                {
                    newNodeContext.MaxOccur = extnElem.Attribute("maxOccurs").Value;
                }

                parentForm.SetExtensionValDataTypeNode(SelectedNode, newNodeContext);
            }
            else
            {
                if(dtPicker.Visible == true)
                {
                    nodeContext.ValueAttr = dtPicker.Text;
                }
                else if (txtBoxValue.Visible == true)
                {
                    nodeContext.ValueAttr = txtBoxValue.Text;
                }
                else if (comboBox1.Visible == true)
                {
                    nodeContext.ValueAttr = comboBox1.Text;
                }                
                parentForm.txtValue_KeyUp(null, null);
            }
            this.Close();
        }

        //private void dtPicker_ValueChanged(object sender, EventArgs e)
        //{
        //    nodeContext.ValueAttr = dtPicker.Text;
        //   // parentForm.txtValue_KeyUp(null, null);
        //}

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtBoxValue_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void txtBoxValue_KeyUp(object sender, KeyEventArgs e)
        {
            if (nodeContext.TypeName == "integer")
            {
                int result = 0;
                if (!int.TryParse(txtBoxValue.Text, out result))
                {
                    txtBoxValue.Text = string.Empty;
                    MessageBox.Show("Enter integer value.", "Invaid Value");
                }
            }
            if (nodeContext.TypeName == "decimal")
            {
                decimal result = 0;
                if (!decimal.TryParse(txtBoxValue.Text, out result))
                {
                    txtBoxValue.Text = string.Empty;
                    MessageBox.Show("Enter integer value.", "Invaid Value");
                }
            }           
        }

        private bool IsJsonReservedChar(char inputChar)
        {
            if(inputChar == '\"' || inputChar == '\\')
            {
                return true;
            }
            return false;
        }

        private string EscapeJsonChar(string inputString)
        {
            string escapedString = inputString;
            escapedString = escapedString.Replace(@"\", @"\\");
            escapedString = escapedString.Replace("\"", "\"");           
            return escapedString;
        }

        private void txtBoxValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (IsJsonReservedChar(e.KeyChar))
            {

                MessageBox.Show("Json reserved char","Invalid char");
                e.KeyChar = '\0';
            }     
        }
    }

    public class DropDownItem
    {
        public string Text { get; set; }
        public string Value { get; set; }

    }
}
