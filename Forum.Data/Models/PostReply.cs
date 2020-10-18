
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Data.Models
{
    public class PostReply
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
        public virtual Post Post { get; set; }
    }
}
