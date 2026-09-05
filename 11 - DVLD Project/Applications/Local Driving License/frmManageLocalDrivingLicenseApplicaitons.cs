using _11___DVLD_Project.Applications.Local_Driving_License;
using _11___DVLD_Project.Licenses;
using _11___DVLD_Project.Licenses.Local_Licenses;
using _11___DVLD_Project.Tests;
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

namespace _11___DVLD_Project
{
    public partial class frmManageLocalDrivingLicenseApplicaitons : Form
    {
        private DataTable _LocalDrivingLicenseApplications;
        private clsLocalDrivingLicenseApplication _LDLA;
        private clsApplication _Application;
        public frmManageLocalDrivingLicenseApplicaitons()
        {
            InitializeComponent();
        }

        private void _RefreshApplicationList()
        {
            _LocalDrivingLicenseApplications = clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplication();

            dataGridView1.DataSource = _LocalDrivingLicenseApplications;
            dataGridView1.Font = new Font(dataGridView1.Font.FontFamily, 14);
            dataGridView1.RowTemplate.Height = 35;

            dataGridView1.Columns[0].Width = 140;
            dataGridView1.Columns[1].Width = 400;
            dataGridView1.Columns[2].Width = 140;
            dataGridView1.Columns[3].Width = 440;
            dataGridView1.Columns[4].Width = 230;
            dataGridView1.Columns[5].Width = 143;
            dataGridView1.Columns[6].Width = 165;
            lblCount.Text = dataGridView1.Rows.Count.ToString();

        }
        private void frmManageLocalDrivingLicenseApplicaitons_Load(object sender, EventArgs e)
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

            comboBox1.SelectedIndex = 0;
            _RefreshApplicationList();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void ValidateNumbers_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterField.Text = "";
            if (_LocalDrivingLicenseApplications != null)
                _LocalDrivingLicenseApplications.DefaultView.RowFilter = "";

            txtFilterField.KeyPress -= ValidateNumbers_KeyPress;

            if (comboBox1.SelectedIndex == 0)
            {
                txtFilterField.Visible = false;
                return;
            }



            txtFilterField.Visible = true;


            if (comboBox1.SelectedIndex == 1) 
                txtFilterField.KeyPress += ValidateNumbers_KeyPress;
        }

        private void txtFilterField_TextChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 1:
                    int LDLAppID;

                    if (int.TryParse(txtFilterField.Text, out LDLAppID))
                        _LocalDrivingLicenseApplications.DefaultView.RowFilter = string.Format("[L.D.L AppID] = {0}", LDLAppID);
                    else
                        _LocalDrivingLicenseApplications.DefaultView.RowFilter = "";
                    break;

                case 2:
                    _LocalDrivingLicenseApplications.DefaultView.RowFilter = string.Format("[National No] LIKE '%{0}%'", txtFilterField.Text);
                    break;

                case 3:
                    _LocalDrivingLicenseApplications.DefaultView.RowFilter = string.Format("[Full Name] LIKE '%{0}%'", txtFilterField.Text);
                    break;


                case 4:
                    _LocalDrivingLicenseApplications.DefaultView.RowFilter = string.Format("[Status] LIKE '%{0}%'", txtFilterField.Text);
                    break;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Form AddNewLocalLicenseApplication = new frmNewLocalDrivingLicenseApplication();
            AddNewLocalLicenseApplication.ShowDialog();

