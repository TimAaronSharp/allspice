namespace allspice.Services;

public class FavoritesService
{
  // NOTE 💉 Dependency injections.

  private readonly FavoritesRepository _repo;

  // NOTE 🏗️ Class constructor.

  public FavoritesService(FavoritesRepository repo, RecipesService recipesService, IngredientsService ingredientsService)
  {
    _repo = repo;
  }

  // NOTE 🛠️ Create favorite method. Passes favoriteData to repo for creation in database.

  public FavoriteRecipe Create(Favorite favoriteData)
  {
    return _repo.Create(favoriteData);
  }

  // NOTE 💣 Delete favorite method. Gets favorite by id, verifies user is the favorite creator (if not, throws exception), and sends favorite.Id to repo for deletion from database.

  public string Delete(int favoriteId, Profile userInfo)
  {
    Favorite favorite = GetById(favoriteId);

    if (favorite.AccountId != userInfo.Id)
    {
      throw new Exception($"You cannot delete another user's favorite, {userInfo.Name}.".ToUpper());
    }

    _repo.Delete(favorite.Id);
    return $"Favorite id:{favorite.Id} has been deleted. You monster.";
  }

  // NOTE 🔍 Get favorite by id method. Sends favoriteId to repo to retrieve favorite by id and checks if what is returned is null (if so, throws exception) before returning retrieved favorite.

  private Favorite GetById(int favoriteId)
  {

    Favorite favorite = _repo.GetById(favoriteId);

    if (favorite == null)
    {
      throw new Exception($"Invalid favorite id: {favoriteId}");
    }

    return favorite;
  }

  // NOTE 🔍🧑‍🦲 Get favorite recipes(data transfer object or DTO) by account id method. Sends the user's id to repo to retrieve favorite recipes by account id from database. Returns a list of FavoriteRecipe objects because the favorite class model does not have all the recipe data needed for necessary usage (only has Id, CreatedAt, UpdatedAt, RecipeId, and AccountId. The FavoriteRecipe class model acts as a DTO by having FavoriteId and AccountId properties while inheriting the Recipe class.)

  public List<FavoriteRecipe> GetFavoriteRecipesByAccountId(string userInfoId)
  {
    return _repo.GetFavoriteRecipesByAccountId(userInfoId);
  }
}