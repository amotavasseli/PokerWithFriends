using PokerWithFriends.Service.Domains;
using PokerWithFriends.Service.Requests;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerWithFriends.Service.Services
{
    public class UsersService : IUsersService
    {
        string connectionString = ConfigurationManager.ConnectionStrings["PokerConnection"].ConnectionString;
        
        public List<User> GetAll()
        {
            using(SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                ConnectionWrapper wrap = new ConnectionWrapper();
                SqlCommand cmd = wrap.SqlWrapper("Users_getall", con);
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    List<User> users = new List<User>();
                    while (reader.Read())
                    {
                        User user = new User();
                        user.Id = reader.GetInt32(0);
                        user.FirstName = reader.GetString(1);
                        user.LastName = reader.GetString(2);
                        user.Email = reader.GetString(3);
                        user.Password = reader.GetString(4);
                        user.Username = reader.GetString(5);
                        user.DateCreated = reader.GetDateTime(6);
                        user.DateModified = reader.GetDateTime(7);
                        users.Add(user);
                    }
                    return users;
                }
            }
        }

        public User GetUserById(int id)
        {
            using(SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "Users_getbyid";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);

                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    User user = new User();

                    reader.Read();
                    user.Id = reader.GetInt32(0);
                    user.FirstName = reader.GetString(1);
                    user.LastName = reader.GetString(2);
                    user.Email = reader.GetString(3);
                    user.Username = reader.GetString(4);
                    user.Password = reader.GetString(5);
                    user.DateCreated = reader.GetDateTime(6);
                    user.DateModified = reader.GetDateTime(7);

                    return user;
                }
            }
        }

        public int Create(UserRequest req)
        {
            using(SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "Users_insert";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@first_name", req.FirstName);
                cmd.Parameters.AddWithValue("@last_name", req.LastName);
                cmd.Parameters.AddWithValue("@email", req.Email);
                cmd.Parameters.AddWithValue("@username", req.Username);
                cmd.Parameters.AddWithValue("@password", req.Password);

                cmd.Parameters.Add("@id", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();

                return (int)cmd.Parameters["@id"].Value;
            }
        }

        public void Update(UserUpdateRequest req)
        {
            using(SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "Users_update";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id", req.Id);
                cmd.Parameters.AddWithValue("@first_name", req.FirstName);
                cmd.Parameters.AddWithValue("@last_name", req.LastName);
                cmd.Parameters.AddWithValue("@email", req.Email);
                cmd.Parameters.AddWithValue("@password", req.Password);
                cmd.Parameters.AddWithValue("@username", req.Username);

                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "Users_delete";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
            }
        }

        public User Login(LoginRequest login)
        {
            using(SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "Users_getbylogin";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@email", login.Email);
                cmd.Parameters.AddWithValue("@password", login.Password);

                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    User user = new User();
                    user.Id = reader.GetInt32(0);
                    user.FirstName = reader.GetString(1);
                    user.LastName = reader.GetString(2);
                    user.Username = reader.GetString(3);
                    user.DateCreated = reader.GetDateTime(4);
                    user.DateModified = reader.GetDateTime(5);
                    return user;
                }
            }
        }

        public List<ChallengerMatches> GetAllMatches(int id)
        {
            using(SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "Users_Matches_getresultbyuserid";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);

                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    List<ChallengerMatches> matches = null;
                    while (reader.Read())
                    {
                        ChallengerMatches match = new ChallengerMatches();
                        match.UserId = reader.GetInt32(0);
                        match.Email = reader.GetString(1);
                        match.MatchId = reader.GetInt32(2);
                        match.MatchGuid = reader.GetGuid(3);
                        match.MatchStartTime = reader["m.match_start_time"] is DBNull ? (DateTime?)null : (DateTime?)reader["m.match_start_time"];
                        match.Winner = reader.GetInt32(5);
                        match.Opponents = reader["m.opponents"] is DBNull ? (int?[])null : (int?[])reader["m.opponents"];
                        match.DateCreated = reader.GetDateTime(7);
                        match.DateModified = reader.GetDateTime(8);

                        if (matches == null)
                            matches = new List<ChallengerMatches>();
                        matches.Add(match);
                    }
                    return matches;
                }
            }
        }
    }
}
