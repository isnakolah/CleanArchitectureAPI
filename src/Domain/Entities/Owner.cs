using System;
using Domain.Common;

namespace Domain.Entities
{
    public class Owner : AuditableEntity
    {
        public Guid ID { get; set; } 

        public string Name { get; set; }

        public DateTime DOB { get; set; }
    }
}