using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;   
using System.Windows.Forms;
using System.Xml;
using DGVPrinterHelper;

namespace Test1
{
    public partial class Form5 : Form
    {
        //variable
        int currentrow;
        public static bool cancelexit;

        public static string savefile = "";

        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }

        private void Form5_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
            else
            {
                if ((Form1.j > -1) && (savefile == ""))
                {
                    DialogResult dr = MessageBox.Show("Want to save your changes?", "Cable Sizing", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                    if (dr == DialogResult.Yes)
                    {
                        ExportDgvToXML();
                        savefile = "";
                        clearTable();
                    }
                    else if (dr == DialogResult.No)
                    {
                        savefile = "";
                        clearTable();
                    }
                    else
                    {
                        e.Cancel = true;
                        cancelexit = true;
                    }
                }
                else if (savefile != "")
                {
                    DialogResult dr = MessageBox.Show("Want to save your changes to " + savefile + "?", "Cable Sizing", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                    if (dr == DialogResult.Yes)
                    {
                        saveExportDgvToXML();
                        savefile = "";
                        clearTable();
                    }
                    else if (dr == DialogResult.No)
                    {
                        savefile = "";
                        clearTable();
                    }
                    else
                    {
                        e.Cancel = true;
                        cancelexit = true;
                    }
                }
                else
                {
                    this.Close();
                }
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell.Selected)
            {
                this.dataGridView1.Rows.RemoveAt(currentrow);
                for (int i = currentrow; i < Form1.j; i++)
                {
                    this.dataGridView1.Rows[i].Cells[0].Value = i + 1;
                }
                Form1.j--;
            }
            if ((dataGridView1.CurrentCell != null) && dataGridView1.CurrentCell.Selected)
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }


        private void Button3_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Delete all data?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                clearTable();
            }
        }

        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if ((dataGridView1.CurrentCell != null) && (dataGridView1.CurrentCell.Selected))
            {
                button1.Enabled = true;
                currentrow = dataGridView1.CurrentCell.RowIndex;
            }
            else
            {
                button1.Enabled = false;
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (Form1.j > -1)
            {
                dataGridView1.SelectAll();
                DataObject dataObj = dataGridView1.GetClipboardContent();
                Clipboard.SetDataObject(dataObj, true);
            }
            else
            {
                Clipboard.Clear();
            }
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((Form1.j > -1) && (savefile == ""))
            {
                DialogResult dr = MessageBox.Show("Want to save your changes?", "Cable Sizing", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
                    ExportDgvToXML();
                    savefile = "";
                    clearTable();
                    openFile();
                }
                else if (dr == DialogResult.No)
                {
                    savefile = "";
                    clearTable();
                    openFile();
                }
            }
            else if (savefile != "")
            {
                DialogResult dr = MessageBox.Show("Want to save your changes to " + savefile + "?", "Cable Sizing", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
                    saveExportDgvToXML();
                    savefile = "";
                    clearTable();
                    openFile();
                }
                else if (dr == DialogResult.No)
                {
                    savefile = "";
                    clearTable();
                    openFile();
                }
            }
            else
            {
                savefile = "";
                clearTable();
                openFile();
            }

        }

        private void openFile()
        {
            DataTable dx = new DataTable();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "XML|*.xml";
            DataSet ds = new DataSet();


            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ds.ReadXml(ofd.FileName);
                    dx = ds.Tables[0];

                    int dxcolumn = dx.Columns.Count;
                    int dxrow = dx.Rows.Count;

                    dataGridView1.Rows.Clear();

                    for (int i = 0; i < dxrow; i++)
                    {
                        dataGridView1.RowCount++;
                        for (int k = 0; k < dxcolumn; k++)
                        {
                            dataGridView1.Rows[i].Cells[k].Value = dx.Rows[i].ItemArray[k];
                        }

                    }
                    Form1.j = dxrow - 1;
                    savefile = ofd.FileName;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (savefile == "")
            {
                ExportDgvToXML();
            }
            else //safefile == last savepath
            {
                saveExportDgvToXML();
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportDgvToXML();
        }


        public void ExportDgvToXML()
        {
            DataTable dt = new DataTable();

            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                dt.Columns.Add(col.Name);
            }

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                DataRow dRow = dt.NewRow();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    dRow[cell.ColumnIndex] = cell.Value;
                }
                dt.Rows.Add(dRow);
            }

            DataSet ds = new DataSet();
            ds.Tables.Add(dt);

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "XML|*.xml";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    XmlTextWriter xmlSave = new XmlTextWriter(sfd.FileName, Encoding.UTF8);
                    xmlSave.Formatting = Formatting.Indented;
                    ds.DataSetName = "data";
                    ds.WriteXml(xmlSave);
                    xmlSave.Close();
                    savefile = sfd.FileName;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        public void saveExportDgvToXML()
        {
            DataTable dt = new DataTable();

            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                dt.Columns.Add(col.Name);
            }

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                DataRow dRow = dt.NewRow();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    dRow[cell.ColumnIndex] = cell.Value;
                }
                dt.Rows.Add(dRow);
            }

            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            try
            {
                XmlTextWriter xmlSave = new XmlTextWriter(savefile, Encoding.UTF8);
                xmlSave.Formatting = Formatting.Indented;
                ds.DataSetName = "data";
                ds.WriteXml(xmlSave);
                xmlSave.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }

        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((Form1.j > -1) && (savefile == ""))
            {
                DialogResult dr = MessageBox.Show("Want to save your changes?", "Cable Sizing", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
                    ExportDgvToXML();
                    savefile = "";
                    clearTable();
                }
                else if (dr == DialogResult.No)
                {
                    savefile = "";
                    clearTable();
                }
            }
            else if (savefile != "")
            {
                DialogResult dr = MessageBox.Show("Want to save your changes to " + savefile + "?", "Cable Sizing", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
                    saveExportDgvToXML();
                    savefile = "";
                    clearTable();
                }
                else if (dr == DialogResult.No)
                {
                    savefile = "";
                    clearTable();
                }
            }
            else
            {
                savefile = "";
                clearTable();
            }
        }

        private void clearTable()
        {
            if (dataGridView1.DataSource != null)
            {
                dataGridView1.DataSource = null;
            }
            else
            {
                dataGridView1.Rows.Clear();
            }
            Form1.j = -1;
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            DGVPrinter printer = new DGVPrinter();

            printer.Title = "Cable Sizing Result";

            printer.SubTitle = "\n\n\n";

            printer.SubTitleFormatFlags = StringFormatFlags.LineLimit |

                                          StringFormatFlags.NoClip;

            printer.PageNumbers = true;

            printer.PageNumberInHeader = false;

            printer.PorportionalColumns = true;

            printer.HeaderCellAlignment = StringAlignment.Near;

            printer.Footer = "PT. Singgar Mulia";

            printer.FooterSpacing = 15;

            printer.PrintPreviewDataGridView(dataGridView1);
        }

    }
}
