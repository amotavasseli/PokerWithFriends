﻿using PokerWithFriends.Service.Domains;
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

                int newId = (int)cmd.Parameters["@id"].Value;

                return newId;
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
    }
}
