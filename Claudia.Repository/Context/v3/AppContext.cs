using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Claudia.Domain.Models;
using Claudia.Domain.Models.v3;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;

namespace Claudia.Repository.Context.v3
{
    public class AppContext : IdentityDbContext<User>
    {
        public AppContext()
            : base("V3Connection")//, throwIfV1Schema: false)
        {
            ((IObjectContextAdapter)this).ObjectContext.SavingChanges += ObjectContext_SavingChanges;
        }

        static AppContext()
        {
            System.Data.Entity.Database.SetInitializer<AppContext>(null);
        }

        public virtual IDbSet<EntityCategory> EntityCategories { get; set; }
        public virtual IDbSet<EntityList> EntityList { get; set; }
        public virtual IDbSet<AskClaudia> AskClaudia { get; set; }
        public virtual IDbSet<Comment> Comments { get; set; }
        public virtual IDbSet<Link> Links { get; set; }
        public virtual IDbSet<Rating> Ratings { get; set; }
        public virtual IDbSet<Recipe> Recipes { get; set; }
        public virtual IDbSet<RecipeCategory> RecipeCategory { get; set; }
        public virtual IDbSet<Widget> Widgets { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EntityCategory>()
                .HasKey(k => k.Id);

            modelBuilder.Entity<EntityList>()
                .HasKey(k => k.Id)
                .HasRequired(r => r.Category).WithMany(m => m.EntityList).HasForeignKey(k => k.CategoryId);

            modelBuilder.Entity<AskClaudia>().HasKey(k => k.Id);
            modelBuilder.Entity<Comment>().HasKey(k => k.Id);
            modelBuilder.Entity<Link>().HasKey(k => k.Id);
            modelBuilder.Entity<Rating>().HasKey(k => k.Id);
            modelBuilder.Entity<Widget>().HasKey(k => k.Id);

            modelBuilder.Entity<Recipe>().HasKey(k => k.Id)
                .HasRequired(r => r.RecipeCategory).WithMany(m => m.Recipes).HasForeignKey(k => k.RecipeCategoryId);
    
            modelBuilder.Entity<RecipeCategory>().HasKey(k => k.Id);

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

    }
}
