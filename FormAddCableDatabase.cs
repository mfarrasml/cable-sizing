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

        private DataTable dtNEC00;
        private DataTable dtNEC10;
        private DataTable dtNEC20;
        private DataTable dtNEC01;
        private DataTable dtNEC11;
        private DataTable dtNEC21;

        private DataTable dtNEC00final;
        private DataTable dtNEC10final;
        private DataTable dtNEC20final;
        private DataTable dtNEC01final;
        private DataTable dtNEC11final;
        private DataTable dtNEC21final;

        int noCores;
        string insulation, conductor;
        string saveName;

        bool inValid;


        bool IECStandard, NECStandard;

        internal static bool databaseEdited; //used to alert main form whether database is edited

        double[] cablesize = new double[17];
        string[] nec_cablesize = new string[21]
        {
            "14",
            "12",
            "10",
            "8",
            "6",
            "4",
            "3",
            "2",
            "1",
            "1/0",
            "2/0",
            "3/0",
            "4/0",
            "250",
            "300",
            "350",
            "400",
            "500",
            "600",
            "750",
            "1000"
        };

        public FormAddCableDatabase()
        {
            InitializeComponent();
        }

        private void FormAddCableDatabase_Load(object sender, EventArgs e)
        {
            //reset database edited status
            databaseEdited = false;

            //initializing new database datatable
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
            dtXLPE2.Columns.Add("Size");
            dtXLPE2.Columns.Add("AC_Resistance");
            dtXLPE2.Columns.Add("Reactance");
            dtXLPE2.Columns.Add("DC_Resistance");
            dtXLPE2.Columns.Add("UG_CCC");
            dtXLPE2.Columns.Add("AG_CCC");
            /*
            dtXLPE2.Columns[0].ColumnName = "Size";
            dtXLPE2.Columns[1].ColumnName = "AC_Resistance";
            dtXLPE2.Columns[2].ColumnName = "Reactance";
            dtXLPE2.Columns[3].ColumnName = "DC_Resistance";
            dtXLPE2.Columns[4].ColumnName = "UG_CCC";
            dtXLPE2.Columns[5].ColumnName = "AG_CCC"; */

            dtXLPE2.TableName = "XLPE_2CORE";


            //initializing XLPE3 table column
            dtXLPE3.Columns.Add("Size");
            dtXLPE3.Columns.Add("AC_Resistance");
            dtXLPE3.Columns.Add("Reactance");
            dtXLPE3.Columns.Add("DC_Resistance");
            dtXLPE3.Columns.Add("UG_CCC");
            dtXLPE3.Columns.Add("AG_CCC");
            /*
            dtXLPE3.Columns[0].ColumnName = "Size";
            dtXLPE3.Columns[1].ColumnName = "AC_Resistance";
            dtXLPE3.Columns[2].ColumnName = "Reactance";
            dtXLPE3.Columns[3].ColumnName = "DC_Resistance";
            dtXLPE3.Columns[4].ColumnName = "UG_CCC";
            dtXLPE3.Columns[5].ColumnName = "AG_CCC"; */

            dtXLPE3.TableName = "XLPE_3CORE";

            //initializing XLPE4 table column
            dtXLPE4.Columns.Add("Size");
            dtXLPE4.Columns.Add("AC_Resistance");
            dtXLPE4.Columns.Add("Reactance");
            dtXLPE4.Columns.Add("DC_Resistance");
            dtXLPE4.Columns.Add("UG_CCC");
            dtXLPE4.Columns.Add("AG_CCC");
            /*
            dtXLPE4.Columns[0].ColumnName = "Size";
            dtXLPE4.Columns[1].ColumnName = "AC_Resistance";
            dtXLPE4.Columns[2].ColumnName = "Reactance";
            dtXLPE4.Columns[3].ColumnName = "DC_Resistance";
            dtXLPE4.Columns[4].ColumnName = "UG_CCC";
            dtXLPE4.Columns[5].ColumnName = "AG_CCC"; */

            dtXLPE4.TableName = "XLPE_4CORE";


            //initializing PVC2 table column
            dtPVC2.Columns.Add("Size");
            dtPVC2.Columns.Add("AC_Resistance");
            dtPVC2.Columns.Add("Reactance");
            dtPVC2.Columns.Add("DC_Resistance");
            dtPVC2.Columns.Add("UG_CCC");
            dtPVC2.Columns.Add("AG_CCC");
            /*
            dtPVC2.Columns[0].ColumnName = "Size";
            dtPVC2.Columns[1].ColumnName = "AC_Resistance";
            dtPVC2.Columns[2].ColumnName = "Reactance";
            dtPVC2.Columns[3].ColumnName = "DC_Resistance";
            dtPVC2.Columns[4].ColumnName = "UG_CCC";
            dtPVC2.Columns[5].ColumnName = "AG_CCC"; */

            dtPVC2.TableName = "PVC_2CORE";

            //initializing PVC3 table column
            dtPVC3.Columns.Add("Size");
            dtPVC3.Columns.Add("AC_Resistance");
            dtPVC3.Columns.Add("Reactance");
            dtPVC3.Columns.Add("DC_Resistance");
            dtPVC3.Columns.Add("UG_CCC");
            dtPVC3.Columns.Add("AG_CCC");
            /*
            dtPVC3.Columns[0].ColumnName = "Size";
            dtPVC3.Columns[1].ColumnName = "AC_Resistance";
            dtPVC3.Columns[2].ColumnName = "Reactance";
            dtPVC3.Columns[3].ColumnName = "DC_Resistance";
            dtPVC3.Columns[4].ColumnName = "UG_CCC";
            dtPVC3.Columns[5].ColumnName = "AG_CCC"; */

            dtPVC3.TableName = "PVC_3CORE";

            //initializing PVC4 table column
            dtPVC4.Columns.Add("Size");
            dtPVC4.Columns.Add("AC_Resistance");
            dtPVC4.Columns.Add("Reactance");
            dtPVC4.Columns.Add("DC_Resistance");
            dtPVC4.Columns.Add("UG_CCC");
            dtPVC4.Columns.Add("AG_CCC");
            /*
            dtPVC4.Columns[0].ColumnName = "Size";
            dtPVC4.Columns[1].ColumnName = "AC_Resistance";
            dtPVC4.Columns[2].ColumnName = "Reactance";
            dtPVC4.Columns[3].ColumnName = "DC_Resistance";
            dtPVC4.Columns[4].ColumnName = "UG_CCC";
            dtPVC4.Columns[5].ColumnName = "AG_CCC"; */

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
            FillEditCB();



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


            //NEC
            dtNEC00 = new DataTable();
            dtNEC10 = new DataTable();
            dtNEC20 = new DataTable();
            dtNEC01 = new DataTable();
            dtNEC11 = new DataTable();
            dtNEC21 = new DataTable();

            //initializing NEC0 table column
            dtNEC00.Columns.Add("Size");
            dtNEC00.Columns.Add("Resistance");
            dtNEC00.Columns.Add("Reactance");
            dtNEC00.Columns.Add("CurrentCarryingCapacity");

            dtNEC00.TableName = "NEC00";

            //initializing NEC1 table column
            dtNEC10.Columns.Add("Size");
            dtNEC10.Columns.Add("Resistance");
            dtNEC10.Columns.Add("Reactance");
            dtNEC10.Columns.Add("CurrentCarryingCapacity");

            dtNEC10.TableName = "NEC10";


            //initializing NEC2 table column
            dtNEC20.Columns.Add("Size");
            dtNEC20.Columns.Add("Resistance");
            dtNEC20.Columns.Add("Reactance");
            dtNEC20.Columns.Add("CurrentCarryingCapacity");

            dtNEC20.TableName = "NEC20";

            //initializing NEC0 table column
            dtNEC01.Columns.Add("Size");
            dtNEC01.Columns.Add("Resistance");
            dtNEC01.Columns.Add("Reactance");
            dtNEC01.Columns.Add("CurrentCarryingCapacity");

            dtNEC01.TableName = "NEC01";

            //initializing NEC1 table column
            dtNEC11.Columns.Add("Size");
            dtNEC11.Columns.Add("Resistance");
            dtNEC11.Columns.Add("Reactance");
            dtNEC11.Columns.Add("CurrentCarryingCapacity");

            dtNEC11.TableName = "NEC11";


            //initializing NEC2 table column
            dtNEC21.Columns.Add("Size");
            dtNEC21.Columns.Add("Resistance");
            dtNEC21.Columns.Add("Reactance");
            dtNEC21.Columns.Add("CurrentCarryingCapacity");

            dtNEC21.TableName = "NEC_21";


            //initialize NEC wiresize
            for (int i = 0; i < 21; i++)
            {
                dtNEC00.Rows.Add(nec_cablesize[i]);
                dtNEC10.Rows.Add(nec_cablesize[i]);
                dtNEC20.Rows.Add(nec_cablesize[i]);
                dtNEC01.Rows.Add(nec_cablesize[i]);
                dtNEC11.Rows.Add(nec_cablesize[i]);
                dtNEC21.Rows.Add(nec_cablesize[i]);
            }


            //initialize datagridview properties
            dataGridView1.DoubleBuffered(true);
            dataGridView2.DoubleBuffered(true);


            //initialize edit/view tab
            if (radioButton2.Checked)
            {
                LoadIECDatabase();
            }
            else
            {
                LoadNECDatabase();
            }

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
            dtXLPE2view.Columns.Add("Size");
            dtXLPE2view.Columns.Add("AC_Resistance");
            dtXLPE2view.Columns.Add("Reactance");
            dtXLPE2view.Columns.Add("DC_Resistance");
            dtXLPE2view.Columns.Add("UG_CCC");
            dtXLPE2view.Columns.Add("AG_CCC");

            dtXLPE2.TableName = "XLPE_2CORE";


            //initializing XLPE3 table column
            dtXLPE3view.Columns.Add("Size");
            dtXLPE3view.Columns.Add("AC_Resistance");
            dtXLPE3view.Columns.Add("Reactance");
            dtXLPE3view.Columns.Add("DC_Resistance");
            dtXLPE3view.Columns.Add("UG_CCC");
            dtXLPE3view.Columns.Add("AG_CCC");

            dtXLPE3view.TableName = "XLPE_3CORE";

            //initializing XLPE4 table column
            dtXLPE4view.Columns.Add("Size");
            dtXLPE4view.Columns.Add("AC_Resistance");
            dtXLPE4view.Columns.Add("Reactance");
            dtXLPE4view.Columns.Add("DC_Resistance");
            dtXLPE4view.Columns.Add("UG_CCC");
            dtXLPE4view.Columns.Add("AG_CCC");

            dtXLPE4view.TableName = "XLPE_4CORE";


            //initializing PVC2 table column
            dtPVC2view.Columns.Add("Size");
            dtPVC2view.Columns.Add("AC_Resistance");
            dtPVC2view.Columns.Add("Reactance");
            dtPVC2view.Columns.Add("DC_Resistance");
            dtPVC2view.Columns.Add("UG_CCC");
            dtPVC2view.Columns.Add("AG_CCC");

            dtPVC2view.TableName = "PVC_2CORE";

            //initializing PVC3 table column
            dtPVC3view.Columns.Add("Size");
            dtPVC3view.Columns.Add("AC_Resistance");
            dtPVC3view.Columns.Add("Reactance");
            dtPVC3view.Columns.Add("DC_Resistance");
            dtPVC3view.Columns.Add("UG_CCC");
            dtPVC3view.Columns.Add("AG_CCC");

            dtPVC3view.TableName = "PVC_3CORE";

            //initializing PVC4 table column
            dtPVC4view.Columns.Add("Size");
            dtPVC4view.Columns.Add("AC_Resistance");
            dtPVC4view.Columns.Add("Reactance");
            dtPVC4view.Columns.Add("DC_Resistance");
            dtPVC4view.Columns.Add("UG_CCC");
            dtPVC4view.Columns.Add("AG_CCC");

            dtPVC4view.TableName = "PVC_4CORE";

            //LOADING DATATABLE FOR NEC DATABASE
            dtNEC00view = new DataTable();
            dtNEC10view = new DataTable();
            dtNEC20view = new DataTable();
            dtNEC01view = new DataTable();
            dtNEC11view = new DataTable();
            dtNEC21view = new DataTable();

            //initializing NEC0 table column
            dtNEC00view.Columns.Add("Size");
            dtNEC00view.Columns.Add("Resistance");
            dtNEC00view.Columns.Add("Reactance");
            dtNEC00view.Columns.Add("CurrentCarryingCapacity");

            dtNEC00view.TableName = "NEC00";

            //initializing NEC1 table column
            dtNEC10view.Columns.Add("Size");
            dtNEC10view.Columns.Add("Resistance");
            dtNEC10view.Columns.Add("Reactance");
            dtNEC10view.Columns.Add("CurrentCarryingCapacity");

            dtNEC10view.TableName = "NEC10";


            //initializing NEC2 table column
            dtNEC20view.Columns.Add("Size");
            dtNEC20view.Columns.Add("Resistance");
            dtNEC20view.Columns.Add("Reactance");
            dtNEC20view.Columns.Add("CurrentCarryingCapacity");

            dtNEC20view.TableName = "NEC20";

            //initializing NEC0 table column
            dtNEC01view.Columns.Add("Size");
            dtNEC01view.Columns.Add("Resistance");
            dtNEC01view.Columns.Add("Reactance");
            dtNEC01view.Columns.Add("CurrentCarryingCapacity");

            dtNEC01view.TableName = "NEC01";

            //initializing NEC1 table column
            dtNEC11view.Columns.Add("Size");
            dtNEC11view.Columns.Add("Resistance");
            dtNEC11view.Columns.Add("Reactance");
            dtNEC11view.Columns.Add("CurrentCarryingCapacity");

            dtNEC11view.TableName = "NEC11";


            //initializing NEC2 table column
            dtNEC21view.Columns.Add("Size");
            dtNEC21view.Columns.Add("Resistance");
            dtNEC21view.Columns.Add("Reactance");
            dtNEC21view.Columns.Add("CurrentCarryingCapacity");

            dtNEC21view.TableName = "NEC_21";

        }

        private void ComboBoxInsulation_SelectedIndexChanged(object sender, EventArgs e)
        {
            insulation = comboBoxInsulation.Text;

            EnableSave();
            ChooseDatabase();
        }

        private void ComboBoxCores_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxCores.Text != "")
            {
                noCores = int.Parse(comboBoxCores.Text);
            }
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

            if ((saveName != "") && IECStandard) //save IEC cable database
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
                            if (IECSelected)
                            {
                                LoadIECDatabase();
                                ResetEditView();
                            }
                            
                            ResetStateAfterSave();
                            databaseEdited = true;
                        }
                        else //file with the same name detected
                        {
                            DialogResult dr = MessageBox.Show("File with the same name already exist, want to overwrite the file?", "Cable Sizing",
                                MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                            if (dr == DialogResult.OK)
                            {
                                SaveIECDatabase();
                                if (IECSelected)
                                {
                                    LoadIECDatabase();
                                    ResetEditView();
                                }
                                ResetStateAfterSave();
                                databaseEdited = true;
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
            else if ((saveName != "") && NECStandard) //save NEC cable database
            {
                dtNEC00final = new DataTable();
                dtNEC10final = new DataTable();
                dtNEC20final = new DataTable();
                dtNEC01final = new DataTable();
                dtNEC11final = new DataTable();
                dtNEC21final = new DataTable();


                inValid = cekValidasiTableNEC();
                if (!inValid)
                {
                    checkAvailabilityNEC();
                    finalCableDataNEC();
                    if ((dtNEC00final.Rows.Count > 0) || (dtNEC10final.Rows.Count > 0) || (dtNEC20final.Rows.Count > 0) || (dtNEC01final.Rows.Count > 0)
                        || (dtNEC11final.Rows.Count > 0) || (dtNEC21final.Rows.Count > 0))
                    {
                        var systemPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                        Directory.CreateDirectory(systemPath + "/Cable Sizing/NEC_database");
                        var saveDir = Path.Combine(systemPath + "/Cable Sizing/NEC_database", saveName + ".xml");

                        if (!File.Exists(saveDir))
                        {
                            SaveNECDatabase();
                            if (NECSelected)
                            {
                                LoadNECDatabase();
                                ResetEditView();
                            }
                            ResetStateAfterSave();
                            databaseEdited = true;
                            if (buttonEdit.Text == "Save")
                            {
                                comboBoxDatabase.Text = fileName;
                            }
                        }
                        else //file with the same name detected
                        {
                            DialogResult dr = MessageBox.Show("File with the same name already exist, want to overwrite the file?", "Cable Sizing",
                                MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                            if (dr == DialogResult.OK)
                            {
                                SaveNECDatabase();
                                if (NECSelected)
                                {
                                    LoadNECDatabase();
                                    ResetEditView();
                                }
                                ResetStateAfterSave();
                                databaseEdited = true;
                                if (buttonEdit.Text == "Save")
                                {
                                    comboBoxDatabase.Text = fileName;
                                }
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

        private void ResetEditView()
        {
            dataGridView2.DataSource = null;
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            buttonEdit.Enabled = false;
            buttonRename.Enabled = false;
            buttonDelete.Enabled = false;
        }

        //Save the new IEC Database
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
            ds.DataSetName = "IEC_Cable_Database";
            ds.WriteXml(xmlSave);
            xmlSave.Close();

            MessageBox.Show("Data Saved!", "Cable Database", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //save the new NEC database
        private void SaveNECDatabase()
        {
            var systemPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            Directory.CreateDirectory(systemPath + "/Cable Sizing/NEC_database");
            var saveDir = Path.Combine(systemPath + "/Cable Sizing/NEC_database", saveName + ".xml");

            DataSet ds = new DataSet();
            ds.Tables.Add(dtNEC00final);
            ds.Tables.Add(dtNEC10final);
            ds.Tables.Add(dtNEC20final);
            ds.Tables.Add(dtNEC01final);
            ds.Tables.Add(dtNEC11final);
            ds.Tables.Add(dtNEC21final);

            XmlTextWriter xmlSave = new XmlTextWriter(saveDir, Encoding.UTF8);
            xmlSave.Formatting = Formatting.Indented;
            ds.DataSetName = "NEC_Cable_Database";
            ds.WriteXml(xmlSave);
            xmlSave.Close();

            MessageBox.Show("Data Saved!", "Cable Database", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ResetStateAfterSave()
        {
            dataGridView1.DataSource = null;
            comboBoxInsulation.SelectedIndex = -1;
            insulation = "";
            comboBoxConductor.SelectedIndex = -1;
            conductor = "";
            comboBoxCores.SelectedIndex = -1;
            noCores = 0;
            textBoxName.Text = "";
            saveName = "";
            
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
            FillEditCB();
        }

        private void FillEditCB()
        {
            if (IECStandard)
            {
                dataGridView1.DataSource = null;
                comboBoxInsulation.Items.Clear();
                comboBoxConductor.Items.Clear();

                //fill insulation combobox
                comboBoxInsulation.SelectedIndex = -1;
                comboBoxInsulation.DropDownWidth = 98;
                insulation = "";
                comboBoxInsulation.Items.Insert(0, "XLPE");
                comboBoxInsulation.Items.Insert(1, "PVC");

                //fill conductor type
                comboBoxConductor.SelectedIndex = -1;
                conductor = "";
                comboBoxConductor.Items.Insert(0, "Copper");

                //show no of cores option
                comboBoxCores.SelectedIndex = -1;
                noCores =  0;
                comboBoxCores.Visible = true;
                labelCores.Visible = true;

            }
            else if (NECStandard)
            {
                dataGridView1.DataSource = null;
                comboBoxInsulation.Items.Clear();
                comboBoxConductor.Items.Clear();

                //fill insulation combobox
                comboBoxInsulation.SelectedIndex = -1;
                comboBoxInsulation.DropDownWidth = 500;
                insulation = "";
                comboBoxInsulation.Items.Insert(0, "TW/UF");
                comboBoxInsulation.Items.Insert(1, "RHW/THW/THWN/USE/ZW");
                comboBoxInsulation.Items.Insert(2, "TBS/SA/SIS/FEP/FEPB/MI/RHH/RHW-2/THHN/THW-2/THWN-2/USE-2/XHH/XHHW-2/ZW-2");

                //fill conductor type
                comboBoxConductor.SelectedIndex = -1;
                conductor = "";
                comboBoxConductor.Items.Insert(0, "Copper");
                comboBoxConductor.Items.Insert(1, "Aluminium");

                //hide no of cores option
                comboBoxCores.SelectedIndex = -1;
                noCores = 0;
                comboBoxCores.Visible = false;
                labelCores.Visible = false;
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
            if (IECStandard)
            {
                ChooseIECDatabase();
            }
            else if (NECStandard)
            {
                ChooseNECDatabase();
            }
        }

        private void ChooseIECDatabase()
        {

            if (comboBoxConductor.Text == "Copper")
            {
                if (comboBoxInsulation.Text == "XLPE")
                {
                    switch (noCores)
                    {
                        case 2:
                            //bound datagridview to datatable
                            ResetDataTable(dtXLPE2);
                            dataGridView1.DataSource = dtXLPE2;
                            dataGridView1.Columns[0].HeaderText = "Size (mm²)";
                            dataGridView1.Columns[1].HeaderText = "AC Resistance at 90°C(Ω/km)";
                            dataGridView1.Columns[2].HeaderText = "Reactance at 50Hz(Ω/km)";
                            dataGridView1.Columns[3].HeaderText = "DC Resistance at 90°C(Ω/km)";
                            dataGridView1.Columns[4].HeaderText = "In Ground CCC(A)";
                            dataGridView1.Columns[5].HeaderText = "In Air CCC(A)";
                            SetDGVRuntimeProperties(dataGridView1, 6);
                            break;
                        case 3:
                            //bound datagridview to datatable
                            ResetDataTable(dtXLPE3);
                            dataGridView1.DataSource = dtXLPE3;
                            dataGridView1.Columns[0].HeaderText = "Size (mm²)";
                            dataGridView1.Columns[1].HeaderText = "AC Resistance at 90°C(Ω/km)";
                            dataGridView1.Columns[2].HeaderText = "Reactance at 50Hz(Ω/km)";
                            dataGridView1.Columns[3].HeaderText = "DC Resistance at 90°C(Ω/km)";
                            dataGridView1.Columns[4].HeaderText = "In Ground CCC(A)";
                            dataGridView1.Columns[5].HeaderText = "In Air CCC(A)";
                            SetDGVRuntimeProperties(dataGridView1, 6);
                            break;
                        case 4:
                            //bound datagridview to datatable
                            ResetDataTable(dtXLPE4);
                            dataGridView1.DataSource = dtXLPE4;
                            dataGridView1.Columns[0].HeaderText = "Size (mm²)";
                            dataGridView1.Columns[1].HeaderText = "AC Resistance at 90°C(Ω/km)";
                            dataGridView1.Columns[2].HeaderText = "Reactance at 50Hz(Ω/km)";
                            dataGridView1.Columns[3].HeaderText = "DC Resistance at 90°C(Ω/km)";
                            dataGridView1.Columns[4].HeaderText = "In Ground CCC(A)";
                            dataGridView1.Columns[5].HeaderText = "In Air CCC(A)";
                            SetDGVRuntimeProperties(dataGridView1, 6);
                            break;
                        default:
                            dataGridView1.DataSource = null;
                            break;
                    }
                }
                else if (comboBoxInsulation.Text == "PVC")
                {
                    switch (noCores)
                    {
                        case 2:
                            //bound datagridview to datatable
                            ResetDataTable(dtPVC2);
                            dataGridView1.DataSource = dtPVC2;
                            dataGridView1.Columns[0].HeaderText = "Size (mm²)";
                            dataGridView1.Columns[1].HeaderText = "AC Resistance at 70°C(Ω/km)";
                            dataGridView1.Columns[2].HeaderText = "Reactance at 50Hz(Ω/km)";
                            dataGridView1.Columns[3].HeaderText = "DC Resistance at 70°C(Ω/km)";
                            dataGridView1.Columns[4].HeaderText = "In Ground CCC(A)";
                            dataGridView1.Columns[5].HeaderText = "In Air CCC(A)";
                            SetDGVRuntimeProperties(dataGridView1, 6);
                            break;
                        case 3:
                            //bound datagridview to datatable
                            ResetDataTable(dtPVC3);
                            dataGridView1.DataSource = dtPVC3;
                            dataGridView1.Columns[0].HeaderText = "Size (mm²)";
                            dataGridView1.Columns[1].HeaderText = "AC Resistance at 70°C(Ω/km)";
                            dataGridView1.Columns[2].HeaderText = "Reactance at 50Hz(Ω/km)";
                            dataGridView1.Columns[3].HeaderText = "DC Resistance at 70°C(Ω/km)";
                            dataGridView1.Columns[4].HeaderText = "In Ground CCC(A)";
                            dataGridView1.Columns[5].HeaderText = "In Air CCC(A)";
                            SetDGVRuntimeProperties(dataGridView1, 6);
                            break;
                        case 4:
                            //bound datagridview to datatable
                            ResetDataTable(dtPVC4);
                            dataGridView1.DataSource = dtPVC4;
                            dataGridView1.Columns[0].HeaderText = "Size (mm²)";
                            dataGridView1.Columns[1].HeaderText = "AC Resistance at 70°C(Ω/km)";
                            dataGridView1.Columns[2].HeaderText = "Reactance at 50Hz(Ω/km)";
                            dataGridView1.Columns[3].HeaderText = "DC Resistance at 70°C(Ω/km)";
                            dataGridView1.Columns[4].HeaderText = "In Ground CCC(A)";
                            dataGridView1.Columns[5].HeaderText = "In Air CCC(A)";
                            SetDGVRuntimeProperties(dataGridView1, 6);
                            break;
                        default:
                            dataGridView1.DataSource = null;
                            break;
                    }

                }
                else
                {
                    dataGridView1.DataSource = null;
                }
            }
            else
            {
                dataGridView1.DataSource = null;
            }
        }

        private void ResetDataTable(DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                for (int k = 1; k < dt.Columns.Count; k++)
                {
                    dr[k] = null;
                }
            }
        }

        private void ChooseNECDatabase()
        {
            dataGridView1.DataSource = null;
            if (comboBoxConductor.Text == "Copper")
            {
                switch (comboBoxInsulation.Text)
                {
                    case "TW/UF":
                        ResetDataTable(dtNEC00);
                        dataGridView1.DataSource = dtNEC00;
                        dataGridView1.Columns[0].HeaderText = "Size (AWM/kcmil)";
                        dataGridView1.Columns[1].HeaderText = "AC Resistance at 60°C(Ω/kfeet)";
                        dataGridView1.Columns[2].HeaderText = "Reactance at 60Hz(Ω/kfeet)";
                        dataGridView1.Columns[3].HeaderText = "Current Carrying Capacity (A)";
                        SetDGVRuntimeProperties(dataGridView1, 4);
                        break;

                    case "RHW/THW/THWN/USE/ZW":
                        ResetDataTable(dtNEC10);
                        dataGridView1.DataSource = dtNEC10;
                        dataGridView1.Columns[0].HeaderText = "Size (AWM/kcmil)";
                        dataGridView1.Columns[1].HeaderText = "AC Resistance at 75°C(Ω/kfeet)";
                        dataGridView1.Columns[2].HeaderText = "Reactance at 60Hz(Ω/kfeet)";
                        dataGridView1.Columns[3].HeaderText = "Current Carrying Capacity (A)";
                        SetDGVRuntimeProperties(dataGridView1, 4);
                        break;
                    case "TBS/SA/SIS/FEP/FEPB/MI/RHH/RHW-2/THHN/THW-2/THWN-2/USE-2/XHH/XHHW-2/ZW-2":
                        ResetDataTable(dtNEC20);
                        dataGridView1.DataSource = dtNEC20;
                        dataGridView1.Columns[0].HeaderText = "Size (AWM/kcmil)";
                        dataGridView1.Columns[1].HeaderText = "AC Resistance at 90°C(Ω/kfeet)";
                        dataGridView1.Columns[2].HeaderText = "Reactance at 60Hz(Ω/kfeet)";
                        dataGridView1.Columns[3].HeaderText = "Current Carrying Capacity (A)";
                        SetDGVRuntimeProperties(dataGridView1, 4);
                        break;
                    default:
                        dataGridView1.DataSource = null;
                        break;
                }
            }
            else if (comboBoxConductor.Text == "Aluminium")
            {

                switch (comboBoxInsulation.Text)
                {
                    case "TW/UF":
                        ResetDataTable(dtNEC01);
                        dataGridView1.DataSource = dtNEC01;
                        dataGridView1.Columns[0].HeaderText = "Size (AWM/kcmil)";
                        dataGridView1.Columns[1].HeaderText = "AC Resistance at 60°C(Ω/kfeet)";
                        dataGridView1.Columns[2].HeaderText = "Reactance at 60Hz(Ω/kfeet)";
                        dataGridView1.Columns[3].HeaderText = "Current Carrying Capacity (A)";
                        SetDGVRuntimeProperties(dataGridView1, 4);
                        break;

                    case "RHW/THW/THWN/USE/ZW":
                        ResetDataTable(dtNEC11);
                        dataGridView1.DataSource = dtNEC11;
                        dataGridView1.Columns[0].HeaderText = "Size (AWM/kcmil)";
                        dataGridView1.Columns[1].HeaderText = "AC Resistance at 75°C(Ω/kfeet)";
                        dataGridView1.Columns[2].HeaderText = "Reactance at 60Hz(Ω/kfeet)";
                        dataGridView1.Columns[3].HeaderText = "Current Carrying Capacity (A)";
                        SetDGVRuntimeProperties(dataGridView1, 4);
                        break;
                    case "TBS/SA/SIS/FEP/FEPB/MI/RHH/RHW-2/THHN/THW-2/THWN-2/USE-2/XHH/XHHW-2/ZW-2":
                        ResetDataTable(dtNEC21);
                        dataGridView1.DataSource = dtNEC21;
                        dataGridView1.Columns[0].HeaderText = "Size (AWM/kcmil)";
                        dataGridView1.Columns[1].HeaderText = "AC Resistance at 90°C(Ω/kfeet)";
                        dataGridView1.Columns[2].HeaderText = "Reactance at 60Hz(Ω/kfeet)";
                        dataGridView1.Columns[3].HeaderText = "Current Carrying Capacity (A)";
                        SetDGVRuntimeProperties(dataGridView1, 4);
                        break;
                    default:
                        dataGridView1.DataSource = null;
                        break;
                }
            }
            else
            {
                dataGridView1.DataSource = null;
            }
        }

        //Disable DataGridView Column Sorting and makes first column (wire size data) Read only 
        private void SetDGVRuntimeProperties(DataGridView dgv, int len)
        {
            //disable column sorting
            for (int i = 0; i < len; i++)
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
            if ((textBoxName.Text != "") && (comboBoxInsulation.Text != "") && (comboBoxConductor.Text != "") && ((IECStandard && (comboBoxCores.Text != "")) || (NECStandard)))
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
        //  VALIDATION FUNCTIONS
        //
        //

        //variables
        bool[,] datAvailable = new bool[17, 6];
        bool[,] datAvailableNEC = new bool[21, 6];
        int lengthColNEC = 4;
        int lengthRowNEC = 21;

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

        private void checkAvailabilityNEC()
        {
            bool terisi;
            //NEC00
            for (int i = 0; i < lengthRowNEC; i++)
            {
                int j = 0;
                terisi = true;
                while (j < lengthColNEC)
                {
                    if (Convert.ToString(dtNEC00.Rows[i].ItemArray[j]) == "")
                    {
                        terisi = false;
                    }
                    j++;
                }
                if (terisi)
                {
                    datAvailableNEC[i, 0] = true;
                }
                else
                {
                    datAvailableNEC[i, 0] = false;
                }
            }


            //NEC10
            for (int i = 0; i < lengthRowNEC; i++)
            {
                int j = 0;
                terisi = true;
                while (j < lengthColNEC)
                {
                    if (Convert.ToString(dtNEC10.Rows[i].ItemArray[j]) == "")
                    {
                        terisi = false;
                    }
                    j++;
                }
                if (terisi)
                {
                    datAvailableNEC[i, 1] = true;
                }
                else
                {
                    datAvailableNEC[i, 1] = false;
                }
            }


            //NEC20
            for (int i = 0; i < lengthRowNEC; i++)
            {
                int j = 0;
                terisi = true;
                while (j < lengthColNEC)
                {
                    if (Convert.ToString(dtNEC20.Rows[i].ItemArray[j]) == "")
                    {
                        terisi = false;
                    }
                    j++;
                }
                if (terisi)
                {
                    datAvailableNEC[i, 2] = true;
                }
                else
                {
                    datAvailableNEC[i, 2] = false;
                }
            }


            //NEC01
            for (int i = 0; i < lengthRowNEC; i++)
            {
                int j = 0;
                terisi = true;
                while (j < lengthColNEC)
                {
                    if (Convert.ToString(dtNEC01.Rows[i].ItemArray[j]) == "")
                    {
                        terisi = false;
                    }
                    j++;
                }
                if (terisi)
                {
                    datAvailableNEC[i, 3] = true;
                }
                else
                {
                    datAvailableNEC[i, 3] = false;
                }
            }


            //NEC11
            for (int i = 0; i < lengthRowNEC; i++)
            {
                int j = 0;
                terisi = true;
                while (j < lengthColNEC)
                {
                    if (Convert.ToString(dtNEC11.Rows[i].ItemArray[j]) == "")
                    {
                        terisi = false;
                    }
                    j++;
                }
                if (terisi)
                {
                    datAvailableNEC[i, 4] = true;
                }
                else
                {
                    datAvailableNEC[i, 4] = false;
                }
            }


            //NEC21
            for (int i = 0; i < lengthRowNEC; i++)
            {
                int j = 0;
                terisi = true;
                while (j < lengthColNEC)
                {
                    if (Convert.ToString(dtNEC21.Rows[i].ItemArray[j]) == "")
                    {
                        terisi = false;
                    }
                    j++;
                }
                if (terisi)
                {
                    datAvailableNEC[i, 5] = true;
                }
                else
                {
                    datAvailableNEC[i, 5] = false;
                }
            }

        }

        //check if the new IEC database input is valid
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

        //check whether the new NEC database input is valid
        private bool cekValidasiTableNEC()
        {
            bool inVal;
            int count;
            inVal = false;
            int i = 0;
            int j;

            //validate NEC00
            while ((i < lengthRowNEC) && !inVal)
            {
                j = 1;
                count = 0;
                while (j < 4)
                {
                    if (Convert.ToString(dtNEC00.Rows[i].ItemArray[j]) == "")
                    {
                        count++;
                    }
                    j++;
                }
                if ((count < 3) && (count > 0))
                {
                    inVal = true;
                }
                i++;
            }


            //validate NEC10
            i = 0;
            while ((i < lengthRowNEC) && !inVal)
            {
                j = 1;
                count = 0;
                while (j < 4)
                {
                    if (Convert.ToString(dtNEC10.Rows[i].ItemArray[j]) == "")
                    {
                        count++;
                    }
                    j++;
                }
                if ((count < 3) && (count > 0))
                {
                    inVal = true;
                }
                i++;
            }


            //validate NEC20
            i = 0;
            while ((i < lengthRowNEC) && !inVal)
            {
                j = 1;
                count = 0;
                while (j < 4)
                {
                    if (Convert.ToString(dtNEC20.Rows[i].ItemArray[j]) == "")
                    {
                        count++;
                    }
                    j++;
                }
                if ((count < 3) && (count > 0))
                {
                    inVal = true;
                }
                i++;
            }


            //validate NEC01
            i = 0;
            while ((i < lengthRowNEC) && !inVal)
            {
                j = 1;
                count = 0;
                while (j < 4)
                {
                    if (Convert.ToString(dtNEC01.Rows[i].ItemArray[j]) == "")
                    {
                        count++;
                    }
                    j++;
                }
                if ((count < 3) && (count > 0))
                {
                    inVal = true;
                }
                i++;
            }


            //validate NEC011
            i = 0;
            while ((i < lengthRowNEC) && !inVal)
            {
                j = 1;
                count = 0;
                while (j < 4)
                {
                    if (Convert.ToString(dtNEC11.Rows[i].ItemArray[j]) == "")
                    {
                        count++;
                    }
                    j++;
                }
                if ((count < 3) && (count > 0))
                {
                    inVal = true;
                }
                i++;
            }


            //validate NEC21
            i = 0;
            while ((i < lengthRowNEC) && !inVal)
            {
                j = 1;
                count = 0;
                while (j < 4)
                {
                    if (Convert.ToString(dtNEC21.Rows[i].ItemArray[j]) == "")
                    {
                        count++;
                    }
                    j++;
                }
                if ((count < 3) && (count > 0))
                {
                    inVal = true;
                }
                i++;
            }

            //return the validity of NEC database input
            return inVal;
        }

        //finalizing IEC cable database
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

        //finalizing NEC cable database
        private void finalCableDataNEC()
        {
            int k;

            //Save NEC00 Cable Data
            k = 0;
            dtNEC00final = dtNEC00.Copy();
            for (int i = 0; i < lengthRowNEC; i++)
            {
                if (!datAvailableNEC[i, 0])
                {
                    dtNEC00final.Rows.RemoveAt(k);
                }
                else
                {
                    k++;
                }
            }


            //Save NEC10 Cable Data
            k = 0;
            dtNEC10final = dtNEC10.Copy();
            for (int i = 0; i < lengthRowNEC; i++)
            {
                if (!datAvailableNEC[i, 1])
                {
                    dtNEC10final.Rows.RemoveAt(k);
                }
                else
                {
                    k++;
                }
            }


            //Save NEC20 Cable Data
            k = 0;
            dtNEC20final = dtNEC20.Copy();
            for (int i = 0; i < lengthRowNEC; i++)
            {
                if (!datAvailableNEC[i, 2])
                {
                    dtNEC20final.Rows.RemoveAt(k);
                }
                else
                {
                    k++;
                }
            }


            //Save NEC01 Cable Data
            k = 0;
            dtNEC01final = dtNEC01.Copy();
            for (int i = 0; i < lengthRowNEC; i++)
            {
                if (!datAvailableNEC[i, 3])
                {
                    dtNEC01final.Rows.RemoveAt(k);
                }
                else
                {
                    k++;
                }
            }


            //Save NEC11 Cable Data
            k = 0;
            dtNEC11final = dtNEC11.Copy();
            for (int i = 0; i < lengthRowNEC; i++)
            {
                if (!datAvailableNEC[i, 4])
                {
                    dtNEC11final.Rows.RemoveAt(k);
                }
                else
                {
                    k++;
                }
            }


            //Save NEC10 Cable Data
            k = 0;
            dtNEC21final = dtNEC21.Copy();
            for (int i = 0; i < lengthRowNEC; i++)
            {
                if (!datAvailableNEC[i, 5])
                {
                    dtNEC21final.Rows.RemoveAt(k);
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

        string sysPath, oldFile, NewFile;
        DirectoryInfo di;

        DataSet viewCableDS;
        DataTable dtXLPE2view;
        DataTable dtXLPE3view;
        DataTable dtXLPE4view;
        DataTable dtPVC2view;
        DataTable dtPVC3view;
        DataTable dtPVC4view;


        DataTable dtNEC00view;
        DataTable dtNEC10view;
        DataTable dtNEC20view;
        DataTable dtNEC01view;
        DataTable dtNEC11view;
        DataTable dtNEC21view;

        int xlpe2coreLength, xlpe3coreLength, xlpe4coreLength, pvc2coreLength, pvc3coreLength, pvc4coreLength;

        // reflect the number of IEC/NEC database count
        int IECFiles, NECFiles;


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

        }

        private void ButtonCancel2_Click(object sender, EventArgs e)
        {
            buttonEdit.Text = "Edit";
            dataGridView2.ReadOnly = true;
            if (IECSelected)
            {
                ReadIECDatabase();
            }
            else if (NECSelected)
            {
                ReadNECDatabase();
            }
            ChooseDatabaseView();
            comboBoxDatabase.Enabled = true;
            buttonRename.Enabled = true;
            buttonDelete.Enabled = true;
            buttonCancel2.Visible = false;
            radioButton2.Enabled = true;
            radioButtonNECView.Enabled = true;
            tabControl1.TabPages[1].Enabled = true;
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
                comboBoxDatabase.Enabled = false;
                radioButton2.Enabled = false;
                radioButtonNECView.Enabled = false;
                if (IECSelected)
                {
                    FillDataTableEdit();
                }
                else if (NECSelected)
                {
                    FillDataTableNECEdit();
                }

                //Make data editable
                dataGridView2.ReadOnly = false;
                dataGridView2.Columns[0].ReadOnly = true;
                tabControl1.TabPages[1].Enabled = false;
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
                                databaseEdited = true;
                                radioButton2.Enabled = true;
                                radioButtonNECView.Enabled = true;
                                tabControl1.TabPages[1].Enabled = true;
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
                else if ((fileName != "") && NECSelected)
                {
                    dtNEC00final = new DataTable();
                    dtNEC10final = new DataTable();
                    dtNEC20final = new DataTable();
                    dtNEC01final = new DataTable();
                    dtNEC11final = new DataTable();
                    dtNEC21final = new DataTable();


                    inValid = CheckEditTableValidationNEC();
                    if (!inValid)
                    {
                        CheckEditAvailabilityNEC();
                        FinalCableDataEditNEC();
                        if ((dtNEC00final.Rows.Count > 0) || (dtNEC10final.Rows.Count > 0) || (dtNEC20final.Rows.Count > 0) || (dtNEC01final.Rows.Count > 0)
                            || (dtNEC11final.Rows.Count > 0) || (dtNEC21final.Rows.Count > 0))
                        {
                            DialogResult dr = MessageBox.Show("Edit existing database?", "Confirm Edit",
                                MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                            if (dr == DialogResult.OK)
                            {
                                SaveNECDatabaseView();
                                buttonEdit.Text = "Edit";
                                dataGridView2.ReadOnly = true;
                                ReadNECDatabase();
                                ChooseDatabaseView();
                                comboBoxDatabase.Enabled = true;
                                buttonRename.Enabled = true;
                                buttonDelete.Enabled = true;
                                buttonCancel2.Visible = false;
                                databaseEdited = true;
                                radioButton2.Enabled = true;
                                radioButtonNECView.Enabled = true;
                                tabControl1.TabPages[1].Enabled = true;
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
            ds.DataSetName = "IEC_Cable_Database";
            ds.WriteXml(xmlSave);
            xmlSave.Close();

            MessageBox.Show("Data Saved!", "Cable Database", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        //save the edited NEC database
        private void SaveNECDatabaseView()
        {
            var systemPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            Directory.CreateDirectory(systemPath + "/Cable Sizing/NEC_database");
            var saveDir = Path.Combine(systemPath + "/Cable Sizing/NEC_database", fileName + ".xml");

            DataSet ds = new DataSet();
            ds.Tables.Add(dtNEC00final);
            ds.Tables.Add(dtNEC10final);
            ds.Tables.Add(dtNEC20final);
            ds.Tables.Add(dtNEC01final);
            ds.Tables.Add(dtNEC11final);
            ds.Tables.Add(dtNEC21final);

            XmlTextWriter xmlSave = new XmlTextWriter(saveDir, Encoding.UTF8);
            xmlSave.Formatting = Formatting.Indented;
            ds.DataSetName = "NEC_Cable_Database";
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


        //check whether the new NEC database input is valid
        private bool CheckEditTableValidationNEC()
        {
            bool inVal;
            int count;
            inVal = false;
            int i = 0;
            int j;

            //validate NEC00
            while ((i < lengthRowNEC) && !inVal)
            {
                j = 1;
                count = 0;
                while (j < 4)
                {
                    if (Convert.ToString(dtNEC00view.Rows[i].ItemArray[j]) == "")
                    {
                        count++;
                    }
                    j++;
                }
                if ((count < 3) && (count > 0))
                {
                    inVal = true;
                }
                i++;
            }


            //validate NEC10
            i = 0;
            while ((i < lengthRowNEC) && !inVal)
            {
                j = 1;
                count = 0;
                while (j < 4)
                {
                    if (Convert.ToString(dtNEC10view.Rows[i].ItemArray[j]) == "")
                    {
                        count++;
                    }
                    j++;
                }
                if ((count < 3) && (count > 0))
                {
                    inVal = true;
                }
                i++;
            }


            //validate NEC20
            i = 0;
            while ((i < lengthRowNEC) && !inVal)
            {
                j = 1;
                count = 0;
                while (j < 4)
                {
                    if (Convert.ToString(dtNEC20view.Rows[i].ItemArray[j]) == "")
                    {
                        count++;
                    }
                    j++;
                }
                if ((count < 3) && (count > 0))
                {
                    inVal = true;
                }
                i++;
            }


            //validate NEC01
            i = 0;
            while ((i < lengthRowNEC) && !inVal)
            {
                j = 1;
                count = 0;
                while (j < 4)
                {
                    if (Convert.ToString(dtNEC01view.Rows[i].ItemArray[j]) == "")
                    {
                        count++;
                    }
                    j++;
                }
                if ((count < 3) && (count > 0))
                {
                    inVal = true;
                }
                i++;
            }


            //validate NEC011
            i = 0;
            while ((i < lengthRowNEC) && !inVal)
            {
                j = 1;
                count = 0;
                while (j < 4)
                {
                    if (Convert.ToString(dtNEC11view.Rows[i].ItemArray[j]) == "")
                    {
                        count++;
                    }
                    j++;
                }
                if ((count < 3) && (count > 0))
                {
                    inVal = true;
                }
                i++;
            }


            //validate NEC21
            i = 0;
            while ((i < lengthRowNEC) && !inVal)
            {
                j = 1;
                count = 0;
                while (j < 4)
                {
                    if (Convert.ToString(dtNEC21view.Rows[i].ItemArray[j]) == "")
                    {
                        count++;
                    }
                    j++;
                }
                if ((count < 3) && (count > 0))
                {
                    inVal = true;
                }
                i++;
            }

            //return the validity of NEC database input
            return inVal;
        }

        string deleteDir;
        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Delete cable data file \"" + fileName + "\"?", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (dr == DialogResult.OK)
            {
                if (IECSelected)
                {
                    sysPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    Directory.CreateDirectory(sysPath + "/Cable Sizing/IEC_database");
                    deleteDir = Path.Combine(sysPath + "/Cable Sizing/IEC_database", fileName + ".xml");
                }
                else if (NECSelected)
                {

                    sysPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    Directory.CreateDirectory(sysPath + "/Cable Sizing/NEC_database");
                    deleteDir = Path.Combine(sysPath + "/Cable Sizing/NEC_database", fileName + ".xml");
                }
                
                File.Delete(deleteDir);
                MessageBox.Show("Data \"" + fileName + "\" deleted successfully!", "Cable Sizing", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (IECSelected)
                {
                    LoadIECDatabase();
                }
                else if (NECSelected)
                {
                    LoadNECDatabase();
                }
                
                databaseEdited = true;
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
                if (IECSelected)
                {
                    sysPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    oldFile = Path.Combine(sysPath + "/Cable Sizing/IEC_database", fileName + ".xml");
                    NewFile = Path.Combine(sysPath + "/Cable Sizing/IEC_database", FormRename.NewName + ".xml");
                }
                else if (NECSelected)
                {

                    sysPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    oldFile = Path.Combine(sysPath + "/Cable Sizing/NEC_database", fileName + ".xml");
                    NewFile = Path.Combine(sysPath + "/Cable Sizing/NEC_database", FormRename.NewName + ".xml");
                }


                File.Move(oldFile, NewFile);
                if (IECSelected)
                {
                    LoadIECDatabase();
                }
                else
                {
                    LoadNECDatabase();
                }
                comboBoxDatabase.Text = FormRename.NewName;
                databaseEdited = true;
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


        private void CheckEditAvailabilityNEC()
        {
            bool terisi;
            //NEC00
            for (int i = 0; i < lengthRowNEC; i++)
            {
                int j = 0;
                terisi = true;
                while (j < lengthColNEC)
                {
                    if (Convert.ToString(dtNEC00view.Rows[i].ItemArray[j]) == "")
                    {
                        terisi = false;
                    }
                    j++;
                }
                if (terisi)
                {
                    datAvailableNEC[i, 0] = true;
                }
                else
                {
                    datAvailableNEC[i, 0] = false;
                }
            }


            //NEC10
            for (int i = 0; i < lengthRowNEC; i++)
            {
                int j = 0;
                terisi = true;
                while (j < lengthColNEC)
                {
                    if (Convert.ToString(dtNEC10view.Rows[i].ItemArray[j]) == "")
                    {
                        terisi = false;
                    }
                    j++;
                }
                if (terisi)
                {
                    datAvailableNEC[i, 1] = true;
                }
                else
                {
                    datAvailableNEC[i, 1] = false;
                }
            }


            //NEC20
            for (int i = 0; i < lengthRowNEC; i++)
            {
                int j = 0;
                terisi = true;
                while (j < lengthColNEC)
                {
                    if (Convert.ToString(dtNEC20view.Rows[i].ItemArray[j]) == "")
                    {
                        terisi = false;
                    }
                    j++;
                }
                if (terisi)
                {
                    datAvailableNEC[i, 2] = true;
                }
                else
                {
                    datAvailableNEC[i, 2] = false;
                }
            }


            //NEC01
            for (int i = 0; i < lengthRowNEC; i++)
            {
                int j = 0;
                terisi = true;
                while (j < lengthColNEC)
                {
                    if (Convert.ToString(dtNEC01view.Rows[i].ItemArray[j]) == "")
                    {
                        terisi = false;
                    }
                    j++;
                }
                if (terisi)
                {
                    datAvailableNEC[i, 3] = true;
                }
                else
                {
                    datAvailableNEC[i, 3] = false;
                }
            }


            //NEC11
            for (int i = 0; i < lengthRowNEC; i++)
            {
                int j = 0;
                terisi = true;
                while (j < lengthColNEC)
                {
                    if (Convert.ToString(dtNEC11view.Rows[i].ItemArray[j]) == "")
                    {
                        terisi = false;
                    }
                    j++;
                }
                if (terisi)
                {
                    datAvailableNEC[i, 4] = true;
                }
                else
                {
                    datAvailableNEC[i, 4] = false;
                }
            }


            //NEC21
            for (int i = 0; i < lengthRowNEC; i++)
            {
                int j = 0;
                terisi = true;
                while (j < lengthColNEC)
                {
                    if (Convert.ToString(dtNEC21view.Rows[i].ItemArray[j]) == "")
                    {
                        terisi = false;
                    }
                    j++;
                }
                if (terisi)
                {
                    datAvailableNEC[i, 5] = true;
                }
                else
                {
                    datAvailableNEC[i, 5] = false;
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

        //Fill selected NEC database to the table
        private void FillDataTableNECEdit()
        {
            //For XLPE 2 Core
            for (int i = 0; i < lengthRowNEC; i++)
            {
                DataRow row = dtNEC00view.NewRow();
                if (i > dtNEC00view.Rows.Count - 1)
                {
                    row[0] = nec_cablesize[i];
                    dtNEC00view.Rows.InsertAt(row, i);
                }
                else if (nec_cablesize[i] != Convert.ToString(dtNEC00view.Rows[i].ItemArray[0]))
                {
                    row[0] = nec_cablesize[i];
                    dtNEC00view.Rows.InsertAt(row, i);
                }
            }

            //For XLPE 3 Core
            for (int i = 0; i < lengthRowNEC; i++)
            {
                DataRow row = dtNEC10view.NewRow();
                if (i > dtNEC10view.Rows.Count - 1)
                {
                    row[0] = nec_cablesize[i];
                    dtNEC10view.Rows.InsertAt(row, i);
                }
                else if (nec_cablesize[i] != Convert.ToString(dtNEC10view.Rows[i].ItemArray[0]))
                {
                    row[0] = nec_cablesize[i];
                    dtNEC10view.Rows.InsertAt(row, i);
                }
            }

            //For XLPE 4 Core
            for (int i = 0; i < lengthRowNEC; i++)
            {
                DataRow row = dtNEC20view.NewRow();
                if (i > dtNEC20view.Rows.Count - 1)
                {
                    row[0] = nec_cablesize[i];
                    dtNEC20view.Rows.InsertAt(row, i);
                }
                else if (nec_cablesize[i] != Convert.ToString(dtNEC20view.Rows[i].ItemArray[0]))
                {
                    row[0] = nec_cablesize[i];
                    dtNEC20view.Rows.InsertAt(row, i);
                }
            }

            //For PVC 2 Core
            for (int i = 0; i < lengthRowNEC; i++)
            {
                DataRow row = dtNEC01view.NewRow();
                if (i > dtNEC01view.Rows.Count - 1)
                {
                    row[0] = nec_cablesize[i];
                    dtNEC01view.Rows.InsertAt(row, i);
                }
                else if (nec_cablesize[i] != Convert.ToString(dtNEC01view.Rows[i].ItemArray[0]))
                {
                    row[0] = nec_cablesize[i];
                    dtNEC01view.Rows.InsertAt(row, i);
                }
            }

            //For PVC 3 Core
            for (int i = 0; i < lengthRowNEC; i++)
            {
                DataRow row = dtNEC11view.NewRow();
                if (i > dtNEC11view.Rows.Count - 1)
                {
                    row[0] = nec_cablesize[i];
                    dtNEC11view.Rows.InsertAt(row, i);
                }
                else if (nec_cablesize[i] != Convert.ToString(dtNEC11view.Rows[i].ItemArray[0]))
                {
                    row[0] = nec_cablesize[i];
                    dtNEC11view.Rows.InsertAt(row, i);
                }
            }


            //For PVC 4 Core
            for (int i = 0; i < lengthRowNEC; i++)
            {
                DataRow row = dtNEC21view.NewRow();
                if (i > dtNEC21view.Rows.Count - 1)
                {
                    row[0] = nec_cablesize[i];
                    dtNEC21view.Rows.InsertAt(row, i);
                }
                else if (nec_cablesize[i] != Convert.ToString(dtNEC21view.Rows[i].ItemArray[0]))
                {
                    row[0] = nec_cablesize[i];
                    dtNEC21view.Rows.InsertAt(row, i);
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


        //finalizing Edit on NEC cable database
        private void FinalCableDataEditNEC()
        {
            int k;

            //Save NEC00 Cable Data
            k = 0;
            dtNEC00final = dtNEC00view.Copy();
            for (int i = 0; i < lengthRowNEC; i++)
            {
                if (!datAvailableNEC[i, 0])
                {
                    dtNEC00final.Rows.RemoveAt(k);
                }
                else
                {
                    k++;
                }
            }


            //Save NEC10 Cable Data
            k = 0;
            dtNEC10final = dtNEC10view.Copy();
            for (int i = 0; i < lengthRowNEC; i++)
            {
                if (!datAvailableNEC[i, 1])
                {
                    dtNEC10final.Rows.RemoveAt(k);
                }
                else
                {
                    k++;
                }
            }


            //Save NEC20 Cable Data
            k = 0;
            dtNEC20final = dtNEC20view.Copy();
            for (int i = 0; i < lengthRowNEC; i++)
            {
                if (!datAvailableNEC[i, 2])
                {
                    dtNEC20final.Rows.RemoveAt(k);
                }
                else
                {
                    k++;
                }
            }


            //Save NEC01 Cable Data
            k = 0;
            dtNEC01final = dtNEC01view.Copy();
            for (int i = 0; i < lengthRowNEC; i++)
            {
                if (!datAvailableNEC[i, 3])
                {
                    dtNEC01final.Rows.RemoveAt(k);
                }
                else
                {
                    k++;
                }
            }


            //Save NEC11 Cable Data
            k = 0;
            dtNEC11final = dtNEC11view.Copy();
            for (int i = 0; i < lengthRowNEC; i++)
            {
                if (!datAvailableNEC[i, 4])
                {
                    dtNEC11final.Rows.RemoveAt(k);
                }
                else
                {
                    k++;
                }
            }


            //Save NEC10 Cable Data
            k = 0;
            dtNEC21final = dtNEC21view.Copy();
            for (int i = 0; i < lengthRowNEC; i++)
            {
                if (!datAvailableNEC[i, 5])
                {
                    dtNEC21final.Rows.RemoveAt(k);
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
                LoadIECDatabase();
            }
            else
            {
                IECSelected = false;
                NECSelected = true;
                LoadNECDatabase();
            }
            FillEditCbView();
            buttonEdit.Enabled = false;
            buttonRename.Enabled = false;
            buttonDelete.Enabled = false;
        }
        private void FillEditCbView()
        {
            if (IECSelected)
            {
                //fill insulation combobox
                dataGridView2.DataSource = null;
                comboBox1.SelectedIndex = -1;
                comboBox1.DropDownWidth = 98;
                viewinsulation = "";
                comboBox1.Items.Clear();
                comboBox1.Items.Insert(0, "XLPE");
                comboBox1.Items.Insert(1, "PVC");

                //fill conductor type
                comboBox2.SelectedIndex = -1;
                viewconductor = "";
                comboBox2.Items.Clear();
                comboBox2.Items.Insert(0, "Copper");

                //show no of cores option
                comboBox3.SelectedIndex = -1;
                viewCores = 0;
                comboBox3.Visible = true;
                label6.Visible = true;

            }
            else if (NECSelected)
            {
                //fill insulation combobox
                dataGridView2.DataSource = null;
                comboBox1.SelectedIndex = -1;
                comboBox1.DropDownWidth = 500;
                viewinsulation = "";
                comboBox1.Items.Clear();
                comboBox1.Items.Insert(0, "TW/UF");
                comboBox1.Items.Insert(1, "RHW/THW/THWN/USE/ZW");
                comboBox1.Items.Insert(2, "TBS/SA/SIS/FEP/FEPB/MI/RHH/RHW-2/THHN/THW-2/THWN-2/USE-2/XHH/XHHW-2/ZW-2");

                //fill conductor type
                comboBox2.SelectedIndex = -1;
                viewconductor = "";
                comboBox2.Items.Clear();
                comboBox2.Items.Insert(0, "Copper");
                comboBox2.Items.Insert(1, "Aluminium");

                //hide no of cores option
                comboBox3.SelectedIndex = -1;
                viewCores = 0;
                comboBox3.Visible = false;
                label6.Visible = false;
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
            comboBoxDatabase.ResetText();
            fileName = "";
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

        private void LoadNECDatabase()
        {
            //read all file in database directory
            string systemPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            Directory.CreateDirectory(systemPath + "/Cable Sizing/NEC_database");
            string saveDir = (systemPath + "/Cable Sizing/NEC_database");

            di = new DirectoryInfo(saveDir);
            //get all file name in database directory
            FileInfo[] files = di.GetFiles("*.xml");
            NECFiles = files.Length;
            //fill vendor data from database
            comboBoxDatabase.Items.Clear();
            comboBoxDatabase.ResetText();
            fileName = "";
            //fill all saved database created by users
            for (int z = 0; z < NECFiles; z++)
            {
                string tempstring;
                tempstring = files[z].ToString();
                tempstring = tempstring.Replace(".xml", "");
                comboBoxDatabase.Items.Insert(z, tempstring);
            }
        }

        private void ComboBoxDatabase_SelectedIndexChanged(object sender, EventArgs e)
        {
            fileName = comboBoxDatabase.Text;
            if (IECSelected)
            {
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
            else if (NECSelected)
            {
                if (fileName != "")
                {
                    ReadNECDatabase();
                }
                else
                {
                    dtNEC00view = null;
                    dtNEC10view = null;
                    dtNEC20view = null;
                    dtNEC01view = null;
                    dtNEC11view = null;
                    dtNEC21view = null;
                }

                EnableRenameDelete();
                ChooseDatabaseView();
                EnableEdit();
            }
        }

        private void EnableEdit()
        {
            if ((fileName != "") && (fileName != "Sumi Indo Cable (Default)") && (viewconductor != "") && (viewinsulation != "") && ((IECSelected && (viewCores != 0)) ||(NECSelected)) )
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
            if (comboBox3.Text != "")
            {
                viewCores = int.Parse(comboBox3.Text);
            }
            else
            {
                viewCores = 0;
            }
            
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

        //Read the selected NEC database data
        internal void ReadNECDatabase()
        {
            string systemPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            Directory.CreateDirectory(systemPath + "/Cable Sizing/NEC_database");
            string saveDir = Path.Combine(systemPath + "/Cable Sizing/NEC_database", fileName + ".xml");

            //save database to a new dataset
            viewCableDS = new DataSet();
            viewCableDS.ReadXml(saveDir);

            //save each cable data table to their respective datatable
            if (viewCableDS.Tables.Contains("NEC00"))
            {
                dtNEC00view = viewCableDS.Tables["NEC00"].Copy();
            }
            else
            {
                dtNEC00view.Rows.Clear();
            }
            if (viewCableDS.Tables.Contains("NEC10"))
            {
                dtNEC10view = viewCableDS.Tables["NEC10"].Copy();
            }
            else
            {
                dtNEC10view.Rows.Clear();
            }
            if (viewCableDS.Tables.Contains("NEC20"))
            {
                dtNEC20view = viewCableDS.Tables["NEC20"].Copy();
            }
            else
            {
                dtNEC20view.Rows.Clear();
            }
            if (viewCableDS.Tables.Contains("NEC01"))
            {
                dtNEC01view = viewCableDS.Tables["NEC01"].Copy();
            }
            else
            {
                dtNEC01view.Rows.Clear();
            }
            if (viewCableDS.Tables.Contains("NEC11"))
            {
                dtNEC11view = viewCableDS.Tables["NEC11"].Copy();
            }
            else
            {
                dtNEC11view.Rows.Clear();
            }
            if (viewCableDS.Tables.Contains("NEC21"))
            {
                dtNEC21view = viewCableDS.Tables["NEC21"].Copy();
            }
            else
            {
                dtNEC21view.Rows.Clear();
            }
        }

        private void ChooseDatabaseView()
        {
            if (IECSelected)
            {
                ChooseIECDatabaseView();
            }
            else if (NECSelected)
            {
                ChooseNECDatabaseView();
            }
        }

        private void ChooseIECDatabaseView()
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
                            dataGridView2.Columns[0].HeaderText = "Size (mm²)";
                            dataGridView2.Columns[1].HeaderText = "AC Resistance at 90°C(Ω/km)";
                            dataGridView2.Columns[2].HeaderText = "Reactance at 50Hz(Ω/km)";
                            dataGridView2.Columns[3].HeaderText = "DC Resistance at 90°C(Ω/km)";
                            dataGridView2.Columns[4].HeaderText = "In Ground CCC(A)";
                            dataGridView2.Columns[5].HeaderText = "In Air CCC(A)";
                            SetDGVRuntimeProperties(dataGridView2, 6);
                            break;
                        case 3:
                            //bound datagridview to datatable
                            dataGridView2.DataSource = dtXLPE3view;
                            dataGridView2.Columns[0].HeaderText = "Size (mm²)";
                            dataGridView2.Columns[1].HeaderText = "AC Resistance at 90°C(Ω/km)";
                            dataGridView2.Columns[2].HeaderText = "Reactance at 50Hz(Ω/km)";
                            dataGridView2.Columns[3].HeaderText = "DC Resistance at 90°C(Ω/km)";
                            dataGridView2.Columns[4].HeaderText = "In Ground CCC(A)";
                            dataGridView2.Columns[5].HeaderText = "In Air CCC(A)";
                            SetDGVRuntimeProperties(dataGridView2, 6);
                            break;
                        case 4:
                            //bound datagridview to datatable
                            dataGridView2.DataSource = dtXLPE4view;
                            dataGridView2.Columns[0].HeaderText = "Size (mm²)";
                            dataGridView2.Columns[1].HeaderText = "AC Resistance at 90°C(Ω/km)";
                            dataGridView2.Columns[2].HeaderText = "Reactance at 50Hz(Ω/km)";
                            dataGridView2.Columns[3].HeaderText = "DC Resistance at 90°C(Ω/km)";
                            dataGridView2.Columns[4].HeaderText = "In Ground CCC(A)";
                            dataGridView2.Columns[5].HeaderText = "In Air CCC(A)";
                            SetDGVRuntimeProperties(dataGridView2, 6);
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
                            dataGridView2.Columns[0].HeaderText = "Size (mm²)";
                            dataGridView2.Columns[1].HeaderText = "AC Resistance at 70°C(Ω/km)";
                            dataGridView2.Columns[2].HeaderText = "Reactance at 50Hz(Ω/km)";
                            dataGridView2.Columns[3].HeaderText = "DC Resistance at 70°C(Ω/km)";
                            dataGridView2.Columns[4].HeaderText = "In Ground CCC(A)";
                            dataGridView2.Columns[5].HeaderText = "In Air CCC(A)";
                            SetDGVRuntimeProperties(dataGridView2, 6);
                            break;
                        case 3:
                            //bound datagridview to datatable
                            dataGridView2.DataSource = dtPVC3view;
                            dataGridView2.Columns[1].HeaderText = "AC Resistance at 70°C(Ω/km)";
                            dataGridView2.Columns[2].HeaderText = "Reactance at 50Hz(Ω/km)";
                            dataGridView2.Columns[3].HeaderText = "DC Resistance at 70°C(Ω/km)";
                            dataGridView2.Columns[4].HeaderText = "In Ground CCC(A)";
                            dataGridView2.Columns[5].HeaderText = "In Air CCC(A)";
                            SetDGVRuntimeProperties(dataGridView2, 6);
                            break;
                        case 4:
                            //bound datagridview to datatable
                            dataGridView2.DataSource = dtPVC4view;
                            dataGridView2.Columns[1].HeaderText = "AC Resistance at 70°C(Ω/km)";
                            dataGridView2.Columns[2].HeaderText = "Reactance at 50Hz(Ω/km)";
                            dataGridView2.Columns[3].HeaderText = "DC Resistance at 70°C(Ω/km)";
                            dataGridView2.Columns[4].HeaderText = "In Ground CCC(A)";
                            dataGridView2.Columns[5].HeaderText = "In Air CCC(A)";
                            SetDGVRuntimeProperties(dataGridView2, 6);
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

        private void ChooseNECDatabaseView()
        {
            if (viewconductor == "Copper")
            {
                switch (viewinsulation)
                {
                    case "TW/UF":
                        dataGridView2.DataSource = dtNEC00view;
                        dataGridView2.Columns[0].HeaderText = "Size (AWM/kcmil)";
                        dataGridView2.Columns[1].HeaderText = "AC Resistance at 60°C(Ω/kfeet)";
                        dataGridView2.Columns[2].HeaderText = "Reactance at 60Hz(Ω/kfeet)";
                        dataGridView2.Columns[3].HeaderText = "Current Carrying Capacity (A)";
                        SetDGVRuntimeProperties(dataGridView2, 4);
                        break;

                    case "RHW/THW/THWN/USE/ZW":
                        dataGridView2.DataSource = dtNEC10view;
                        dataGridView2.Columns[0].HeaderText = "Size (AWM/kcmil)";
                        dataGridView2.Columns[1].HeaderText = "AC Resistance at 75°C(Ω/kfeet)";
                        dataGridView2.Columns[2].HeaderText = "Reactance at 60Hz(Ω/kfeet)";
                        dataGridView2.Columns[3].HeaderText = "Current Carrying Capacity (A)";
                        SetDGVRuntimeProperties(dataGridView2, 4);
                        break;
                    case "TBS/SA/SIS/FEP/FEPB/MI/RHH/RHW-2/THHN/THW-2/THWN-2/USE-2/XHH/XHHW-2/ZW-2":
                        dataGridView2.DataSource = dtNEC20view;
                        dataGridView2.Columns[0].HeaderText = "Size (AWM/kcmil)";
                        dataGridView2.Columns[1].HeaderText = "AC Resistance at 90°C(Ω/kfeet)";
                        dataGridView2.Columns[2].HeaderText = "Reactance at 60Hz(Ω/kfeet)";
                        dataGridView2.Columns[3].HeaderText = "Current Carrying Capacity (A)";
                        SetDGVRuntimeProperties(dataGridView2, 4);
                        break;
                    default:
                        dataGridView2.DataSource = null;
                        break;
                }
            }
            else if (viewconductor == "Aluminium")
            {

                switch (viewinsulation)
                {
                    case "TW/UF":
                        dataGridView2.DataSource = dtNEC01view;
                        dataGridView2.Columns[0].HeaderText = "Size (AWM/kcmil)";
                        dataGridView2.Columns[1].HeaderText = "AC Resistance at 60°C(Ω/kfeet)";
                        dataGridView2.Columns[2].HeaderText = "Reactance at 60Hz(Ω/kfeet)";
                        dataGridView2.Columns[3].HeaderText = "Current Carrying Capacity (A)";
                        SetDGVRuntimeProperties(dataGridView2, 4);
                        break;

                    case "RHW/THW/THWN/USE/ZW":
                        dataGridView2.DataSource = dtNEC11view;
                        dataGridView2.Columns[0].HeaderText = "Size (AWM/kcmil)";
                        dataGridView2.Columns[1].HeaderText = "AC Resistance at 75°C(Ω/kfeet)";
                        dataGridView2.Columns[2].HeaderText = "Reactance at 60Hz(Ω/kfeet)";
                        dataGridView2.Columns[3].HeaderText = "Current Carrying Capacity (A)";
                        SetDGVRuntimeProperties(dataGridView2, 4);
                        break;
                    case "TBS/SA/SIS/FEP/FEPB/MI/RHH/RHW-2/THHN/THW-2/THWN-2/USE-2/XHH/XHHW-2/ZW-2":
                        dataGridView2.DataSource = dtNEC21view;
                        dataGridView2.Columns[0].HeaderText = "Size (AWM/kcmil)";
                        dataGridView2.Columns[1].HeaderText = "AC Resistance at 90°C(Ω/kfeet)";
                        dataGridView2.Columns[2].HeaderText = "Reactance at 60Hz(Ω/kfeet)";
                        dataGridView2.Columns[3].HeaderText = "Current Carrying Capacity (A)";
                        SetDGVRuntimeProperties(dataGridView2, 4);
                        break;
                    default:
                        dataGridView2.DataSource = null;
                        break;
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
