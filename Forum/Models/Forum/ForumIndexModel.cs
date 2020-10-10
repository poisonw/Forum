using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum.Models.Forum
{
    public class ForumIndexModel
    {
        public IEnumerable<ForumListingModel> ForumList { get; set; }
    }
}