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
    public partial class PersonDetails : UserControl
    {
        clsPerson _Person;
        public PersonDetails()
        {
            InitializeComponent();

        }

        public void LoadPersonInfo(int PersonID)
        {

            _Person = clsPerson.Find(PersonID);

            if (_Person == null)
            {
                lblPersonID.Text = "";
                lblName.Text = "";
                lblNationalNo.Text = "";

                lblGendor.Text = "";

                lblEmail.Text = "";
                lblAddress.Text = "";
                lblDateOfBirth.Text = "";
                lblPhone.Text = "";
                lblCountry.Text = "";

                pictureBox1.Image = Properties.Resources.anonymous;
                return;
            }

            lblPersonID.Text = _Person.PersonID.ToString();
            lblName.Text = _Person.FirstName + " " + _Person.SecondName + " " + _Person.ThirdName + " " + _Person.LastName;
            lblNationalNo.Text = _Person.NationalNo;
            if (_Person.Gendor == 0)
            {
                lblGendor.Text = "Male";
            }
            else
            {
                lblGendor.Text = "Female";
                pbMale.Visible = false;
                pbFemale.Visible = true;
            }
            lblEmail.Text = _Person.Email;
            lblAddress.Text = _Person.Address;
            lblDateOfBirth.Text = _Person.DataOfBirth.ToShortDateString();
            lblPhone.Text = _Person.Phone;
            lblCountry.Text = clsCountry.Find(_Person.NationalityCountryID).CountryName;
            if (_Person.ImagePath != null)
            {
                try
                {
                    pictureBox1.Load(_Person.ImagePath);
                }
                catch
                {

                }
            } 
        }


        private void PersonDetails_Load(object sender, EventArgs e)
        {
            

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new AddNewPerson(_Person.PersonID);
            frm.ShowDialog();

            LoadPersonInfo(_Person.PersonID);
        }
    }
}
