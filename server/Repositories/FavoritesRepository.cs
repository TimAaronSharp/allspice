
namespace allspice.Repositories;

public class FavoritesRepository
{
  private readonly IDbConnection _db;
  public FavoritesRepository(IDbConnection dbConnection)
  {
    _db = dbConnection;
  }

  public FavoriteRecipe Create(Favorite favoriteData)
  {
    string sql = @"
    INSERT INTO
    allspice_favorites(recipe_id, account_id)
    VALUES(@RecipeId, @AccountId);
    
    SELECT
    allspice_favorites.*,
    allspice_recipes.*,
    accounts.*
    FROM allspice_favorites
    INNER JOIN allspice_recipes ON allspice_recipes.id = allspice_favorites.recipe_id
    INNER JOIN accounts ON accounts.id = allspice_favorites.account_id
    WHERE allspice_favorites.id = LAST_INSERT_ID();";

    return _db.Query(sql, (Favorite favorite, FavoriteRecipe favoriteRecipe, Profile account) =>
    {
      favoriteRecipe.Creator = account;
      favoriteRecipe.FavoriteId = favorite.Id;
      return favoriteRecipe;
    }, favoriteData).SingleOrDefault();
  }

  public List<FavoriteRecipe> GetFavoriteRecipesByAccountId(string userInfoId)
  {
    string sql = @"
    SELECT
    allspice_favorites.*,
    allspice_recipes.*,
    accounts.*
    FROM allspice_favorites
    INNER JOIN allspice_recipes ON allspice_recipes.id = allspice_favorites.recipe_id
    INNER JOIN accounts ON accounts.id = allspice_favorites.account_id
    WHERE allspice_favorites.account_id = @userInfoId;";

    return _db.Query(sql, (Favorite favorite, FavoriteRecipe favoriteRecipe, Profile account) =>
    {
      favoriteRecipe.Creator = account;
      favoriteRecipe.FavoriteId = favorite.Id;
      return favoriteRecipe;
    }, new { userInfoId }).ToList();
  }
}