using DVLD___Dataccess_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD___Business_Layer
{
    public class clsApplicationType
    {
        public enum enMode { AddNew = 0, Update =1 };
        public enMode Mode;

        public int ApplicationTypesID {  get; set; }
        public string ApplicationTypesTitle { get; set; }
        public float ApplicationTypesFees { get; set; }

        public clsApplicationType()
        {
            this.ApplicationTypesID = -1;
            this.ApplicationTypesTitle = "";
            this.ApplicationTypesFees = 0;

            Mode = enMode.AddNew;
        }

        private clsApplicationType(int ApplicationTypesID, string ApplicationTypesTitle, float ApplicationTypesFees)
        {
            this.ApplicationTypesID = ApplicationTypesID;
            this.ApplicationTypesTitle = ApplicationTypesTitle;
            this.ApplicationTypesFees = ApplicationTypesFees;

            Mode = enMode.Update;
        }

        private bool _AddNew()
        {
            return false;
        }

        private bool _UpdateApplicationType()
        {
            return ApplicationType.UpdateApplicationType(this.ApplicationTypesID, this.ApplicationTypesTitle, this.ApplicationTypesFees);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNew())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdateApplicationType();
            }

            return false;
        }

        public static clsApplicationType Find(int ApplicationTypesID)
        {
            string ApplicationTypesTitle = "";
            float ApplicationTypesFees = 0;

            if (ApplicationType.GetApplicationTypeByID((int)ApplicationTypesID, ref ApplicationTypesTitle, ref ApplicationTypesFees))
                return new clsApplicationType(ApplicationTypesID, ApplicationTypesTitle, ApplicationTypesFees);
            else
                return null;
        }

        public static clsApplicationType FindByName(string ApplicationTypesTitle)
        {
            int ApplicationTypesID = -1;
            float ApplicationTypesFees = 0;


            if (ApplicationType.GetApplicationTypeByTitle(ref ApplicationTypesID, ApplicationTypesTitle, ref ApplicationTypesFees))
                return new clsApplicationType(ApplicationTypesID, ApplicationTypesTitle, ApplicationTypesFees);
            else
                return null;
        }

        public static DataTable GetAllApplicationTypes()
        {
            DataTable dt = new DataTable();
            dt = ApplicationType.GetAllApplicationTypes();
            return dt;
        }
    }
}
