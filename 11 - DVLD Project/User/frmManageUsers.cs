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
    public partial class frmManageUsers : Form
    {
        private DataTable _dtUsers;
        public frmManageUsers()
        {
            InitializeComponent();
        }

        private void _RefreshPeopleList()
        {
            _dtUsers = clsUser.GetAllUsers();
            dataGridView1.DataSource = _dtUsers;
            dataGridView1.Font = new Font(dataGridView1.Font.FontFamily, 16);
            dataGridView1.RowTemplate.Height = 35;
            dataGridView1.Columns[0].Width = 180;
            dataGridView1.Columns[1].Width = 190;
            dataGridView1.Columns[2].Width = 600;
            dataGridView1.Columns[3].Width = 300;
            dataGridView1.Columns[4].Width = 208;
            lblCount.Text = dataGridView1.Rows.Count.ToString();
            
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Form AddNewPerson = new AddNewPerson(-1);
            AddNewPerson.ShowDialog();

            _RefreshPeopleList();
        }


        private void editToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form frm = new AddNewPerson((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();


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



        private void frmManageUsers_Load(object sender, EventArgs e)
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            dataGridView1.Font = new Font("Segoe UI", 12);
            dataGridView1.RowTemplate.Height = 35;

            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

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

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            Form AddOrEditUser = new frmAddNewUser(-1);
            AddOrEditUser.ShowDialog();
            _RefreshPeopleList();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtFilterField_TextChanged_1(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 1:
                    int UserID;

                    if (int.TryParse(txtFilterField.Text, out UserID))
                        _dtUsers.DefaultView.RowFilter = string.Format("[User ID] = {0}", UserID);
                    else
                        _dtUsers.DefaultView.RowFilter = "";
                    break;

                case 2:
                    _dtUsers.DefaultView.RowFilter = string.Format("[UserName] LIKE '%{0}%'", txtFilterField.Text);
                    break;

                case 3:
                    int PersonID;

                    if (int.TryParse(txtFilterField.Text, out PersonID))
                        _dtUsers.DefaultView.RowFilter = string.Format("[Person ID] = {0}", PersonID);
                    else
                        _dtUsers.DefaultView.RowFilter = "";
                    break;

                case 4:
                    _dtUsers.DefaultView.RowFilter = string.Format("[Full Name] LIKE '%{0}%'", txtFilterField.Text);
                    break;

                




            }
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            txtFilterField.Text = "";
            if (_dtUsers != null)
                _dtUsers.DefaultView.RowFilter = "";

            txtFilterField.KeyPress -= ValidateNumbers_KeyPress;
            txtFilterField.KeyPress -= ValidateNumbers_KeyPress;

            if (comboBox1.SelectedIndex == 0)
            {
                txtFilterField.Visible = false;
                return;
            }

            if(comboBox1.SelectedIndex == 5)
            {
                txtFilterField.Visible = false;
                comboBox2.Visible = true;
                comboBox2.SelectedIndex = 0;
                //_dtUsers.DefaultView.RowFilter = string.Format("[Is Active] = true");
                return;
            }

            txtFilterField.Visible = true;


            if (comboBox1.SelectedIndex == 1 || comboBox1.SelectedIndex == 3) // PersonID
                txtFilterField.KeyPress += ValidateNumbers_KeyPress;
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form AddOrEditUser = new frmAddNewUser((int)dataGridView1.CurrentRow.Cells[1].Value);
            AddOrEditUser.ShowDialog();
            _RefreshPeopleList();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0)
            {
                //_dtUsers.DefaultView.RowFilter = string.Format("[Is Active] = true");
                _dtUsers.DefaultView.RowFilter = "";
                return;
            }

            if (comboBox2.SelectedIndex == 1)
            {
                _dtUsers.DefaultView.RowFilter = string.Format("[Is Active] = true");
                return;
            }

            if (comboBox2.SelectedIndex == 2)
            {
                _dtUsers.DefaultView.RowFilter = string.Format("[Is Active] = false");
                return;
            }
        }

        private void deleteToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to delete user with: [" + dataGridView1.CurrentRow.Cells[0].Value + "]", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, 0) == DialogResult.OK)
            {
                if (clsUser.DeleteUser((int)dataGridView1.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("User Deleted Successfully.", "Sucessfully", MessageBoxButtons.OK, MessageBoxIcon.Information, 0);
                    _RefreshPeopleList();
                }
                else
                    MessageBox.Show("User was not deleted because it has data linked to it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 0);
            }
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form ChangePassword = new frmChangePassword((int)dataGridView1.CurrentRow.Cells[1].Value);
            ChangePassword.ShowDialog();
            _RefreshPeopleList();
        }

        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form AddOrEditUser = new frmAddNewUser(-1);
            AddOrEditUser.ShowDialog();
            _RefreshPeopleList();
        }

        private void showDetailsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Form ShowDetails = new frmShowUserDetails((int)dataGridView1.CurrentRow.Cells[0].Value);
            ShowDetails.ShowDialog();
            _RefreshPeopleList();
        }
    }
}
