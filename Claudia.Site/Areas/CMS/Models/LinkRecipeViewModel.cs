using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Claudia.Domain.Models.v1;

namespace Claudia.Site.Areas.CMS.Models
{
    public class LinkRecipeViewModel
    {
        public LinkRecipeViewModel()
        {
            this.Link = new Link();
            this.Recipe = new Recipe();
            this.CreatorUser=new User();
        }
        public Link Link { get; set; }
        public Recipe Recipe { get; set; }
        public User CreatorUser { get; set; } 
    }
}