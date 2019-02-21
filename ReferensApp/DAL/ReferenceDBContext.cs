using Microsoft.AspNet.Identity.EntityFramework;
using ReferensApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ReferensApp.DAL
{
    public class ReferenceDBContext : IdentityDbContext<AppUser>
    {

        public ReferenceDBContext() : base("BookingConnection")
        {
        }
        public DbSet<Meeting> Meetings { get; set; }

    }
}