using System;
using System.Collections.Generic;

namespace repo;

public partial class DirectMessage
{
    public Guid ChatId { get; set; }

    public Guid UserAId { get; set; }

    public Guid UserBId { get; set; }

    public DateTime DateCreated { get; set; }

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual User UserA { get; set; } = null!;

    public virtual User UserB { get; set; } = null!;
}
