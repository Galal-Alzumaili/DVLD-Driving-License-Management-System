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
    public partial class frmShowUserDetails : Form
    {
        private int _UserID;
        private clsUser _User;
        public frmShowUserDetails(int UserID)
        {
            InitializeComponent();

            _UserID = UserID;
        }

        private void personDetails1_Load(object sender, EventArgs e)
        {
            _User = clsUser.Find(_UserID);
            personDetails1.LoadPersonInfo(_User.PersonID);
            lblUserID.Text = _User.UserID.ToString();
            lblUsername.Text = _User.UserName;
            lblIsActive.Text = _User.IsActive.ToString();
        }

        private void frmShowUserDetails_Load(object sender, EventArgs e)
        {

        }
    }
}
