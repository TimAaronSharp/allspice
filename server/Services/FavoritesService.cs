
namespace allspice.Services;

public class FavoritesService
{
  private readonly FavoritesRepository _repo;

  public FavoritesService(FavoritesRepository repo, RecipesService recipesService, IngredientsService ingredientsService)
  {
    _repo = repo;
  }

  public FavoriteRecipe Create(Favorite favoriteData)
  {
    return _repo.Create(favoriteData);
  }

  public List<FavoriteRecipe> GetFavoriteRecipesByAccountId(string userInfoId)
  {
    return _repo.GetFavoriteRecipesByAccountId(userInfoId);
  }
}