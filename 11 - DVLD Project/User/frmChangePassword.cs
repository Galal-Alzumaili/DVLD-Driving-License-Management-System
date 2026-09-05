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
    public partial class frmChangePassword : Form
    {
        private int _PersonID;
        private clsUser _User;
        public frmChangePassword(int PersonID)
        {
            InitializeComponent();

            _PersonID = PersonID;
        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            personDetails1.LoadPersonInfo(_PersonID);
            _User = clsUser.FindByPersonID(_PersonID);
            lblUserID.Text = _User.UserID.ToString();
            lblUsername.Text = _User.UserName;
            lblIsActive.Text = _User.IsActive.ToString(); 
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txbCurrentPassword.Text != _User.Password)
            {
                MessageBox.Show("The entered password is not correct!");
                return;
            }

            if (string.IsNullOrWhiteSpace(txbCurrentPassword.Text))
            {
                errorProvider1.SetError(txbCurrentPassword, "You need to enter the current password!");
                txbCurrentPassword.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txbNewPassword.Text))
            {
                errorProvider1.SetError(txbNewPassword, "You need to enter the new password!");
                txbNewPassword.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txbConfirmPassword.Text))
            {
                errorProvider1.SetError(txbConfirmPassword, "You need to confirm the password!");
                txbConfirmPassword.Focus();
                return;
            }

            if (txbConfirmPassword.Text != txbNewPassword.Text)
            {
                errorProvider1.SetError(txbConfirmPassword, "the password is not matching the new password!");
                txbConfirmPassword.Focus();
                return;
            }

            _User.Password = txbNewPassword.Text;
            if (_User.Save())
                MessageBox.Show("The password updated sucessfully!");
            else
                MessageBox.Show("the password can't be changed");
        }

        private void txbCurrentPassword_Leave(object sender, EventArgs e)
        {
            if(txbCurrentPassword.Text != _User.Password)
            {
                errorProvider1.SetError(txbCurrentPassword, "The password is not matching the user password!");
            }
            else
                errorProvider1.SetError(txbCurrentPassword, "");

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txbCurrentPassword.PasswordChar = '\0';

                txbNewPassword.PasswordChar = '\0';

                txbConfirmPassword.PasswordChar = '\0';
            }
            else
            {
                txbCurrentPassword.PasswordChar = '*';

                txbNewPassword.PasswordChar = '*';

                txbConfirmPassword.PasswordChar = '*';
            }
        }

        private void txbConfirmPassword_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
