using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD___DataAccess_Layer;
using DVLD___Dataccess_Layer;

namespace DVLD___Business_Layer
{
    public class clsCountry
    {
        public int CountryID { get; set; }
        public string CountryName { get; set; }

        public clsCountry()
        {
            CountryID = -1;
            CountryName = "";

        }

        private clsCountry (int CountryID, string CountryName)
        {
            this.CountryID = CountryID;
            this.CountryName = CountryName;

        }

        public static clsCountry Find (int ID)
        {
            string CountryName = "";

            if (clsCountryData.GetCountryInfoByID(ID, ref CountryName))
                return new clsCountry(ID, CountryName);
            else
                return null;
        }

        public static clsCountry FindByName(string CountryName)
        {
            int CountryID = -1;
           

            if (clsCountryData.GetCountryInfoByName(ref CountryID, CountryName))
                return new clsCountry(CountryID, CountryName);
            else
                return null;
        }

        public static DataTable GetAllCountries()
        {
            DataTable dt = new DataTable();
            dt = clsCountryData.GetAllCountries();
            return  dt;
        }
    }
}
