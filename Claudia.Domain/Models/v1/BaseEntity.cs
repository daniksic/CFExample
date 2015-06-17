using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Claudia.Domain.Models.v1
{
    public abstract class BaseEntity:IEntity
    {
        [Key, Column(Order = 0)]
        public int Id { get; set; }

        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

        private DateTime? _dtstamp;
        public DateTime DateTimeStamp
        {
            get { return _dtstamp.HasValue ? _dtstamp.Value : DateTime.Now; }
            set { _dtstamp = value; }
        }
    }
}
