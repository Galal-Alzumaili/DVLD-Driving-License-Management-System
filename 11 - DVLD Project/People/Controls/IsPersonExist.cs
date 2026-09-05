using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD___Business_Layer;

namespace _11___DVLD_Project
{
    public partial class IsPersonExist : UserControl
    {
        public IsPersonExist()
        {
            InitializeComponent();
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {

            int PersonID;
            if (int.TryParse(textBox1.Text, out PersonID))
            {
                MessageBox.Show(PersonID.ToString());
                if (clsPerson.IsPersonExistByID(PersonID))
                    MessageBox.Show("Person with ID: " + PersonID + " Is Exist");
                else
                    MessageBox.Show("There is no person with ID: " + PersonID);
            }
            else
            {
                MessageBox.Show("Please ensure to enter an Integer");
            }

        }
    }
}
