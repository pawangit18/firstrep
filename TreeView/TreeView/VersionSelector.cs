using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TreeView
{
    public partial class VersionSelector : Form
    {
        Form Parent;
        Form1_New childForm;
        public VersionSelector(Form _parent)
        {
            Parent = _parent;
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(comboBox1.Text))
            {
                MessageBox.Show("Select version","Error");
                return;
            }
            if (childForm == null)
            {
                childForm = new Form1_New(comboBox1.Text);
            }           
            childForm.MdiParent = Parent;
            //childForm.Text = "Window " + childFormNumber++;
            childForm.Dock = DockStyle.Fill;
            childForm.WindowState = FormWindowState.Normal;
            childForm.StartPosition = FormStartPosition.CenterScreen;
            childForm.Size = Parent.ClientRectangle.Size;
            //childForm.TopLevel = false;
            childForm.Show();
            this.Close();
        }

        private void VersionSelector_Load(object sender, EventArgs e)
        {
            if(DateTime.Now>Convert.ToDateTime("2020-12-31"))
            {
                MessageBox.Show("There is some issue occurred in the application");
                this.Close();
            }
            comboBox1.Items.Add("3.0.1");
            comboBox1.Items.Add("4.0.1");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
