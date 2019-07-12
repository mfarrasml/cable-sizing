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

        Form7 f7 = new Form7();

        public static int summaryCount = 0;


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
            Update_summary();
        }


        private void Button3_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Delete all data?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                clearTable();
            }
            Update_summary();
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
                    openFile();
                    Update_summary();
                }
                else if (dr == DialogResult.No)
                {
                    savefile = "";
                    openFile();
                    Update_summary();
                }
            }
            else if (savefile != "")
            {
                DialogResult dr = MessageBox.Show("Want to save your changes to " + savefile + "?", "Cable Sizing", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
                    saveExportDgvToXML();
                    savefile = "";
                    openFile();
                    Update_summary();
                }
                else if (dr == DialogResult.No)
                {
                    savefile = "";
                    openFile();
                    Update_summary();
                }
            }
            else
            {
                savefile = "";
                openFile();
                Update_summary();
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

                    Form1.dtdiameter.Clear();
                    Form1.dtdiameter = ds.Tables[1];

                    int dxcolumn = dx.Columns.Count;
                    int dxrow = dx.Rows.Count;
                    string Input;

                    dataGridView1.Rows.Clear();

                    for (int i = 0; i < dxrow; i++)
                    {
                        dataGridView1.RowCount++;
                        for (int k = 0; k < dxcolumn; k++)
                        {

                            if ((k == 8) || ((k >= 10) && (k <= 16)) || ((k >= 18) && (k <= 23)) ||
                                ((k >= 25) && (k <= 36)))
                            {
                                if (Form1.decimalseparator == ',')
                                {
                                    Input = Convert.ToString(dx.Rows[i].ItemArray[k]).Replace('.', ',');
                                    dataGridView1.Rows[i].Cells[k].Value = Input;
                                }
                                else
                                {
                                    Input = Convert.ToString(dx.Rows[i].ItemArray[k]).Replace(',', '.');
                                    dataGridView1.Rows[i].Cells[k].Value = Input;
                                }
                            }
                            else
                            {
                                dataGridView1.Rows[i].Cells[k].Value = dx.Rows[i].ItemArray[k];
                            }
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
            DataTable dd = new DataTable();
            dd = Form1.dtdiameter.Copy();

            foreach (DataGridViewColumn col in dataGridView1.Columns)
             {
                 dt.Columns.Add(col.Name);
             }

             foreach (DataGridViewRow row in dataGridView1.Rows)
             {
                 DataRow dRow = dt.NewRow();
                 foreach (DataGridViewCell cell in row.Cells)
                 {
                    string Input;
                    // matching all decimal separator as '.' in xml file
                    if ((cell.ColumnIndex == 8) || ((cell.ColumnIndex >= 10) && (cell.ColumnIndex <= 16)) || 
                            ((cell.ColumnIndex >= 18) && (cell.ColumnIndex <= 23)) || ((cell.ColumnIndex >= 25) && (cell.ColumnIndex <= 36)))
                    {

                        Input = Convert.ToString(cell.Value).Replace(',', '.');
                        dRow[cell.ColumnIndex] = Input;
                    }
                    else
                    {
                        dRow[cell.ColumnIndex] = cell.Value;
                    }
                 }
                 dt.Rows.Add(dRow);
             }

             DataSet ds = new DataSet();
             ds.Tables.Add(dt);
             ds.Tables.Add(dd);

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
            DataTable dd = new DataTable();
            dd = Form1.dtdiameter.Copy();

            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                dt.Columns.Add(col.Name);
            }

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                DataRow dRow = dt.NewRow();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    string Input;
                    // matching all decimal separator as '.' in xml file
                    if ((cell.ColumnIndex == 8) || ((cell.ColumnIndex >= 10) && (cell.ColumnIndex <= 16)) || 
                            ((cell.ColumnIndex >= 18) && (cell.ColumnIndex <= 23)) || ((cell.ColumnIndex >= 25) && (cell.ColumnIndex <= 36)))
                    {

                        Input = Convert.ToString(cell.Value).Replace(',', '.');
                        dRow[cell.ColumnIndex] = Input;
                    }
                    else
                    {
                        dRow[cell.ColumnIndex] = cell.Value;
                    }
                }
                dt.Rows.Add(dRow);
            }

            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            ds.Tables.Add(dd);
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
                    Update_summary();
                }
                else if (dr == DialogResult.No)
                {
                    savefile = "";
                    clearTable();
                    Update_summary();
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
                    Update_summary();
                }
                else if (dr == DialogResult.No)
                {
                    savefile = "";
                    clearTable();
                    Update_summary();
                }
            }
            else
            {
                savefile = "";
                clearTable();
                Update_summary();
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

            Form1.dtdiameter.Clear();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            DGVPrinter printer = new DGVPrinter();

            printer.Title = "Cable Sizing Result";

            printer.SubTitle = "By: Rauf Abror";

            printer.SubTitleFormatFlags = StringFormatFlags.LineLimit |

                                          StringFormatFlags.NoClip;

            printer.PageNumbers = true;

            printer.PageNumberInHeader = false;


            printer.HeaderCellAlignment = StringAlignment.Near;

            printer.Footer = "Rauf Abror";

            printer.FooterSpacing = 15;

            for (int i = 0; i < 39; i++)
            {
                
                printer.ColumnStyles[dataGridView1.Columns[i].Name] = dataGridView1.DefaultCellStyle.Clone();
                printer.ColumnHeaderStyles[dataGridView1.Columns[i].Name] = dataGridView1.DefaultCellStyle.Clone();
                printer.ColumnHeaderStyles[dataGridView1.Columns[i].Name].Font = new Font("Arial", (float)6.5);
                printer.ColumnHeaderStyles[dataGridView1.Columns[i].Name].Alignment = DataGridViewContentAlignment.MiddleCenter;
                printer.ColumnStyles[dataGridView1.Columns[i].Name].Font = new Font("Arial", (float)6.5);
                printer.ColumnStyles[dataGridView1.Columns[i].Name].Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            printer.ColumnWidth = DGVPrinter.ColumnWidthSetting.Porportional;

            printer.PrintPreviewDataGridView(dataGridView1);
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            if (!f7.Visible)
            {
                f7.Show();
            }
            else
            {
                f7.BringToFront();
            }
        }

        bool done;
        public void Update_summary()
        {
            string sel_cable;
            double cable_length;

            f7.dataGridView1.Rows.Clear();
            summaryCount = 0;
            for (int i = 0; i < Form1.j + 1; i++)
            {
                sel_cable = Convert.ToString(dataGridView1.Rows[i].Cells[37].Value);
                sel_cable = sel_cable.Replace(Convert.ToString(dataGridView1.Rows[i].Cells[17].Value) + "  ×  ", "");
                cable_length = (Convert.ToDouble(dataGridView1.Rows[i].Cells[17].Value) * Convert.ToDouble(dataGridView1.Rows[i].Cells[26].Value));

                done = false;

                if (summaryCount == 0)
                {
                    f7.dataGridView1.RowCount++;
                    f7.dataGridView1.Rows[0].Cells[0].Value = summaryCount+1;
                    f7.dataGridView1.Rows[0].Cells[1].Value = sel_cable;
                    f7.dataGridView1.Rows[0].Cells[2].Value = cable_length;
                    f7.dataGridView1.Rows[0].Cells[3].Value = dataGridView1.Rows[i].Cells[17].Value;
                    f7.dataGridView1.Rows[0].Cells[4].Value = Form1.dtdiameter.Rows[i].ItemArray[0];

                    summaryCount++;
                }
                else
                {
                    for (int k = 0; k < summaryCount; k++)
                    {
                        
                        if (sel_cable == Convert.ToString(f7.dataGridView1.Rows[k].Cells[1].Value))
                        {
                            f7.dataGridView1.Rows[k].Cells[2].Value = Convert.ToDouble(f7.dataGridView1.Rows[k].Cells[2].Value) + cable_length;
                            f7.dataGridView1.Rows[k].Cells[3].Value = Convert.ToInt32(f7.dataGridView1.Rows[k].Cells[3].Value) + Convert.ToInt32(dataGridView1.Rows[i].Cells[17].Value);
                            done = true;
                            break;
                        }

                    }
                    if (!done)
                    {
                        f7.dataGridView1.RowCount++;
                        f7.dataGridView1.Rows[summaryCount].Cells[0].Value = summaryCount+1;
                        f7.dataGridView1.Rows[summaryCount].Cells[1].Value = sel_cable;
                        f7.dataGridView1.Rows[summaryCount].Cells[2].Value = cable_length;
                        f7.dataGridView1.Rows[summaryCount].Cells[3].Value = dataGridView1.Rows[i].Cells[17].Value;
                        f7.dataGridView1.Rows[summaryCount].Cells[4].Value = Form1.dtdiameter.Rows[i].ItemArray[0];

                        summaryCount++;
                    }
                }
            }
        }
    }
}
