using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Breeze.ContextProvider.EF6;
using Claudia.Domain.Models;
using Claudia.Domain.Models.v1;
using Claudia.Repository.Context;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Claudia.Repository
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private UserManager<User> _userManager;
        private ApplicationContext _applicationContext;
        private CategoryRepository _category;
        private AnnouncementRepository _announcement;
        private AskClaudiaRepository _askClaudia;
        private CommentRepository _comment;
        private LinkRepository _link;
        private LinksUrlsRepository _linkUrl;
        private RatingRepositroy _rating;
        private RecipeRepository _recipe;
        private RecipeCategoryRepository _recipeCategory;
        private BreezeRepository _breezeRepository;

        public UserManager<User> UserManager
        {
            get {
                return _userManager ??
                       (_userManager = new UserManager<User>(new UserStore<User>(new ApplicationContext())));
            }
        }

        public ApplicationContext ApplicationContext
        {
            get { return _applicationContext ?? (_applicationContext = new ApplicationContext()); }
        }

        public CategoryRepository Category
        {
            get {
                if (_category == null)
                {
                    return _category = new CategoryRepository(ApplicationContext);
                }

                return _category;
            }
        }

        public AnnouncementRepository Announcement
        {
            get
            {
                if (_announcement == null)
                {
                    return _announcement = new AnnouncementRepository(ApplicationContext);
                }

                return _announcement;
            }
        }

        public AskClaudiaRepository AskClaudia
        {
            get
            {
                if (_askClaudia == null)
                {
                    return _askClaudia = new AskClaudiaRepository(ApplicationContext);
                }

                return _askClaudia;
            }
        }

        public CommentRepository Comment
        {
            get
            {
                if (_comment == null)
                {
                    return _comment = new CommentRepository(ApplicationContext);
                }

                return _comment;
            }
        }

        public LinkRepository Link
        {
            get
            {
                if (_link==null)
                {
                    return _link = new LinkRepository(ApplicationContext);
                }

                return _link;
            }
        }

        public LinksUrlsRepository LinkUrls
        {
            get
            {
                if (_linkUrl == null)
                {
                    return _linkUrl = new LinksUrlsRepository(ApplicationContext);
                }

                return _linkUrl;
            }
        }

        public RatingRepositroy Rating
        {
            get
            {
                if (_rating==null)
                {
                    return _rating = new RatingRepositroy(ApplicationContext);
                }

                return _rating;
            }
        }

        public RecipeRepository Recipe
        {
            get
            {
                if (_recipe == null)
                {
                    return _recipe = new RecipeRepository(ApplicationContext);
                }

                return _recipe;
            }
        }

        public RecipeCategoryRepository RecipeCategory
        {
            get
            {
                if (_recipeCategory==null)
                {
                    return _recipeCategory = new RecipeCategoryRepository(ApplicationContext);
                }

                return _recipeCategory;
            }
        }

        public BreezeRepository BreezeRepository
        {
            get
            {
                if (_breezeRepository == null)
                {
                    return _breezeRepository = new BreezeRepository();
                }

                return _breezeRepository;
            }
        }

        public int SaveChanges()
        {
            try
            {
                return _applicationContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_applicationContext != null) { _applicationContext.Dispose(); }
                if (_userManager != null) { _userManager.Dispose(); }
                if (_category != null) { _category.Dbcontext.Dispose();}
                if (_announcement != null) { _announcement.Dbcontext.Dispose();}
                if (_askClaudia != null) { _askClaudia.Dbcontext.Dispose();}
                if (_link != null) { _link.Dbcontext.Dispose();}
                if (_rating != null) { _rating.Dbcontext.Dispose();}
                if (_recipe != null) { _recipe.Dbcontext.Dispose();}
                if (_recipeCategory != null) { _recipeCategory.Dbcontext.Dispose();}
                if (_breezeRepository != null) { _breezeRepository.Context.Dispose(); }
            }
        }
    }
}
