using _11___DVLD_Project.Licenses.Local_Licenses;
using DVLD___Business_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _11___DVLD_Project.Licenses.Controls
{
    public partial class ctrlDriverLicenses : UserControl
    {
        private int _DriverID;
        private clsDriver _Driver;
        private DataTable _dtDriverLocalLicensesHistory;
        private DataTable _dtDriverInternationalLicensesHistory;
        public ctrlDriverLicenses()
        {
            InitializeComponent();
        }
        //private void _RefreshPeopleList()
        //{
        //    _dtDrivers = clsDriver.GetAllDrivers();
        //    dataGridView1.DataSource = _dtDrivers;
        //    dataGridView1.Font = new Font(dataGridView1.Font.FontFamily, 16);
        //    dataGridView1.RowTemplate.Height = 35;
        //    dataGridView1.Columns[0].Width = 140;
        //    dataGridView1.Columns[1].Width = 140;
        //    dataGridView1.Columns[2].Width = 160;
        //    dataGridView1.Columns[3].Width = 506;
        //    dataGridView1.Columns[4].Width = 285;
        //    dataGridView1.Columns[5].Width = 250;
        //    lblCount.Text = dataGridView1.Rows.Count.ToString();

        //}

        private void _LoadLocalLicenseInfo()
        {

            _dtDriverLocalLicensesHistory = clsDriver.GetLicenses(_DriverID);


            dgvLocalLicensesHistory.DataSource = _dtDriverLocalLicensesHistory;
            lblLocalLicensesRecords.Text = dgvLocalLicensesHistory.Rows.Count.ToString();

            if (dgvLocalLicensesHistory.Rows.Count > 0)
            {
                dgvLocalLicensesHistory.Columns[0].HeaderText = "Lic.ID";
                dgvLocalLicensesHistory.Columns[0].Width = 110;

                dgvLocalLicensesHistory.Columns[1].HeaderText = "App.ID";
                dgvLocalLicensesHistory.Columns[1].Width = 110;

                dgvLocalLicensesHistory.Columns[2].HeaderText = "Class Name";
                dgvLocalLicensesHistory.Columns[2].Width = 270;

                dgvLocalLicensesHistory.Columns[3].HeaderText = "Issue Date";
                dgvLocalLicensesHistory.Columns[3].Width = 173;

                dgvLocalLicensesHistory.Columns[4].HeaderText = "Expiration Date";
                dgvLocalLicensesHistory.Columns[4].Width = 173;

                dgvLocalLicensesHistory.Columns[5].HeaderText = "Is Active";
                dgvLocalLicensesHistory.Columns[5].Width = 100;

            }
        }

        private void _LoadInternationalLicenseInfo()
        {

            _dtDriverInternationalLicensesHistory = clsDriver.GetInternationalLicenses(_DriverID);


            dgvInternationalLicensesHistory.DataSource = _dtDriverInternationalLicensesHistory;
            lblInternationalLicensesRecords.Text = dgvInternationalLicensesHistory.Rows.Count.ToString();

            if (dgvInternationalLicensesHistory.Rows.Count > 0)
            {
                dgvInternationalLicensesHistory.Columns[0].HeaderText = "Int.License ID";
                dgvInternationalLicensesHistory.Columns[0].Width = 160;

                dgvInternationalLicensesHistory.Columns[1].HeaderText = "Application ID";
                dgvInternationalLicensesHistory.Columns[1].Width = 160;

                dgvInternationalLicensesHistory.Columns[2].HeaderText = "L.License ID";
                dgvInternationalLicensesHistory.Columns[2].Width = 140;

                dgvInternationalLicensesHistory.Columns[3].HeaderText = "Issue Date";
                dgvInternationalLicensesHistory.Columns[3].Width = 182;

                dgvInternationalLicensesHistory.Columns[4].HeaderText = "Expiration Date";
                dgvInternationalLicensesHistory.Columns[4].Width = 182;

                dgvInternationalLicensesHistory.Columns[5].HeaderText = "Is Active";
                dgvInternationalLicensesHistory.Columns[5].Width = 128;

            }
        }

        public void LoadInfo(int DriverID)
        {
            _DriverID = DriverID;
            _Driver = clsDriver.FindByDriverID(_DriverID);

            _LoadLocalLicenseInfo();
            _LoadInternationalLicenseInfo();

        }

        public void LoadInfoByPersonID(int PersonID)
        {

            _Driver = clsDriver.FindByPersonID(PersonID);

            if (_Driver == null)
            {
                MessageBox.Show("There is no driver linked with person with id = " + PersonID);
                return;
                
            }

            _DriverID = _Driver.DriverID;

            _LoadLocalLicenseInfo();
            _LoadInternationalLicenseInfo();
        }

        public void Clear()
        {
            _dtDriverLocalLicensesHistory.Clear();
            _dtDriverInternationalLicensesHistory.Clear();

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void ctrlDriverLicenses_Load(object sender, EventArgs e)
        {
            dgvLocalLicensesHistory.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            dgvInternationalLicensesHistory.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 13, FontStyle.Bold);

            dgvLocalLicensesHistory.Font = new Font("Segoe UI", 12);
            dgvInternationalLicensesHistory.Font = new Font("Segoe UI", 12);

            dgvLocalLicensesHistory.RowTemplate.Height = 35;
            dgvInternationalLicensesHistory.RowTemplate.Height = 35;

            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            dgvLocalLicensesHistory.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            dgvLocalLicensesHistory.BackgroundColor = Color.FromArgb(15, 23, 42);

            dgvLocalLicensesHistory.DefaultCellStyle.BackColor =
                Color.FromArgb(15, 23, 42);

            dgvLocalLicensesHistory.DefaultCellStyle.ForeColor = Color.White;

            dgvLocalLicensesHistory.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(10, 15, 30);

            dgvLocalLicensesHistory.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.Gold;

            dgvLocalLicensesHistory.EnableHeadersVisualStyles = false;


            dgvInternationalLicensesHistory.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            dgvInternationalLicensesHistory.BackgroundColor = Color.FromArgb(15, 23, 42);

            dgvInternationalLicensesHistory.DefaultCellStyle.BackColor =
                Color.FromArgb(15, 23, 42);

            dgvInternationalLicensesHistory.DefaultCellStyle.ForeColor = Color.White;

            dgvInternationalLicensesHistory.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(10, 15, 30);

            dgvInternationalLicensesHistory.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.Gold;

            dgvInternationalLicensesHistory.EnableHeadersVisualStyles = false;
        }

        private void showLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvLocalLicensesHistory.CurrentRow.Cells[0].Value;
            frmShowLicenseInfo  frm = new frmShowLicenseInfo(LicenseID);
            frm.ShowDialog();
        }
    }
}
