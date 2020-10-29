using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum.Models.ApplicationUser
{
    public class ProfileListModel
    {
        public IEnumerable<ProfileModel> Profiles { get; set; }
    }
}