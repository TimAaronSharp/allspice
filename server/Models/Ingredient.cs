using System.ComponentModel.DataAnnotations;

namespace allspice.Models;

public class Ingredient : RepoItem<int>
{
  [MaxLength(255)] public string Name { get; set; }
  [MaxLength(255)] public string Quantity { get; set; }
  [MaxLength(255)] public int RecipeId { get; set; }
  [MaxLength(255)] public string CreatorId { get; set; }
  [MaxLength(255)] public Profile Creator { get; set; }
}