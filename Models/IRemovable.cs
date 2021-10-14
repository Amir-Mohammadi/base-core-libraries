using System;

namespace core.Models
{
  public interface IRemovable
  {
    DateTime? DeletedAt { get; set; }
  }
}