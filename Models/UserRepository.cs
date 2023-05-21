using Microsoft.Data.SqlClient;

namespace BlogApp.Models
{
    public class UserRepository
    {
        public static List<User> users = new List<User>();

        static string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=mydb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        static SqlConnection conn = new SqlConnection(connectionString);


        

        static public bool registerUser(User u)
        {
            string query = $"insert into Users(Username, Email, Password, picture) values('{u.Username}', '{u.Email}', '{u.Password}', 'UserIcon.jpg')";
            try
            {
                SqlCommand command = new SqlCommand(query, conn);
                conn.Open();
                int r = command.ExecuteNonQuery();
                if (r > 0)
                {
                    conn.Close();
                    return true;
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
            catch (Exception e)
            {
                conn.Close();
                return false;
            }
        }
        static public (bool, User) validUser(string email, string password)
        {
            string query = $"select * from Users where Email = '{email}' and Password = '{password}'";
            User u = new User();
            try
            {
                conn.Open();
                SqlCommand sqlCommand = new SqlCommand(query, conn);
                SqlDataReader dr = sqlCommand.ExecuteReader();

                if (dr.Read())
                {

                    u.Id = (int)dr.GetValue(0);
                    u.Username = (string)dr.GetValue(1);
                    u.Email = (string)dr.GetValue(2);
                    u.Password = (string)dr.GetValue(3);
                    u.pictureFileName = (string)dr.GetValue(4);
                    conn.Close();
                    return (true, u);
                }
                else
                {
                    conn.Close();
                    return (false, u);
                }
            }
            catch (Exception e)
            {
                conn.Close();
                return (false, u);
            }

        }

        static public User GetUser(string email)
        {
            string query = $"select * from Users where Email='{email}'";
            User u = new User();
            try
            {
                conn.Open();
                SqlCommand sqlCommand = new SqlCommand(query, conn);
                SqlDataReader dr = sqlCommand.ExecuteReader();

                if (dr.Read())
                {
                    u.Id = (int)dr.GetValue(0);
                    u.Username = (string)dr.GetValue(1);
                    u.Email = (string)dr.GetValue(2);
                    u.Password = (string)dr.GetValue(3);
                    u.pictureFileName = (string)dr.GetValue(4);
                    conn.Close();
                    return (u);
                }
                conn.Close();
                return (u);

            }
            catch (Exception e)
            {
                conn.Close();
                return (u);
            }
        }
        static public bool UpdateUser(User u, string newPassword, string filename)
        {
            string query = $"Update Users set Username = '{u.Username}' , Password = '{newPassword}', picture = '{filename}' where Email='{u.Email}'";
            try
            {
                SqlCommand command = new SqlCommand(query, conn);
                conn.Open();
                int r = command.ExecuteNonQuery();
                if (r > 0)
                {
                    conn.Close();
                    return true;
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
            catch (Exception e)
            {
                conn.Close();
                return false;
            }
        }

        static public bool UpdateUserByAdmin(User u)
        {
            string query = $"Update Users set Username = '{u.Username}' , Password = '{u.Password}' where Id='{u.Id}'";
            try
            {
                SqlCommand command = new SqlCommand(query, conn);
                conn.Open();
                int r = command.ExecuteNonQuery();
                if (r > 0)
                {
                    conn.Close();
                    return true;
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
            catch (Exception e)
            {
                conn.Close();
                return false;
            }
        }

        static public List<User> getUsersList()
        {
            string query = $"select * from Users";
            List<User> users = new List<User>();
            try
            {
                conn.Open();
                SqlCommand sqlCommand = new SqlCommand(query, conn);
                SqlDataReader dr = sqlCommand.ExecuteReader();

                while (dr.Read())
                {
                    User u = new User();
                    u.Id = (int)dr.GetValue(0);
                    u.Username = (string)dr.GetValue(1);
                    u.Email = (string)dr.GetValue(2);
                    u.Password = (string)dr.GetValue(3);
                    u.pictureFileName = (string)dr.GetValue(4);
                    users.Add(u);

                }
                conn.Close();
                return (users);

            }
            catch (Exception e)
            {
                conn.Close();
                return (users);
            }
        }

        static public bool DeleteUser(int id)
        {
            string query = $"Delete from Users where Id='{id}'";
            try
            {
                SqlCommand command = new SqlCommand(query, conn);
                conn.Open();
                int r = command.ExecuteNonQuery();
                if (r > 0)
                {
                    conn.Close();
                    return true;
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
            catch (Exception e)
            {
                conn.Close();
                return false;
            }

        }












    }
}
