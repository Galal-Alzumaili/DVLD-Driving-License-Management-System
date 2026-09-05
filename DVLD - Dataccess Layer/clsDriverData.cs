using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD___Dataccess_Layer
{
    public class clsDriverData
    {
        public static bool GetDriverInfoByDriverID(int DriverID, ref int PersonID, ref int CreatedByUserID, ref DateTime CreatedDate)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(DVLD___DataAccess_Layer.clsDataAccessSettings.ConnectionString);

            string query = @"SELECT *
                             FROM Drivers
                             WHERE DriverID=@DriverID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;

                    PersonID = (int)reader["PersonID"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    CreatedDate = (DateTime)reader["CreatedDate"];
                }
                else
                    IsFound = false;

                reader.Close();

            }
            catch (Exception ex)
            {
                IsFound = false;
            }
            finally
            {
                connection.Close();
            }

            return IsFound;
        }

        public static bool GetDriverInfoByPersonID(ref int DriverID, int PersonID, ref int CreatedByUserID, ref DateTime CreatedDate)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(DVLD___DataAccess_Layer.clsDataAccessSettings.ConnectionString);

            string query = @"SELECT *
                             FROM Drivers
                             WHERE PersonID = @PersonID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;

                    DriverID = (int)reader["DriverID"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    CreatedDate = (DateTime)reader["CreatedDate"];
                }
                else
                    IsFound = false;

                reader.Close();

            }
            catch (Exception ex)
            {
                IsFound = false;
            }
            finally
            {
                connection.Close();
            }

            return IsFound;
        }

        public static int AddNewDriver(int PersonID, int CreatedByUserID)
        {
            int DriverID = -1;

            SqlConnection connection = new SqlConnection(DVLD___DataAccess_Layer.clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO Drivers (PersonID, CreatedByUserID, CreatedDate)
                            VALUES (@PersonID, @CreatedByUserID, @CreatedDate)
                            SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand (query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@CreatedDate", DateTime.Now);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                    DriverID = insertedID;
            }
            catch (Exception ex)
            {
                DriverID = -1;
            }
            finally
            {
                connection.Close();
            }

            return DriverID;
        }

        public static bool UpdateDriverInfo (int DriverID, int PersonID, int CreatedByUserID)
        {
            bool IsUpdated = false;
            SqlConnection connection = new SqlConnection(DVLD___DataAccess_Layer.clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE Drivers SET 
                            PersonID = @PersonID,
                            CreatedByUserID = @CreatedByUserID
                            WHERE DriverID = @DriverID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                connection.Open();
                int AffectedRows = command.ExecuteNonQuery();

                if (AffectedRows > 0)
                    IsUpdated = true;
                else
                    IsUpdated = false;
            }
            catch (Exception ex)
            {
                IsUpdated = false;
            }
            finally
            {
                connection.Close();
            }
            return IsUpdated;
        }

        public static bool DeleteDriver(int DriverID)
        {
            bool IsDeleted = false;
            SqlConnection connection = new SqlConnection(DVLD___DataAccess_Layer.clsDataAccessSettings.ConnectionString);

            string query = "DELETE FROM Drivers WHERE DriverID=@DriverID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                connection.Open();
                int AffectedRows = command.ExecuteNonQuery();
                if (AffectedRows > 0)
                    IsDeleted = true;
                else
                    IsDeleted = false;
            }
            catch (Exception ex)
            {
                IsDeleted = false;
            }
            finally
            {
                connection.Close();
            }
            return IsDeleted;
        }

        public static DataTable GetAllDrivers()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(DVLD___DataAccess_Layer.clsDataAccessSettings.ConnectionString);

            string query = @"SELECT *
                            FROM Drivers_View
                            ORDER by FullName;";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                    dt.Load(reader);

                reader.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

        public static bool IsDriverExistByDriverID(int DriverID)
        {
            bool IsExist = false;

            SqlConnection connection = new SqlConnection(DVLD___DataAccess_Layer.clsDataAccessSettings.ConnectionString);
            string query = "SELECT COUNT(1) FROM Drivers WHERE DriverID = @DriverID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                IsExist = (int.Parse(result.ToString()) > 0);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }

            return IsExist;
        }

        public static bool IsDriverExistByPersonID(int PersonID)
        {
            bool IsExist = false;

            SqlConnection connection = new SqlConnection(DVLD___DataAccess_Layer.clsDataAccessSettings.ConnectionString);
            string query = "SELECT COUNT(1) FROM Drivers WHERE PersonID=@PersonID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                IsExist = (int.Parse(result.ToString()) > 0);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }

            return IsExist;
        }
    }
}
