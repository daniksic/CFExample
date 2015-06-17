using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Claudia.Domain.Models.v1
{
    public class Recipe : BaseEntity
    {
        [Display(Name = "Recipe name")]
        public string Title { get; set; }
        [Display(Name = "Created by")]
        public string CreatedBy { get; set; }
        [Display(Name = "Preparation time")]
        public string PrepTime { get; set; }
        [Display(Name = "Ingredients")]
        public string Ingredients { get; set; }
        [Display(Name = "Directions")]
        public string Directions { get; set; }
        [Display(Name = "How many serves")]
        public int Serves { get; set; }


        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        [ForeignKey("Link")]
        public int LinkId { get; set; }

        public virtual RecipeCategory Category { get; set; }

        public virtual User User { get; set; }

        public virtual Link Link { get; set; }

    }

    public class RecipeCategory : BaseEntity
    {
        [MaxLength(30)]
        public string Title { get; set; }
        public string Description { get; set; }

        public virtual IEnumerable<Recipe> Recipes { get; set; }
    }
}
