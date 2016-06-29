using System;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        

        public Form1()
        {
            InitializeComponent();
            treeView1.Scrollable = true;
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            

        }

        private void DisplayTreeView(string pathname)
        {
            try
            {
                // SECTION 1. Create a DOM Document and load the XML data into it.
                var dom = new XmlDocument();
                dom.Load(pathname);

                // SECTION 2. Initialize the TreeView control.
                treeView1.Nodes.Clear();
                

                // SECTION 3. Populate the TreeView with the XML nodes.
                foreach (XmlNode xNode in dom.ChildNodes)
                {
                    var tNode = treeView1.Nodes[treeView1.Nodes.Add(new TreeNode(xNode.Name))];
                    AddNode(xNode, tNode);
                }
            }
            catch (XmlException xmlEx)
            {
                MessageBox.Show(xmlEx.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddNode(XmlNode inXmlNode, TreeNode inTreeNode)
        {
            // Loop through the XML nodes until the leaf is reached.
            // Add the nodes to the TreeView during the looping process.
      
            if (inXmlNode.HasChildNodes)
            {
                //Check if the XmlNode has attributes
                foreach (XmlAttribute att in inXmlNode.Attributes)
                {
                    inTreeNode.Text = inTreeNode.Text + " " + att.Name + ": " + att.Value;
                }

                var nodeList = inXmlNode.ChildNodes;
                for (int i = 0; i < nodeList.Count; i++)
                {
                    var xNode = inXmlNode.ChildNodes[i];
                    var tNode = inTreeNode.Nodes[inTreeNode.Nodes.Add(new TreeNode(xNode.Name))];
                    AddNode(xNode, tNode);
                }
            }
            else
            {
                // Here you need to pull the data from the XmlNode based on the
                // type of node, whether attribute values are required, and so forth.
                inTreeNode.Text =(inXmlNode.OuterXml).Trim();
            }
           // treeView1.ExpandAll();
        }

      

        private void button1_Click(object sender, EventArgs e)
        {

            if (treeView1.SelectedNode == null)
            {
                MessageBox.Show("Please select a node first!");
            }
            else
            {
                textBox1.Text = treeView1.SelectedNode.FullPath.ToString();
            }
                
            
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            
        }

      

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void openToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.ShowDialog();

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string fileName;
                fileName = dlg.FileName;
                DisplayTreeView(fileName);
            }
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            MessageBox.Show(
                "Just open your XML file by going to File->Open.Once the XML file is loaded,click on the required attribute in the tree view and click 'Show Path' button.The full path of the attribute will appear on the textbox below the menu strip.");
        }

        private void showPathToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (treeView1.SelectedNode == null)
            {
                MessageBox.Show("Please select a node first!");
            }
            else
            {
                textBox1.Text = treeView1.SelectedNode.FullPath.ToString();
            }
        }
    }
}