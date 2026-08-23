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
        public ConnectionData cd = new ConnectionData();
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
                    cmd.Parameters.AddWithValue("User_Name", login.User_Name );
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
    }
}