using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace movieBooking.Models
{
    public class LoginModel
    {
        public ConnectionData cd { get; set; } = new ConnectionData();
        public int User_ID { get; set; }
        public string User_Name { get; set; }
        public string User_password { get; set; }

        public int fnLogin(LoginModel login)
        {

            using (SqlConnection conn = new SqlConnection(cd.Connection()))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("LoginUser", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("User_Name", login.User_Name);
                    cmd.Parameters.AddWithValue("User_password", login.User_password);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            User_ID = Convert.ToInt32(reader["User_ID"]);
                        }
                    }
                }
                return User_ID;
            }
        }
        public int fnAddUsers(LoginModel model)
        {
            using (SqlConnection conn = new SqlConnection(cd.Connection()))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("AddUser", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Name", cd.User_Name ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("Email", cd.Email_ID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("Password", cd.User_password ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("City", cd.City ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("Phone", cd.Phone ?? (object)DBNull.Value);
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                        return User_ID = fnLogin(model);
                }
            }
            return 0;
        }
        public List<Movie> fnDisplayUsers()
        {
            List<Movie> userList = new List<Movie>();
            using (SqlConnection conn = new SqlConnection(cd.Connection()))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("DisplayUsers", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        foreach (DataRow row in dt.Rows)
                        {
                            ConnectionData userData = new ConnectionData
                            {
                                User_ID = Convert.ToInt32(row["User_ID"]),
                                User_Name = row["User_Name"].ToString(),
                                Email_ID = row["Email_ID"].ToString(),
                                Phone = row["PhoneNo"].ToString(),
                                City = row["City"].ToString(),
                                User_password = row["User_Password"].ToString()
                            };
                            Movie movie = new Movie();
                            movie.cd = userData;
                            userList.Add(movie);
                        }
                    }
                }
            }
            return userList;
        }
        public bool fnDeleteUser(int userId)
        {
            using (SqlConnection conn = new SqlConnection(cd.Connection()))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("DeleteUser", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("User_ID", userId);
                    int i = cmd.ExecuteNonQuery();
                    return i > 0;
                }
            }
        }
        public bool fnUpdateUser(LoginModel model)
        {
            using (SqlConnection conn = new SqlConnection(cd.Connection()))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("UpdateUser", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("User_ID", model.User_ID );
                    cmd.Parameters.AddWithValue("User_Name", model.User_Name ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("Email_ID", model.cd.Email_ID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("User_Password", model.User_password ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("City", model.cd.City ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("PhoneNo", model.cd.Phone ?? (object)DBNull.Value);
                    int i = cmd.ExecuteNonQuery();
                    return i > 0;
                }
            }
        }
    }
}