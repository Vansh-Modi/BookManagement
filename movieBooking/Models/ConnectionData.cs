using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace movieBooking.Models
{
    public class ConnectionData
    {
        public string Connection()
        {
            return ConfigurationManager.ConnectionStrings["MovieDB"].ConnectionString;
        }
        public int booking_ID { get; set; }
        public int User_ID { get; set; }
        public int Cat_ID { get; set; }
        public int Movie_ID { get; set; }
        public int no_of_Tickets { get; set; }
        public int amount { get; set; }
        public int rate { get; set; }
        public string Release_Date { get; set; }
        public string Movie_name { get; set; }
        public string Cat_Type { get; set; }
        public string User_Name { get; set; }
        public string Email_ID { get; set; }
        public string User_password { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
    }
}