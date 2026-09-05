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
using System.Net.Mail;
using System.IO;
using static DVLD___Business_Layer.clsPerson;
using _11___DVLD_Project.Global_Classes;

namespace _11___DVLD_Project
{
    public partial class AddNewPerson : Form
    {
        // Declare a delegate
        public delegate void DataBackEventHandler(object sender, int PersonID);

        // Declare an event using the delegate
        public event DataBackEventHandler DataBack;

        public enum enMode { AddNew = 0, Update = 1 }
        private enMode _Mode;

        int _PersonID;
        clsPerson _Person;

        public AddNewPerson(int PersonID)
        {
            InitializeComponent();


            _PersonID = PersonID;

            if (_PersonID == -1)
                _Mode = enMode.AddNew;
            else
                _Mode = enMode.Update;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _FillCountriesInComboBox()
        {
            DataTable dtCountries = clsCountry.GetAllCountries();
            foreach (DataRow row in dtCountries.Rows)
            {
                comboBox1.Items.Add(row["CountryName"]);
            }
        }

        private void _LoadData()
        {
            // Set Minimum and Maximum Dates in DataTimePicker
            dateTimePicker1.MaxDate = DateTime.Today.AddYears(-15);
            dateTimePicker1.MinDate = DateTime.Today.AddYears(-120);
            dateTimePicker1.Value = DateTime.Today.AddYears(-15);

            // Fill Countries In ComboBox
            _FillCountriesInComboBox();
            comboBox1.SelectedIndex = comboBox1.FindString("Saudi Arabia");

            if (_Mode == enMode.AddNew)
            {
                lblHeader.Text = "Add New Person";
                _Person = new clsPerson();
                llDeleteImage.Visible = false;
                return;
            }

            _Person = clsPerson.Find(_PersonID);

            if (_Person == null)
            {
                MessageBox.Show("This form will be closed because no person with this ID");
                this.Close();

                return;
            }

            lblHeader.Text = "Edit Person With ID: " + _PersonID;
            lblPersonID.Text = _PersonID.ToString();
            txbFirstName.Text = _Person.FirstName;
            txbSecondName.Text = _Person.SecondName;
            txbThirdName.Text = _Person.ThirdName;
            txbLastName.Text = _Person.LastName;
            txbNationalNo.Text = _Person.NationalNo;
            dateTimePicker1.Value = _Person.DataOfBirth;
            txbPhone.Text = _Person.Phone;
            txbEmail.Text = _Person.Email;
            txbAddress.Text = _Person.Address;
            if (_Person.Gendor == 0)
                radioButton1.Checked = true;
            else
                radioButton2.Checked = true;

            if (_Person.ImagePath != "")
            {
                try
                {
                    pictureBox1.Load(_Person.ImagePath);
                }
                catch 
                {

                }
            } 

            llDeleteImage.Visible = (_Person.ImagePath != "");

            comboBox1.SelectedIndex = comboBox1.FindString(clsCountry.Find(_Person.NationalityCountryID).CountryName);

        }

        private bool _HandlePersonImage()
        {
            if (_Person.ImagePath != pictureBox1.ImageLocation)
            {
                if (_Person.ImagePath != "")
                {
                    try
                    {
                        File.Delete(_Person.ImagePath);
                    }
                    catch (IOException)
                    {
                    }
                }
                if (pictureBox1.ImageLocation != null)
                {
                    string SourceImageFile = pictureBox1.ImageLocation.ToString();

                    if (clsUtil.CopyImageToProjectImagesFolder(ref SourceImageFile))
                    {
                        pictureBox1.ImageLocation = SourceImageFile;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Error Copying Image File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            return true;
        }

        private void AddNewPerson_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void ValidateName_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (!char.IsLetter(e.KeyChar) &&
                !char.IsControl(e.KeyChar) &&
                e.KeyChar != ' ' &&
                e.KeyChar != '-')
            {
                e.Handled = true;
                errorProvider1.SetError(textBox, "Only letters allowed!");
            }
            else
            {
                errorProvider1.SetError(textBox, "");
            }
        }


        private void txbPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                errorProvider1.SetError(txbPhone, "Only digits allowed!");
            }
            else
            {
                errorProvider1.SetError(txbPhone, "");
            }
        }

        private void txbNationalNo_Leave(object sender, EventArgs e)
        {
            if (clsPerson.IsPersonExistByNationalNo(txbNationalNo.Text))
                errorProvider1.SetError(txbNationalNo, "National Number is Already Exist!");
            else
                errorProvider1.SetError(txbNationalNo, "");
        }

        private void _FillFormInfoIntoPerson()
        {
            _Person.FirstName = txbFirstName.Text;
            _Person.SecondName = txbSecondName.Text;
            _Person.ThirdName = txbThirdName.Text;
            _Person.LastName = txbLastName.Text;
            _Person.NationalNo = txbNationalNo.Text;
            _Person.DataOfBirth = dateTimePicker1.Value;
            _Person.Phone = txbPhone.Text;
            _Person.Email = txbEmail.Text;
            _Person.Address = txbAddress.Text;

            if (radioButton1.Checked)
                _Person.Gendor = 0;
            else
                _Person.Gendor = 1;

            _Person.NationalityCountryID = (clsCountry.FindByName(comboBox1.Text).CountryID);



        }
            

        private void btnSave_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();

            if (string.IsNullOrWhiteSpace(txbFirstName.Text))
            {
                errorProvider1.SetError(txbFirstName, "You must Enter a name!");
                txbFirstName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txbSecondName.Text))
            {
                errorProvider1.SetError(txbSecondName, "You must Enter a name!");
                txbSecondName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txbLastName.Text))
            {
                errorProvider1.SetError(txbLastName, "You must Enter a name!");
                txbLastName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txbNationalNo.Text))
            {
                errorProvider1.SetError(txbNationalNo, "You must enter a national number!");
                txbNationalNo.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txbPhone.Text))
            {
                errorProvider1.SetError(txbPhone, "You must enter a phone!");
                txbPhone.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txbAddress.Text))
            {
                errorProvider1.SetError(txbAddress, "You must enter an address!");
                txbAddress.Focus();
                return;
            }

            if(errorProvider1.GetError(txbNationalNo) != "")
            {
                txbNationalNo.Focus();
                return;
            }

            if (errorProvider1.GetError(txbEmail) != "")
            {
                txbEmail.Focus();
                return;
            }

            // Handle Image
            if (!_HandlePersonImage())
                return;

            _FillFormInfoIntoPerson();
            if (_Person.Save())
            {

                _Mode = enMode.Update;
                _PersonID = _Person.PersonID;

                lblHeader.Text = "Edit Person With ID: " + _PersonID;
                lblPersonID.Text = _PersonID.ToString();

                MessageBox.Show("Person Saved Sucessfully!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);


                DataBack?.Invoke(this, _Person.PersonID);
            }
            else
                MessageBox.Show("Person addes Failed!");


        }

        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Process the selected file
                string selectedFilePath = openFileDialog1.FileName;
                //MessageBox.Show("Selected Image is:" + selectedFilePath);

                pictureBox1.Load(selectedFilePath);
                _Person.ImagePath = selectedFilePath;

                //string ImageFolder = @"C:\DVLD_People_Images";
                
                //if(!Directory.Exists(ImageFolder))
                //    Directory.CreateDirectory(ImageFolder);

                //string FileExtension = Path.GetExtension(selectedFilePath);
                //string newFileName = Guid.NewGuid().ToString() + FileExtension;
                //string newFilePath = Path.Combine(ImageFolder, newFileName);

                //File.Copy(selectedFilePath,newFilePath, true);
                
                llDeleteImage.Visible = true;
            }
        }

        private void llDeleteImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            //if(File.Exists(_Person.ImagePath))
            //    File.Delete(_Person.ImagePath);


            pictureBox1.ImageLocation = null;
            _Person.ImagePath = "";
            pictureBox1.Image = Properties.Resources.anonymous;



            llDeleteImage.Visible = false;
        }

        private void txbEmail_Leave(object sender, EventArgs e)
        {
            errorProvider1.SetError(txbEmail, "");

            if ( !string.IsNullOrWhiteSpace(txbEmail.Text))
            {
                try
                {
                    MailAddress mail = new MailAddress(txbEmail.Text);
                }
                catch
                {
                    errorProvider1.SetError(txbEmail, "Please enter a valid email!");
                }
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
