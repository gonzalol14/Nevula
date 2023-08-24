using System;
using System.Collections.Generic;

namespace NevulaForo.Models.DB;

public partial class Publication
{
    public int Id { get; set; }

    public int IdUser { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual User IdUserNavigation { get; set; } = null!;
}
