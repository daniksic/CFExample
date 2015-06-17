using System.CodeDom;
using System.Collections.Generic;
using Claudia.Domain.Models;
using Claudia.Domain.Models.v1;

namespace Claudia.Repository.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Claudia.Repository.Context.ApplicationContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Claudia.Repository.Context.ApplicationContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            var _uow = new UnitOfWork();
            _uow.UserManager.CreateAsync(new User("daniksic"), "123123");

            context.Categories.AddOrUpdate(c => c.Title,
                new ObjectCategory {Title = "Gallery picture", EntityName = "gallery"},
                new ObjectCategory {Title = "Youtube link", EntityName = "youtube"},
                new ObjectCategory {Title = "Ask Claudia", EntityName = "askclaudia"},
                new ObjectCategory {Title = "Recipe", EntityName = "recipe"},
                new ObjectCategory {Title = "Comment", EntityName = "comment"},
                new ObjectCategory {Title = "Carousel picture", EntityName = "carousel"},
                new ObjectCategory {Title = "Announcement", EntityName = "announcement"},
                new ObjectCategory {Title = "News", EntityName = "news"},
                new ObjectCategory {Title = "Video blog", EntityName = "videoblog"},
                new ObjectCategory {Title = "Link addresses", EntityName = "linkurls"}
                );

            context.SaveChanges();

            context.Links.AddOrUpdate(l=>l.Title,
                    new Link
                    {
                        CategoryId = context.Categories.First(c => c.EntityName == "youtube").Id,
                        Title = "Watch me on YouTube",
                        ServerFileName = "http://www.youtube.com/embed/WaNYnBkEhkk",
                        IsDeleted = false
                    },
                    new Link
                    {
                        CategoryId = context.Categories.First(c => c.EntityName == "news").Id,
                        Title = "Welcome to Claudia Floraunce website!",
                        Description = "Working as a plus size model and inspiring actress can be challenging at times. I want to share with you my experiences and challenges as a plus size woman in my line of work and daily life and how to face them with confidence. I am determined to show the world that you can be beautiful and sexy at any size and any age. I also would like to invite you to share your challenges and experiences with me in my \"Embrace your curves\" video series. Since I am blessed with chef skills and love to eat healthy, I decided to share my favorite recipes with you. Come and join me and let's eat healthy together. I would love it if you could share your favorite healthy recipe with me and i will post them on my site.",
                        ServerFileName = "http://www.youtube.com/user/ClaudiaFloraunce",
                        IsDeleted = false
                    },
                    new Link { 
                        CategoryId = context.Categories.First(c => c.EntityName == "news").Id, 
                        Title = "Latest and hot news", 
                        IsDeleted = false
                    },
                    new Link { 
                        CategoryId = context.Categories.First(c => c.EntityName == "news").Id, 
                        Title = "New section - Ask Claudia",
                        Description="If there is something you want to ask me that will help you go through your rough times or you just want some advice, please do contact me via Ask Claudia page. I have been blessed with a lot of confidence and want to show people that you can be beautiful at any size and any age.",
                        ServerFileName="#",
                        IsDeleted = false,
                        LinksUrls = new List<LinksUrl> { new LinksUrl() { ServerFileName = "#" } }
                    },
                    new Link { 
                        CategoryId = context.Categories.First(c => c.EntityName == "news").Id, 
                        Title = "New section - Video blog show",
                        Description="On my video blog you can get my advice for people who are like me. I'm dedicated to people who think and feel like me so come and watch some of my videos and find out the thruth that is behind my happiness. Also, you can send me your suggestions or share your personal experience.",
                        ServerFileName="#",
                        IsDeleted = false
                    },
                    new Link { 
                        CategoryId = context.Categories.First(c => c.EntityName == "news").Id, 
                        Title = "New section - Recipes & Delices",
                        Description="On recipe section you will find healthy food recipes and food that I adore :) Food that you will have on disposal and that you can use in everyday life. There will be also an option for you to send me your recipes and if i will like them, i will put them on my website and post it with your credits. Section is open for you all day and you can use it as you please :)",
                        ServerFileName="#",
                        IsDeleted = false
                    },
                    new Link { 
                        CategoryId = context.Categories.First(c => c.EntityName == "announcement").Id, 
                        Title = "News",
                        Description="Hello dear fans, i've been busy updating my new \"Photobook\" so don't hesitate to watch it :) Thank you for beeing with me :) Love, Claudia",
                        IsDeleted = false
                    }
                );

            context.SaveChanges();

            context.LinksUrls.AddOrUpdate(new[]{
                    new LinksUrl
                    {
                        LinkId = context.Links.Single(l=>l.Title == "Watch me on YouTube").Id,
                        ServerFileName = "http://www.youtube.com/embed/WaNYnBkEhkk"
                    },
                    new LinksUrl()
                    {
                        LinkId = context.Links.Single(l=>l.Title == "Welcome to Claudia Floraunce website!").Id,
                        ServerFileName = "http://www.youtube.com/user/ClaudiaFloraunce"
                    },
                    new LinksUrl()
                    {
                        LinkId = context.Links.Single(l=>l.Title == "New section - Ask Claudia").Id,
                        ServerFileName = "#"
                    },
                    new LinksUrl()
                    {
                        LinkId = context.Links.Single(l=>l.Title == "New section - Video blog show").Id,
                        ServerFileName = "#"
                    },
                    new LinksUrl()
                    {
                        LinkId = context.Links.Single(l=>l.Title == "New section - Recipes & Delices").Id,
                        ServerFileName = "#"
                    }
                });

            context.SaveChanges();

            context.Widgets.AddOrUpdate(w => w.Description,
                new Widget { 
                    Description = "Main page youtube movie", 
                    SortOrder = 1, 
                    WidgetName = "_Widget0608", 
                    SubWidgetName = "_YouTubeSubWidget", 
                    ReferenceType = "youtube", 
                    ReferenceId = context.Links.First(l=>l.Title=="Watch me on YouTube").Id
                },
                new Widget { 
                    Description = "Welcome to Claudia Floraunce website", 
                    SortOrder = 2, 
                    WidgetName = "_Widget0604", 
                    SubWidgetName = "_SubWidget", 
                    ReferenceType = "news", 
                    ReferenceId = context.Links.First(l=>l.Title=="Welcome to Claudia Floraunce website!").Id
                },
                new Widget { 
                    Description = "News section", 
                    SortOrder = 3, 
                    WidgetName = "_Widget12", 
                    SubWidgetName = "_SubWidget", 
                    ReferenceType = "news", 
                    ReferenceId = context.Links.First(l=>l.Title=="Latest and hot news").Id 
                },
                new Widget { 
                    Description = "New section - Ask Claudia", 
                    SortOrder = 4, 
                    WidgetName = "_Widget04", 
                    SubWidgetName = "_SubWidget", 
                    ReferenceType = "news", 
                    ReferenceId = context.Links.First(l=>l.Title=="New section - Ask Claudia").Id 
                },
                new Widget { 
                    Description = "New section - Video blog show", 
                    SortOrder = 5, 
                    WidgetName = "_Widget04", 
                    SubWidgetName = "_SubWidget", 
                    ReferenceType = "news", 
                    ReferenceId = context.Links.First(l=>l.Title=="New section - Video blog show").Id 
                },
                new Widget { 
                    Description = "New section - Recipes & Delices", 
                    SortOrder = 6, 
                    WidgetName = "_Widget04", 
                    SubWidgetName = "_SubWidget", 
                    ReferenceType = "news", 
                    ReferenceId = context.Links.First(l=>l.Title=="New section - Recipes & Delices").Id 
                }
                );

            context.RecipeCategory.AddOrUpdate(r=>r.Title,
                    new RecipeCategory()
                    {
                        Title = "Soups & Salads",
                        Description = "Soups & Salads"
                    },
                    new RecipeCategory()
                    {
                        Title = "Appetizers & Beverages",
                        Description = "Appetizers & Beverages"
                    }
                );

            context.SaveChanges();
        }
    }
}
