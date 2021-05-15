using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TreeView.Class;


namespace TreeView
{
    public partial class NodeValueEditor : Form
    {
        NodeContext nodeContext;
        Form1_New parentForm;
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
            comboBox1.Text = nodeContext.ValueAttr;
            textBox2.Text = nodeContext.ValueAttr;

            if (nodeContext.ValueOptions.Count()==0)
            {
                comboBox1.Visible = false;
                textBox2.Visible = true;
            }
            else
            {
                comboBox1.Visible = true;
                textBox2.Visible = false;
            }
            foreach(string str in nodeContext.ValueOptions)
            {
                comboBox1.Items.Add(str);
            }
        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            nodeContext.ValueAttr = textBox2.Text;
            parentForm.txtValue_KeyUp(null,null);
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            nodeContext.ValueAttr = comboBox1.Text;
            parentForm.txtValue_KeyUp(null, null);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
