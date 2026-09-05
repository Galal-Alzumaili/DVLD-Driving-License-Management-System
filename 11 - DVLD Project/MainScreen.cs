using _11___DVLD_Project.Applications.International_License;
using _11___DVLD_Project.Applications.Release_Detained_License;
using _11___DVLD_Project.Applications.Renew_Local_License;
using _11___DVLD_Project.Applications.ReplaceLostOrDamagedLicense;
using _11___DVLD_Project.Drivers;
using _11___DVLD_Project.Licenses.Detain_License;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _11___DVLD_Project
{
    public partial class MainScreen : Form
    {
        public MainScreen()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void SetMenuItemsForeColor(ToolStripItemCollection items, Color color)
        {
            foreach (ToolStripItem item in items)
            {
                item.ForeColor = color;

                if (item is ToolStripMenuItem menuItem && menuItem.HasDropDownItems)
                {
                    SetMenuItemsForeColor(menuItem.DropDownItems, color);
                }
            }
        }

        private void MainScreen_Load(object sender, EventArgs e)
        {
            menuStrip1.BackColor = Color.Transparent;
            menuStrip1.ForeColor = Color.White; // أو Color.White
            menuStrip1.RenderMode = ToolStripRenderMode.Professional;
            menuStrip1.Renderer = new ToolStripProfessionalRenderer(new DVLDMenuColors());

            SetMenuItemsForeColor(menuStrip1.Items, Color.White);
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form1 = new ListPeople();
            form1.ShowDialog();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frmManageUser = new frmManageUsers();
            frmManageUser.ShowDialog();
        }

        private void cuurentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form ShowDetails = new frmShowUserDetails(GlobalSettings.CurrentUser.UserID);
            ShowDetails.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form ChangePassword = new frmChangePassword(GlobalSettings.CurrentUser.UserID);
            ChangePassword.ShowDialog();
        }

        private void manageApplicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form ManageApplicationTypes = new frmManageApplicationsTypes();
            ManageApplicationTypes.ShowDialog();
        }

        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form ManageTestTypes = new frmManageTestTypes();
            ManageTestTypes.ShowDialog();
        }

        private void localLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form NewLocalLicenseApplication = new frmNewLocalDrivingLicenseApplication();
            NewLocalLicenseApplication.ShowDialog();
        }

        private void internationalLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListInternationalLicesnseApplications frm = new frmListInternationalLicesnseApplications();
            frm.ShowDialog();
        }

        private void localDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form LocalDrivingLicensApplications = new frmManageLocalDrivingLicenseApplicaitons();
            LocalDrivingLicensApplications.ShowDialog();
        }

        private void retakeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form LocalDrivingLicensApplications = new frmManageLocalDrivingLicenseApplicaitons();
            LocalDrivingLicensApplications.ShowDialog();
        }

        private void renewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRenewLocalDrivingLicenseApplication frm = new frmRenewLocalDrivingLicenseApplication();
            frm.ShowDialog();
        }

        private void replacementForLostOrDamagedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReplaceLostOrDamagedLicenseApplication frm = new frmReplaceLostOrDamagedLicenseApplication();
            frm.ShowDialog();
        }

        private void driversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListDrivers frm = new frmListDrivers();
            frm.ShowDialog();
        }

        private void detaiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDetainLicenseApplication frm = new frmDetainLicenseApplication();
            frm.ShowDialog();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicenseApplication frm = new frmReleaseDetainedLicenseApplication();
            frm.ShowDialog();
        }

        private void manageDetainedLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListDetainedLicenses frm = new frmListDetainedLicenses();
            frm.ShowDialog();
        }

        private void internationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNewInternationalLicenseApplication frm = new frmNewInternationalLicenseApplication();
            frm.ShowDialog();
        }
    }
}
