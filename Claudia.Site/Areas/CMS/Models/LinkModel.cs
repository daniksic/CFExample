using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Claudia.Domain.Configuration;
using Claudia.Domain.Models.v1;

namespace Claudia.Site.Areas.CMS.Models
{
    public class LinkViewModel
    {

        public string CategoryName { get; set; }

        public int Id{get; set; }
        [Required, MaxLength(150)]
        public string Title { get;set;}
        public string Description { get;set;}
        [Required]
        public int CategoryId { get;set;}
        public IEnumerable<LinksUrl> Urls { get; set; }

        public string GetThumnailUrl()
        {
            var fna = this.Urls.FirstOrDefault();
            return fna != null ? AppConfig.ImageData["gallery"].GetThumnailUrl(fna.ServerFileName) : string.Empty;
        }
        public string GetImageUrl()
        {
            var fna = this.Urls.FirstOrDefault();
            return fna != null ? AppConfig.ImageData["gallery"].GetThumnailUrl(fna.ServerFileName) : string.Empty;
        }

    }
}