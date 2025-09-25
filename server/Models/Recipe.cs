using System.ComponentModel.DataAnnotations;

namespace allspice.Models;

// NOTE CreatedFromRecipeId will be used to distinguish altered recipes from base recipes. If the recipe's id and CreatedFromRecipeId match then it is a base recipe, if they do not then it is an altered recipe.
public class Recipe : RepoItem<int>
{
  [MaxLength(255)] public string Title { get; set; }
  [MaxLength(5000)] public string Instructions { get; set; }
  [Url, MaxLength(2000)] public string Img { get; set; }
  [MaxLength(255)] public string Category { get; set; }
  public int CreatedFromRecipeId { get; set; }
  [MaxLength(255)] public string CreatorId { get; set; }
  public Profile Creator { get; set; }
}