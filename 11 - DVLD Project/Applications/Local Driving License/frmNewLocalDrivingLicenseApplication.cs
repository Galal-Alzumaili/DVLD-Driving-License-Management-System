using DVLD___Business_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _11___DVLD_Project
{
    public partial class frmNewLocalDrivingLicenseApplication : Form
    {
        private clsApplication _Application;
        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
        private int _PersonID;
        public frmNewLocalDrivingLicenseApplication()
        {
            InitializeComponent();
            findPersonWithFilter1.OnPersonSelected += PersonDetails_OnPersonSelected;
            findPersonWithFilter1.OnPersonNotFound += PersonDetails_OnPersonNotFound;
        }

        private void PersonDetails_OnPersonSelected(int personID)
        {
            btnNext.Visible = true;
            _PersonID = personID;
        }

        private void PersonDetails_OnPersonNotFound(int personID)
        {
            btnNext.Visible = false;
            _PersonID = personID;
        }

        private void _LoadLicenseClasses()
        {
            DataTable dtLicenseClasses = clsLicenseClass.GetAllLicenseClasses();
            foreach (DataRow row in dtLicenseClasses.Rows)
            {
                comboBox1.Items.Add(row["ClassName"]);
            }

        }

        private void frmNewLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            _LoadLicenseClasses();
            comboBox1.SelectedIndex = 2;

            lblApplicationDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
            lblApplicationFees.Text = "15";
            lblCurrentUser.Text = GlobalSettings.CurrentUser.UserName;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
            btnSave.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int LicenseClassID = comboBox1.SelectedIndex +1 ;
            
            _Application = clsApplication.IsLocalApplicationIsAlreadyThere(_PersonID, LicenseClassID);
            if (_Application != null)
            {
                MessageBox.Show("Choose another License Class, the selected Person Already have an active application for the selected class with id = " + _Application.ApplicantPersonID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox1.Focus();
                return;
            }

            /*
            // Check if user already have issued license of the same driving class
            if(clsLicense.IsLicenseExistByPersonID(_PersonID, LicenseClassID))
            {
                MessageBox.Show("Person already have a license with the same applied driving license type.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            */

            _Application = new clsApplication();
            _Application.ApplicantPersonID = _PersonID;
            _Application.ApplicationDate = DateTime.Now;
            _Application.ApplicationTypeID = 1;
            _Application.ApplicationStatus = clsApplication.enApplicationStatus.New;
            _Application.LastStatusDate = DateTime.Now;
            _Application.PaidFees = 15;
            _Application.CreatedByUserID = GlobalSettings.CurrentUser.UserID;


            if (_Application.Save())
            {
                lblApplicationID.Text = _Application.ApplicationID.ToString();

                _LocalDrivingLicenseApplication = new clsLocalDrivingLicenseApplication();
                _LocalDrivingLicenseApplication.ApplicationID = _Application.ApplicationID;
                _LocalDrivingLicenseApplication.LicenseClassID = LicenseClassID;


                if (_LocalDrivingLicenseApplication.Save())
                {
                    MessageBox.Show("Your application has been submitted successfully!");
                }
                else
                {
                    MessageBox.Show("Unable to create new application! please try again!");
                }


            }
            else
            {

            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
