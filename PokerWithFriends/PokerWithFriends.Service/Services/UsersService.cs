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
using BCr = BCrypt.Net;

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
                string hashPassword = BCr.BCrypt.HashPassword(req.Password);
                cmd.Parameters.AddWithValue("@password", hashPassword);
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

                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    User user = new User();
                    user.Id = reader.GetInt32(0);
                    user.FirstName = reader.GetString(1);
                    user.LastName = reader.GetString(2);
                    user.Username = reader.GetString(3);
                    string passwordHash = reader.GetString(4);
                    user.DateCreated = reader.GetDateTime(5);
                    user.DateModified = reader.GetDateTime(6);

                    if (!BCr.BCrypt.Verify(login.Password, passwordHash))
                        throw new InvalidLoginException(("Incorrect Password"));
                    if (user == null)
                        throw new InvalidLoginException("Incorrect Email");

                    return user;
                }
            }
        }

        public List<ChallengerMatches> GetChallengerMatches(int id)
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
                        match.Username = reader.GetString(2);
                        match.MatchId = reader.GetInt32(3);
                        match.MatchGuid = reader.GetGuid(4);
                        var startTime = reader["match_start_time"] is DBNull ? (DateTime?)null : (DateTime?)reader["match_start_time"];
                        match.MatchStartTime = startTime;
                        match.Winner = reader["winner"] is DBNull ? (int?)null : (int?)reader["winner"];
                        match.Opponents = reader["opponents"] is DBNull ? (int?[])null : (int?[])reader["opponents"];
                        match.DateCreated = reader.GetDateTime(8);
                        match.DateModified = reader.GetDateTime(9);

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
