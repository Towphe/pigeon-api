using System;
using System.Collections.Generic;

namespace repo;

public partial class User
{
    public Guid UserId { get; set; }

    public string Username { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public DateTime DateCreated { get; set; }

    public virtual ICollection<DirectMessage> DirectMessageUserAs { get; set; } = new List<DirectMessage>();

    public virtual ICollection<DirectMessage> DirectMessageUserBs { get; set; } = new List<DirectMessage>();

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
}
