using System.ComponentModel.DataAnnotations;

namespace allspice.Models;

// NOTE To be inherited by all other models.
public abstract class RepoItem<T>
{
  public T Id { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
}