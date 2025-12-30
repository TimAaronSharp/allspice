namespace allspice.Repositories;

public class FavoritesRepository
{
  // NOTE 💉 Dependency injections.

  private readonly IDbConnection _db;

  // NOTE 🏗️ Class constructor.

  public FavoritesRepository(IDbConnection dbConnection)
  {
    _db = dbConnection;
  }

  // NOTE 🛠️ Create favorite method. Creates a new favorite in the database. Returns a FavoriteRecipe object (DTO) because a Favorite object does not contain all needed data itself.
  // NOTE MAKE DUPLICATE PROOF!!! (Creates the favorite but is returning null for some reason)
  public Favorite Create(Favorite favoriteData)
  {
    string sql = @"
    INSERT INTO allspice_favorites (recipe_id, account_id)
    SELECT @RecipeId, @AccountId
    WHERE NOT EXISTS (
      SELECT 1 FROM allspice_favorites
      WHERE recipe_id = @RecipeId AND account_id = @AccountId);
      
    SELECT * FROM allspice_favorites WHERE id = LAST_INSERT_ID();";

    Favorite createdFavorite = _db.Query<Favorite>(sql, favoriteData).SingleOrDefault();
    return createdFavorite;
  }

  // NOTE 💣 Delete favorite method. Deletes favorite from database.

  public void Delete(int favoriteId)
  {
    string sql = "DELETE FROM allspice_favorites WHERE id = @favoriteId LIMIT 1;";

    int rowsAffected = _db.Execute(sql, new { favoriteId });

    if (rowsAffected != 1)
    {
      throw new Exception($"{rowsAffected} favorites were deleted, which means your code is bad and you should feel bad. - Dr. Johnathan Alfred Zoidberg");
    }
  }

  // NOTE 🔍 Get favorite by id method. Queries database for favorite by id.

  public Favorite GetById(int favoriteId)
  {
    string sql = "SELECT * FROM allspice_favorites WHERE allspice_favorites.id = @favoriteId;";

    return _db.Query<Favorite>(sql, new { favoriteId }).SingleOrDefault();
  }

  // NOTE 🔍🧺🧑‍🦲 Get all favorite recipes by account id method. Queries database for favorites, recipes, and accounts, and returns a list of FavoriteRecipe (DTO) objects that have the relevant recipe and creator (account) data.

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

  public Favorite GetByRecipeIdAndAccountId(Favorite favoriteData)
  {
    string sql = @"
    SELECT * FROM allspice_favorites 
      WHERE allspice_favorites.recipe_id = @RecipeId 
      AND allspice_favorites.account_id = @AccountId;";

    return _db.Query<Favorite>(sql, favoriteData).SingleOrDefault();
  }
}

