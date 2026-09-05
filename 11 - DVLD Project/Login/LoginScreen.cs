using DVLD___Business_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace _11___DVLD_Project
{
    public partial class LoginScreen : Form
    {
        private bool LoginInfoSaved = false;
        public LoginScreen()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            clsUser user = clsUser.FindByUsername(txbUsername.Text);


            if (user == null || user.Password != txbPassword.Text)
            {
                MessageBox.Show("Invalid username or password!");
                return;
            }

            if (!user.IsActive)
            {
                MessageBox.Show("This user is not active!");
                return;
            }

            GlobalSettings.CurrentUser = user;

            if (chbRemeberME.Checked)
            {
                using (StreamWriter writer = File.CreateText("LoginInformation.txt"))
                {
                    writer.WriteLine(txbUsername.Text.Trim());
                    writer.WriteLine(txbPassword.Text);
                }
            }
            else
            {
                File.WriteAllText("LoginInformation.txt", "");
            }

            //Form mainScreen = new MainScreen();
            //mainScreen.ShowDialog();

            // this for closing login form
            this.Hide();

            MainScreen mainScreen = new MainScreen();
            mainScreen.ShowDialog();

            if (!chbRemeberME.Checked)
            {
                txbUsername.Clear();
                txbPassword.Clear();
            }

            this.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            clsPerson person1 = clsPerson.Find(1);
            MessageBox.Show(person1.FirstName.ToString());
        }

        private void chbRemeberME_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chbShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (chbShowPassword.Checked)
            {
                txbPassword.PasswordChar = '\0';

            }
            else
            {
                txbPassword.PasswordChar = '*';

            }

        }

        private void LoginScreen_Load(object sender, EventArgs e)
        {
            if (!File.Exists("LoginInformation.txt"))
                return;

            string[] lines = File.ReadAllLines("LoginInformation.txt");

            if (lines.Length >= 2 &&
                !string.IsNullOrWhiteSpace(lines[0]) &&
                !string.IsNullOrWhiteSpace(lines[1]))
            {
                txbUsername.Text = lines[0];
                txbPassword.Text = lines[1];
                chbRemeberME.Checked = true;
            }
            else
            {
                txbUsername.Text = "";
                txbPassword.Text = "";
                chbRemeberME.Checked = false;
            }

            this.AcceptButton = btnLogin;
        }
    }
}
