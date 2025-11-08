using System.ComponentModel.DataAnnotations;

namespace allspice.Models;

public class RecipeIngredientLink : RepoItem<int>
{
  public int RecipeId { get; set; }
  public int IngredientId { get; set; }
  [MaxLength(255)] public string CreatorId { get; set; }
}

public class RecipeIngredient : Ingredient
{
  public int RecipeId { get; set; }
  public int RecipeIngredientLinkId { get; set; }
}