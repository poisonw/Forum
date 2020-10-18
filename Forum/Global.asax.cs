using Forum.Data.Context;
using Forum.Data.Models;
using Forum.Domain.Abstract;
using Forum.Domain.Concrete;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Common;
using Ninject.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;


namespace Forum
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


           // NinjectModule registrations = new NinjectRegistrations();
            var kernel = new StandardKernel();
           var ninjectResolver = new NinjectDependencyResolver(kernel);
           DependencyResolver.SetResolver(ninjectResolver);
            kernel.Bind<IPost>().To<PostService>();
            kernel.Bind<IMyForum>().To<MyForumService>();
            
           kernel.Bind<ApplicationDbContext>().ToSelf().InSingletonScope();
           //  kernel.Bind<IUserStore<ApplicationUser>>().To<UserStore<ApplicationUser>>();
           kernel.Bind<IUserStore<ApplicationUser>>().To<UserStore<ApplicationUser>>().WithConstructorArgument("context", kernel.Get<ApplicationDbContext>());
           kernel.Bind<UserManager<ApplicationUser>>().ToSelf();
            //DependencyResolver.SetResolver(new Ninject.NinjectDependencyResolver(kernel));
            //DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
           kernel.Unbind<ModelValidatorProvider>();
        }
    }
}
