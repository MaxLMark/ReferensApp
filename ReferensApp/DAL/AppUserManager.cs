using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using ReferensApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace ReferensApp.DAL
{
    public class AppUserManager : UserManager<AppUser>
    {
        public AppUserManager(IUserStore<AppUser> store)
            : base(store)
        {
        }

        // this method is called by Owin therefore best place to configure your User Manager
        public static AppUserManager Create(
            IdentityFactoryOptions<AppUserManager> options, IOwinContext context)
        {
            var manager = new AppUserManager(
                new UserStore<AppUser>(context.Get<ReferenceDBContext>()));

            // optionally configure your manager
            // ...

            return manager;
        }
    }
}