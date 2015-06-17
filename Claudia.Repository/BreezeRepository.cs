using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Breeze.ContextProvider;
using Breeze.ContextProvider.EF6;
using Claudia.Domain.Models;
using Claudia.Domain.Models.v1;
using Claudia.Repository.Context;
using Newtonsoft.Json.Linq;

namespace Claudia.Repository
{
    public class BreezeRepository : EFContextProvider<ApplicationContext>
    {
        //private readonly EFContextProvider<ApplicationContext> _breezeProvider = new EFContextProvider<ApplicationContext>();

        
        public BreezeRepository() :base()
        {
        }
        //private ApplicationContext Context { get { return _breezeProvider.Context; } }
        
        //public string Metadata{ get { return _breezeProvider.Metadata(); } }

        public SaveResult SaveChanges(JObject saveBundle)
        {
            return base.SaveChanges(saveBundle);
        }

        public string Metadata { get { return base.Metadata(); } }
        public IQueryable<AskClaudia> AskClaudia { get { return Context.AskClaudia; } }

        public IQueryable<Announcement> Announcements { get { return Context.Announcements; } }

        public IQueryable<Comment> Comments { get { return Context.Comments; } }

        public IQueryable<Link> Links { get { return Context.Links; } }

        public IQueryable<ObjectCategory> Categories { get { return Context.Categories; } }

        public IQueryable<Rating> Ratings { get { return Context.Ratings; } }

        public IQueryable<Recipe> Recipes { get { return Context.Recipes; } }

        public IQueryable<RecipeCategory> RecipeCategories { get { return Context.RecipeCategory; } }

        public IQueryable<User> Users { get { return Context.Users; } }

        protected override void AfterSaveEntities(Dictionary<Type, List<EntityInfo>> saveMap, List<KeyMapping> keyMappings)
        {

            var links = saveMap[typeof(Link)];
            if (links != null)
            {
                foreach (var link in links)
                {
                    if (link.EntityState != EntityState.Unchanged)
                    {
                        //dooooo something
                        Claudia.Domain.EventManager.EventManager.Instance.Publish("breeze.savedlinktodb");
                    }
                }
            }

            base.AfterSaveEntities(saveMap, keyMappings);
        }
    }
}
