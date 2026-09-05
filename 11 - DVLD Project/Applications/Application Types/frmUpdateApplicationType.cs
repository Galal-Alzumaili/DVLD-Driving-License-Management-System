using DVLD___Business_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace _11___DVLD_Project
{
    public partial class frmUpdateApplicationType : Form
    {
        clsApplicationType ApplicationType;
        private int _ApplicationTypeID;
        public frmUpdateApplicationType(int ApplicationID)
        {
            InitializeComponent();

            _ApplicationTypeID = ApplicationID;

        }

        private void frmUpdateApplicationType_Load(object sender, EventArgs e)
        {

            ApplicationType = clsApplicationType.Find(_ApplicationTypeID);

            if (ApplicationType == null)
            {
                MessageBox.Show("Could't find this application type");
                return;
            }

            lblID.Text = ApplicationType.ApplicationTypesID.ToString();
            txbTitle.Text = ApplicationType.ApplicationTypesTitle;
            txbFees.Text = ApplicationType.ApplicationTypesFees.ToString("0.00");

            txbTitle.Focus();


            this.AcceptButton = btnSave;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ApplicationType.ApplicationTypesTitle = txbTitle.Text;

            float fees;
            if(float.TryParse(txbFees.Text, out fees))
                ApplicationType.ApplicationTypesFees = fees;
            else
            {
                MessageBox.Show("Fees must be a number");
                txbFees.Focus();
            }

            if (ApplicationType.Save())
                MessageBox.Show("Application type information updated sucessfully!");
            else
                MessageBox.Show("Updated can't be done!");
        }
    }
}
