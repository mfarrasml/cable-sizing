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
    public partial class Form5 : GradientForm
    {
        //variable
        public static int currentrow;
        public static bool cancelexit;

        public static string savefile = "";

        public static Form7 f7 = new Form7();

        public static int summaryCount = 0;

        string[] arrTemp = new string[40];

        public static bool toolbarTextActivated;
        public static bool toolbarDescShown;
        internal static int Standard; //1 = IEC, 2 = NEC

        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            dataGridView1.DoubleBuffered(true);

            toolbarTextActivated = Properties.Settings.Default.ToolbarTextProperties;
            //set toolbar text status based on saved settings
            if (toolbarTextActivated)
            {
                toolbarMenuDescriptionToolStripMenuItem.Checked = true;
                btnUp.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                btnDown.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                btnDelete.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                btnDeleteAll.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                editRow.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                clipboardCopy.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            }
            else
            {

                toolbarMenuDescriptionToolStripMenuItem.Checked = false;
                btnUp.DisplayStyle = ToolStripItemDisplayStyle.Image;
                btnDown.DisplayStyle = ToolStripItemDisplayStyle.Image;
                btnDelete.DisplayStyle = ToolStripItemDisplayStyle.Image;
                btnDeleteAll.DisplayStyle = ToolStripItemDisplayStyle.Image;
                editRow.DisplayStyle = ToolStripItemDisplayStyle.Image;
                clipboardCopy.DisplayStyle = ToolStripItemDisplayStyle.Image;
            }

            toolbarDescShown = Properties.Settings.Default.ToolbarDescriptionProperties;
            if(toolbarDescShown)
            {
                toolStrip1.Visible = true;
                toolbarDescShown = true;
                showToolStripMenuItem.Text = "Hide Toolbar";
            }
            else //toolstrip1 is close (!visible)
            {
                toolStrip1.Visible = false;
                toolbarDescShown = false;
                showToolStripMenuItem.Text = "Show Toolbar";
            }
        }

        private void Form5_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((e.CloseReason == CloseReason.UserClosing) && !OpenForm.formMainClose)
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
            ToolsAndButtonsEnabler();
        }

        private void ToolsAndButtonsEnabler()
        {
            if ((dataGridView1.CurrentCell != null) && (dataGridView1.CurrentCell.Selected))
            {
                currentrow = dataGridView1.CurrentCell.RowIndex;

                //enable row delete
                btnDelete.Enabled = true;
                deleteRowToolStripMenuItem.Enabled = true;

                //enable row edit
                editRow.Enabled = true;
                editRowDataToolStripMenuItem.Enabled = true;

                //turn btnUp on/off
                if (currentrow > 0)
                {
                    btnUp.Enabled = true;
                    moveRowUpToolStripMenuItem.Enabled = true;
                }
                else
                {
                    btnUp.Enabled = false;
                    moveRowUpToolStripMenuItem.Enabled = false;
                }

                //turn btnDown on/off
                if (currentrow < Form1.j)
                {
                    btnDown.Enabled = true;
                    moveRowDownToolStripMenuItem.Enabled = true;
                }
                else
                {
                    btnDown.Enabled = false;
                    moveRowDownToolStripMenuItem.Enabled = false;
                }
            }
            else
            {
                btnDelete.Enabled = false;
                deleteRowToolStripMenuItem.Enabled = false;
                editRow.Enabled = false;
                editRowDataToolStripMenuItem.Enabled = false;
                btnUp.Enabled = false;
                btnDown.Enabled = false;
            }
        }
        private void Button2_Click(object sender, EventArgs e)
        {
            CopyAll();
        }

        private void CopyAll()
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
            OpenFileDecide();
        }

        public void OpenFileDecide()
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
            if (Standard == 1)
            {
                ofd.Filter = "IEC|*.iec";
            }
            else if (Standard == 2)
            {
                ofd.Filter = "NEC|*.nec";
            }
            
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

                    //NOTE: ALL '.' and ',' still got changed, TODO: Change decimal separator replacing to only decimal data
                    foreach (DataRow row in Form1.dtdiameter.Rows)
                    {
                        if (Form1.decimalseparator == ',')
                        {
                            for (int i = 0; i < Form1.dtdiameter.Columns.Count; i++)
                            {
                                row[i] = Convert.ToString(row[i]).Replace('.', ',');
                            }
                        }
                        else
                        {
                            for (int i = 0; i < Form1.dtdiameter.Columns.Count; i++)
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

                            if ((k == 8) || ((k >= 10) && (k <= 16)) || ((k >= 18) && (k <= 24)) ||
                                ((k >= 26) && (k <= 37)))
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
                    if (Form1.OpenFromMain)
                    {
                        Form1.FileOpened = true;
                    }
                    else
                    {
                        Form1.FileOpened = false;
                    }
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
                            ((cell.ColumnIndex >= 18) && (cell.ColumnIndex <= 24)) || ((cell.ColumnIndex >= 26) && (cell.ColumnIndex <= 37)))
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


            //NOTE: ALL '.' and ',' still got changed, TODO: Change decimal separator replacing to only decimal data
            // matching all decimal separator as '.' in xml file
            foreach (DataRow row in dd.Rows)
            {
                for (int i = 0; i < Form1.dtdiameter.Columns.Count; i++)
                {
                    row[i] = Convert.ToString(row[i]).Replace(',', '.');
                }
            }

            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            ds.Tables.Add(dd);

            SaveFileDialog sfd = new SaveFileDialog();
            if (Standard == 1)
            {
                sfd.Filter = "IEC|*.iec";
            }
            else if (Standard == 2)
            {
                sfd.Filter = "NEC|*.nec";
            }
            
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    XmlTextWriter xmlSave = new XmlTextWriter(sfd.FileName, Encoding.UTF8);
                    xmlSave.Formatting = Formatting.Indented;
                    if (Standard == 1)
                    {
                        ds.DataSetName = "IEC_Cable_Data";
                    }
                    else if (Standard == 2)
                    {
                        ds.DataSetName = "NEC_Cable_Data";
                    }

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
                            ((cell.ColumnIndex >= 18) && (cell.ColumnIndex <= 24)) || ((cell.ColumnIndex >= 26) && (cell.ColumnIndex <= 37)))
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

            //NOTE: ALL '.' and ',' still got changed, TODO: Change decimal separator replacing to only decimal data
            // matching all decimal separator as '.' in xml file
            foreach (DataRow row in dd.Rows)
            {
                for (int i = 0; i < Form1.dtdiameter.Columns.Count; i++)
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
                if (Standard == 1)
                {
                    ds.DataSetName = "IEC_Cable_Data";
                }
                else if (Standard == 2)
                {
                    ds.DataSetName = "NEC_Cable_Data";
                }
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
            Print_Table();
        }

        private void Print_Table()
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


            for (int i = 0; i < 40; i++)
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
                sel_cable = Convert.ToString(dataGridView1.Rows[i].Cells[38].Value);
                sel_cable = sel_cable.Replace(Convert.ToString(dataGridView1.Rows[i].Cells[17].Value) + "  ×  ", "");
                cable_length = (Convert.ToDouble(dataGridView1.Rows[i].Cells[17].Value) * Convert.ToDouble(dataGridView1.Rows[i].Cells[27].Value));

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
            for (int i = 1; i < 40; i++)
            {
                arrTemp[i] = Convert.ToString(dataGridView1.Rows[currentrow - 1].Cells[i].Value);
                dataGridView1.Rows[currentrow - 1].Cells[i].Value = dataGridView1.Rows[currentrow].Cells[i].Value;
                dataGridView1.Rows[currentrow].Cells[i].Value = arrTemp[i];
            }

            DataRow dataRow = Form1.dtdiameter.NewRow();
            for (int i = 0; i < Form1.dtdiameter.Columns.Count; i++)
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
            for (int i = 1; i < 40; i++)
            {
                arrTemp[i] = Convert.ToString(dataGridView1.Rows[currentrow + 1].Cells[i].Value);
                dataGridView1.Rows[currentrow + 1].Cells[i].Value = dataGridView1.Rows[currentrow].Cells[i].Value;
                dataGridView1.Rows[currentrow].Cells[i].Value = arrTemp[i];
            }


            DataRow dataRow = Form1.dtdiameter.NewRow();
            for (int i = 0; i < Form1.dtdiameter.Columns.Count; i++)
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
            DeleteAll();
            Update_summary();
        }

        private void DeleteAll()
        {
            DialogResult dialogResult = MessageBox.Show("Delete all data?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                clearTable();
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            DeleteRow();
            Update_summary();
        }

        private void DeleteRow()
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
            // revalidate buttons/toolbar menu enabled status
            ToolsAndButtonsEnabler();
        }

        private void BtnUp_Click(object sender, EventArgs e)
        {
            EditRowUp();
        }

        private void EditRowUp()
        {
            if (currentrow > 0)
            {
                RowUp();
            }
            //turn btnUp on/off
            if (dataGridView1.CurrentCell.RowIndex > 0)
            {
                btnUp.Enabled = true;
                moveRowUpToolStripMenuItem.Enabled = true;
            }
            else
            {
                btnUp.Enabled = false;
                moveRowUpToolStripMenuItem.Enabled = false;
            }

            Update_summary();
        }

        private void BtnDown_Click(object sender, EventArgs e)
        {
            EditRowDown();
        }

        private void EditRowDown()
        {
            if (currentrow < Form1.j)
            {
                RowDown();
            }

            //turn btnDown on/off
            if (dataGridView1.CurrentCell.RowIndex < Form1.j)
            {
                btnDown.Enabled = true;
                moveRowDownToolStripMenuItem.Enabled = true;
            }
            else
            {
                btnDown.Enabled = false;
                moveRowDownToolStripMenuItem.Enabled = false;
            }
            Update_summary();
        }

        private void SummaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenSummary();
        }

        private void ClipboardCopy_Click(object sender, EventArgs e)
        {
            CopyAll();
        }

        private void MoveRowUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditRowUp();
        }

        private void MoveRowDownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditRowDown();
        }

        private void DeleteRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteRow();
            Update_summary();
        }

        private void DeleteAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteAll();
            Update_summary();
        }

        private void CopyToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyAll();
        }

        private void ShowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (toolbarDescShown)
            {
                toolStrip1.Visible = false;
                toolbarDescShown = false;
                showToolStripMenuItem.Text = "Show Toolbar";
            }
            else //toolstrip1 is close (!visible)
            {
                toolStrip1.Visible = true;
                toolbarDescShown = true;
                showToolStripMenuItem.Text = "Hide Toolbar";
            }
            Properties.Settings.Default.ToolbarDescriptionProperties = toolbarDescShown;
        }

        private void ToolbarMenuDescriptionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeToolbarTextStatus();
        }

        private void ChangeToolbarTextStatus()
        {
            if (toolbarTextActivated)
            {
                toolbarTextActivated = false;
                toolbarMenuDescriptionToolStripMenuItem.Checked = false;
                btnUp.DisplayStyle = ToolStripItemDisplayStyle.Image;
                btnDown.DisplayStyle = ToolStripItemDisplayStyle.Image;
                btnDelete.DisplayStyle = ToolStripItemDisplayStyle.Image;
                btnDeleteAll.DisplayStyle = ToolStripItemDisplayStyle.Image;
                editRow.DisplayStyle = ToolStripItemDisplayStyle.Image;
                clipboardCopy.DisplayStyle = ToolStripItemDisplayStyle.Image;
            }
            else
            {
                toolbarTextActivated = true;
                toolbarMenuDescriptionToolStripMenuItem.Checked = true;
                btnUp.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                btnDown.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                btnDelete.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                btnDeleteAll.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                editRow.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                clipboardCopy.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            }
            Properties.Settings.Default.ToolbarTextProperties = toolbarTextActivated;
        }

        private void PrintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Print_Table();
        }

        private void CloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}