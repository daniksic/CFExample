using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Claudia.Domain.Models.v3;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Claudia.Repository.Context
{
    //public class DbContext : System.Data.Entity.DbContext//IdentityDbContext<User>
    //{
    //    public DbContext()
    //        : base("DefaultConnection")//, throwIfV1Schema: false)
    //    {
    //    }

    //    static DbContext()
    //    {
    //        System.Data.Entity.Database.SetInitializer<DbContext>(null);
    //    }

    //    public IDbSet<Link> Links { get; set; }

    //    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    //    {
    //        modelBuilder.Entity<Link>().ToTable("Links");

    //        base.OnModelCreating(modelBuilder);
    //    }
    //}
}
