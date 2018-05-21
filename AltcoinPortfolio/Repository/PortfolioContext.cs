using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using AltcoinPortfolio.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AltcoinPortfolio.Repository
{
    public class PortfolioContext : IdentityDbContext<User>
    {
        public PortfolioContext() : base("PortfolioContext")
        {
        }
        
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<Coin> Coins { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            // the all important base class call! Add this line to make your problems go away.

            Database.SetInitializer<PortfolioContext>(null);
            base.OnModelCreating(modelBuilder);
        }
    }
}