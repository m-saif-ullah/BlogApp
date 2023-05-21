using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models
{
    public class Post
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "* Required Field")]
        public string Title { get; set; }
        [Required(ErrorMessage = "* Required Field")]
        public string Content { get; set; }
        public int UserId { get; set; }
        public string UserEmail { get; set; }
        public string UserProfilePicture { get; set; }
        public string Date { get; set; }

    }
}
