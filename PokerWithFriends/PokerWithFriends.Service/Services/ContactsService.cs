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
    public class ContactsService : IContactsService
    {
        string connectionString = ConfigurationManager.ConnectionStrings["PokerConnection"].ConnectionString;

        public List<Contact> GetAll()
        {
            using(SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "Contacts_getall";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    List<Contact> contacts = null;
                    while (reader.Read())
                    {
                        Contact contact = new Contact();
                        contact.Id = reader.GetInt32(0);
                        contact.Name = reader.GetString(1);
                        contact.Email = reader.GetString(2);
                        contact.Subject = reader.GetString(3);
                        contact.Message = reader.GetString(4);
                        contact.DateCreated = reader.GetDateTime(5);
                        contact.DateModified = reader.GetDateTime(6);
                        if(contact == null)
                        {
                            contacts = new List<Contact>();
                        }
                        contacts.Add(contact);
                    }
                    return contacts;
                }
            }
        }

        public Contact GetById(int id)
        {
            using(SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "Contacts_getbyid";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    Contact contact = new Contact();

                    contact.Id = reader.GetInt32(0);
                    contact.Name = reader.GetString(1);
                    contact.Email = reader.GetString(2);
                    contact.Subject = reader.GetString(3);
                    contact.Message = reader.GetString(4);
                    contact.DateCreated = reader.GetDateTime(5);
                    contact.DateModified = reader.GetDateTime(6);

                    return contact;
                }
            }
        }

        public int Create(ContactRequest req)
        {
            using(SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "Contacts_insert";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", req.Name);
                cmd.Parameters.AddWithValue("@email", req.Email);
                cmd.Parameters.AddWithValue("@subject", req.Subject);
                cmd.Parameters.AddWithValue("@message", req.Message);
                cmd.Parameters.Add("@id", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();

                return (int)cmd.Parameters["@id"].Value;
            }
        }

        public void Update(ContactUpdateRequest req)
        {
            using(SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "Contacts_update";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id", req.Id);
                cmd.Parameters.AddWithValue("@name", req.Name);
                cmd.Parameters.AddWithValue("@email", req.Email);
                cmd.Parameters.AddWithValue("@subject", req.Subject);
                cmd.Parameters.AddWithValue("@message", req.Message);

                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using(SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "Contacts_delete";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
