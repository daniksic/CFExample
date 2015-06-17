using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Claudia.Domain.Models.v1
{
    public class AskClaudia : BaseEntity
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
