using Claudia.Domain.Models.v1;
using Claudia.Repository.Context;
using Microsoft.AspNet.Identity;

namespace Claudia.Repository
{
    public interface IUnitOfWork
    {
        UserManager<User> UserManager { get; }
        ApplicationContext ApplicationContext { get; }
        CategoryRepository Category { get; }
        AnnouncementRepository Announcement { get; }
        AskClaudiaRepository AskClaudia { get; }
        CommentRepository Comment { get; }
        LinkRepository Link { get; }
        LinksUrlsRepository LinkUrls { get; }
        RatingRepositroy Rating { get; }
        RecipeRepository Recipe { get; }
        RecipeCategoryRepository RecipeCategory { get; }
        BreezeRepository BreezeRepository { get; }
        int SaveChanges();
        void Dispose();
    }
}