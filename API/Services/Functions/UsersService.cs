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
            try
            {
                connection.Open();
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

        public static DataTable FindUser(string id)
        {
            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();

            try
            {
                using (SqlCommand cmd = new SqlCommand("Sp_Find_Users", connection, transaction))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@pUsername", id);
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
                transaction.Rollback();
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        public static string InsertUsers(Users nuevo)
        {
            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();

            try
            {
                using(SqlCommand cmd = new SqlCommand("Sp_Insert_Users", connection, transaction))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@pUsername", nuevo.username);
                    cmd.Parameters.AddWithValue("@pName", nuevo.name);
                    cmd.Parameters.AddWithValue("@pEmail", nuevo.email);
                    cmd.Parameters.AddWithValue("@pPassword", nuevo.password);
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

        public static string UpdateUsers(Users nuevo)
        {
            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();

            try
            {
                using (SqlCommand cmd = new SqlCommand("Sp_Update_Users", connection, transaction))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@pUsername", nuevo.username);
                    cmd.Parameters.AddWithValue("@pName", nuevo.name);
                    cmd.Parameters.AddWithValue("@pEmail", nuevo.email);
                    cmd.Parameters.AddWithValue("@pPassword", nuevo.password);
                    cmd.ExecuteNonQuery();

                    transaction.Commit();
                    return "User update";
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

        public static string DeleteUsers(string id)
        {
            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();

            try
            {
                using (SqlCommand cmd = new SqlCommand("Sp_Delete_Users", connection, transaction))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@pUsername", id);
                    cmd.ExecuteNonQuery();

                    transaction.Commit();
                    return "User Delete";
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
