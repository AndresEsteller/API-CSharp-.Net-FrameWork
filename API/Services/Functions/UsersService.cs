using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Services.Sql;

namespace Services.Functions
{
    public static class UsersService
    {
        public static DataTable SelectUsers()
        {
            connection.Open();
            try 
            {
                using (SqlCommand cmd = new SqlCommand("Sp_Select_Users", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable data = new DataTable();
                        adapter.Fill(data);
                        return data;
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        public static DataTable FindUser(string username)
        {
            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();

            try
            {
                using (SqlCommand cmd = new SqlCommand("Sp_Find_Users", connection, transaction))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@pUsername", username);
                    cmd.ExecuteNonQuery();
                    transaction.Commit();

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable data = new DataTable();
                        adapter.Fill(data);
                        return data;
                    }
                }
            }
            catch (Exception)
            {
                message = ex.Message;
                transaction.Rollback();
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        public static string InsertUser(Users newUser)
        {
            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();

            try
            {
                using(SqlCommand cmd = new SqlCommand("Sp_Insert_Users", connection, transaction))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@pUsername", newUser.username);
                    cmd.Parameters.AddWithValue("@pName", newUser.name);
                    cmd.Parameters.AddWithValue("@pEmail", newUser.email);
                    cmd.Parameters.AddWithValue("@pPassword", newUser.password);
                    cmd.ExecuteNonQuery();

                    transaction.Commit();
                    return "Register";
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return ex.Message;
            }
            finally
            {
                connection.Close();
            }
        }

        public static string UpdateUser(Users editUser)
        {
            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();

            try
            {
                using (SqlCommand cmd = new SqlCommand("Sp_Update_Users", connection, transaction))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@pUsername", editUser.username);
                    cmd.Parameters.AddWithValue("@pName", editUser.name);
                    cmd.Parameters.AddWithValue("@pEmail", editUser.email);
                    cmd.Parameters.AddWithValue("@pPassword", editUser.password);
                    cmd.ExecuteNonQuery();

                    transaction.Commit();
                    return "Update";
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return ex.Message;
            }
            finally
            {
                connection.Close();
            }
        }

        public static string DeleteUser(string username)
        {
            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();

            try
            {
                using (SqlCommand cmd = new SqlCommand("Sp_Delete_Users", connection, transaction))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@pUsername", username);
                    cmd.ExecuteNonQuery();

                    transaction.Commit();
                    return "Delete";
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return ex.Message;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
