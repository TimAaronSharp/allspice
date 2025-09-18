using System.ComponentModel.DataAnnotations;

namespace allspice.Models;

public class Favorite : RepoItem<int>
{
  [MaxLength(255)] public int RecipeId { get; set; }
  [MaxLength(255)] public string AccountId { get; set; }
}

// NOTE DTO (data transfer objects)
// NOTE supports sending data from the recipe table and accounts(Profile) table combined into one flattened out object
public class FavoriteProfile : Profile
{
  [MaxLength(255)] public int FavoriteId { get; set; }
  [MaxLength(255)] public int RecipeId { get; set; }
}

public class FavoriteRecipe : Recipe
{
  [MaxLength(255)] public int FavoriteId { get; set; }
  [MaxLength(255)] public string AccountId { get; set; }
}