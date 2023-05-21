using Microsoft.Data.SqlClient;

namespace BlogApp.Models
{
    public class PostRepository
    {
        public static List<Post> posts = new List<Post>();

        static string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=mydb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        static SqlConnection conn = new SqlConnection(connectionString);

        static public bool addPost(Post p)
        {
            string query = $"insert into Posts(Title, Content, UserId, Email, Date, UserProfilePicture) values('{p.Title}', '{p.Content}', '{p.UserId}','{p.UserEmail}','{System.DateTime.Now.ToString("MMMM dd, yyyy")}', '{p.UserProfilePicture}')";
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
        static public List<Post> GetPosts()
        {
            string query = $"select * from Posts";
            List<Post> posts = new List<Post>();
            try
            {
                conn.Open();
                SqlCommand sqlCommand = new SqlCommand(query, conn);
                SqlDataReader dr = sqlCommand.ExecuteReader();

                while (dr.Read())
                {
                    Post p = new Post();
                    p.Id = (int)dr.GetValue(0);
                    p.Title = (string)dr.GetValue(1);
                    p.Content = (string)dr.GetValue(2);
                    p.UserId = (int)dr.GetValue(3);
                    p.UserEmail = (string)dr.GetValue(4);
                    p.Date = (string)dr.GetValue(5);
                    p.UserProfilePicture = (string)dr.GetValue(6);
                    posts.Add(p);

                }
                conn.Close();
                return (posts);

            }
            catch (Exception e)
            {
                conn.Close();
                return (posts);
            }
        }
        static public bool UpdatePost(Post p)
        {
            string query = $"Update Posts set Title = '{p.Title}' , Content = '{p.Content}' where Id='{p.Id}'";
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
        static public bool DeletePost(int id)
        {
            string query = $"Delete from Posts where Id='{id}'";
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












