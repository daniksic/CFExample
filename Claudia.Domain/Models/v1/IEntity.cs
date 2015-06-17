using System;

namespace Claudia.Domain.Models.v1
{
    public interface IEntity
    {
        int Id { get; set; }
        bool IsDeleted { get; set; }
        DateTime DateTimeStamp { get; set; }
    }
}
