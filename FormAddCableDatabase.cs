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

namespace Test1
{
    public partial class FormAddCableDatabase : Form
    {
        // Variables

        public static DataTable dtXLPE2;
        public static DataTable dtXLPE3;
        public static DataTable dtXLPE4;
        public static DataTable dtPVC2;
        public static DataTable dtPVC3;
        public static DataTable dtPVC4;

        public static DataTable dtXLPE2final;
        public static DataTable dtXLPE3final;
        public static DataTable dtXLPE4final;
        public static DataTable dtPVC2final;
        public static DataTable dtPVC3final;
        public static DataTable dtPVC4final;

        int noCores;
        string insulation, conductor;
        string saveName;

        bool inValid;

        int XLPE2, XLPE3, XLPE4, PVC2, PVC3, PVC4;

        bool IECStandard, NECStandard;

        double[] cablesize = new double[17];

        public FormAddCableDatabase()
        {
            InitializeComponent();
        }

        private void FormAddCableDatabase_Load(object sender, EventArgs e)
        {
            dtXLPE2 = new DataTable();
            dtXLPE3 = new DataTable();
            dtXLPE4 = new DataTable();
            dtPVC2 = new DataTable();
            dtPVC3 = new DataTable();
            dtPVC4 = new DataTable();

            //initializing cable size data

            cablesize[0] = 1.5;
            cablesize[1] = 2.5;
            cablesize[2] = 4;
            cablesize[3] = 6;
            cablesize[4] = 10;
            cablesize[5] = 16;
            cablesize[6] = 25;
            cablesize[7] = 35;
            cablesize[8] = 50;
            cablesize[9] = 70;
            cablesize[10] = 95;
            cablesize[11] = 120;
            cablesize[12] = 150;
            cablesize[13] = 185;
            cablesize[14] = 240;
            cablesize[15] = 300;
            cablesize[16] = 400;

            //initializing XLPE2 table column
            dtXLPE2.Columns.Add("Size (mm²)");
            dtXLPE2.Columns.Add("AC Resistance at 90°C(Ω/km)");
            dtXLPE2.Columns.Add("Reactance at 50Hz(Ω/km)");
            dtXLPE2.Columns.Add("DC Resistance at 90°C(Ω/km)");
            dtXLPE2.Columns.Add("Under Ground CCC at 20°C(A)");
            dtXLPE2.Columns.Add("Above Ground CCC at 30°C(A)");

            dtXLPE2.Columns[0].ColumnName = "Size";
            dtXLPE2.Columns[1].ColumnName = "AC_Resistance";
            dtXLPE2.Columns[2].ColumnName = "Reactance";
            dtXLPE2.Columns[3].ColumnName = "DC_Resistance";
            dtXLPE2.Columns[4].ColumnName = "UG_CCC";
            dtXLPE2.Columns[5].ColumnName = "AG_CCC";

            dtXLPE2.TableName = "XLPE_2CORE";


            //initializing XLPE3 table column
            dtXLPE3.Columns.Add("Size (mm²)");
            dtXLPE3.Columns.Add("AC Resistance at 90°C(Ω/km)");
            dtXLPE3.Columns.Add("Reactance at 50Hz(Ω/km)");
            dtXLPE3.Columns.Add("DC Resistance at 90°C(Ω/km)");
            dtXLPE3.Columns.Add("Under Ground CCC at 20°C(A)");
            dtXLPE3.Columns.Add("Above Ground CCC at 30°C(A)");

            dtXLPE3.Columns[0].ColumnName = "Size";
            dtXLPE3.Columns[1].ColumnName = "AC_Resistance";
            dtXLPE3.Columns[2].ColumnName = "Reactance";
            dtXLPE3.Columns[3].ColumnName = "DC_Resistance";
            dtXLPE3.Columns[4].ColumnName = "UG_CCC";
            dtXLPE3.Columns[5].ColumnName = "AG_CCC";

            dtXLPE3.TableName = "XLPE_3CORE";

            //initializing XLPE4 table column
            dtXLPE4.Columns.Add("Size (mm²)");
            dtXLPE4.Columns.Add("AC Resistance at 90°C(Ω/km)");
            dtXLPE4.Columns.Add("Reactance at 50Hz(Ω/km)");
            dtXLPE4.Columns.Add("DC Resistance at 90°C(Ω/km)");
            dtXLPE4.Columns.Add("Under Ground CCC at 20°C(A)");
            dtXLPE4.Columns.Add("Above Ground CCC at 30°C(A)");

            dtXLPE4.Columns[0].ColumnName = "Size";
            dtXLPE4.Columns[1].ColumnName = "AC_Resistance";
            dtXLPE4.Columns[2].ColumnName = "Reactance";
            dtXLPE4.Columns[3].ColumnName = "DC_Resistance";
            dtXLPE4.Columns[4].ColumnName = "UG_CCC";
            dtXLPE4.Columns[5].ColumnName = "AG_CCC";

            dtXLPE4.TableName = "XLPE_4CORE";


            //initializing PVC2 table column
            dtPVC2.Columns.Add("Size (mm²)");
            dtPVC2.Columns.Add("AC Resistance at 70°C(Ω/km)");
            dtPVC2.Columns.Add("Reactance at 50Hz(Ω/km)");
            dtPVC2.Columns.Add("DC Resistance at 70°C(Ω/km)");
            dtPVC2.Columns.Add("Under Ground CCC at 20°C(A)");
            dtPVC2.Columns.Add("Above Ground CCC at 30°C(A)");

            dtPVC2.Columns[0].ColumnName = "Size";
            dtPVC2.Columns[1].ColumnName = "AC_Resistance";
            dtPVC2.Columns[2].ColumnName = "Reactance";
            dtPVC2.Columns[3].ColumnName = "DC_Resistance";
            dtPVC2.Columns[4].ColumnName = "UG_CCC";
            dtPVC2.Columns[5].ColumnName = "AG_CCC";

            dtPVC2.TableName = "PVC_2CORE";

            //initializing PVC3 table column
            dtPVC3.Columns.Add("Size (mm²)");
            dtPVC3.Columns.Add("AC Resistance at 70°C(Ω/km)");
            dtPVC3.Columns.Add("Reactance at 50Hz(Ω/km)");
            dtPVC3.Columns.Add("DC Resistance at 70°C(Ω/km)");
            dtPVC3.Columns.Add("Under Ground CCC at 20°C(A)");
            dtPVC3.Columns.Add("Above Ground CCC at 30°C(A)");

            dtPVC3.Columns[0].ColumnName = "Size";
            dtPVC3.Columns[1].ColumnName = "AC_Resistance";
            dtPVC3.Columns[2].ColumnName = "Reactance";
            dtPVC3.Columns[3].ColumnName = "DC_Resistance";
            dtPVC3.Columns[4].ColumnName = "UG_CCC";
            dtPVC3.Columns[5].ColumnName = "AG_CCC";

            dtPVC3.TableName = "PVC_3CORE";

            //initializing PVC4 table column
            dtPVC4.Columns.Add("Size (mm²)");
            dtPVC4.Columns.Add("AC Resistance at 70°C(Ω/km)");
            dtPVC4.Columns.Add("Reactance at 50Hz(Ω/km)");
            dtPVC4.Columns.Add("DC Resistance at 70°C(Ω/km)");
            dtPVC4.Columns.Add("Under Ground CCC at 20°C(A)");
            dtPVC4.Columns.Add("Above Ground CCC at 30°C(A)");

            dtPVC4.Columns[0].ColumnName = "Size";
            dtPVC4.Columns[1].ColumnName = "AC_Resistance";
            dtPVC4.Columns[2].ColumnName = "Reactance";
            dtPVC4.Columns[3].ColumnName = "DC_Resistance";
            dtPVC4.Columns[4].ColumnName = "UG_CCC";
            dtPVC4.Columns[5].ColumnName = "AG_CCC";

            dtPVC4.TableName = "PVC_4CORE";


            //initializing database table column
            LoadDataViewColumn();


            //initialize wiresizes

            for (int i = 0; i < 17; i++)
            {
                dtXLPE2.Rows.Add(cablesize[i]);
                dtXLPE3.Rows.Add(cablesize[i]);
                dtXLPE4.Rows.Add(cablesize[i]);
            }

            for (int i = 0; i < 16; i++)
            {
                dtPVC2.Rows.Add(cablesize[i]);
                dtPVC3.Rows.Add(cablesize[i]);
                dtPVC4.Rows.Add(cablesize[i]);
            }

            //checking standard used
            if (radioButtonIEC.Checked)
            {
                IECStandard = true;
                NECStandard = false;
            }
            else
            {
                IECStandard = false;
                NECStandard = true;
            }

            //checking standard used
            if (radioButtonIEC.Checked)
            {
                IECSelected = true;
                NECSelected = false;
            }
            else
            {
                IECSelected = false;
                NECSelected = true;
            }

            //initialize datagridview properties
            dataGridView1.DoubleBuffered(true);
            dataGridView2.DoubleBuffered(true);


            //initialize edit/view tab
            LoadIECDatabase();
            comboBoxDatabase.SelectedIndex = 0;

        }


