using _11___DVLD_Project.Licenses;
using _11___DVLD_Project.Licenses.International_Licenses;
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

namespace _11___DVLD_Project.Applications.International_License
{
    public partial class frmListInternationalLicesnseApplications : Form
    {
        private DataTable _dtInternationalLicenseApplications;
        public frmListInternationalLicesnseApplications()
        {
            InitializeComponent();
        }

        private void _RefreshInternationalLicesnseList()
        {
            _dtInternationalLicenseApplications = clsInternationalLicense.GetAllInternationalLicenses();
            dataGridView1.DataSource = _dtInternationalLicenseApplications;
            dataGridView1.Font = new Font(dataGridView1.Font.FontFamily, 14);
            dataGridView1.RowTemplate.Height = 35;
            dataGridView1.Columns[0].HeaderText = "Int.License ID";
            dataGridView1.Columns[0].Width = 180;

            dataGridView1.Columns[1].HeaderText = "Application ID";
            dataGridView1.Columns[1].Width = 180;

            dataGridView1.Columns[2].HeaderText = "Driver ID";
            dataGridView1.Columns[2].Width = 180;

            dataGridView1.Columns[3].HeaderText = "L.License ID";
            dataGridView1.Columns[3].Width = 180;

            dataGridView1.Columns[4].HeaderText = "Issue Date";
            dataGridView1.Columns[4].Width = 240;

            dataGridView1.Columns[5].HeaderText = "Expiration Date";
            dataGridView1.Columns[5].Width = 240;

            dataGridView1.Columns[6].HeaderText = "Is Active";
            dataGridView1.Columns[6].Width = 200;
            lblInternationalLicensesRecords.Text = dataGridView1.Rows.Count.ToString();

        }

        private void frmListInternationalLicesnseApplications_Load(object sender, EventArgs e)
        {
            _dtInternationalLicenseApplications = clsInternationalLicense.GetAllInternationalLicenses();
            cbFilterBy.SelectedIndex = 0;

            dataGridView1.DataSource = _dtInternationalLicenseApplications;
            lblInternationalLicensesRecords.Text = dataGridView1.Rows.Count.ToString();

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
                _RefreshInternationalLicesnseList();

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.Text == "Is Active")
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
            string FilterColumn = "IsActive";
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
                _dtInternationalLicenseApplications.DefaultView.RowFilter = "";
            else
                //in this case we deal with numbers not string.
                _dtInternationalLicenseApplications.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, FilterValue);

            lblInternationalLicensesRecords.Text = _dtInternationalLicenseApplications.Rows.Count.ToString();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {


            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilterBy.Text)
            {
                case "International License ID":
                    FilterColumn = "InternationalLicenseID";
                    break;
                case "Application ID":
                    {
                        FilterColumn = "ApplicationID";
                        break;
                    }
                    ;

                case "Driver ID":
                    FilterColumn = "DriverID";
                    break;

                case "Local License ID":
                    FilterColumn = "IssuedUsingLocalLicenseID";
                    break;

                case "Is Active":
                    FilterColumn = "IsActive";
                    break;


                default:
                    FilterColumn = "None";
                    break;
            }


            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtInternationalLicenseApplications.DefaultView.RowFilter = "";
                lblInternationalLicensesRecords.Text = dataGridView1.Rows.Count.ToString();
                return;
            }



            _dtInternationalLicenseApplications.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());

            lblInternationalLicensesRecords.Text = _dtInternationalLicenseApplications.Rows.Count.ToString();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            //we allow numbers only becasue all fiters are numbers.
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btnNewApplication_Click(object sender, EventArgs e)
        {
            frmNewInternationalLicenseApplication frm = new frmNewInternationalLicenseApplication();
            frm.ShowDialog();
            //refresh
            _RefreshInternationalLicesnseList();

        }

        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int DriverID = (int)dataGridView1.CurrentRow.Cells[2].Value;
            int PersonID = clsDriver.FindByDriverID(DriverID).PersonID;

            Person_Details frm = new Person_Details(PersonID);
            frm.ShowDialog();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int DriverID = (int)dataGridView1.CurrentRow.Cells[2].Value;
            int PersonID = clsDriver.FindByDriverID(DriverID).PersonID;
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(PersonID);
            frm.ShowDialog();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int InternationalLicenseID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            frmShowInternationalLicenseInfo frm = new frmShowInternationalLicenseInfo(InternationalLicenseID);
            frm.ShowDialog();
        }
    }
}
