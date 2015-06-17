using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Claudia.Domain.Models;
using Claudia.Domain.Models.v1;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;

namespace Claudia.Repository.Context
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public ApplicationContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            ((IObjectContextAdapter)this).ObjectContext.SavingChanges+=ObjectContext_SavingChanges;
        }


        static ApplicationContext()
        {
            System.Data.Entity.Database.SetInitializer<ApplicationContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Link>().HasKey(k => k.Id);

            //one-to-many 
            modelBuilder.Entity<LinksUrl>().HasKey(k => k.Id)
                .HasRequired<Link>(r => r.Link).WithMany(m => m.LinksUrls)
                .HasForeignKey(k => k.LinkId);

            base.OnModelCreating(modelBuilder);
        }
        private void ObjectContext_SavingChanges(object sender, EventArgs e)
        {
            ObjectContext context = sender as ObjectContext;
            if (context != null)
            {
                // You can use other EntityState constants here
                foreach (ObjectStateEntry entry in context.ObjectStateManager.GetObjectStateEntries(EntityState.Added | EntityState.Modified))
                {
                    // You can handle multiple Entity Types here.
                    if (entry.Entity is IEntity)
                    {
                        IEntity entity = entry.Entity as IEntity;
                        // Do whatever you want with your entities.
                        // You can throw an exception here if you want to
                        // prevent SaveChanges().
                        entity.DateTimeStamp = DateTime.Now;
                    }
                }
            }
        }

        

        public virtual IDbSet<ObjectCategory> Categories { get; set; }
        public virtual IDbSet<Announcement> Announcements { get; set; }
        public virtual IDbSet<AskClaudia> AskClaudia { get; set; }
        public virtual IDbSet<Comment> Comments { get; set; }
        public virtual IDbSet<Link> Links { get; set; }
        public virtual IDbSet<Rating> Ratings { get; set; }
        public virtual IDbSet<Recipe> Recipes { get; set; }
        public virtual IDbSet<RecipeCategory> RecipeCategory { get; set; }
        public virtual IDbSet<Widget> Widgets { get; set; }
        public virtual IDbSet<LinksUrl> LinksUrls { get; set; }

    }
}
