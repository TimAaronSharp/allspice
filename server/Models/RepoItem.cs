using System.ComponentModel.DataAnnotations;

namespace allspice.Models;

// NOTE To be inherited by all other models.
public abstract class RepoItem<T>
{
  [MaxLength(255)] public T Id { get; set; }
  [MaxLength(255)] public DateTime CreatedAt { get; set; }
  [MaxLength(255)] public DateTime UpdatedAt { get; set; }
}