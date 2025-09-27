using System.ComponentModel.DataAnnotations;

namespace allspice.Models;

public class Note : RepoItem<int>
{
  public int RecipeId { get; set; }
  [MaxLength(5000)] public string Body { get; set; }
  [MaxLength(255)] public string CreatorId { get; set; }
}