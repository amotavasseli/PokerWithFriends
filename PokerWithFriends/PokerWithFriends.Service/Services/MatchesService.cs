using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    public class MatchesService : IMatchesService
    {
        string connectionString = ConfigurationManager.ConnectionStrings["PokerConnection"].ConnectionString;

        public List<Match> GetAllMatches()
        {
            using(SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "Matches_getall";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    List<Match> matches = null;
                    while (reader.Read())
                    {
                        Match match = new Match();
                        match.Id = reader.GetInt32(0);
                        match.MatchGuid = reader.GetGuid(1);
                        match.ChallengerId = reader.GetInt32(2);
                        match.MatchStartTime = reader["match_start_time"] is DBNull ? (DateTime?)null : (DateTime?)reader["match_start_time"];
                        match.Winner = reader["winner"] is DBNull ? (int?)null : (int?)reader["winner"];
                        string opponents = reader.GetString(5);
                        if(opponents != null)
                        {
                            match.Opponents =
                                JArray.Parse(opponents)
                                .Select(item => item.Value<int?>("opponents"))
                                .ToArray();
                        }
                        match.DateCreated = reader.GetDateTime(6);
                        match.DateModified = reader.GetDateTime(7);

                        if (matches == null)
                            matches = new List<Match>(); 

                        matches.Add(match);
                    }
                    return matches;
                }
            }
        }

        public Match GetMatchById(int id)
        {
            using(SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "Matches_getbyid";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);

                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    Match match = new Match();
                    match.Id = reader.GetInt32(0);
                    match.MatchGuid = reader.GetGuid(1);
                    match.ChallengerId = reader.GetInt32(2);
                    match.MatchStartTime = reader["match_start_time"] is DBNull ? (DateTime?)null : (DateTime?)reader["match_start_time"];
                    match.Winner = reader["winner"] is DBNull ? (int?)null : (int?)reader["winner"];
                    string opponents = reader.GetString(5);
                    if(opponents != null)
                    {
                        match.Opponents =
                            JArray.Parse(opponents)
                            .Select(item => item.Value<int?>("opponents"))
                            .ToArray();
                    }
                    match.DateCreated = reader.GetDateTime(6);
                    match.DateModified = reader.GetDateTime(7);
                    return match;
                }
            }
        }

        public int CreateMatch(MatchRequest req)
        {
            using(SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "Matches_insert";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@challenger_id", req.ChallengerId);
                cmd.Parameters.AddWithValue("@match_start_time", req.MatchStartTime);
                cmd.Parameters.AddWithValue("@winner", req.Winner);
                cmd.Parameters.AddWithValue("@opponents", req.Opponents);
                cmd.Parameters.Add("@id", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                return (int)cmd.Parameters["@id"].Value;
            }
        }
        
        public void UpdateMatch(MatchUpdateRequest req)
        {
            using(SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "Matches_update";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id", req.Id);
                cmd.Parameters.AddWithValue("@challenger_id", req.ChallengerId);
                cmd.Parameters.AddWithValue("@match_start_time", req.MatchStartTime);
                cmd.Parameters.AddWithValue("@winner", req.Winner);
                cmd.Parameters.AddWithValue("@opponents", req.Opponents);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteMatch(int id)
        {
            using(SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "Matches_delete";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
