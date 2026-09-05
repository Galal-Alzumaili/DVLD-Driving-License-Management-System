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

namespace _11___DVLD_Project
{
    public partial class ShowApplicationInfo : UserControl
    {
        private clsLocalDrivingLicenseApplication _LDLA;
        private clsApplication _App;

        private int _LocalDrivingLicenseApplicationID = -1;

        private int _LicenseID;

        public int LocalDrivingLicenseApplicationID
        {
            get { return _LocalDrivingLicenseApplicationID; }
        }

        public ShowApplicationInfo()
        {
            InitializeComponent();
        }

        private void ShowApplicationInfo_Load(object sender, EventArgs e)
        {

        }


        public void ShowApplicationAndLDLAInfo(int LDLAppID)
        {

            _LDLA = clsLocalDrivingLicenseApplication.Find(LDLAppID);
            if (_LDLA == null)
            {
                return;

            }
            _App = clsApplication.Find(_LDLA.ApplicationID);
            if (_LDLA == null || _App == null)
            {
                lblDLAppID.Text = "";
                lblLicenseClass.Text = "";
                lblNumberOfTests.Text = "0";

                lblApplicationID.Text = "";
                lblAppStatus.Text = "";
                lblAppFees.Text = "";
                lblAppType.Text = "";
                lblAppApplicantName.Text = "";


                lblAppDate.Text = "";
                lblStatusDate.Text = "";
                lblAppCreatedBy.Text = "";

                return;
            }
            // Upper GroupBox
            lblDLAppID.Text = _LDLA.LocalDrivingLicenseApplicationID.ToString();
            lblLicenseClass.Text = (clsLicenseClass.Find(_LDLA.LicenseClassID).ClassName);
            lblNumberOfTests.Text = clsTest.CountAllPassedTests(LDLAppID).ToString();

            // Lower GroupBox
            lblApplicationID.Text = _App.ApplicationID.ToString();

            switch (_App.ApplicationStatus)
            {
                case clsApplication.enApplicationStatus.New:
                    lblAppStatus.Text = "New";
                    break;
                case clsApplication.enApplicationStatus.Cancelled:
                    lblAppStatus.Text = "Cancelled";
                    break;
                case clsApplication.enApplicationStatus.Completed:
                    lblAppStatus.Text = "Completed";
                    break;
            }

            lblAppFees.Text = Convert.ToInt32(_App.PaidFees).ToString();
            lblAppType.Text = (clsApplicationType.Find(_App.ApplicationTypeID).ApplicationTypesTitle);
            lblAppApplicantName.Text = _App.ApplicantFullName.ToString();

            lblAppDate.Text = _App.ApplicationDate.ToShortDateString().ToString();
            lblStatusDate.Text = _App.LastStatusDate.ToShortDateString().ToString();
            lblAppCreatedBy.Text = (clsUser.Find(_App.CreatedByUserID).UserName);

        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new Person_Details(_App.ApplicantPersonID);
            frm.ShowDialog();
        }

        private void lblNumberOfTests_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
