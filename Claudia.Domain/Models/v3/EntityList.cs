
using System;
using System.Collections.Generic;
namespace Claudia.Domain.Models.v3
{
    public class EntityList : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public int CategoryId { get; set; }
        public virtual EntityCategory Category { get; set; }
    }

    public class EntityCategory : BaseEntity
    {
        public string Title { get; set; }
        public string EntityName { get; set; }

        public virtual ICollection<EntityList> EntityList { get; set; }
    }

    public class Link : BaseEntity
    {
        public string ServerFileName { get; set; }

        public string ClientLocalFileName { get; set; }

        public int EntityListId { get; set; }
        public virtual EntityList EntityList { get; set; }
    }

    public class Recipe : BaseEntity
    {
        public string CreatedBy { get; set; }
        public string PrepTime { get; set; }
        public string Ingredients { get; set; }
        public string Directions { get; set; }
        public int Serves { get; set; }

        public int EntityListId { get; set; }
        public virtual EntityList EntityList { get; set; }

        public int RecipeCategoryId { get; set; }
        public virtual RecipeCategory RecipeCategory { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }
    }

    public class RecipeCategory : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Recipe> Recipes { get; set; }
    }

    public class AskClaudia : BaseEntity
    {
        public string Question { get; set; }
        public bool IsPrivate { get; set; }
        public string Answer { get; set; }
        public DateTime AnswerDate { get; set; }
        public bool IsAnswered { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }
    }

    public class Widget : BaseEntity
    {
        public int SortOrder { get; set; }
        public string WidgetName { get; set; }
        public string SubWidgetName { get; set; }

        public int EntityCategoryId { get; set; }
        public virtual EntityCategory EntityCategory { get; set; }
        public int EntityListId { get; set; }
        public virtual EntityList EntityList { get; set; }
    }



    public class Comment : BaseEntity
    {
        public string Text { get; set; }

        public int EntityListId { get; set; }
        public virtual EntityList EntityList { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }
    }

    public class Rating : BaseEntity
    {
        public int RatingScore { get; set; }
        public int TotalCount { get; set; }

        public int EntityListId { get; set; }
        public virtual EntityList EntityList { get; set; }
    }

}