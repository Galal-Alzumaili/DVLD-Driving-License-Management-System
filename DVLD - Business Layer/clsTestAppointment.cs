using DVLD___Dataccess_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD___Business_Layer
{
    public class clsTestAppointment
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode;

        public int TestAppointmentID { get; set; }
        public clsTestType.enTestType TestTypeID { set; get; }
        //public int TestTypeID { get; set; }
        public int LocalDrivingLicenseApplicationID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public float PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsLocked { get; set; }
        public int RetakeTestApplicationID { get; set; }
        public clsApplication RetakeTestAppInfo { get; set; }
        public int TestID
        {
            get { return _GetTestID(); }

        }

        public clsTestAppointment ()
        {
            this.TestAppointmentID = -1;
            this.TestTypeID = clsTestType.enTestType.VisionTest;
            this.AppointmentDate = DateTime.Now;
            this.PaidFees = 0;
            this.CreatedByUserID = -1;
            this.RetakeTestApplicationID = -1;
            


            Mode = enMode.AddNew;
        }

        private clsTestAppointment (int TestAppointmentID, clsTestType.enTestType TestTypeID, int LocalDrivingLicenseApplicationID, DateTime AppointmentDate, float PaidFees, int CreatedByUserID, bool IsLocked, int RetakeTestApplicationID)
        {
            this.TestAppointmentID = TestAppointmentID;
            this.TestTypeID = TestTypeID;
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.AppointmentDate = AppointmentDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.IsLocked = IsLocked;
            this.RetakeTestApplicationID = RetakeTestApplicationID;
            this.RetakeTestAppInfo = clsApplication.FindBaseApplication(RetakeTestApplicationID);


            Mode = enMode.Update;
        }

        private bool _AddNewTestAppointment()
        {
            int TestAppointmentID = clsTestAppointmentData.AddNewTestAppointment((int)this.TestTypeID, this.LocalDrivingLicenseApplicationID, this.AppointmentDate, this.PaidFees, this.CreatedByUserID, this.RetakeTestApplicationID);

            return (TestAppointmentID != -1);
        }

        private bool _UpdateTestAppointment()
        {
            return (clsTestAppointmentData.UpdateTestAppointment(this.TestAppointmentID, (int)this.TestTypeID, this.LocalDrivingLicenseApplicationID, this.AppointmentDate, this.PaidFees, this.CreatedByUserID, this.IsLocked, this.RetakeTestApplicationID));
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTestAppointment())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdateTestAppointment();
            }

            return false;
        }

        public static clsTestAppointment GetTestAppointmentByTestAppointmentID(int TestAppointmentID)
        {
            int TestTypeID = 1; int LocalDrivingLicenseApplicationID = -1;
            DateTime AppointmentDate = DateTime.Now; float PaidFees = 0;
            int CreatedByUserID = -1; bool IsLocked = false; 
            int RetakeTestApplicationID = -1;

            if (clsTestAppointmentData.GetTestAppointmentByTestAppointmentID(TestAppointmentID, ref TestTypeID, ref LocalDrivingLicenseApplicationID, ref AppointmentDate, ref PaidFees, ref CreatedByUserID, ref IsLocked, ref RetakeTestApplicationID))
                return new clsTestAppointment(TestAppointmentID, (clsTestType.enTestType)TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID);
            else
                return null;
        }

        public static clsTestAppointment GetTestAppointmentByLocalDrivingLicenseApplicationID(int LocalDrivingLicenseApplicationID)
        {
            int TestAppointmentID = -1, TestTypeID = -1, CreatedByUserID = -1;
            DateTime AppointmentDate = DateTime.Now;
            float PaidFees = 0;
            bool IsLocked = true;
            int RetakeTestApplicationID = -1;

            if (clsTestAppointmentData.GetTestAppointmentByLocalDrivingLicenseApplicationID(ref TestAppointmentID, ref TestTypeID, LocalDrivingLicenseApplicationID, ref AppointmentDate, ref PaidFees, ref CreatedByUserID, ref IsLocked, ref RetakeTestApplicationID))
                return new clsTestAppointment(TestAppointmentID, (clsTestType.enTestType) TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID);
            else
                return null;
        }

        public bool DeleteTestAppointment(int TestAppointmentID)
        {
            return clsTestAppointmentData.DeleteTestAppointment(TestAppointmentID);
        }

        public static DataTable GetAllTestAppointments()
        {
            DataTable dt = new DataTable();
            dt = clsTestAppointmentData.GetAllTestAppointments();

            return dt;
        }

        public static bool IsThereAnyActiveAppointmentForThisLocalLicenseApplication(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            return clsTestAppointmentData.IsThereAnyActiveAppointmentForThisLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID, TestTypeID);
        }

        public static bool IsThisAppoinmentIsLocked(int AppointmentID, int TestTypeID)
        {
            return clsTestAppointmentData.IsThisAppointmentIsLocked(AppointmentID, TestTypeID);
        }

        public static DataTable GetAllTestAppointmentsRelatedToLocalDrivingLicenseApplicationID(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)
        {
            DataTable dt = new DataTable();
            dt = clsTestAppointmentData.GetAllTestAppointmentsRelatedToLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID, (int)TestTypeID);

            return dt;
        }

        private int _GetTestID()
        {
            return clsTestAppointmentData.GetTestID(TestAppointmentID);
        }

        public static clsTestAppointment Find(int TestAppointmentID)
        {
            int TestTypeID = 1; int LocalDrivingLicenseApplicationID = -1;
            DateTime AppointmentDate = DateTime.Now; float PaidFees = 0;
            int CreatedByUserID = -1; bool IsLocked = false; int RetakeTestApplicationID = -1;

            if (clsTestAppointmentData.GetTestAppointmentByTestAppointmentID(TestAppointmentID, ref TestTypeID, ref LocalDrivingLicenseApplicationID,
            ref AppointmentDate, ref PaidFees, ref CreatedByUserID, ref IsLocked, ref RetakeTestApplicationID))

                return new clsTestAppointment(TestAppointmentID, (clsTestType.enTestType)TestTypeID, LocalDrivingLicenseApplicationID,
             AppointmentDate, PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID);
            else
                return null;

        }

    }
}

