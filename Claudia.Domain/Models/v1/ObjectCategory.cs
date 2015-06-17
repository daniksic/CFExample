using System.ComponentModel.DataAnnotations;

namespace Claudia.Domain.Models.v1
{
    /// <summary>
    /// default values: Claudia specials, entertainment, event, social media, recepies, gallery, front news
    /// </summary>
    public class ObjectCategory : BaseEntity
    {
        [MaxLength(20)]
        public string Title { get; set; }

        public string EntityName { get; set; }
    }
}
