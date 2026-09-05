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

namespace _11___DVLD_Project.Applications.Local_Driving_License
{
    public partial class frmShowApplicationDetails : Form
    {
        private int _LDLAID;
        public frmShowApplicationDetails(int LocalID)
        {
            InitializeComponent();

            _LDLAID = LocalID;
        }


        private void frmShowApplicationDetails_Load(object sender, EventArgs e)
        {
            //ShowApplicationInfo.ShowApplicationAndLDLAInfo(_LDLAID);
        }

        private void showApplicationInfo1_Load(object sender, EventArgs e)
        {
            showApplicationInfo1.ShowApplicationAndLDLAInfo(_LDLAID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
