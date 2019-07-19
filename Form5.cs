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
        public static int currentrow;
        public static bool cancelexit;

        public static string savefile = "";

        public static Form7 f7 = new Form7();

        public static int summaryCount = 0;

        string[] arrTemp = new string[39];

        public Form5()
        {
            InitializeComponent();
        }



        private void Form5_Load(object sender, EventArgs e)
        {
            dataGridView1.DoubleBuffered(true);
        }

        private void Form5_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((e.CloseReason == CloseReason.UserClosing) && !Form1.form1Close)
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
                        f7.Close();
                    }
                    else if (dr == DialogResult.No)
                    {
                        savefile = "";
                        clearTable();
                        f7.Close();
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
                        f7.Close();
                    }
                    else if (dr == DialogResult.No)
                    {
                        savefile = "";
                        clearTable();
                        f7.Close();
                    }
                    else
                    {
                        e.Cancel = true;
                        cancelexit = true;
                    }
                }
                else
                {
                    f7.Close();
                }
            }
        }


        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if ((dataGridView1.CurrentCell != null) && (dataGridView1.CurrentCell.Selected))
            {
                btnDelete.Enabled = true;
                editRow.Enabled = true;
                currentrow = dataGridView1.CurrentCell.RowIndex;

                //turn btnUp on/off
                if (currentrow > 0)
                {
                    btnUp.Enabled = true;
                }
                else
                {
                    btnUp.Enabled = false;
                }

                //turn btnDown on/off
                if (currentrow < Form1.j)
                {
                    btnDown.Enabled = true;
                }
                else
                {
                    btnDown.Enabled = false;
                }
            }
            else
            {
                btnDelete.Enabled = false;
                editRow.Enabled = false;
                btnUp.Enabled = false;
                btnDown.Enabled = false;
            }


        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (Form1.j > -1)
            {
                dataGridView1.SelectAll();
                dataGridView1.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
                DataObject dataObj = dataGridView1.GetClipboardContent();
                Clipboard.SetDataObject(dataObj, true);
                dataGridView1.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithAutoHeaderText;
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
            ofd.Filter = "IEC|*.iec";
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

                    foreach (DataRow row in Form1.dtdiameter.Rows)
                    {
                        if (Form1.decimalseparator == ',')
                        {
                            for (int i = 0; i < 55; i++)
                            {
                                row[i] = Convert.ToString(row[i]).Replace('.', ',');
                            }
                        }
                        else
                        {
                            for (int i = 0; i < 55; i++)
                            {
                                row[i] = Convert.ToString(row[i]).Replace(',', '.');
                            }
                        }
                    }

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
                dataGridView1.ClearSelection();
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
            // matching all decimal separator as '.' in xml file
            foreach (DataRow row in dd.Rows)
            {
                for (int i = 0; i < 55; i++)
                {
                    row[i] = Convert.ToString(row[i]).Replace(',', '.');
                }
            }

            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            ds.Tables.Add(dd);

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "IEC|*.iec";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    XmlTextWriter xmlSave = new XmlTextWriter(sfd.FileName, Encoding.UTF8);
                    xmlSave.Formatting = Formatting.Indented;
                    ds.DataSetName = "IEC_Cable_Data";
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
            // matching all decimal separator as '.' in xml file
            foreach (DataRow row in dd.Rows)
            {
                for (int i = 0; i < 55; i++)
                {
                    row[i] = Convert.ToString(row[i]).Replace(',', '.');
                }
            }

            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            ds.Tables.Add(dd);
            try
            {
                XmlTextWriter xmlSave = new XmlTextWriter(savefile, Encoding.UTF8);
                xmlSave.Formatting = Formatting.Indented;
                ds.DataSetName = "IEC_Cable_Data";
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
                printer.ColumnHeaderStyles[dataGridView1.Columns[i].Name].Font = new Font("Arial", (float)4);
                printer.ColumnHeaderStyles[dataGridView1.Columns[i].Name].Alignment = DataGridViewContentAlignment.MiddleCenter;
                printer.ColumnStyles[dataGridView1.Columns[i].Name].Font = new Font("Arial", (float)4);
                printer.ColumnStyles[dataGridView1.Columns[i].Name].Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            printer.ColumnWidth = DGVPrinter.ColumnWidthSetting.DataWidth;

            printer.PrintPreviewDataGridView(dataGridView1);
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            OpenSummary();
        }

        public static void OpenSummary()
        {
            if (f7.IsDisposed)
            {
                f7 = new Form7();
            }
            if (!f7.Visible)
            {
                f7.Show();
            }
            else if (f7.WindowState == FormWindowState.Minimized)
            {
                f7.WindowState = FormWindowState.Normal;
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
                    f7.dataGridView1.Rows[0].Cells[0].Value = summaryCount + 1;
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
                        f7.dataGridView1.Rows[summaryCount].Cells[0].Value = summaryCount + 1;
                        f7.dataGridView1.Rows[summaryCount].Cells[1].Value = sel_cable;
                        f7.dataGridView1.Rows[summaryCount].Cells[2].Value = cable_length;
                        f7.dataGridView1.Rows[summaryCount].Cells[3].Value = dataGridView1.Rows[i].Cells[17].Value;
                        f7.dataGridView1.Rows[summaryCount].Cells[4].Value = Form1.dtdiameter.Rows[i].ItemArray[0];

                        summaryCount++;
                    }
                }
            }
        }

        private void RowUp()
        {
            for (int i = 1; i < 39; i++)
            {
                arrTemp[i] = Convert.ToString(dataGridView1.Rows[currentrow - 1].Cells[i].Value);
                dataGridView1.Rows[currentrow - 1].Cells[i].Value = dataGridView1.Rows[currentrow].Cells[i].Value;
                dataGridView1.Rows[currentrow].Cells[i].Value = arrTemp[i];
            }

            DataRow dataRow = Form1.dtdiameter.NewRow();
            for (int i = 0; i < 55; i++)
            {
                dataRow[i] = Form1.dtdiameter.Rows[currentrow - 1].ItemArray[i];
            }
            //dataRow[0] = Form1.dtdiameter.Rows[currentrow - 1].ItemArray[0];
            Form1.dtdiameter.Rows.Remove(Form1.dtdiameter.Rows[currentrow - 1]);
            Form1.dtdiameter.Rows.InsertAt(dataRow, currentrow);


            dataGridView1.CurrentCell = dataGridView1.Rows[currentrow - 1].Cells[dataGridView1.CurrentCell.ColumnIndex];
            dataGridView1.ClearSelection();
            dataGridView1.CurrentCell.Selected = true;
        }

        private void RowDown()
        {
            for (int i = 1; i < 39; i++)
            {
                arrTemp[i] = Convert.ToString(dataGridView1.Rows[currentrow + 1].Cells[i].Value);
                dataGridView1.Rows[currentrow + 1].Cells[i].Value = dataGridView1.Rows[currentrow].Cells[i].Value;
                dataGridView1.Rows[currentrow].Cells[i].Value = arrTemp[i];
            }


            DataRow dataRow = Form1.dtdiameter.NewRow();
            for (int i = 0; i < 55; i++)
            {
                dataRow[i] = Form1.dtdiameter.Rows[currentrow + 1].ItemArray[i];
                // dataRow[0] = Form1.dtdiameter.Rows[currentrow + 1].ItemArray[0];
            }
            Form1.dtdiameter.Rows.Remove(Form1.dtdiameter.Rows[currentrow + 1]);
            Form1.dtdiameter.Rows.InsertAt(dataRow, currentrow);


            dataGridView1.CurrentCell = dataGridView1.Rows[currentrow + 1].Cells[dataGridView1.CurrentCell.ColumnIndex];
            dataGridView1.ClearSelection();
            dataGridView1.CurrentCell.Selected = true;
        }

        private void BtnDeleteAll_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Delete all data?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                clearTable();
            }
            Update_summary();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell.Selected)
            {
                Form1.dtdiameter.Rows.RemoveAt(currentrow);
                dataGridView1.Rows.RemoveAt(currentrow);
                for (int i = currentrow; i < Form1.j; i++)
                {
                    dataGridView1.Rows[i].Cells[0].Value = i + 1;
                }
                Form1.j--;


            }
            if ((dataGridView1.CurrentCell != null) && dataGridView1.CurrentCell.Selected)
            {
                btnDelete.Enabled = true;
                //turn btnUp on/off
                if (currentrow > 0)
                {
                    btnUp.Enabled = true;
                }
                else
                {
                    btnUp.Enabled = false;
                }

                //turn btnDown on/off
                if (currentrow < Form1.j)
                {
                    btnDown.Enabled = true;
                }
                else
                {
                    btnDown.Enabled = false;
                }
            }
            else
            {
                btnDelete.Enabled = false;
                btnUp.Enabled = false;
                btnDown.Enabled = false;
            }
            Update_summary();
        }

        private void BtnUp_Click(object sender, EventArgs e)
        {
            if (currentrow > 0)
            {
                RowUp();
            }

            //turn btnUp on/off
            if (dataGridView1.CurrentCell.RowIndex > 0)
            {
                btnUp.Enabled = true;
            }
            else
            {
                btnUp.Enabled = false;
            }
            Update_summary();
        }

        private void BtnDown_Click(object sender, EventArgs e)
        {
            if (currentrow < Form1.j)
            {
                RowDown();
            }

            //turn btnDown on/off
            if (dataGridView1.CurrentCell.RowIndex < Form1.j)
            {
                btnDown.Enabled = true;
            }
            else
            {
                btnDown.Enabled = false;
            }
            Update_summary();
        }

        private void SummaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenSummary();
        }
    }
}