            _RefreshApplicationList();
        }

        private void cancelApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Please select an application first!");
                return;
            }

            _LDLA = clsLocalDrivingLicenseApplication.Find((int)dataGridView1.CurrentRow.Cells[0].Value);

            if (_LDLA == null)
            {
                MessageBox.Show("Can't find this local driving license application!");
                return;
            }

            _Application = clsApplication.Find(_LDLA.ApplicationID);

            if (_Application == null)
            {
                MessageBox.Show("Can't find this application!");
                return;
            }

            if (_Application.ApplicationStatus == clsApplication.enApplicationStatus.Cancelled)
            {
                MessageBox.Show("The application is already cancelled!");
                return;
            }

            if (MessageBox.Show("Are you sure you want to cancel this application?",
                "Confirm",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button1) != DialogResult.Yes)
            {
                return;
            }

            _Application.ApplicationStatus = clsApplication.enApplicationStatus.Cancelled;
            _Application.LastStatusDate = DateTime.Now;

            if (_Application.Save())
            {
                MessageBox.Show("Application Canceled Successfully.",
                    "Cancelled",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                _RefreshApplicationList();
            }
            else
            {
                MessageBox.Show("Can't cancel this application!");
            }
        }

        private void _ScheduleTest(clsTestType.enTestType TestType)
        {

            int LocalDrivingLicenseApplicationID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            frmListTestAppointments frm = new frmListTestAppointments(LocalDrivingLicenseApplicationID, TestType);
            frm.ShowDialog();
            //refresh
            _RefreshApplicationList();

        }

        private void scheduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ScheduleTest(clsTestType.enTestType.VisionTest);
        }

        
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            
            _LDLA = clsLocalDrivingLicenseApplication.Find((int)dataGridView1.CurrentRow.Cells[0].Value);
            _Application = clsApplication.Find(_LDLA.ApplicationID);
            if (_Application.ApplicationStatus == clsApplication.enApplicationStatus.Cancelled)
            {

                sechduleTestsToolStripMenuItem.Enabled = false;
                issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;
                showLicenseToolStripMenuItem.Enabled = false;

                issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;
            }
            else
            {
                sechduleTestsToolStripMenuItem.Enabled = true;
                showLicenseToolStripMenuItem.Enabled = false;

                issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;

                switch (clsTest.CountAllPassedTests(_LDLA.LocalDrivingLicenseApplicationID))
                {
                    case 0:
                        scheduleVisionTestToolStripMenuItem.Enabled = true;
                        scheduleWrittenTestToolStripMenuItem.Enabled = false;
                        schduleStreetTestToolStripMenuItem.Enabled = false;
                        break;
                    case 1:
                        scheduleVisionTestToolStripMenuItem.Enabled = false;
                        scheduleWrittenTestToolStripMenuItem.Enabled = true;
                        schduleStreetTestToolStripMenuItem.Enabled = false;
                        break;
                    case 2:
                        scheduleVisionTestToolStripMenuItem.Enabled = false;
                        scheduleWrittenTestToolStripMenuItem.Enabled = false;
                        schduleStreetTestToolStripMenuItem.Enabled = true;
                        break;
                    case 3:
                        scheduleVisionTestToolStripMenuItem.Enabled = false;
                        scheduleWrittenTestToolStripMenuItem.Enabled = false;
                        schduleStreetTestToolStripMenuItem.Enabled = false;
                        sechduleTestsToolStripMenuItem.Enabled = false;
                        bool LicenseExists = _LDLA.IsLicenseIssued();
                        if (LicenseExists)
                        {
                            showLicenseToolStripMenuItem.Enabled = true;
                        }
                        else
                        {
                            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = true;
                        }
                        break;

                }
            }
            
        }
        
        private void scheduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ScheduleTest(clsTestType.enTestType.WrittenTest);
        }

        private void schduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ScheduleTest(clsTestType.enTestType.StreetTest);
        }

        private void showApplicationDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frmShowAppDetails = new frmShowApplicationDetails((int)dataGridView1.CurrentRow.Cells[0].Value);
            frmShowAppDetails.ShowDialog();
        }

        private void sechduleTestsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void issueDrivingLicenseFirstTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            frmIssueDriverLicenseFirstTime frm = new frmIssueDriverLicenseFirstTime(LocalDrivingLicenseApplicationID);
            frm.ShowDialog();
            //refresh
            _RefreshApplicationList();
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dataGridView1.CurrentRow.Cells[0].Value;

            int LicenseID = clsLocalDrivingLicenseApplication.Find(LocalDrivingLicenseApplicationID).GetActiveLicenseID();

            if (LicenseID != -1)
            {
                frmShowLicenseInfo frm = new frmShowLicenseInfo(LicenseID);
                frm.ShowDialog();

            }
            else
            {
                MessageBox.Show("No License Found!", "No License", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            clsLocalDrivingLicenseApplication localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.Find(LocalDrivingLicenseApplicationID);

            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(localDrivingLicenseApplication.ApplicantPersonID);
            frm.ShowDialog();
        }
    }
}
