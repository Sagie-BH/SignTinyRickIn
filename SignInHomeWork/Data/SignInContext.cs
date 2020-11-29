using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SignInHomeWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignInHomeWork.Data
{
    public class SignInContext : IdentityDbContext
    {
        public SignInContext(DbContextOptions<SignInContext> opt) : base(opt) { }
        DbSet<TinyRick> TinyRick { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<TinyRick>().HasData(
                new TinyRick
                {
                    Email = "Tiny@Rick.Com",
                    PasswordHash = "tinyrick"
                });
        }
    }
}
