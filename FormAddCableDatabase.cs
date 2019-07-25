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

            //initializing database table column

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

            //initialize datagridview properties
            dataGridView1.DoubleBuffered(true);
            dataGridView2.DoubleBuffered(true);


            //initialize edit/view tab
            LoadIECDatabase();
            comboBoxDatabase.SelectedIndex = 1;

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
                        Directory.CreateDirectory(systemPath + "/Cable Sizing");
                        var saveDir = Path.Combine(systemPath + "/Cable Sizing", saveName + ".xml");

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
            Directory.CreateDirectory(systemPath + "/Cable Sizing");
            var saveDir = Path.Combine(systemPath + "/Cable Sizing", saveName + ".xml");

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
                            SetDGVRuntimeProperties();
                            break;
                        case 3:
                            //bound datagridview to datatable
                            dataGridView1.DataSource = dtXLPE3;
                            SetDGVRuntimeProperties();
                            break;
                        case 4:
                            //bound datagridview to datatable
                            dataGridView1.DataSource = dtXLPE4;
                            SetDGVRuntimeProperties();
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
                            SetDGVRuntimeProperties();
                            break;
                        case 3:
                            //bound datagridview to datatable
                            dataGridView1.DataSource = dtPVC3;
                            SetDGVRuntimeProperties();
                            break;
                        case 4:
                            //bound datagridview to datatable
                            dataGridView1.DataSource = dtPVC4;
                            SetDGVRuntimeProperties();
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

        private void SetDGVRuntimeProperties()
        {
            //disable column sorting
            for (int i = 0; i < 6; i++)
            {
                dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            //disable changing the wire size data
            dataGridView1.Columns[0].ReadOnly = true;
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


        int IECFiles;


        int viewCores;
        string viewinsulation, viewconductor;


        string fileName;

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
            Directory.CreateDirectory(systemPath + "/Cable Sizing");
            string saveDir = (systemPath + "/Cable Sizing");

            di = new DirectoryInfo(saveDir);
            //get all file name in database directory
            FileInfo[] files = di.GetFiles("*.xml");
            IECFiles = files.Length;
            //fill vendor data from database
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
        }


        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            viewinsulation = comboBox1.Text;
        }


        private void ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            viewCores = int.Parse(comboBox3.Text);
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            viewconductor = comboBox2.Text;
        }

        private void FillDataTable()
        {
            if ((fileName != "Sumi Indo Cable (Default)") && (fileName != ""))
            {

            }
        }

        DataSet viewCableDS;

        /*
        internal void ReadIECDatabase()
        {
            string systemPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            Directory.CreateDirectory(systemPath + "/Cable Sizing");
            string saveDir = Path.Combine(systemPath + "/Cable Sizing", SelectedDatabase + ".xml");

            //save database to a new dataset
            viewCableDS = new DataSet();
            viewCableDS.ReadXml(saveDir);

            //save each cable data table to their respective datatable
            if (viewCableDS.Tables.Contains("XLPE_2CORE"))
            {
                dtXLPE2DB = viewCableDS.Tables["XLPE_2CORE"].Copy();
                xlpe2coreDB = new double[dtXLPE2DB.Rows.Count, dtXLPE2DB.Columns.Count];
                DTToArrayDouble(dtXLPE2DB, xlpe2coreDB);
                xlpe2coreLength = dtXLPE2DB.Rows.Count;
            }
            else
            {
                xlpe2coreLength = 0;
            }
            if (cableDS.Tables.Contains("XLPE_3CORE"))
            {
                dtXLPE3DB = cableDS.Tables["XLPE_3CORE"].Copy();
                xlpe3coreDB = new double[dtXLPE3DB.Rows.Count, dtXLPE3DB.Columns.Count];
                DTToArrayDouble(dtXLPE3DB, xlpe3coreDB);
                xlpe3coreLength = dtXLPE3DB.Rows.Count;
            }
            else
            {
                xlpe3coreLength = 0;
            }
            if (cableDS.Tables.Contains("XLPE_4CORE"))
            {
                dtXLPE4DB = cableDS.Tables["XLPE_4CORE"].Copy();
                xlpe4coreDB = new double[dtXLPE4DB.Rows.Count, dtXLPE4DB.Columns.Count];
                DTToArrayDouble(dtXLPE4DB, xlpe4coreDB);
                xlpe4coreLength = dtXLPE4DB.Rows.Count;
            }
            else
            {
                xlpe4coreLength = 0;
            }
            if (cableDS.Tables.Contains("PVC_2CORE"))
            {
                dtPVC2DB = cableDS.Tables["PVC_2CORE"].Copy();
                pvc2coreDB = new double[dtPVC2DB.Rows.Count, dtPVC2DB.Columns.Count];
                DTToArrayDouble(dtPVC2DB, pvc2coreDB);
                pvc2coreLength = dtPVC2DB.Rows.Count;
            }
            else
            {
                pvc2coreLength = 0;
            }
            if (cableDS.Tables.Contains("PVC_3CORE"))
            {
                dtPVC3DB = cableDS.Tables["PVC_3CORE"].Copy();
                pvc3coreDB = new double[dtPVC3DB.Rows.Count, dtPVC3DB.Columns.Count];
                DTToArrayDouble(dtPVC3DB, pvc3coreDB);
                pvc3coreLength = dtPVC3DB.Rows.Count;
            }
            else
            {
                pvc3coreLength = 0;
            }
            if (cableDS.Tables.Contains("PVC_4CORE"))
            {
                dtPVC4DB = cableDS.Tables["PVC_4CORE"].Copy();
                pvc4coreDB = new double[dtPVC4DB.Rows.Count, dtPVC4DB.Columns.Count];
                DTToArrayDouble(dtPVC2DB, pvc4coreDB);
                pvc4coreLength = dtPVC4DB.Rows.Count;
            }
            else
            {
                pvc4coreLength = 0;
            }
        }

        private void DTToArrayDouble(DataTable dt, double[,] arr)
        {
            int dtRow = dt.Rows.Count;
            int dtColumn = dt.Columns.Count;

            int row = 0;
            foreach (DataRow dr in dt.Rows)
            {
                for (int col = 0; col < dtColumn; col++)
                {
                    arr[row, col] = Convert.ToDouble(dr[col]);
                }
                row++;
            }
        }
        */

    }

}
