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
    public partial class NodeProperty : Form
    {
        NodeContext nodeContext;
        public NodeProperty()
        {
            InitializeComponent();
        }

        public NodeProperty(NodeContext context)
        {
            nodeContext = context;
            InitializeComponent();
        }

        private void NodeProperty_Load(object sender, EventArgs e)
        {
            txtCardialityMax.Text = nodeContext.MaxOccur;
            txtCardialityMin.Text = nodeContext.MinOccur;
            txtType.Text = nodeContext.TypeName;
            txtNodeDes.Text = nodeContext.NodeDescription;
            txtNodeTypeDes.Text = nodeContext.NodeTypeDescription;
        }
    }
}
