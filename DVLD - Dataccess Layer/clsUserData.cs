using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD___Dataccess_Layer
{
    public class clsUserData
    {

        public static bool GetUserInfoByID(int UserID, ref int PersonID, ref string UserName, ref string Password, ref bool IsActive)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(DVLD___DataAccess_Layer.clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Users WHERE UserID=@UserID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;

                    PersonID = (int)reader["PersonID"];
                    UserName = (string)reader["UserName"];
                    Password = (string)reader["Password"];
                    IsActive = (bool)reader["IsActive"];


                    reader.Close();
                }
                else
                {
                    IsFound = false;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                IsFound = false;
            }
            finally
            {
                connection.Close();
            }
            return IsFound;
        }

        public static bool GetUserInfoByPersonID(ref int UserID, int PersonID, ref string UserName, ref string Password, ref bool IsActive)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(DVLD___DataAccess_Layer.clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Users WHERE PersonID=@PersonID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;

                    UserID = (int)reader["UserID"];
                    UserName = (string)reader["UserName"];
                    Password = (string)reader["Password"];
                    IsActive = (bool)reader["IsActive"];


                    reader.Close();
                }
                else
                {
                    IsFound = false;
                }
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

        public static bool GetUserInfoByUsername(ref int UserID, ref int PersonID, string UserName, ref string Password, ref bool IsActive)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(DVLD___DataAccess_Layer.clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Users WHERE UserName=@UserName";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserName", UserName);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;

                    UserID = (int)reader["UserID"];
                    PersonID = (int)reader["PersonID"];
                    //UserName = (string)reader["UserName"];
                    Password = (string)reader["Password"];
                    IsActive = (bool)reader["IsActive"];


                    reader.Close();
                }
                else
                {
                    IsFound = false;
                }
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

        public static int AddNewUser(int PersonID, string UserName, string Password, bool IsActive)
        {
            int userID = -1;

            SqlConnection connection = new SqlConnection(DVLD___DataAccess_Layer.clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO Users (PersonID, UserName, Password, IsActive)
                            VALUES (@PersonID, @UserName, @Password, @IsActive)
                            SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@IsActive", IsActive);



            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                    userID = insertedID;
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return userID;

        }

        public static bool UpdateUser(int UserID, int PersonID, string UserName, string Password, bool IsActive)
        {
            bool IsUpdated = false;
            SqlConnection connection = new SqlConnection(DVLD___DataAccess_Layer.clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE Users SET 
                            UserName = @UserName,
                            Password = @Password,
                            IsActive = @IsActive 
                            WHERE UserID = @UserID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@UserID", UserID);

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

        public static bool DeleteUser(int UserID)
        {
            bool IsDeleted = false;
            SqlConnection connection = new SqlConnection(DVLD___DataAccess_Layer.clsDataAccessSettings.ConnectionString);

            string query = "DELETE FROM Users WHERE UserID=@UserID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);

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

        public static DataTable GetAllUsers()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(DVLD___DataAccess_Layer.clsDataAccessSettings.ConnectionString);

            string query = @"SELECT u.UserID AS [User ID], u.PersonID AS [Person ID],
                            p.FirstName + ' ' + p.SecondName + ' ' + p.ThirdName + ' ' + p.LastName AS [Full Name], 
                            u.UserName, u.IsActive AS [Is Active]
                            FROM Users u
                            JOIN People p ON u.PersonID = p.PersonID";

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

        public static bool IsUserExistByID(int UserID)
        {
            bool IsExist = false;

            SqlConnection connection = new SqlConnection(DVLD___DataAccess_Layer.clsDataAccessSettings.ConnectionString);
            string query = "SELECT COUNT(1) FROM Users WHERE UserID = @ID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", UserID);
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

        public static bool IsUserExistByPersonID(int PersonID)
        {
            bool IsExist = false;

            SqlConnection connection = new SqlConnection(DVLD___DataAccess_Layer.clsDataAccessSettings.ConnectionString);
            string query = "SELECT COUNT(1) FROM Users WHERE PersonID=@PersonID";

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

        public static bool IsUserExistByUsername(string Username)
        {
            bool IsExist = false;

            SqlConnection connection = new SqlConnection(DVLD___DataAccess_Layer.clsDataAccessSettings.ConnectionString);
            string query = "SELECT COUNT(1) FROM Users WHERE UserName=@UserName";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserName", Username);
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
