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
    public partial class FindPersonWithFilter : UserControl
    {
        public enum enMode { AddNew = 0, Update = 1 }
        enMode _Mode;

        clsPerson _Person;
        public FindPersonWithFilter()
        {
            InitializeComponent();
        }

        // this event for showing up the next button
        public event Action<int> OnPersonSelected;
        public event Action<int> OnPersonNotFound;

        private void personDetails1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }

        private void ValidateNumbers_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private bool _FilterEnabled = true;
        public bool FilterEnabled
        {
            get
            {
                return _FilterEnabled;
            }
            set
            {
                _FilterEnabled = value;
                groupBox1.Enabled = _FilterEnabled;
            }
        }

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
                        OnPersonSelected?.Invoke(_Person.PersonID);
                    }
                    else
                    {

                        MessageBox.Show("There is not person with inserted information!");
                        personDetails1.LoadPersonInfo(-1);
                        OnPersonNotFound?.Invoke(-1);

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
                            OnPersonSelected?.Invoke(_Person.PersonID);
                        }
                        else
                        {
                            
                            MessageBox.Show("There is not person with inserted information!");
                            personDetails1.LoadPersonInfo(-1);
                            OnPersonNotFound?.Invoke(-1);
                        }
                    }
                    else
                        errorProvider1.SetError(txtFilterField, "Please enter a value here!");
                }

        }
            

        private void pbSearch_Click(object sender, EventArgs e)
        {
            _PerformSearch();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
        }

        private void pbAddNewPerson_Click(object sender, EventArgs e)
        {

        }

        private void AddNewPerson_DataBack(object sender, int PersonID)
        {
            //_Person.PersonID = PersonID;
            _Person = clsPerson.Find(PersonID);
            comboBox1.SelectedIndex = 1;
            personDetails1.LoadPersonInfo(PersonID);
            groupBox1.Enabled = false;
            txtFilterField.Visible = true;
            txtFilterField.Text = PersonID.ToString();
            pbAddNewPerson.Visible = false;
            pbSearch.Visible = false;
            //btnNext.Visible = true;
        }

        public void LoadPersonInfo(int PersonID)
        {

            comboBox1.SelectedIndex = 1;
            txtFilterField.Text = PersonID.ToString();
            _PerformSearch();

        }

        public void FilterFocus()
        {
            txtFilterField.Focus();
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

        private void pbAddNewPerson_Click_1(object sender, EventArgs e)
        {
            AddNewPerson form = new AddNewPerson(-1);
            form.DataBack += AddNewPerson_DataBack;
            form.ShowDialog();
        }
    }
}
