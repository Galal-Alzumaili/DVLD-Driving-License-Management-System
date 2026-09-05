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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace _11___DVLD_Project
{
    public partial class frmManageApplicationsTypes : Form
    {
        private DataTable _ApplicationTypes;
        public frmManageApplicationsTypes()
        {
            InitializeComponent();
        }

        private void _RefreshApplicationsList()
        {
            _ApplicationTypes = clsApplicationType.GetAllApplicationTypes();
            dataGridView1.DataSource = _ApplicationTypes;

            dataGridView1.Font = new Font(dataGridView1.Font.FontFamily, 16);
            dataGridView1.RowTemplate.Height = 35;
            dataGridView1.Columns[0].Width = 180;
            dataGridView1.Columns[1].Width = 600;
            dataGridView1.Columns[2].Width = 155;


            lblCount.Text = dataGridView1.Rows.Count.ToString();
        }

        private void frmManageApplicationsTypes_Load(object sender, EventArgs e)
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

            _RefreshApplicationsList();
        }

        private void lblCount_Click(object sender, EventArgs e)
        {

        }

        private void editApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form EditApplicationType = new frmUpdateApplicationType((int)dataGridView1.CurrentRow.Cells[0].Value);
            EditApplicationType.ShowDialog();

            _RefreshApplicationsList();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
