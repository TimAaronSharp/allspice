using System.ComponentModel.DataAnnotations;

namespace allspice.Models;

// NOTE CreatedFromRecipeId will be used to distinguish altered recipes from base recipes. If CreatedFromRecipeId is null it is a base recipe, if it is not null it is an altered recipe.
public class Recipe : RepoItem<int>
{
  [MaxLength(255)] public string Title { get; set; }
  [MaxLength(5000)] public string Instructions { get; set; }
  [Url, MaxLength(2000)] public string Img { get; set; }
  [MaxLength(255)] public string Category { get; set; }
  public int? CreatedFromRecipeId { get; set; }
  [MaxLength(255)] public string CreatorId { get; set; }
  public Profile Creator { get; set; }
}