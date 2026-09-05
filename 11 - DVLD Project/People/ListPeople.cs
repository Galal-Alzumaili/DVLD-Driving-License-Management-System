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
    public partial class ListPeople : Form
    {
        private DataTable _dtPeople;
        public ListPeople()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void _RefreshPeopleList()
        {
            _dtPeople = clsPerson.GetAllPeople();
            dataGridView1.DataSource = _dtPeople;
            lblCount.Text = dataGridView1.Rows.Count.ToString();
        }

        private void ListPeople_Load(object sender, EventArgs e)
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            dataGridView1.Font = new Font("Segoe UI", 12);
            dataGridView1.RowTemplate.Height = 35;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            dataGridView1.BackgroundColor = Color.FromArgb(15, 23, 42);

            dataGridView1.DefaultCellStyle.BackColor =
                Color.FromArgb(15, 23, 42);

            dataGridView1.DefaultCellStyle.ForeColor = Color.White;

            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(10, 15, 30);

            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.Gold;

            dataGridView1.EnableHeadersVisualStyles = false;

            comboBox1.SelectedIndex = 0;
            _RefreshPeopleList();
            
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Form AddNewPerson = new AddNewPerson(-1);
            AddNewPerson.ShowDialog();

            _RefreshPeopleList();
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form frm = new AddNewPerson((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            _RefreshPeopleList();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new Person_Details((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to delete Person[" + dataGridView1.CurrentRow.Cells[0].Value + "]", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, 0) == DialogResult.OK)
            {
                if (clsPerson.DeletePerson((int)dataGridView1.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Person Deleted Successfully.", "Sucessfully", MessageBoxButtons.OK, MessageBoxIcon.Information, 0);
                    _RefreshPeopleList();
                }
                else
                    MessageBox.Show("Person was not deleted because it has data linked to it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 0);

            }
        }

        // Handle String and Digits

        private void ValidateNumbers_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterField.Text = "";
            if (_dtPeople != null)
                _dtPeople.DefaultView.RowFilter = "";

            txtFilterField.KeyPress -= ValidateNumbers_KeyPress;

            if (comboBox1.SelectedIndex == 0)
            {
                txtFilterField.Visible = false;
                return;
            }

            txtFilterField.Visible = true;


            if (comboBox1.SelectedIndex == 1) // PersonID
                txtFilterField.KeyPress += ValidateNumbers_KeyPress;
        }

        private void txtFilterField_TextChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 1:
                    int PersonID;

                    if (int.TryParse(txtFilterField.Text, out PersonID))
                        _dtPeople.DefaultView.RowFilter = string.Format("[PersonID] = {0}", PersonID);
                    else
                        _dtPeople.DefaultView.RowFilter = "";
                    break;

                case 2:
                    _dtPeople.DefaultView.RowFilter = string.Format("[NationalNo] LIKE '%{0}%'", txtFilterField.Text);
                    break;
                    
                case 3:
                    _dtPeople.DefaultView.RowFilter = string.Format("[FirstName] LIKE '%{0}%'", txtFilterField.Text);
                    break;

                case 4:
                    _dtPeople.DefaultView.RowFilter = string.Format("[SecondName] LIKE '%{0}%'", txtFilterField.Text);
                    break;

                case 5:
                    _dtPeople.DefaultView.RowFilter = string.Format("[ThirdName] LIKE '%{0}%'", txtFilterField.Text);
                    break;

                case 6:
                    _dtPeople.DefaultView.RowFilter = string.Format("[LastName] LIKE '%{0}%'", txtFilterField.Text);
                    break;

                case 7:
                    _dtPeople.DefaultView.RowFilter = string.Format("[CountryName] LIKE '%{0}%'", txtFilterField.Text);
                    break;

                case 8:
                    _dtPeople.DefaultView.RowFilter = string.Format("[Gendor] LIKE '%{0}%'", txtFilterField.Text);
                    break;

                case 9:
                    _dtPeople.DefaultView.RowFilter = string.Format("[Phone] LIKE '%{0}%'", txtFilterField.Text);
                    break;

                case 10:
                    _dtPeople.DefaultView.RowFilter = string.Format("[Email] LIKE '%{0}%'", txtFilterField.Text);
                    break;


            }
               
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form AddNewPerson = new AddNewPerson(-1);
            AddNewPerson.ShowDialog();

            _RefreshPeopleList();
        }
    }
}
