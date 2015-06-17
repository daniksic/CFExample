using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Claudia.Domain.Models.v2
{
    public interface IEntity
    {
        int EntityGroup { get; set; }

        int Id { get; set; }

        bool IsDeleted { get; set; }

        DateTime DateTimeStamp { get; set; }
    }
}
