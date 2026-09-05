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
    public partial class frmUpdateTestType : Form
    {
        private int _TestID;

        private clsTestType.enTestType _TestTypeID = clsTestType.enTestType.VisionTest;
        private clsTestType _TestType;
        public frmUpdateTestType(clsTestType.enTestType TestTypeID)
        {
            InitializeComponent();


            _TestTypeID = TestTypeID;
        }

        private void frmUpdateTestType_Load(object sender, EventArgs e)
        {
            _TestType = clsTestType.Find(_TestTypeID);

            if (_TestType == null)
            {
                MessageBox.Show("Test type is not found!");
                return;
            }

            lblID.Text = ((int)_TestTypeID).ToString();
            txbTitle.Text = _TestType.TestTypeTitle;
            txbDescription.Text = _TestType.TestTypeDescription;
            txbFees.Text = _TestType.TestTypeFees.ToString();

            txbTitle.Focus();


            this.AcceptButton = btnSave;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _TestType.TestTypeTitle = txbTitle.Text;
            _TestType.TestTypeDescription = txbDescription.Text;

            float fees;
            if (float.TryParse(txbFees.Text, out fees))
                _TestType.TestTypeFees = fees;
            else
            {
                MessageBox.Show("Fees must be a number");
                txbFees.Focus();
            }

            if (_TestType.Save())
                MessageBox.Show("Test type information updated sucessfully!");
            else
                MessageBox.Show("Updated can't be done!");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
