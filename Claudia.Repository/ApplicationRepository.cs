using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Claudia.Domain.Models;
using Claudia.Domain.Models.v1;
using Claudia.Repository.Context;

namespace Claudia.Repository
{
    public class CategoryRepository : BaseRepository<ObjectCategory>
    {
        public CategoryRepository(System.Data.Entity.DbContext context)
            : base(context)
        {
        }
    }

    public class AnnouncementRepository : BaseRepository<Announcement>
    {
        public AnnouncementRepository(System.Data.Entity.DbContext context) : base(context)
        {
        }
    }

    public class AskClaudiaRepository : BaseRepository<AskClaudia>
    {
        public AskClaudiaRepository(System.Data.Entity.DbContext context) : base(context)
        {
        }
    }

    public class CommentRepository : BaseRepository<AskClaudia>
    {
        public CommentRepository(System.Data.Entity.DbContext context)
            : base(context)
        {
        }
    }

    public class LinkRepository : BaseRepository<Link>
    {
        public LinkRepository(System.Data.Entity.DbContext context) : base(context)
        {
        }
    }

    public class RatingRepositroy : BaseRepository<Rating>
    {
        public RatingRepositroy(System.Data.Entity.DbContext context) : base(context)
        {
        }
    }

    public class RecipeRepository : BaseRepository<Recipe>
    {
        public RecipeRepository(System.Data.Entity.DbContext context) : base(context)
        {
        }
    }

    public class RecipeCategoryRepository : BaseRepository<RecipeCategory>
    {
        public RecipeCategoryRepository(System.Data.Entity.DbContext context) : base(context)
        {
        }
    }
    public class LinksUrlsRepository : BaseRepository<LinksUrl>
    {
        public LinksUrlsRepository(System.Data.Entity.DbContext context)
            : base(context)
        {
        }
    }
}