        //initializing table view/edit columns
        private void LoadDataViewColumn()
        {
            // LOADING DATA TABLES FOR IEC DATABASE
            //
            //

            dtXLPE2view = new DataTable();
            dtXLPE3view = new DataTable();
            dtXLPE4view = new DataTable();
            dtPVC2view = new DataTable();
            dtPVC3view = new DataTable();
            dtPVC4view = new DataTable();

            //Initializing view/edit database columns
            //initializing XLPE2 table column
            dtXLPE2view.Columns.Add("Size (mm²)");
            dtXLPE2view.Columns.Add("AC Resistance at 90°C(Ω/km)");
            dtXLPE2view.Columns.Add("Reactance at 50Hz(Ω/km)");
            dtXLPE2view.Columns.Add("DC Resistance at 90°C(Ω/km)");
            dtXLPE2view.Columns.Add("Under Ground CCC at 20°C(A)");
            dtXLPE2view.Columns.Add("Above Ground CCC at 30°C(A)");

            dtXLPE2view.Columns[0].ColumnName = "Size";
            dtXLPE2view.Columns[1].ColumnName = "AC_Resistance";
            dtXLPE2view.Columns[2].ColumnName = "Reactance";
            dtXLPE2view.Columns[3].ColumnName = "DC_Resistance";
            dtXLPE2view.Columns[4].ColumnName = "UG_CCC";
            dtXLPE2view.Columns[5].ColumnName = "AG_CCC";

            dtXLPE2.TableName = "XLPE_2CORE";


            //initializing XLPE3 table column
            dtXLPE3view.Columns.Add("Size (mm²)");
            dtXLPE3view.Columns.Add("AC Resistance at 90°C(Ω/km)");
            dtXLPE3view.Columns.Add("Reactance at 50Hz(Ω/km)");
            dtXLPE3view.Columns.Add("DC Resistance at 90°C(Ω/km)");
            dtXLPE3view.Columns.Add("Under Ground CCC at 20°C(A)");
            dtXLPE3view.Columns.Add("Above Ground CCC at 30°C(A)");

            dtXLPE3view.Columns[0].ColumnName = "Size";
            dtXLPE3view.Columns[1].ColumnName = "AC_Resistance";
            dtXLPE3view.Columns[2].ColumnName = "Reactance";
            dtXLPE3view.Columns[3].ColumnName = "DC_Resistance";
            dtXLPE3view.Columns[4].ColumnName = "UG_CCC";
            dtXLPE3view.Columns[5].ColumnName = "AG_CCC";

            dtXLPE3view.TableName = "XLPE_3CORE";

            //initializing XLPE4 table column
            dtXLPE4view.Columns.Add("Size (mm²)");
            dtXLPE4view.Columns.Add("AC Resistance at 90°C(Ω/km)");
            dtXLPE4view.Columns.Add("Reactance at 50Hz(Ω/km)");
            dtXLPE4view.Columns.Add("DC Resistance at 90°C(Ω/km)");
            dtXLPE4view.Columns.Add("Under Ground CCC at 20°C(A)");
            dtXLPE4view.Columns.Add("Above Ground CCC at 30°C(A)");

            dtXLPE4view.Columns[0].ColumnName = "Size";
            dtXLPE4view.Columns[1].ColumnName = "AC_Resistance";
            dtXLPE4view.Columns[2].ColumnName = "Reactance";
            dtXLPE4view.Columns[3].ColumnName = "DC_Resistance";
            dtXLPE4view.Columns[4].ColumnName = "UG_CCC";
            dtXLPE4view.Columns[5].ColumnName = "AG_CCC";

            dtXLPE4view.TableName = "XLPE_4CORE";


            //initializing PVC2 table column
            dtPVC2view.Columns.Add("Size (mm²)");
            dtPVC2view.Columns.Add("AC Resistance at 70°C(Ω/km)");
            dtPVC2view.Columns.Add("Reactance at 50Hz(Ω/km)");
            dtPVC2view.Columns.Add("DC Resistance at 70°C(Ω/km)");
            dtPVC2view.Columns.Add("Under Ground CCC at 20°C(A)");
            dtPVC2view.Columns.Add("Above Ground CCC at 30°C(A)");

            dtPVC2view.Columns[0].ColumnName = "Size";
            dtPVC2view.Columns[1].ColumnName = "AC_Resistance";
            dtPVC2view.Columns[2].ColumnName = "Reactance";
            dtPVC2view.Columns[3].ColumnName = "DC_Resistance";
            dtPVC2view.Columns[4].ColumnName = "UG_CCC";
            dtPVC2view.Columns[5].ColumnName = "AG_CCC";

            dtPVC2view.TableName = "PVC_2CORE";

            //initializing PVC3 table column
            dtPVC3view.Columns.Add("Size (mm²)");
            dtPVC3view.Columns.Add("AC Resistance at 70°C(Ω/km)");
            dtPVC3view.Columns.Add("Reactance at 50Hz(Ω/km)");
            dtPVC3view.Columns.Add("DC Resistance at 70°C(Ω/km)");
            dtPVC3view.Columns.Add("Under Ground CCC at 20°C(A)");
            dtPVC3view.Columns.Add("Above Ground CCC at 30°C(A)");

            dtPVC3view.Columns[0].ColumnName = "Size";
            dtPVC3view.Columns[1].ColumnName = "AC_Resistance";
            dtPVC3view.Columns[2].ColumnName = "Reactance";
            dtPVC3view.Columns[3].ColumnName = "DC_Resistance";
            dtPVC3view.Columns[4].ColumnName = "UG_CCC";
            dtPVC3view.Columns[5].ColumnName = "AG_CCC";

            dtPVC3view.TableName = "PVC_3CORE";

            //initializing PVC4 table column
            dtPVC4view.Columns.Add("Size (mm²)");
            dtPVC4view.Columns.Add("AC Resistance at 70°C(Ω/km)");
            dtPVC4view.Columns.Add("Reactance at 50Hz(Ω/km)");
            dtPVC4view.Columns.Add("DC Resistance at 70°C(Ω/km)");
            dtPVC4view.Columns.Add("Under Ground CCC at 20°C(A)");
            dtPVC4view.Columns.Add("Above Ground CCC at 30°C(A)");

            dtPVC4view.Columns[0].ColumnName = "Size";
            dtPVC4view.Columns[1].ColumnName = "AC_Resistance";
            dtPVC4view.Columns[2].ColumnName = "Reactance";
            dtPVC4view.Columns[3].ColumnName = "DC_Resistance";
            dtPVC4view.Columns[4].ColumnName = "UG_CCC";
            dtPVC4view.Columns[5].ColumnName = "AG_CCC";

            dtPVC4view.TableName = "PVC_4CORE";

            //LOADING DATATABLE FOR NEC DATABASE
        }

