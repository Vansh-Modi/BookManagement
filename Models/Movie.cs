using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Web;

namespace movieBooking.Models
{
    public class Movie
    {
        public ConnectionData cd { get; set; } = new ConnectionData();
        public bool fnAddCategory()
        {
            using (SqlConnection conn = new SqlConnection(cd.Connection()))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("AddMovieCategory", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Name", cd.Cat_Type);
                    int i = cmd.ExecuteNonQuery();
                    return i > 0;
                }
            }
        }
        public bool fnAddMovie()
        {
            DateTime parsedDate = DateTime.Parse(cd.Release_Date);
            using (SqlConnection conn = new SqlConnection(cd.Connection()))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("AddMovie", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Name", cd.Movie_name);
                    cmd.Parameters.AddWithValue("Date", parsedDate);
                    cmd.Parameters.AddWithValue("Cat_ID", cd.Cat_ID);
                    cmd.Parameters.AddWithValue("Rate", cd.rate);
                    int i = cmd.ExecuteNonQuery();
                    return i > 0;
                }
            }
        }
        public bool fnAddBooking()
        {
            using (SqlConnection conn = new SqlConnection(cd.Connection()))
            {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand("GetRate", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Movie_ID", cd.Movie_ID);
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        cd.rate = Convert.ToInt32(result);
                    }
                }
                using (SqlCommand cmd = new SqlCommand("AddBooking", conn))
                {
                    cd.amount = cd.no_of_Tickets * cd.rate;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("UserID", cd.User_ID);
                    cmd.Parameters.AddWithValue("MovieId", cd.Movie_ID);
                    cmd.Parameters.AddWithValue("NoOfTickets", cd.no_of_Tickets);
                    cmd.Parameters.AddWithValue("CatId", cd.Cat_ID);
                    cmd.Parameters.AddWithValue("Amount", cd.amount);
                    int i = cmd.ExecuteNonQuery();
                    return i > 0;
                }
            }
        }
        public List<Movie> fnDisplayMovie(string search)
        {
            List<Movie> movies = new List<Movie>();
            using (SqlConnection conn = new SqlConnection(cd.Connection()))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("DisplayMovies", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (string.IsNullOrEmpty(search))
                        cmd.Parameters.AddWithValue("Cat_ID", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("Cat_ID", Convert.ToInt32(search));

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        foreach (DataRow row in dt.Rows)
                        {
                            DateTime parseDate = Convert.ToDateTime(row["Release_Date"]);
                            ConnectionData movieData = new ConnectionData
                            {
                                Movie_ID = Convert.ToInt32(row["Movie_ID"]),
                                Movie_name = row["Movie_Name"].ToString(),
                                Release_Date = parseDate.ToString("yyyy-MM-dd"), // Fixed MM for months
                                Cat_ID = Convert.ToInt32(row["Cat_ID"]),
                                rate = Convert.ToInt32(row["Rate"])
                            };
                            Movie movie = new Movie();
                            movie.cd = movieData;
                            movies.Add(movie);
                        }
                    }
                }
            }
            return movies;
        }

        
        public List<Movie> fnDisplayBooking()
        {
            List<Movie> movies = new List<Movie>();
            using (SqlConnection conn = new SqlConnection(cd.Connection()))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("DisplayBooking", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        foreach (DataRow row in dt.Rows)
                        {
                            ConnectionData movieData = new ConnectionData
                            {
                                booking_ID = Convert.ToInt32(row["booking_ID"]),
                                User_ID = Convert.ToInt32(row["User_ID"]),
                                Movie_ID = Convert.ToInt32(row["Movie_ID"]),
                                Cat_ID = Convert.ToInt32(row["Cat_ID"]),
                                amount = Convert.ToInt32(row["amount"]),
                                no_of_Tickets = Convert.ToInt32(row["no_of_Tickets"]),
                                Cat_Type = row["Cat_Type"].ToString(),
                                Release_Date = row["date"].ToString(),
                                Movie_name = row["movie_name"].ToString(),
                                rate = Convert.ToInt32(row["rate"])
                            };
                            Movie movie = new Movie();
                            movie.cd = movieData;
                            movies.Add(movie);
                        }
                    }
                }
            }
            return movies;
        }
        public DataTable fnGetCategoryDropdown()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(cd.Connection()))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("DisplayCategory", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            return dt;
        }
        public bool fnDeleteBooking(int bookingId)
        {
            using (SqlConnection conn = new SqlConnection(cd.Connection()))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("DeleteBooking", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("booking_ID", bookingId);
                    int i = cmd.ExecuteNonQuery();
                    return i > 0;
                }
            }
        }
        public bool fnDeleteMovie(int movieId)
        {
            using (SqlConnection conn = new SqlConnection(cd.Connection()))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("DeleteMovie", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Movie_ID", movieId);
                    int i = cmd.ExecuteNonQuery();
                    return i > 0;
                }
            }
        }
        public bool fnGetMovieID(int movieId)
        {
            using (SqlConnection conn = new SqlConnection(cd.Connection()))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("GetMovieID", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Movie_ID", movieId);

                    // Use ExecuteScalar if checking for existence or fetching a scalar value
                    object result = cmd.ExecuteScalar();
                    return result != null && result != DBNull.Value;
                }
            }
        }
    }
}