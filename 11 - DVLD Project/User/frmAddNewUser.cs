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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace _11___DVLD_Project
{
    public partial class frmAddNewUser : Form
    {
        public enum enMode { AddNew = 0, Update = 1 }
        enMode _Mode;
        int _PersonID;

        clsUser _User;
        clsPerson _Person;
        public frmAddNewUser(int PersonID)
        {
            InitializeComponent();
            

            _PersonID = PersonID;

            if(_PersonID == -1)
                _Mode = enMode.AddNew;
            else
                _Mode = enMode.Update;
           
        }

        private void PersonDetails_OnPersonSelected(int personID)
        {
            btnNext.Visible = true;
        }

        // Seach 
        private void _PerformSearch()
        {
            if (comboBox1.SelectedIndex == 0)
            {
                if (string.IsNullOrWhiteSpace(txtFilterField.Text))
                {
                    errorProvider1.SetError(txtFilterField, "Please enter a value here!");
                }
                else
                {
                    errorProvider1.SetError(txtFilterField, "");
                    _Person = clsPerson.FindByNationalNo(txtFilterField.Text);

                    if (_Person != null)
                    {
                        personDetails1.LoadPersonInfo(_Person.PersonID);
                        btnNext.Visible = true;

                    }
                    else
                    {
                        MessageBox.Show("There is not person with inserted information!");
                    }
                }

            }

            if (comboBox1.SelectedIndex == 1)
            {

                int PersonID;
                if (int.TryParse(txtFilterField.Text, out PersonID))
                {
                    errorProvider1.SetError(txtFilterField, "");
                    _Person = clsPerson.Find(PersonID);

                    if (_Person != null)
                    {
                        personDetails1.LoadPersonInfo(_Person.PersonID);
                        btnNext.Visible = true;
                    }
                    else
                    {
                        MessageBox.Show("There is not person with inserted information!");
                    }
                }
                else
                    errorProvider1.SetError(txtFilterField, "Please enter a value here!");

            }
        }

        private void frmAddNewUser_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            if (_Mode == enMode.Update)
            {
                lblHeader.Text = "Update User";
                this.Text = "Update User";
                personDetails1.LoadPersonInfo(_PersonID);
                groupBox1.Enabled = false;
                txtFilterField.Visible = true;
                txtFilterField.Text = _PersonID.ToString();
                pbAddNewPerson.Visible = false;
                pbSearch.Visible = false;
                btnNext.Visible = true;
                _User = clsUser.FindByPersonID(_PersonID);
                txtUserName.Text = _User.UserName;
                txtPassword1.Text = _User.Password;
                txtPassword2.Text = _User.Password;
                chbIsActive.Checked = _User.IsActive;
                lblUserID.Text = _User.UserID.ToString();
            }
            else
            {
                lblHeader.Text = "Add User";
                this.Text = "Add User";
                _User = new clsUser();
            }

            
        }

        private void personDetails1_Load(object sender, EventArgs e)
        {
            
        }

        private void pbSearch_Click(object sender, EventArgs e)
        {

            _PerformSearch();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterField.Text = "";
            txtFilterField.KeyPress -= ValidateNumbers_KeyPress;
            if (comboBox1.SelectedIndex == 1)
            {
                txtFilterField.KeyPress += ValidateNumbers_KeyPress;
            }
        }

        private void txtFilterField_TextChanged(object sender, EventArgs e)
        {

        }

        private void ValidateNumbers_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.Update)
            {
                tabControl1.SelectedIndex = 1;
            }
            else
            {
                if (_Person != null)
                {
                    if (clsUser.IsUserExistByPersonID(_Person.PersonID))
                    {
                        MessageBox.Show("The user with same personID is already exist!");
                    }
                    else
                    {
                        tabControl1.SelectedIndex = 1;
                    }
                }
            }
        }

        private void pbAddNewPerson_Click(object sender, EventArgs e)
        {
            AddNewPerson form = new AddNewPerson(-1);
            form.DataBack += AddNewPerson_DataBack;
            form.ShowDialog();
        }

        private void AddNewPerson_DataBack(object sender, int PersonID)
        {
            _PersonID = PersonID;
            _Person = clsPerson.Find(PersonID);
            comboBox1.SelectedIndex = 1;
            personDetails1.LoadPersonInfo(PersonID);
            groupBox1.Enabled = false;
            txtFilterField.Visible = true;
            txtFilterField.Text = PersonID.ToString();
            pbAddNewPerson.Visible = false;
            pbSearch.Visible = false;
            btnNext.Visible = true ;
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            lblPasswordhint1.Visible = true;
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            lblPasswordhint1.Visible = false;
            if(string.IsNullOrWhiteSpace(txtPassword1.Text))
            {
                errorProvider1.SetError(txtPassword1, "You need to enter a password!");
            }
            else
            {
                errorProvider1.SetError(txtPassword1, "");
            }
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            lblPasswordhint2.Visible = true;
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            lblPasswordhint2.Visible = false;

            if(errorProvider1.GetError(txtPassword1) == "")
            {
                if (string.Compare(txtPassword1.Text, txtPassword2.Text) != 0)
                    errorProvider1.SetError(txtPassword2, "The password is not matching!");
                else
                    errorProvider1.SetError(txtPassword2, "");

            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txtPassword1.PasswordChar = '\0';

                txtPassword2.PasswordChar = '\0';
            }
            else
            {
                txtPassword1.PasswordChar = '*';

                txtPassword2.PasswordChar = '*';
            }
        }

        private void txtPassword2_TextChanged(object sender, EventArgs e)
        {

        }

        private bool _ValidateUserInfo()
        {
            if (string.IsNullOrWhiteSpace(txtUserName.Text))
            {
                errorProvider1.SetError(txtUserName, "Username is required!");
                txtUserName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPassword1.Text))
            {
                errorProvider1.SetError(txtPassword1, "Password is required!");
                txtPassword1.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPassword2.Text))
            {
                errorProvider1.SetError(txtPassword2, "Password is not matching!");
                txtPassword2.Focus();
                return false;
            }

            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!_ValidateUserInfo())
                return;

            if(_Mode == enMode.AddNew)
                _User.PersonID = _Person.PersonID;
            else
                _User.PersonID = _PersonID;
            _User.UserName = txtUserName.Text;
            _User.Password = txtPassword1.Text;
            _User.IsActive = chbIsActive.Checked;
            

            if (_User.Save())
            {
                MessageBox.Show("User Saved Successfully!");
            }
            else
            {
                MessageBox.Show("The user didn't saved!");
            }

        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtUserName_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUserName.Text))
            {
                errorProvider1.SetError(txtUserName, "Username is required!");
            }
            else
            {
                if(clsUser.IsUserExistByUsername(txtUserName.Text))
                    errorProvider1.SetError(txtUserName, "Username is already exist!");
                else
                    errorProvider1.SetError(txtUserName, "");
            }
        }
    }
}
