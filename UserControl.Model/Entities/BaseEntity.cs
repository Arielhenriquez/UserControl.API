﻿using UserControl.Core.Abstractions;

namespace UserControl.Model.Entities;

public class BaseEntity : IBaseEntity
{
    public Guid Id { get; set; }
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset? Modified { get; set; }
}
