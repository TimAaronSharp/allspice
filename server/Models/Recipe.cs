using System.ComponentModel.DataAnnotations;

namespace allspice.Models;

public class Recipe : RepoItem<int>
{
  [MaxLength(255)] public string Name { get; set; }
  [MaxLength(5000)] public string Instructions { get; set; }
  [Url, MaxLength(2000)] public string Img { get; set; }
  [MaxLength(255)] public string Category { get; set; }
  [MaxLength(255)] public string CreatorId { get; set; }
  [MaxLength(255)] public Profile Creator { get; set; }
}

public class AlteredRecipe : Recipe
{
  public int AlteredRecipeId { get; set; }
}