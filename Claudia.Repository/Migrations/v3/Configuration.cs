namespace Claudia.Repository.Migrations.v3
{
    using Claudia.Domain.Models.v3;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Claudia.Repository.Context.v3.AppContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\v3";
        }

        protected override void Seed(Claudia.Repository.Context.v3.AppContext context)
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

            //var _uow = new UnitOfWork();
            //_uow.UserManager.CreateAsync(new User("daniksic"), "123123");

            

            context.EntityCategories.AddOrUpdate(c => c.Title,
                new EntityCategory { Title = "Gallery picture", EntityName = "gallery" },
                new EntityCategory { Title = "Ask Claudia", EntityName = "askclaudia" },
                new EntityCategory { Title = "Recipe", EntityName = "recipe" },
                new EntityCategory { Title = "Carousel picture", EntityName = "carousel" },
                new EntityCategory { Title = "Announcement", EntityName = "announcement" },
                new EntityCategory { Title = "News", EntityName = "news" },
                new EntityCategory { Title = "Video blog", EntityName = "videoblog" },
                new EntityCategory { Title = "Youtube link", EntityName = "youtube" },
                new EntityCategory { Title = "Widget", EntityName = "widget" }
                );

            context.SaveChanges();

            context.EntityList.AddOrUpdate(l => l.Title,
                    new EntityList
                    {
                        CategoryId = context.EntityCategories.First(c => c.EntityName == "youtube").Id,
                        Title = "Watch me on YouTube",
                        IsDeleted = false
                    },
                    new EntityList
                    {
                        CategoryId = context.EntityCategories.First(c => c.EntityName == "news").Id,
                        Title = "Welcome to Claudia Floraunce website!",
                        Description = "Working as a plus size model and inspiring actress can be challenging at times. I want to share with you my experiences and challenges as a plus size woman in my line of work and daily life and how to face them with confidence. I am determined to show the world that you can be beautiful and sexy at any size and any age. I also would like to invite you to share your challenges and experiences with me in my \"Embrace your curves\" video series. Since I am blessed with chef skills and love to eat healthy, I decided to share my favorite recipes with you. Come and join me and let's eat healthy together. I would love it if you could share your favorite healthy recipe with me and i will post them on my site.",
                        IsDeleted = false
                    },
                    new EntityList
                    {
                        CategoryId = context.EntityCategories.First(c => c.EntityName == "news").Id,
                        Title = "Latest and hot news",
                        IsDeleted = false
                    },
                    new EntityList
                    {
                        CategoryId = context.EntityCategories.First(c => c.EntityName == "news").Id,
                        Title = "New section - Ask Claudia",
                        Description = "If there is something you want to ask me that will help you go through your rough times or you just want some advice, please do contact me via Ask Claudia page. I have been blessed with a lot of confidence and want to show people that you can be beautiful at any size and any age.",
                        IsDeleted = false
                    },
                    new EntityList
                    {
                        CategoryId = context.EntityCategories.First(c => c.EntityName == "news").Id,
                        Title = "New section - Video blog show",
                        Description = "On my video blog you can get my advice for people who are like me. I'm dedicated to people who think and feel like me so come and watch some of my videos and find out the thruth that is behind my happiness. Also, you can send me your suggestions or share your personal experience.",
                        IsDeleted = false
                    },
                    new EntityList
                    {
                        CategoryId = context.EntityCategories.First(c => c.EntityName == "news").Id,
                        Title = "New section - Recipes & Delices",
                        Description = "On recipe section you will find healthy food recipes and food that I adore :) Food that you will have on disposal and that you can use in everyday life. There will be also an option for you to send me your recipes and if i will like them, i will put them on my website and post it with your credits. Section is open for you all day and you can use it as you please :)",
                        IsDeleted = false
                    },
                    new EntityList
                    {
                        CategoryId = context.EntityCategories.First(c => c.EntityName == "announcement").Id,
                        Title = "News",
                        Description = "Hello dear fans, i've been busy updating my new \"Photobook\" so don't hesitate to watch it :) Thank you for beeing with me :) Love, Claudia",
                        IsDeleted = false
                    },
                    new EntityList
                    {
                        CategoryId = context.EntityCategories.First(c => c.EntityName == "widget").Id,
                        Title = "Main page youtube movie",
                        Description = "Main page youtube movie",
                        IsDeleted = false
                    }, new EntityList
                    {
                        CategoryId = context.EntityCategories.First(c => c.EntityName == "widget").Id,
                        Title = "Welcome to Claudia Floraunce website!",
                        Description = "Welcome to Claudia Floraunce website!",
                        IsDeleted = false
                    }, new EntityList
                    {
                        CategoryId = context.EntityCategories.First(c => c.EntityName == "widget").Id,
                        Title = "Latest and hot news",
                        Description = "News section",
                        IsDeleted = false
                    }, new EntityList
                    {
                        CategoryId = context.EntityCategories.First(c => c.EntityName == "widget").Id,
                        Title = "New section - Ask Claudia",
                        Description = "New section - Ask Claudia",
                        IsDeleted = false
                    }, new EntityList
                    {
                        CategoryId = context.EntityCategories.First(c => c.EntityName == "widget").Id,
                        Title = "New section - Video blog show",
                        Description = "New section - Video blog show",
                        IsDeleted = false
                    }, new EntityList
                    {
                        CategoryId = context.EntityCategories.First(c => c.EntityName == "widget").Id,
                        Title = "New section - Recipes & Delices",
                        Description = "New section - Recipes & Delices",
                        IsDeleted = false
                    }
                );

            context.SaveChanges();

            context.Links.AddOrUpdate(l => l.ServerFileName,
                new Link
                {
                    ServerFileName = "http://www.youtube.com/embed/WaNYnBkEhkk",
                    EntityListId = context.EntityList.FirstOrDefault(i => i.Title == "Watch me on YouTube").Id,
                    IsDeleted = false
                },
                new Link
                {
                    ServerFileName = "http://www.youtube.com/user/ClaudiaFloraunce",
                    EntityListId = context.EntityList.FirstOrDefault(i => i.Title == "Welcome to Claudia Floraunce website!").Id,
                    IsDeleted = false
                },
                new Link
                {
                    ServerFileName = "#",
                    EntityListId = context.EntityList.FirstOrDefault(i => i.Title == "New section - Ask Claudia").Id,
                    IsDeleted = false
                },
                new Link
                {
                    ServerFileName = "#",
                    EntityListId = context.EntityList.FirstOrDefault(i => i.Title == "New section - Video blog show").Id,
                    IsDeleted = false
                },
                new Link { ServerFileName = "#",
                           EntityListId = context.EntityList.FirstOrDefault(i => i.Title == "New section - Recipes & Delices").Id,
                           IsDeleted = false
                }
                );

            context.Widgets.AddOrUpdate(w => w.EntityListId,
                new Widget
                {
                    SortOrder = 1,
                    WidgetName = "_Widget0608",
                    SubWidgetName = "_YouTubeSubWidget",
                    EntityCategoryId = context.EntityCategories.First(l => l.Title == "youtube").Id,
                    EntityListId = context.EntityList.First(l => l.Title == "Main page youtube movie").Id                    
                },
                new Widget
                {
                    SortOrder = 2,
                    WidgetName = "_Widget0604",
                    SubWidgetName = "_SubWidget",
                    EntityCategoryId = context.EntityCategories.First(l => l.Title == "news").Id,
                    EntityListId = context.EntityList.First(l => l.Title == "Welcome to Claudia Floraunce website!").Id  
                },
                new Widget
                {
                    SortOrder = 3,
                    WidgetName = "_Widget12",
                    SubWidgetName = "_SubWidget",
                    EntityCategoryId = context.EntityCategories.First(l => l.Title == "news").Id,
                    EntityListId = context.EntityList.First(l => l.Title == "Latest and hot news").Id
                },
                new Widget
                {
                    SortOrder = 4,
                    WidgetName = "_Widget04",
                    SubWidgetName = "_SubWidget",
                    EntityCategoryId = context.EntityCategories.First(l => l.Title == "news").Id,
                    EntityListId = context.EntityList.First(l => l.Title == "New section - Ask Claudia").Id
                },
                new Widget
                {
                    SortOrder = 5,
                    WidgetName = "_Widget04",
                    SubWidgetName = "_SubWidget",
                    EntityCategoryId = context.EntityCategories.First(l => l.Title == "news").Id,
                    EntityListId = context.EntityList.First(l => l.Title == "New section - Video blog show").Id
                },
                new Widget
                {
                    SortOrder = 6,
                    WidgetName = "_Widget04",
                    SubWidgetName = "_SubWidget",
                    EntityCategoryId = context.EntityCategories.First(l => l.Title == "news").Id,
                    EntityListId = context.EntityList.First(l => l.Title == "New section - Recipes & Delices").Id
                }
                );

            context.SaveChanges();
        }
    }
}
