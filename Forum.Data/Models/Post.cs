﻿
using System;
using System.Collections.Generic;

namespace Forum.Data.Models
{
   public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual MyForum MyForum { get; set; }
        public virtual ICollection<PostReply> Replies { get; set; }
    }
}
