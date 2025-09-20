using System.ComponentModel.DataAnnotations;

namespace allspice.Models;

public class Favorite : RepoItem<int>
{
  public int RecipeId { get; set; }
  [MaxLength(255)] public string AccountId { get; set; }
}

// NOTE DTO (data transfer objects)
// NOTE supports sending data from the recipe table and accounts(Profile) table combined into one flattened out object
public class FavoriteProfile : Profile
{
  public int FavoriteId { get; set; }
  public int RecipeId { get; set; }
}

public class FavoriteRecipe : Recipe
{
  public int FavoriteId { get; set; }
  [MaxLength(255)] public string AccountId { get; set; }
}