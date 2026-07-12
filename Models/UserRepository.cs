using Microsoft.Data.SqlClient;
using System.Data;

namespace ProductsAPI.Models
{
    public class UserRepository
    {
        string connectionString;
        public UserRepository(IConfiguration config)
        {
            connectionString = config.GetConnectionString("DB_CONNECTION_STRING");
        }

        public List<User> GetUsers()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter("sp_get_user", connection);
                dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                List<User> users = new List<User>();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    users.Add(new User()
                    {
                        //Id = Convert.ToString(dataRow["id"]),
                        Username = Convert.ToString(dataRow["username"]),
                        Email = Convert.ToString(dataRow["email"]),
                        Phone = Convert.ToString(dataRow["phone"]),
                        Gender = Convert.ToString(dataRow["gender"]),
                        Role = Convert.ToString(dataRow["role"]),
                    });
                }

                return users;
            }
        }


        public void AddUser(User user)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand command = new SqlCommand("sp_insert_user", connection);
            command.CommandType = CommandType.StoredProcedure;
            
            command.Parameters.AddWithValue("@username", user.Username);
            command.Parameters.AddWithValue("@email", user.Email);
            command.Parameters.AddWithValue("@pwd", user.Password);
            command.Parameters.AddWithValue("@phone", user.Phone);
            command.Parameters.AddWithValue("@gender", user.Gender);
            command.Parameters.AddWithValue("@age", user.Age);
            command.Parameters.AddWithValue("@role", user.Role);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void UpdateUser(User user)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand command = new SqlCommand("sp_update_user", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@id", user.Id);
            command.Parameters.AddWithValue("@username", user.Username);
            command.Parameters.AddWithValue("@email", user.Email);
            command.Parameters.AddWithValue("@pwd", user.Password);
            command.Parameters.AddWithValue("@phone", user.Phone);
            command.Parameters.AddWithValue("@gender", user.Gender);
            command.Parameters.AddWithValue("@age", user.Age);
            command.Parameters.AddWithValue("@role", user.Role);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void DeleteUser(string id)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand command = new SqlCommand("sp_delete_user", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@id", id);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}
