using _11___DVLD_Project.Licenses;
using _11___DVLD_Project.Licenses.Detain_License;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace _11___DVLD_Project.Applications.Release_Detained_License
{
    public partial class frmListDetainedLicenses : Form
    {
        private DataTable _dtDetainedLicenses;
        public frmListDetainedLicenses()
        {
            InitializeComponent();
        }

        private void _RefreshDetainedList()
        {
            _dtDetainedLicenses = clsDetainedLicense.GetAllDetainedLicenses();
            dataGridView1.DataSource = _dtDetainedLicenses;
            dataGridView1.Font = new Font(dataGridView1.Font.FontFamily, 14);
            dataGridView1.RowTemplate.Height = 35;
            dataGridView1.Columns[0].Width = 90;
            dataGridView1.Columns[1].Width = 90;
            dataGridView1.Columns[2].Width = 200;
            dataGridView1.Columns[3].Width = 120;
            dataGridView1.Columns[4].Width = 120;
            dataGridView1.Columns[5].Width = 200;
            dataGridView1.Columns[6].Width = 90;
            dataGridView1.Columns[7].Width = 380;
            dataGridView1.Columns[8].Width = 155;
            lblRecordsNumber.Text = dataGridView1.Rows.Count.ToString();

        }

        private void frmListDetainedLicenses_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;

            _dtDetainedLicenses = clsDetainedLicense.GetAllDetainedLicenses();

            dataGridView1.DataSource = _dtDetainedLicenses;
            lblRecordsNumber.Text = dataGridView1.Rows.Count.ToString();

            if (dataGridView1.Rows.Count > 0)
            {

                dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 13, FontStyle.Bold);
                dataGridView1.Font = new Font("Segoe UI", 12);
                dataGridView1.RowTemplate.Height = 35;

                //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


                dataGridView1.BackgroundColor = Color.FromArgb(15, 23, 42);

                dataGridView1.DefaultCellStyle.BackColor =
                    Color.FromArgb(15, 23, 42);

                dataGridView1.DefaultCellStyle.ForeColor = Color.White;

                dataGridView1.ColumnHeadersDefaultCellStyle.BackColor =
                    Color.FromArgb(10, 15, 30);

                dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor =
                    Color.Gold;

                dataGridView1.EnableHeadersVisualStyles = false;


                dataGridView1.Columns[0].HeaderText = "D.ID";
                dataGridView1.Columns[1].HeaderText = "L.ID";
                dataGridView1.Columns[2].HeaderText = "D.Date";
                dataGridView1.Columns[3].HeaderText = "Is Released";
                dataGridView1.Columns[4].HeaderText = "Fine Fees";
                dataGridView1.Columns[5].HeaderText = "Release Date";
                dataGridView1.Columns[6].HeaderText = "N.No.";
                dataGridView1.Columns[7].HeaderText = "Full Name";
                dataGridView1.Columns[8].HeaderText = "Rlease App.ID";
               


                _RefreshDetainedList();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.Text == "Is Released")
            {
                txtFilterValue.Visible = false;
                cbIsReleased.Visible = true;
                cbIsReleased.Focus();
                cbIsReleased.SelectedIndex = 0;
            }

            else

            {

                txtFilterValue.Visible = (cbFilterBy.Text != "None");
                cbIsReleased.Visible = false;

                if (cbFilterBy.Text == "None")
                {
                    txtFilterValue.Enabled = false;
                    //_dtDetainedLicenses.DefaultView.RowFilter = "";
                    //lblTotalRecords.Text = dgvDetainedLicenses.Rows.Count.ToString();

                }
                else
                    txtFilterValue.Enabled = true;

                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
        }

        private void cbIsReleased_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "IsReleased";
            string FilterValue = cbIsReleased.Text;

            switch (FilterValue)
            {
                case "All":
                    break;
                case "Yes":
                    FilterValue = "1";
                    break;
                case "No":
                    FilterValue = "0";
                    break;
            }


            if (FilterValue == "All")
                _dtDetainedLicenses.DefaultView.RowFilter = "";
            else
                //in this case we deal with numbers not string.
                _dtDetainedLicenses.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, FilterValue);

            lblRecordsNumber.Text = _dtDetainedLicenses.Rows.Count.ToString();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilterBy.Text)
            {
                case "Detain ID":
                    FilterColumn = "DetainID";
                    break;
                case "Is Released":
                    {
                        FilterColumn = "IsReleased";
                        break;
                    }
                    ;

                case "National No.":
                    FilterColumn = "NationalNo";
                    break;


                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                case "Release Application ID":
                    FilterColumn = "ReleaseApplicationID";
                    break;

                default:
                    FilterColumn = "None";
                    break;
            }
                //Reset the filters in case nothing selected or filter value conains nothing.
                if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
                {
                    _dtDetainedLicenses.DefaultView.RowFilter = "";
                    lblRecordsNumber.Text = dataGridView1.Rows.Count.ToString();
                    return;
                }


                if (FilterColumn == "DetainID" || FilterColumn == "ReleaseApplicationID")
                    //in this case we deal with numbers not string.
                    _dtDetainedLicenses.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
                else
                    _dtDetainedLicenses.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());

                lblRecordsNumber.Text = _dtDetainedLicenses.Rows.Count.ToString();
            }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            //we allow number incase person id or user id is selected.
            if (cbFilterBy.Text == "Detain ID" || cbFilterBy.Text == "Release Application ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btnDetainLicense_Click(object sender, EventArgs e)
        {
            frmDetainLicenseApplication frm = new frmDetainLicenseApplication();
            frm.ShowDialog();
            //refresh
            _RefreshDetainedList();
        }

        private void btnReleaseDetainedLicense_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicenseApplication frm = new frmReleaseDetainedLicenseApplication();
            frm.ShowDialog();
            //refresh
            _RefreshDetainedList();
        }

        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dataGridView1.CurrentRow.Cells[1].Value;
            int PersonID = clsLicense.Find(LicenseID).DriverInfo.PersonID;

            Form frm = new Person_Details(PersonID);
            frm.ShowDialog();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dataGridView1.CurrentRow.Cells[1].Value;

            frmShowLicenseInfo frm = new frmShowLicenseInfo(LicenseID);
            frm.ShowDialog();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dataGridView1.CurrentRow.Cells[1].Value;
            int PersonID = clsLicense.Find(LicenseID).DriverInfo.PersonID;
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(PersonID);
            frm.ShowDialog();
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dataGridView1.CurrentRow.Cells[1].Value;

            frmReleaseDetainedLicenseApplication frm = new frmReleaseDetainedLicenseApplication(LicenseID);
            frm.ShowDialog();
            //refresh
            _RefreshDetainedList();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            sendEmailToolStripMenuItem.Enabled = !(bool)dataGridView1.CurrentRow.Cells[3].Value;
        }
    }
}
