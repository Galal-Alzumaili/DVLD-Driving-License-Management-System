using _11___DVLD_Project.Licenses;
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

namespace _11___DVLD_Project.Drivers
{
    public partial class frmListDrivers : Form
    {
        private DataTable _dtDrivers;
        public frmListDrivers()
        {
            InitializeComponent();
        }
        private void _RefreshPeopleList()
        {
            _dtDrivers = clsDriver.GetAllDrivers();
            dataGridView1.DataSource = _dtDrivers;
            dataGridView1.Font = new Font(dataGridView1.Font.FontFamily, 16);
            dataGridView1.RowTemplate.Height = 35;
            dataGridView1.Columns[0].Width = 140;
            dataGridView1.Columns[1].Width = 140;
            dataGridView1.Columns[2].Width = 160;
            dataGridView1.Columns[3].Width = 506;
            dataGridView1.Columns[4].Width = 285;
            dataGridView1.Columns[5].Width = 250;
            lblCount.Text = dataGridView1.Rows.Count.ToString();

        }

        private void frmListDrivers_Load(object sender, EventArgs e)
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

        private void ValidateNumbers_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtFilterField_TextChanged_1(object sender, EventArgs e)
        {
            
           
        }
        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterField.Text = "";
            if (_dtDrivers != null)
                _dtDrivers.DefaultView.RowFilter = "";

            txtFilterField.KeyPress -= ValidateNumbers_KeyPress;
            txtFilterField.KeyPress -= ValidateNumbers_KeyPress;

            if (comboBox1.SelectedIndex == 0)
            {
                txtFilterField.Visible = false;
                return;
            }


            txtFilterField.Visible = true;


            if (comboBox1.SelectedIndex == 1 || comboBox1.SelectedIndex == 2) // PersonID
                txtFilterField.KeyPress += ValidateNumbers_KeyPress;
        }

        private void txtFilterField_TextChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 1:
                    int DriverID;

                    if (int.TryParse(txtFilterField.Text, out DriverID))
                        _dtDrivers.DefaultView.RowFilter = string.Format("[DriverID] = {0}", DriverID);
                    else
                        _dtDrivers.DefaultView.RowFilter = "";
                    break;

                case 2:
                    int PersonID;

                    if (int.TryParse(txtFilterField.Text, out PersonID))
                        _dtDrivers.DefaultView.RowFilter = string.Format("[PersonID] = {0}", PersonID);
                    else
                        _dtDrivers.DefaultView.RowFilter = "";
                    break;

                case 3:
                    _dtDrivers.DefaultView.RowFilter = string.Format("[NationalNo] LIKE '%{0}%'", txtFilterField.Text);
                    break;

                case 4:
                    _dtDrivers.DefaultView.RowFilter = string.Format("[FullName] LIKE '%{0}%'", txtFilterField.Text);
                    break;






            }
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dataGridView1.CurrentRow.Cells[1].Value;
            Form frm = new Person_Details(PersonID);
            frm.ShowDialog();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dataGridView1.CurrentRow.Cells[1].Value;


            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(PersonID);
            frm.ShowDialog();
        }
    }
}
