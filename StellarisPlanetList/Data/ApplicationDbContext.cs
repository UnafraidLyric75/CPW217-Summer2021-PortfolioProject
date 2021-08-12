using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StellarisPlanetList.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StellarisPlanetList.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<PlanetViewModel> Planets { get; set; }
        public DbSet<UserAccounts> Users { get; set; }
    }
}
