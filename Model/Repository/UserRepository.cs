using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS_TEMA3.Model.Repository
{
    public class UserRepository

    {
        private Repository repository;        

        public UserRepository()
        {
            repository = Repository.Instance;         
        }

        //Utility methods
        private static User RowToUser(DataRow row)
        {
            User user = new User();
            user.Id = Convert.ToInt32(row["id"]);
            user.Name = row["name"].ToString();
            user.Email = row["email"].ToString();
            user.Password = row["password"].ToString();
            user.UserType = (UserType)Enum.Parse(typeof(UserType), row["user_type"].ToString(), true);
            user.Phone = row["phone"].ToString();
            return user;
        }


        //CRUD methods
        public bool CreateUser(User user)
        {
            // Perform an SQL query to check if the user exists (avoids pulling the object)
            string existQuery = $"SELECT * FROM userAccount WHERE email = '{user.Email}'";
            DataTable dataTable = repository.ExecuteQuery(existQuery);
            if (dataTable.Rows.Count > 0)
            {
                return false; // User already exists
            }

            // Constructing SQL statement
            string nonQuery = $"INSERT INTO userAccount (name, email, password, user_type, phone) VALUES ('" +
                              $"{user.Name}', '" +
                              $"{user.Email}', '" +
                              $"{user.Password}', '" +
                              $"{user.UserType}', '" +
                              $"{user.Phone}')";
            return repository.ExecuteNonQuery(nonQuery);
        }

        public User? ReadUserById(int id)
        {
            // Constructing SQL statement
            string query = $"SELECT * FROM userAccount WHERE id = {id}";
            DataTable userTable = repository.ExecuteQuery(query);
            if (userTable.Rows.Count == 0)
            {
                return null; // No user with that id
            }
            // Convert DataTable to User
            return RowToUser(userTable.Rows[0]);
        }

        public List<User>? ReadUsers()
        {
            // Constructing SQL statement
            string query = "SELECT * FROM userAccount";
            DataTable dataTable = repository.ExecuteQuery(query);
            if (dataTable.Rows.Count == 0)
            {
                return null;
            }
            // Convert DataTable to List<User>
            List<User> users = new List<User>();
            foreach (DataRow row in dataTable.Rows)
            {
                users.Add(RowToUser(row));
            }
            return users;
        }

        public bool UpdateUser(User user)
        {
            // Constructing SQL statement
            string nonQuery = $"UPDATE userAccount SET " +
                              $"name = '{user.Name}', " +
                              $"email = '{user.Email}', " +
                              $"password = '{user.Password}', " +
                              $"user_type = '{user.UserType}', " +
                              $"phone = '{user.Phone}' " +
                              $"WHERE id = {user.Id}";
            return repository.ExecuteNonQuery(nonQuery);
        }

        public bool DeleteUser(int id)
        {
            // Constructing SQL statement
            string nonQuery = $"DELETE FROM userAccount WHERE id = {id}";
            return repository.ExecuteNonQuery(nonQuery);
        }


        //Filter methods        
        public List<User>? ReadUsersByUserType(UserType userType)
        {
            // Constructing SQL statement
            string type = userType.ToString();
            string query = $"SELECT * FROM userAccount WHERE user_type = '{type}'";
            DataTable userTable = repository.ExecuteQuery(query);
            if (userTable.Rows.Count == 0)
            {
                return null; // No users with that type
            }
            // Convert DataTable to List<User>
            List<User> users = new List<User>();
            foreach (DataRow row in userTable.Rows)
            {
                users.Add(RowToUser(row));
            }
            return users;
        }

        public User? ReadUserByEmailAndPassword(string email, string password)
        {
            // Constructing SQL statement
            string query = $"SELECT * FROM userAccount WHERE email = '{email}' AND password = '{password}'";
            DataTable userTable = repository.ExecuteQuery(query);
            if (userTable.Rows.Count == 0)
            {
                return null; // No user with that email and password
            }
            // Convert DataTable to User
            return RowToUser(userTable.Rows[0]);
        }

    }
}