        private void ComboBoxInsulation_SelectedIndexChanged(object sender, EventArgs e)
        {
            insulation = comboBoxInsulation.Text;
            EnableSave();
            ChooseDatabase();
        }

        private void ComboBoxCores_SelectedIndexChanged(object sender, EventArgs e)
        {
            noCores = int.Parse(comboBoxCores.Text);
            EnableSave();
            ChooseDatabase();
        }

        private void ComboBoxConductor_SelectedIndexChanged(object sender, EventArgs e)
        {
            conductor = comboBoxConductor.Text;
            EnableSave();
            ChooseDatabase();
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            saveName = textBoxName.Text;

            if ((saveName != "") && IECStandard)
            {
                dtXLPE2final = new DataTable();
                dtXLPE3final = new DataTable();
                dtXLPE4final = new DataTable();
                dtPVC2final = new DataTable();
                dtPVC3final = new DataTable();
                dtPVC4final = new DataTable();

                
                inValid = cekValidasiTable();
                if (!inValid)
                {
                    checkAvailability();
                    finalCableData();
                    if ((dtXLPE2final.Rows.Count > 0) || (dtXLPE3final.Rows.Count > 0) || (dtXLPE4final.Rows.Count > 0) || (dtPVC2final.Rows.Count > 0) 
                        || (dtPVC3final.Rows.Count > 0) || (dtPVC4final.Rows.Count > 0))
                    {
                        var systemPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                        Directory.CreateDirectory(systemPath + "/Cable Sizing/IEC_database");
                        var saveDir = Path.Combine(systemPath + "/Cable Sizing/IEC_database", saveName + ".xml");

                        if (!File.Exists(saveDir))
                        {
                            SaveIECDatabase();
                        }
                        else //file with the same name detected
                        {
                            DialogResult dr = MessageBox.Show("File with the same name already exist, want to overwrite the file?", "Cable Sizing",
                                MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                            if (dr == DialogResult.OK)
                            {
                                SaveIECDatabase();
                                LoadIECDatabase();
                            }
                            else
                            {
                                MessageBox.Show("Failed to save data!", "Cable Sizing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        
                    }
                    else
                    {
                        MessageBox.Show("Cable Database is Empty!", "Database Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Database input invalid, some rows have incomplete data!\n\nHint: Every row of data must be filled entirely or left empty", "Database Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SaveIECDatabase()
        {
            var systemPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            Directory.CreateDirectory(systemPath + "/Cable Sizing/IEC_database");
            var saveDir = Path.Combine(systemPath + "/Cable Sizing/IEC_database", saveName + ".xml");

            DataSet ds = new DataSet();
            ds.Tables.Add(dtXLPE2final);
            ds.Tables.Add(dtXLPE3final);
            ds.Tables.Add(dtXLPE4final);
            ds.Tables.Add(dtPVC2final);
            ds.Tables.Add(dtPVC3final);
            ds.Tables.Add(dtPVC4final);

            XmlTextWriter xmlSave = new XmlTextWriter(saveDir, Encoding.UTF8);
            xmlSave.Formatting = Formatting.Indented;
            ds.DataSetName = "Cable_Database";
            ds.WriteXml(xmlSave);
            xmlSave.Close();

            MessageBox.Show("Data Saved!", "Cable Database", MessageBoxButtons.OK, MessageBoxIcon.Information);

            /*XmlTextWriter xmlSave = new XmlTextWriter("D:" + saveName, Encoding.UTF8);
            ds.WriteXml(xmlSave);
            xmlSave.Close();*/
        }


        private void DataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                e.Control.KeyPress += new KeyPressEventHandler(DataGridView1_KeyPress);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void RadioButtonIEC_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonIEC.Checked)
            {
                IECStandard = true;
                NECStandard = false;
            }
            else
            {
                IECStandard = false;
                NECStandard = true;
            }
        }

        private void DataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex > -1)
            {
                //decimal and separator input only
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator)))
                {
                    e.Handled = true;
                }
            }

        }

        private void DataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            //making sure there are no non decimal input by deleting cell when non decimal value is detected
            double Number;
            if (!double.TryParse(Text, out Number))
            {
                Text = "";
            }
        }

        private void TextBoxName_TextChanged(object sender, EventArgs e)
        {
            EnableSave();
        }

        private void ChooseDatabase()
        {
            if (conductor == "Copper")
            {
                if (insulation == "XLPE")
                {
                    switch (noCores)
                    {
                        case 2:
                            //bound datagridview to datatable
                            dataGridView1.DataSource = dtXLPE2;
                            SetDGVRuntimeProperties(dataGridView1);
                            break;
                        case 3:
                            //bound datagridview to datatable
                            dataGridView1.DataSource = dtXLPE3;
                            SetDGVRuntimeProperties(dataGridView1);
                            break;
                        case 4:
                            //bound datagridview to datatable
                            dataGridView1.DataSource = dtXLPE4;
                            SetDGVRuntimeProperties(dataGridView1);
                            break;
                        default:
                            dataGridView1.DataSource = null;
                            break;
                    }
                }
                else if (insulation == "PVC")
                {
                    switch (noCores)
                    {
                        case 2:
                            //bound datagridview to datatable
                            dataGridView1.DataSource = dtPVC2;
                            SetDGVRuntimeProperties(dataGridView1);
                            break;
                        case 3:
                            //bound datagridview to datatable
                            dataGridView1.DataSource = dtPVC3;
                            SetDGVRuntimeProperties(dataGridView1);
                            break;
                        case 4:
                            //bound datagridview to datatable
                            dataGridView1.DataSource = dtPVC4;
                            SetDGVRuntimeProperties(dataGridView1);
                            break;
                        default:
                            dataGridView1.DataSource = null;
                            break;
                    }

                }
            }
            else
            {
                dataGridView1.DataSource = null;
            }
        }

        //Disable DataGridView Column Sorting and makes first column (wire size data) Read only 
        private void SetDGVRuntimeProperties(DataGridView dgv)
        {
            //disable column sorting
            for (int i = 0; i < 6; i++)
            {
                dgv.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            //disable changing the wire size data
            dgv.Columns[0].ReadOnly = true;
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void DataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            double Number;

            //check if data that the user input is valid, delete if it's not
            if (dataGridView1.CurrentCell.Selected)
            {
                if (!double.TryParse(Convert.ToString(dataGridView1.CurrentCell.Value), out Number))
                {
                    dataGridView1.CurrentCell.Value = null;
                }
            }
        }

        private void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            double Number;

            if (dataGridView1.CurrentCell.Selected)
            {
                if (!double.TryParse(Convert.ToString(dataGridView1.CurrentCell.Value), out Number))
                {
                    dataGridView1.CurrentCell.Value = null;
                }

            }
        }

        //Enable/disable save button
        private void EnableSave()
        {
            if ((textBoxName.Text != "") && (comboBoxInsulation.Text != "") && (comboBoxConductor.Text != "") && (comboBoxCores.Text != ""))
            {
                buttonSave.Enabled = true;
            }
            else
            {
                buttonSave.Enabled = false;
            }
        }



        //
        //
        //VALIDATION FUNCTIONS
        //
        //

        //variables
        bool[,] datAvailable = new bool[17, 6];

        private void checkAvailability()
        {
            bool terisi;
            //XLPE2CORE
            for (int i = 0; i < 17; i++)
            {
                int j = 0;
                terisi = true;
                while (j < 6)
                {
                    if (Convert.ToString(dtXLPE2.Rows[i].ItemArray[j]) == "")
                    {
                        terisi = false;
                    }
                    j++;
                }
                if (terisi)
                {
                    datAvailable[i, 0] = true;
                }
                else
                {
                    datAvailable[i, 0] = false;
                }
            }

            //XLPE3CORE
            for (int i = 0; i < 17; i++)
            {
                int j = 0;
                terisi = true;
                while (j < 6)
                {
                    if (Convert.ToString(dtXLPE3.Rows[i].ItemArray[j]) == "")
                    {
                        terisi = false;
                    }
                    j++;
                }
                if (terisi)
                {
                    datAvailable[i, 1] = true;
                }
                else
                {
                    datAvailable[i, 1] = false;
                }
            }

            //XLPE4CORE
            for (int i = 0; i < 17; i++)
            {
                int j = 0;
                terisi = true;
                while (j < 6)
                {
                    if (Convert.ToString(dtXLPE4.Rows[i].ItemArray[j]) == "")
                    {
                        terisi = false;
                    }
                    j++;
                }
                if (terisi)
                {
                    datAvailable[i, 2] = true;
                }
                else
                {
                    datAvailable[i, 2] = false;
                }
            }

            //PVC2CORE
            for (int i = 0; i < 16; i++)
            {
                int j = 0;
                terisi = true;
                while (j < 6)
                {
                    if (Convert.ToString(dtPVC2.Rows[i].ItemArray[j]) == "")
                    {
                        terisi = false;
                    }
                    j++;
                }
                if (terisi)
                {
                    datAvailable[i, 3] = true;
                }
                else
                {
                    datAvailable[i, 3] = false;
                }
            }

            //PVC3CORE
            for (int i = 0; i < 16; i++)
            {
                int j = 0;
                terisi = true;
                while (j < 6)
                {
                    if (Convert.ToString(dtPVC3.Rows[i].ItemArray[j]) == "")
                    {
                        terisi = false;
                    }
                    j++;
                }
                if (terisi)
                {
                    datAvailable[i, 4] = true;
                }
                else
                {
                    datAvailable[i, 4] = false;
                }
            }

            //PVC4CORE
            for (int i = 0; i < 16; i++)
            {
                int j = 0;
                terisi = true;
                while (j < 6)
                {
                    if (Convert.ToString(dtPVC4.Rows[i].ItemArray[j]) == "")
                    {
                        terisi = false;
                    }
                    j++;
                }
                if (terisi)
                {
                    datAvailable[i, 5] = true;
                }
                else
                {
                    datAvailable[i, 5] = false;
                }
            }

        }

        private bool cekValidasiTable()
        {
            bool inVal;
            int count;
            inVal = false;
            int i = 0;
            int j;

            //validate XLPE2
            while ((i < 17) && !inVal)
            {
                j = 1;
                count = 0;
                while (j < 6)
                {
                    if (Convert.ToString(dtXLPE2.Rows[i].ItemArray[j]) == "")
                    {
                        count++;
                    }
                    j++;
                }
                if ((count < 5) && (count > 0))
                {
                    inVal = true;
                }
                i++;
            }

            //Validate XLPE3
            i = 0;
            while ((i < 17) && !inVal)
            {
                j = 1;
                count = 0;
                while (j < 6)
                {
                    if (Convert.ToString(dtXLPE3.Rows[i].ItemArray[j]) == "")
                    {
                        count++;
                    }
                    j++;
                }
                if ((count < 5) && (count > 0))
                {
                    inVal = true;
                }
                i++;
            }

            //Validate XLPE4
            i = 0;
            while ((i < 17) && !inVal)
            {
                j = 1;
                count = 0;
                while (j < 6)
                {
                    if (Convert.ToString(dtXLPE4.Rows[i].ItemArray[j]) == "")
                    {
                        count++;
                    }
                    j++;
                }
                if ((count < 5) && (count > 0))
                {
                    inVal = true;
                }
                i++;
            }

            //Validate PVC2
            i = 0;
            while ((i < 16) && !inVal)
            {
                j = 1;
                count = 0;
                while (j < 6)
                {
                    if (Convert.ToString(dtPVC2.Rows[i].ItemArray[j]) == "")
                    {
                        count++;
                    }
                    j++;
                }
                if ((count < 5) && (count > 0))
                {
                    inVal = true;
                }
                i++;
            }

            //Validate PVC3
            i = 0;
            while ((i < 16) && !inVal)
            {
                j = 1;
                count = 0;
                while (j < 6)
                {
                    if (Convert.ToString(dtPVC3.Rows[i].ItemArray[j]) == "")
                    {
                        count++;
                    }
                    j++;
                }
                if ((count < 5) && (count > 0))
                {
                    inVal = true;
                }
                i++;
            }

            //Validate PVC4
            i = 0;
            while ((i < 16) && !inVal)
            {
                j = 1;
                count = 0;
                while (j < 6)
                {
                    if (Convert.ToString(dtPVC4.Rows[i].ItemArray[j]) == "")
                    {
                        count++;
                    }
                    j++;
                }
                if ((count < 5) && (count > 0))
                {
                    inVal = true;
                }
                i++;
            }

            return inVal;
        }

        private void finalCableData()
        {
            int k; 

            //Save XLPE2 Cable Data
            k = 0;
            dtXLPE2final = dtXLPE2.Copy();
            for (int i = 0; i < 17; i++)
            {
                if (!datAvailable[i, 0])
                {
                    dtXLPE2final.Rows.RemoveAt(k);
                }
                else
                {
                    k++;
                }
            }

            //Save XLPE3 Cable Data
            k = 0;
            dtXLPE3final = dtXLPE3.Copy();
            for (int i = 0; i < 17; i++)
            {
                if (!datAvailable[i, 1])
                {
                    dtXLPE3final.Rows.RemoveAt(k);
                }
                else
                {
                    k++;
                }
            }

            //Save XLPE4 Cable Data
            k = 0;
            dtXLPE4final = dtXLPE4.Copy();
            for (int i = 0; i < 17; i++)
            {
                if (!datAvailable[i, 2])
                {
                    dtXLPE4final.Rows.RemoveAt(k);
                }
                else
                {
                    k++;
                }
            }

            //Save PVC2 Cable Data
            k = 0;
            dtPVC2final = dtPVC2.Copy();
            for (int i = 0; i < 16; i++)
            {
                if (!datAvailable[i, 3])
                {
                    dtPVC2final.Rows.RemoveAt(k);
                }
                else
                {
                    k++;
                }
            }

            //Save PVC3 Cable Data
            k = 0;
            dtPVC3final = dtPVC3.Copy();
            for (int i = 0; i < 16; i++)
            {
                if (!datAvailable[i, 4])
                {
                    dtPVC3final.Rows.RemoveAt(k);
                }
                else
                {
                    k++;
                }
            }

            //Save PVC4 Cable Data
            k = 0;
            dtPVC4final = dtPVC4.Copy();
            for (int i = 0; i < 16; i++)
            {
                if (!datAvailable[i, 5])
                {
                    dtPVC4final.Rows.RemoveAt(k);
                }
                else
                {
                    k++;
                }
            }
        }

        //
        //
        // TAB VIEW Database
        //
        //

        bool IECSelected;
        bool NECSelected;

        DirectoryInfo di;

        DataSet viewCableDS;
        DataTable dtXLPE2view;
        DataTable dtXLPE3view;
        DataTable dtXLPE4view;
        DataTable dtPVC2view;
        DataTable dtPVC3view;
        DataTable dtPVC4view;

        int xlpe2coreLength, xlpe3coreLength, xlpe4coreLength, pvc2coreLength, pvc3coreLength, pvc4coreLength;

        int IECFiles;


        int viewCores;
        string viewinsulation, viewconductor;


        internal static string fileName;


        private void DataGridView2_CurrentCellChanged(object sender, EventArgs e)
        {
            //making sure there are no non decimal input by deleting cell when non decimal value is detected
            double Number;
            if (!double.TryParse(Convert.ToString(dataGridView2.CurrentCell.Value), out Number))
            {
                dataGridView2.CurrentCell.Value = "";
            }
        }

        private void DataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            double Number;

            if (dataGridView2.CurrentCell.Selected)
            {
                if (!double.TryParse(Convert.ToString(dataGridView2.CurrentCell.Value), out Number))
                {
                    dataGridView2.CurrentCell.Value = null;
                }

            }
        }

