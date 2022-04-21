using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace InfoLab2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void выйтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                InitFolders(folderBrowserDialog.SelectedPath);
            }
        }

        private void шрифтToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            if(fd.ShowDialog() == DialogResult.OK)
            {
                ListView.Font = fd.Font;
            }
        }

        private void цветToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
            {
                BackColor = cd.Color;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Chart.Series[0].Points.AddXY("Большой", 0);
            Chart.Series[0].Points.AddXY("Средний", 0);
            Chart.Series[0].Points.AddXY("Маленький", 0);
            Chart.Series[0].Name = "Количество файлов разного размера";
        }
        private void InitFolders(string path)
        {
            CurrentPath = path;
            TreeView.BeginUpdate();
            DirectoryInfo Info;
            try
            {
                string[] NameOfSubdirectories = Directory.GetDirectories(path);
                if(NameOfSubdirectories.Rank > 1)
                {
                    try
                    {
                        foreach (string f in NameOfSubdirectories)
                        {
                            Info = new DirectoryInfo(f);
                            BuildTree(Info, TreeView.Nodes);
                        }
                    }
                    catch { }
                }
                else
                {
                    Info = new DirectoryInfo(path);
                    BuildTree(Info, TreeView.Nodes);
                }
            }
            catch { }
            TreeView.EndUpdate();
        }
        private void BuildTree(DirectoryInfo directoryInfo, TreeNodeCollection nodeCollection)
        {
            TreeNode CurrentTreeNode = nodeCollection.Add("Folder", directoryInfo.Name);
            foreach(DirectoryInfo DirInf in directoryInfo.GetDirectories())
            {
                BuildTree(DirInf, CurrentTreeNode.Nodes);
            }
            foreach(FileInfo fileInfo in directoryInfo.GetFiles())
            {
                CurrentTreeNode.Nodes.Add("File", fileInfo.Name);
            }
        }
        public string CurrentPath { get; set; }
        public DirectoryInfo SelectDirectoryInfo { get; set; }
        private void TreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            ListView.Items.Clear();
            FindDirectory(e.Node.Text, CurrentPath);
            if(SelectDirectoryInfo != null)
            {
                foreach(FileInfo f in SelectDirectoryInfo.GetFiles())
                {
                    ListViewItem lvi = new ListViewItem(f.Name);
                    lvi.SubItems.Add(Convert.ToString(f.Length));
                    lvi.SubItems.Add(Convert.ToString(f.Extension));
                    lvi.Checked = true;
                    if(f.Extension == ".jpg" || f.Extension == ".png" || f.Extension == ".bmp" || f.Extension == ".gif")
                    {
                        lvi.BackColor = Color.Red;
                    }
                    else if(f.Extension == ".docx" || f.Extension == ".xlsx" || f.Extension == ".pdf" || f.Extension == ".txt")
                    {
                        lvi.BackColor = Color.Green;
                    }
                    else
                    {
                        lvi.BackColor = Color.Yellow;
                    }
                    ListView.Items.Add(lvi);
                }
                ModificationChart();
            }

        }
        private void FindDirectory(string SelectDorectory, string path)
        {
            DirectoryInfo directoryInfo;
            string[] CurrentDirectores = Directory.GetDirectories(path);
            foreach(string f in CurrentDirectores)
            {
                directoryInfo = new DirectoryInfo(f);
                if(directoryInfo.Name == SelectDorectory)
                {
                    SelectDirectoryInfo = directoryInfo;
                }
                else
                {
                    FindDirectory(SelectDorectory, f);
                }
            }
        }
        public void ModificationChart()
        {
            int x = 0, y = 0, z = 0; 
            foreach (ListViewItem item in ListView.Items)
            {
                if (item.Checked)
                {
                    Chart.Series[0].Points.Clear();
                    if(Convert.ToInt32(item.SubItems[1].Text) <= 10000)
                    {
                        Chart.Series[0].Points.AddXY("Большой", x);
                        Chart.Series[0].Points.AddXY("Маленький", z++);
                        Chart.Series[0].Points.AddXY("Средний", y);                        
                    }
                    else if(Convert.ToInt32(item.SubItems[1].Text) > 10000 && Convert.ToInt32(item.SubItems[1].Text) <= 100000)
                    {
                        Chart.Series[0].Points.AddXY("Большой", x);
                        Chart.Series[0].Points.AddXY("Маленький", z);
                        Chart.Series[0].Points.AddXY("Средний", y++);
                    }
                    else if(Convert.ToInt32(item.SubItems[1].Text) > 100000)
                    {
                        Chart.Series[0].Points.AddXY("Большой", x++);
                        Chart.Series[0].Points.AddXY("Маленький", z);
                        Chart.Series[0].Points.AddXY("Средний", y);
                    }
                    
                }
            }
        }
        private void chart1_Click(object sender, EventArgs e)
        {
            ModificationChart();
        }

        private void ListView_ItemActivate(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if(folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(Path.Combine(folderBrowserDialog.SelectedPath, @"DATA.txt")))
                {
                    foreach (TreeNode node in TreeView.Nodes)
                    {
                        sw.WriteLine(node.Text);
                    }
                }
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            ModificationChart();
        }
    }
}
