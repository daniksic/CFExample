using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Claudia.Domain.Models.v1;

namespace Claudia.Site.Models
{
    public class AskClaudiaViewModel : BaseEntity
    {

        [MaxLength]
        public string Question { get; set; }
        public bool IsPrivate { get; set; }

        [MaxLength]
        public string Answer { get; set; }
        public DateTime? AnswerDate { get; set; }
        [DefaultValue(false)]
        public bool IsAnswered { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        public virtual User User { get; set; }

    }
}