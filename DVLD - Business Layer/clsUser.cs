using DVLD___Dataccess_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD___Business_Layer
{
    public class clsUser
    {
        public enum enMode { AddNewUser = 0, UpdateUser = 1 };
        public enMode Mode = enMode.AddNewUser;
        public int UserID { get; set; }
        public int PersonID { get; set; }
        public clsPerson PersonInfo;
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive{ get; set; }


        public clsUser()
        {
            this.UserID = -1;
            this.PersonID = -1;
            this.UserName = "";
            this.Password = "";
            this.IsActive = true;
            

            this.Mode = enMode.AddNewUser;

        }

        private clsUser(int UserID, int PersonID, string UserName, string Password, bool IsActive)
        {

            this.UserID = UserID;
            this.PersonID = PersonID;
            this.PersonInfo = clsPerson.Find(PersonID);
            this.UserName = UserName;
            this.Password = Password;
            this.IsActive = IsActive;

            Mode = enMode.UpdateUser;
        }

        private bool _AddNewUser()
        {
            this.UserID = clsUserData.AddNewUser(this.PersonID, this.UserName, this.Password, this.IsActive);

            return (this.UserID != -1);
        }

        private bool _UpdateUser()
        {
            return clsUserData.UpdateUser(this.UserID, this.PersonID, this.UserName, this.Password, this.IsActive);

        }

        public static clsUser Find(int UserID)
        {
            int PersonID = -1;
            string UserName = "", Password = "";
            bool IsActive = false;


            if (clsUserData.GetUserInfoByID(UserID, ref PersonID, ref UserName, ref Password, ref IsActive))
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            else
                return null;
        }

        public static clsUser FindByPersonID(int PersonID)
        {
            int UserID = -1;
            string UserName = "", Password = "";
            bool IsActive = false;


            if (clsUserData.GetUserInfoByPersonID(ref UserID, PersonID, ref UserName, ref Password, ref IsActive))
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            else
                return null;
        }

        public static clsUser FindByUsername(string UserName)
        {

            int UserID = -1 , PersonID = -1;
            string Password = "";
            bool IsActive = false;


            if (clsUserData.GetUserInfoByUsername(ref UserID, ref PersonID, UserName, ref Password, ref IsActive))
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNewUser:
                    if (_AddNewUser())
                    {
                        Mode = enMode.UpdateUser;
                        return true;
                    }
                    else
                        return false;

                case enMode.UpdateUser:
                    return _UpdateUser();
            }

            return false;
        }

        public static DataTable GetAllUsers()
        {
            return clsUserData.GetAllUsers();
        }

        public static bool IsUserExistByID(int UserID)
        {
            return clsUserData.IsUserExistByID(UserID);
        }

        public static bool IsUserExistByPersonID(int PersonID)
        {
            return clsUserData.IsUserExistByPersonID(PersonID);
        }

        public static bool IsUserExistByUsername(string Username)
        {
            return clsUserData.IsUserExistByUsername(Username);
        }

        public static bool DeleteUser(int UserID)
        {
            return clsUserData.DeleteUser(UserID);
        }
    }
}
