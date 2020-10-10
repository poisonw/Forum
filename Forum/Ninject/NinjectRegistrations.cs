using Forum.Data.Models;
using Forum.Domain.Abstract;
using Forum.Domain.Concrete;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;

namespace Forum.Ninject
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<IMyForum>().To<MyForumService>();
            Bind<IPost>().To<PostService>();
            Bind<IUserStore<IUser>>();
            Bind<IUserStore<ApplicationUser>>().To<UserStore<ApplicationUser>>();
            Bind<UserManager<ApplicationUser>>().ToSelf();
            //Unbind<ModelValidatorProvider>();
        }
    }
}