        private void DataGridView2_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            double Number;

            //check if data that the user input is valid, delete if it's not
            if (dataGridView2.CurrentCell.Selected)
            {
                if (!double.TryParse(Convert.ToString(dataGridView2.CurrentCell.Value), out Number))
                {
                    dataGridView2.CurrentCell.Value = null;
                }
            }
        }

        private void ButtonCancel2_Click(object sender, EventArgs e)
        {
            buttonEdit.Text = "Edit";
            dataGridView2.ReadOnly = true;
            ReadIECDatabase();
            ChooseDatabaseView();
            comboBoxDatabase.Enabled = true;
            buttonRename.Enabled = true;
            buttonDelete.Enabled = true;
            buttonCancel2.Visible = false;
        }

        private void DataGridView2_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                e.Control.KeyPress += new KeyPressEventHandler(DataGridView2_KeyPress);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void DataGridView2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (dataGridView2.CurrentCell.ColumnIndex > -1)
            {
                //decimal and separator input only
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator)))
                {
                    e.Handled = true;
                }
            }

        }

        private void ButtonEdit_Click(object sender, EventArgs e)
        {
            if ((fileName != "") && (buttonEdit.Text == "Edit"))
            {
                buttonEdit.Text = "Save";
                buttonCancel2.Visible = true;
                comboBoxDatabase.Enabled = false;
                buttonRename.Enabled = false;
                buttonDelete.Enabled = false;
                FillDataTableEdit();

                //Make data editable
                dataGridView2.ReadOnly = false;
                dataGridView2.Columns[0].ReadOnly = true;
            }
            else if (buttonEdit.Text == "Save")
            {
                if ((fileName != "") && IECSelected)
                {
                    dtXLPE2final = new DataTable();
                    dtXLPE3final = new DataTable();
                    dtXLPE4final = new DataTable();
                    dtPVC2final = new DataTable();
                    dtPVC3final = new DataTable();
                    dtPVC4final = new DataTable();


                    inValid = CheckEditTableValidation();
                    if (!inValid)
                    {
                        CheckEditAvailability();
                        FinalCableDataEdit();
                        if ((dtXLPE2final.Rows.Count > 0) || (dtXLPE3final.Rows.Count > 0) || (dtXLPE4final.Rows.Count > 0) || (dtPVC2final.Rows.Count > 0)
                            || (dtPVC3final.Rows.Count > 0) || (dtPVC4final.Rows.Count > 0))
                        {
                            DialogResult dr = MessageBox.Show("Edit existing database?", "Confirm Edit",
                                MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                            if (dr == DialogResult.OK)
                            {
                                SaveIECDatabaseView();
                                buttonEdit.Text = "Edit";
                                dataGridView2.ReadOnly = true;
                                ReadIECDatabase();
                                ChooseDatabaseView();
                                comboBoxDatabase.Enabled = true;
                                buttonRename.Enabled = true;
                                buttonDelete.Enabled = true;
                                buttonCancel2.Visible = false;
                            }

                        }
                        else
                        {
                            MessageBox.Show("Cable Database is Empty!", "Database Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Database input invalid, some rows have incomplete data!\n\nHint: Every row of data must be filled entirely or left empty", "Database Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void SaveIECDatabaseView()
        {
            var systemPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            Directory.CreateDirectory(systemPath + "/Cable Sizing/IEC_database");
            var saveDir = Path.Combine(systemPath + "/Cable Sizing/IEC_database", fileName + ".xml");

            DataSet ds = new DataSet();
            ds.Tables.Add(dtXLPE2final);
            ds.Tables.Add(dtXLPE3final);
            ds.Tables.Add(dtXLPE4final);
            ds.Tables.Add(dtPVC2final);
            ds.Tables.Add(dtPVC3final);
            ds.Tables.Add(dtPVC4final);

            XmlTextWriter xmlSave = new XmlTextWriter(saveDir, Encoding.UTF8);
            xmlSave.Formatting = Formatting.Indented;
            ds.DataSetName = "Cable_Database";
            ds.WriteXml(xmlSave);
            xmlSave.Close();

            MessageBox.Show("Data Saved!", "Cable Database", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private bool CheckEditTableValidation()
        {
            bool inVal;
            int count;
            inVal = false;
            int i = 0;
            int j;

            //validate XLPE2
            while ((i < 17) && !inVal)
            {
                j = 1;
                count = 0;
                while (j < 6)
                {
                    if (Convert.ToString(dtXLPE2view.Rows[i].ItemArray[j]) == "")
                    {
                        count++;
                    }
                    j++;
                }
                if ((count < 5) && (count > 0))
                {
                    inVal = true;
                }
                i++;
            }

            //Validate XLPE3
            i = 0;
            while ((i < 17) && !inVal)
            {
                j = 1;
                count = 0;
                while (j < 6)
                {
                    if (Convert.ToString(dtXLPE3view.Rows[i].ItemArray[j]) == "")
                    {
                        count++;
                    }
                    j++;
                }
                if ((count < 5) && (count > 0))
                {
                    inVal = true;
                }
                i++;
            }

            //Validate XLPE4
            i = 0;
            while ((i < 17) && !inVal)
            {
                j = 1;
                count = 0;
                while (j < 6)
                {
                    if (Convert.ToString(dtXLPE4view.Rows[i].ItemArray[j]) == "")
                    {
                        count++;
                    }
                    j++;
                }
                if ((count < 5) && (count > 0))
                {
                    inVal = true;
                }
                i++;
            }

            //Validate PVC2
            i = 0;
            while ((i < 16) && !inVal)
            {
                j = 1;
                count = 0;
                while (j < 6)
                {
                    if (Convert.ToString(dtPVC2view.Rows[i].ItemArray[j]) == "")
                    {
                        count++;
                    }
                    j++;
                }
                if ((count < 5) && (count > 0))
                {
                    inVal = true;
                }
                i++;
            }

            //Validate PVC3
            i = 0;
            while ((i < 16) && !inVal)
            {
                j = 1;
                count = 0;
                while (j < 6)
                {
                    if (Convert.ToString(dtPVC3view.Rows[i].ItemArray[j]) == "")
                    {
                        count++;
                    }
                    j++;
                }
                if ((count < 5) && (count > 0))
                {
                    inVal = true;
                }
                i++;
            }

            //Validate PVC4
            i = 0;
            while ((i < 16) && !inVal)
            {
                j = 1;
                count = 0;
                while (j < 6)
                {
                    if (Convert.ToString(dtPVC4view.Rows[i].ItemArray[j]) == "")
                    {
                        count++;
                    }
                    j++;
                }
                if ((count < 5) && (count > 0))
                {
                    inVal = true;
                }
                i++;
            }
            return inVal;
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Delete cable data file \"" + fileName + "\"?", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (dr == DialogResult.OK)
            {
                string systemPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                Directory.CreateDirectory(systemPath + "/Cable Sizing/IEC_database");
                string deleteDir = Path.Combine(systemPath + "/Cable Sizing/IEC_database", fileName + ".xml");
                File.Delete(deleteDir);
                MessageBox.Show("Data \"" + fileName + "\" deleted successfully!", "Cable Sizing", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadIECDatabase();
            }
        }
        private void EnableRenameDelete()
        {
            if ((comboBoxDatabase.Text != "") && (comboBoxDatabase.Text != "Sumi Indo Cable (Default)"))
            {
                buttonDelete.Enabled = true;
                buttonRename.Enabled = true;
            }
            else
            {
                buttonDelete.Enabled = false;
                buttonRename.Enabled = false;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (fileName != "")
            {
                FormRename fR = new FormRename();
                fR.FormClosed += RenameFile;
                fR.ShowDialog();
            }
        }

        private void RenameFile(object sender, FormClosedEventArgs e)
        {
            if (FormRename.saveClicked)
            {
                string sysPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string oldFile = Path.Combine(sysPath + "/Cable Sizing/IEC_database", fileName + ".xml");
                string NewFile = Path.Combine(sysPath + "/Cable Sizing/IEC_database", FormRename.NewName + ".xml");

                File.Move(oldFile, NewFile);
                LoadIECDatabase();
                comboBoxDatabase.Text = FormRename.NewName;
            }
            FormRename.saveClicked = false;
        }

        private void CheckEditAvailability()
        {
            bool terisi;
            //XLPE2CORE
            for (int i = 0; i < 17; i++)
            {
                int j = 0;
                terisi = true;
                while (j < 6)
                {
                    if (Convert.ToString(dtXLPE2view.Rows[i].ItemArray[j]) == "")
                    {
                        terisi = false;
                    }
                    j++;
                }
                if (terisi)
                {
                    datAvailable[i, 0] = true;
                }
                else
                {
                    datAvailable[i, 0] = false;
                }
            }

            //XLPE3CORE
            for (int i = 0; i < 17; i++)
            {
                int j = 0;
                terisi = true;
                while (j < 6)
                {
                    if (Convert.ToString(dtXLPE3view.Rows[i].ItemArray[j]) == "")
                    {
                        terisi = false;
                    }
                    j++;
                }
                if (terisi)
                {
                    datAvailable[i, 1] = true;
                }
                else
                {
                    datAvailable[i, 1] = false;
                }
            }

            //XLPE4CORE
            for (int i = 0; i < 17; i++)
            {
                int j = 0;
                terisi = true;
                while (j < 6)
                {
                    if (Convert.ToString(dtXLPE4view.Rows[i].ItemArray[j]) == "")
                    {
                        terisi = false;
                    }
                    j++;
                }
                if (terisi)
                {
                    datAvailable[i, 2] = true;
                }
                else
                {
                    datAvailable[i, 2] = false;
                }
            }

            //PVC2CORE
            for (int i = 0; i < 16; i++)
            {
                int j = 0;
                terisi = true;
                while (j < 6)
                {
                    if (Convert.ToString(dtPVC2view.Rows[i].ItemArray[j]) == "")
                    {
                        terisi = false;
                    }
                    j++;
                }
                if (terisi)
                {
                    datAvailable[i, 3] = true;
                }
                else
                {
                    datAvailable[i, 3] = false;
                }
            }

            //PVC3CORE
            for (int i = 0; i < 16; i++)
            {
                int j = 0;
                terisi = true;
                while (j < 6)
                {
                    if (Convert.ToString(dtPVC3view.Rows[i].ItemArray[j]) == "")
                    {
                        terisi = false;
                    }
                    j++;
                }
                if (terisi)
                {
                    datAvailable[i, 4] = true;
                }
                else
                {
                    datAvailable[i, 4] = false;
                }
            }

            //PVC4CORE
            for (int i = 0; i < 16; i++)
            {
                int j = 0;
                terisi = true;
                while (j < 6)
                {
                    if (Convert.ToString(dtPVC4view.Rows[i].ItemArray[j]) == "")
                    {
                        terisi = false;
                    }
                    j++;
                }
                if (terisi)
                {
                    datAvailable[i, 5] = true;
                }
                else
                {
                    datAvailable[i, 5] = false;
                }
            }

        }

        //Function to fill all data table with all the sizes available
        private void FillDataTableEdit()
        {
            //For XLPE 2 Core
            for (int i = 0; i < 17; i++)
            {
                DataRow row = dtXLPE2view.NewRow();
                if (i > dtXLPE2view.Rows.Count - 1)
                {
                    row[0] = cablesize[i];
                    dtXLPE2view.Rows.InsertAt(row, i);
                }
                else if (cablesize[i] != Convert.ToDouble(dtXLPE2view.Rows[i].ItemArray[0]))
                {
                    row[0] = cablesize[i];
                    dtXLPE2view.Rows.InsertAt(row, i);
                }
            }

            //For XLPE 3 Core
            for (int i = 0; i < 17; i++)
            {
                DataRow row = dtXLPE3view.NewRow();
                if (i > dtXLPE3view.Rows.Count - 1)
                {
                    row[0] = cablesize[i];
                    dtXLPE3view.Rows.InsertAt(row, i);
                }
                else if (cablesize[i] != Convert.ToDouble(dtXLPE3view.Rows[i].ItemArray[0]))
                {
                    row[0] = cablesize[i];
                    dtXLPE3view.Rows.InsertAt(row, i);
                }
            }

            //For XLPE 4 Core
            for (int i = 0; i < 17; i++)
            {
                DataRow row = dtXLPE4view.NewRow();
                if (i > dtXLPE4view.Rows.Count - 1)
                {
                    row[0] = cablesize[i];
                    dtXLPE4view.Rows.InsertAt(row, i);
                }
                else if (cablesize[i] != Convert.ToDouble(dtXLPE4view.Rows[i].ItemArray[0]))
                {
                    row[0] = cablesize[i];
                    dtXLPE4view.Rows.InsertAt(row, i);
                }
            }

            //For PVC 2 Core
            for (int i = 0; i < 16; i++)
            {
                DataRow row = dtPVC2view.NewRow();
                if (i > dtPVC2view.Rows.Count - 1)
                {
                    row[0] = cablesize[i];
                    dtPVC2view.Rows.InsertAt(row, i);
                }
                else if (cablesize[i] != Convert.ToDouble(dtPVC2view.Rows[i].ItemArray[0]))
                {
                    row[0] = cablesize[i];
                    dtPVC2view.Rows.InsertAt(row, i);
                }
            }

            //For PVC 3 Core
            for (int i = 0; i < 16; i++)
            {
                DataRow row = dtPVC3view.NewRow();
                if (i > dtPVC3view.Rows.Count - 1)
                {
                    row[0] = cablesize[i];
                    dtPVC3view.Rows.InsertAt(row, i);
                }
                else if (cablesize[i] != Convert.ToDouble(dtPVC3view.Rows[i].ItemArray[0]))
                {
                    row[0] = cablesize[i];
                    dtPVC3view.Rows.InsertAt(row, i);
                }
            }


            //For PVC 4 Core
            for (int i = 0; i < 16; i++)
            {
                DataRow row = dtPVC4view.NewRow();
                if (i > dtPVC4view.Rows.Count - 1)
                {
                    row[0] = cablesize[i];
                    dtPVC4view.Rows.InsertAt(row, i);
                }
                else if (cablesize[i] != Convert.ToDouble(dtPVC4view.Rows[i].ItemArray[0]))
                {
                    row[0] = cablesize[i];
                    dtPVC4view.Rows.InsertAt(row, i);
                }
            }

        }

        private void FinalCableDataEdit()
        {
            int k;

            //Save XLPE2 Cable Data
            k = 0;
            dtXLPE2final = dtXLPE2view.Copy();
            for (int i = 0; i < 17; i++)
            {
                if (!datAvailable[i, 0])
                {
                    dtXLPE2final.Rows.RemoveAt(k);
                }
                else
                {
                    k++;
                }
            }

            //Save XLPE3 Cable Data
            k = 0;
            dtXLPE3final = dtXLPE3view.Copy();
            for (int i = 0; i < 17; i++)
            {
                if (!datAvailable[i, 1])
                {
                    dtXLPE3final.Rows.RemoveAt(k);
                }
                else
                {
                    k++;
                }
            }

            //Save XLPE4 Cable Data
            k = 0;
            dtXLPE4final = dtXLPE4view.Copy();
            for (int i = 0; i < 17; i++)
            {
                if (!datAvailable[i, 2])
                {
                    dtXLPE4final.Rows.RemoveAt(k);
                }
                else
                {
                    k++;
                }
            }

            //Save PVC2 Cable Data
            k = 0;
            dtPVC2final = dtPVC2view.Copy();
            for (int i = 0; i < 16; i++)
            {
                if (!datAvailable[i, 3])
                {
                    dtPVC2final.Rows.RemoveAt(k);
                }
                else
                {
                    k++;
                }
            }

            //Save PVC3 Cable Data
            k = 0;
            dtPVC3final = dtPVC3view.Copy();
            for (int i = 0; i < 16; i++)
            {
                if (!datAvailable[i, 4])
                {
                    dtPVC3final.Rows.RemoveAt(k);
                }
                else
                {
                    k++;
                }
            }

            //Save PVC4 Cable Data
            k = 0;
            dtPVC4final = dtPVC4view.Copy();
            for (int i = 0; i < 16; i++)
            {
                if (!datAvailable[i, 5])
                {
                    dtPVC4final.Rows.RemoveAt(k);
                }
                else
                {
                    k++;
                }
            }
        }

        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                IECSelected = true;
                NECSelected = false;
            }
            else
            {
                IECSelected = false;
                NECSelected = true;
            }
        }

        private void LoadIECDatabase()
        {
            //read all file in database directory
            string systemPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            Directory.CreateDirectory(systemPath + "/Cable Sizing/IEC_database");
            string saveDir = (systemPath + "/Cable Sizing/IEC_database");

            di = new DirectoryInfo(saveDir);
            //get all file name in database directory
            FileInfo[] files = di.GetFiles("*.xml");
            IECFiles = files.Length;
            //fill vendor data from database
            comboBoxDatabase.Items.Clear();
            comboBoxDatabase.Items.Insert(0, "Sumi Indo Cable (Default)"); //default, hardcoded-to-program database
            //fill all saved database created by user
            for (int z = 0; z < IECFiles; z++)
            {
                string tempstring;
                tempstring = files[z].ToString();
                tempstring = tempstring.Replace(".xml", "");
                comboBoxDatabase.Items.Insert(z + 1, tempstring);
            }
        }


        private void ComboBoxDatabase_SelectedIndexChanged(object sender, EventArgs e)
        {
            fileName = comboBoxDatabase.Text;

            if ((fileName != "Sumi Indo Cable (Default)") && (fileName != ""))
            {
                ReadIECDatabase();
            }
            else if (fileName == "Sumi Indo Cable (Default)")
            {
                DoubleArrayToDT(Form1.xlpe2core, 17, 6, dtXLPE2view);
                DoubleArrayToDT(Form1.xlpe3core, 17, 6, dtXLPE3view);
                DoubleArrayToDT(Form1.xlpe4core, 17, 6, dtXLPE4view);
                DoubleArrayToDT(Form1.pvc2core, 16, 6, dtPVC2view);
                DoubleArrayToDT(Form1.pvc3core, 16, 6, dtPVC3view);
                DoubleArrayToDT(Form1.pvc4core, 16, 6, dtPVC4view);
                //Insert default-hard-coded array table (sumi indo) to data table used here
                xlpe2coreLength = 17;
                xlpe3coreLength = 17;
                xlpe4coreLength = 17;
                pvc2coreLength = 16;
                pvc3coreLength = 16;
                pvc4coreLength = 16;
            }
            else if (fileName != "")
            {
                dtXLPE2view = null;
                dtXLPE3view = null;
                dtXLPE4view = null;
                dtPVC2view = null;
                dtPVC3view = null;
                dtPVC4view = null;
            }

            EnableRenameDelete();
            ChooseDatabaseView();
            EnableEdit();
        }

        private void EnableEdit()
        {
            if ((fileName != "") && (fileName != "Sumi Indo Cable (Default)") && (viewconductor != "") && (viewinsulation != "") && (viewCores != 0))
            {
                buttonEdit.Enabled = true;
            }
            else
            {
                buttonEdit.Enabled = false;
            }
        }


        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            viewinsulation = comboBox1.Text;
            ChooseDatabaseView();
            EnableEdit();
        }


        private void ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

            viewCores = int.Parse(comboBox3.Text);
            ChooseDatabaseView();
            EnableEdit();
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            viewconductor = comboBox2.Text;
            ChooseDatabaseView();
            EnableEdit();
        }


        //Read IEC saved Database from the directory, return cable data file + length of each type of cable data
        internal void ReadIECDatabase()
        {
            string systemPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            Directory.CreateDirectory(systemPath + "/Cable Sizing/IEC_database");
            string saveDir = Path.Combine(systemPath + "/Cable Sizing/IEC_database", fileName + ".xml");

            //save database to a new dataset
            viewCableDS = new DataSet();
            viewCableDS.ReadXml(saveDir);

            //save each cable data table to their respective datatable
            if (viewCableDS.Tables.Contains("XLPE_2CORE"))
            {
                dtXLPE2view = viewCableDS.Tables["XLPE_2CORE"].Copy();
                xlpe2coreLength = dtXLPE2view.Rows.Count;
            }
            else
            {
                dtXLPE2view.Rows.Clear();
                xlpe2coreLength = 0;
            }
            if (viewCableDS.Tables.Contains("XLPE_3CORE"))
            {
                dtXLPE3view= viewCableDS.Tables["XLPE_3CORE"].Copy();
                xlpe3coreLength = dtXLPE3view.Rows.Count;
            }
            else
            {
                dtXLPE3view.Rows.Clear();
                xlpe3coreLength = 0;
            }
            if (viewCableDS.Tables.Contains("XLPE_4CORE"))
            {
                dtXLPE4view = viewCableDS.Tables["XLPE_4CORE"].Copy();
                xlpe4coreLength = dtXLPE4view.Rows.Count;
            }
            else
            {
                dtXLPE4view.Rows.Clear();
                xlpe4coreLength = 0;
            }
            if (viewCableDS.Tables.Contains("PVC_2CORE"))
            {
                dtPVC2view = viewCableDS.Tables["PVC_2CORE"].Copy();
                pvc2coreLength = dtPVC2view.Rows.Count;
            }
            else
            {
                dtPVC2view.Rows.Clear();
                pvc2coreLength = 0;
            }
            if (viewCableDS.Tables.Contains("PVC_3CORE"))
            {
                dtPVC3view = viewCableDS.Tables["PVC_3CORE"].Copy();
                pvc3coreLength = dtPVC3view.Rows.Count;
            }
            else
            {
                dtPVC3view.Rows.Clear();
                pvc3coreLength = 0;
            }
            if (viewCableDS.Tables.Contains("PVC_4CORE"))
            {
                dtPVC4view = viewCableDS.Tables["PVC_4CORE"].Copy();
                pvc4coreLength = dtPVC4view.Rows.Count;
            }
            else
            {
                dtPVC4view.Rows.Clear();
                pvc4coreLength = 0;
            }
        }

        private void ChooseDatabaseView()
        {
            if (viewconductor == "Copper")
            {
                if (viewinsulation == "XLPE")
                {
                    switch (viewCores)
                    {
                        case 2:
                            //bound datagridview to datatable
                            dataGridView2.DataSource = dtXLPE2view;
                            SetDGVRuntimeProperties(dataGridView2);
                            break;
                        case 3:
                            //bound datagridview to datatable
                            dataGridView2.DataSource = dtXLPE3view;
                            SetDGVRuntimeProperties(dataGridView2);
                            break;
                        case 4:
                            //bound datagridview to datatable
                            dataGridView2.DataSource = dtXLPE4view;
                            SetDGVRuntimeProperties(dataGridView2);
                            break;
                        default:
                            dataGridView2.DataSource = null;
                            break;
                    }
                }
                else if (viewinsulation == "PVC")
                {
                    switch (viewCores)
                    {
                        case 2:
                            //bound datagridview to datatable
                            dataGridView2.DataSource = dtPVC2view;
                            SetDGVRuntimeProperties(dataGridView2);
                            break;
                        case 3:
                            //bound datagridview to datatable
                            dataGridView2.DataSource = dtPVC3view;
                            SetDGVRuntimeProperties(dataGridView2);
                            break;
                        case 4:
                            //bound datagridview to datatable
                            dataGridView2.DataSource = dtPVC4view;
                            SetDGVRuntimeProperties(dataGridView2);
                            break;
                        default:
                            dataGridView2.DataSource = null;
                            break;
                    }

                }
            }
            else
            {
                dataGridView2.DataSource = null;
            }
        }

        /*
        private void FillDataTable()
        {
            if ((fileName != "Sumi Indo Cable (Default)") && (fileName != ""))
            {

            }
        }
        */

        public static void DoubleArrayToDT(double [,] arr, int Row, int Col, DataTable dt)
        {
            DataRow dr;
            for (int nRow = 0; nRow < Row; nRow ++)
            {
                dr = dt.NewRow();
                for (int nCol = 0; nCol < Col; nCol ++)
                {
                    dr[nCol] = arr[nRow, nCol]; 
                }
                dt.Rows.Add(dr);
            }
            
        }
    }

}
