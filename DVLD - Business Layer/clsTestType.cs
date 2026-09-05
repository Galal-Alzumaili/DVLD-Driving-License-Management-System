using DVLD___Dataccess_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD___Business_Layer
{
    public class clsTestType
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public enum enTestType { VisionTest = 1, WrittenTest = 2, StreetTest = 3 };


        public clsTestType.enTestType ID { get; set; }
        public string TestTypeTitle { get; set; }
        public string TestTypeDescription { get; set; }
        public float TestTypeFees { get; set; }

        public clsTestType()
        {
            this.ID = clsTestType.enTestType.VisionTest;
            this.TestTypeTitle = "";
            this.TestTypeTitle = "";
            this.TestTypeFees = 0;

            Mode = enMode.AddNew;
        }

        private clsTestType(clsTestType.enTestType ID, string TestTypeTitle, string TestTypeDescription, float TestTypeFees)
        {
            this.ID = ID;
            this.TestTypeTitle= TestTypeTitle;
            this.TestTypeDescription = TestTypeDescription;
            this.TestTypeFees = TestTypeFees;

            Mode = enMode.Update;
        }

        private bool _AddNew()
        {
            return false;
        }

        private bool _UpdateTestType()
        {
            return TestTypeData.UpdateTestType((int)this.ID, this.TestTypeTitle, this.TestTypeDescription, this.TestTypeFees);
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
                    return _UpdateTestType();
            }

            return false;
        }

        public static clsTestType Find(clsTestType.enTestType TestTypeID)
        {
            string TestTypeTitle = "",TestTypeDescription = "";
            float TestTypeFees = 0;

            if (TestTypeData.GetTestTypeByID((int)TestTypeID, ref TestTypeTitle, ref TestTypeDescription,  ref TestTypeFees))
                return new clsTestType(TestTypeID, TestTypeTitle, TestTypeDescription, TestTypeFees);
            else
                return null;
        }

        public static clsTestType FindByName(string TestTypeTitle)
        {
            int TestTypeID = (int)clsTestType.enTestType.VisionTest;
            string TestTypeDescription = "";
            float TestTypeFees = 0;


            if (TestTypeData.GetTestTypeByTitle(ref TestTypeID, TestTypeTitle, ref TestTypeDescription, ref TestTypeFees))
                return new clsTestType((enTestType)TestTypeID, TestTypeTitle, TestTypeDescription, TestTypeFees);
            else
                return null;
        }

        public static DataTable GetAllTestsTypes()
        {
            DataTable dt = new DataTable();
            dt = TestTypeData.GetAllTestTypes();
            return dt;
        }
    }
}
