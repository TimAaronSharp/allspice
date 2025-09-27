using System.ComponentModel.DataAnnotations;

namespace allspice.Models;

public class Ingredient : RepoItem<int>
{
  [MaxLength(255)] public string Name { get; set; }
  [MaxLength(255)] public string Quantity { get; set; }
  public int RecipeId { get; set; }
}