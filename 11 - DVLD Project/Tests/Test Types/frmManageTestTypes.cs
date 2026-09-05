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
    public partial class frmManageTestTypes : Form
    {
        private DataTable _TestTypes;
        public frmManageTestTypes()
        {
            InitializeComponent();
        }

        private void _RefreshTestTypesList()
        {
            _TestTypes = clsTestType.GetAllTestsTypes();
            dataGridView1.DataSource = _TestTypes;

            dataGridView1.Font = new Font(dataGridView1.Font.FontFamily, 16);
            dataGridView1.RowTemplate.Height = 35;
            dataGridView1.Columns[0].Width = 80;
            dataGridView1.Columns[1].Width = 300;
            dataGridView1.Columns[2].Width = 647;
            dataGridView1.Columns[3].Width = 155;


            lblCount.Text = dataGridView1.Rows.Count.ToString();
        }


        private void lblCount_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmManageTestTypes_Load(object sender, EventArgs e)
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

            _RefreshTestTypesList();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void editApplicationTypeToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Form UpdateTestType = new frmUpdateTestType((clsTestType.enTestType)dataGridView1.CurrentRow.Cells[0].Value);
            UpdateTestType.ShowDialog();

            _RefreshTestTypesList();
        }
    }
}
