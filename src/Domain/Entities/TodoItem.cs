using Domain.Common;

namespace Domain.Entities;

public class TodoItem : AuditableEntity
{
    public Guid ID { get; set; }

    public string Name { get; set; }

    public Owner Owner { get; set; }
}
