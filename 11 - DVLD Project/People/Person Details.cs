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
    public partial class Person_Details : Form
    {
        int _PersonID;
        public Person_Details(int PersonID)
        {
            InitializeComponent();

            _PersonID = PersonID;
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void personDetails1_Load(object sender, EventArgs e)
        {
            personDetails1.LoadPersonInfo(_PersonID);
        }
    }
}
