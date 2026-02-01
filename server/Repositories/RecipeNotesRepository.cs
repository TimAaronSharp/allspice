namespace allspice.Repositories;

public class RecipeNotesRepository
{
  private readonly IDbConnection _db;

  public RecipeNotesRepository(IDbConnection db)
  {
    _db = db;
  }

  public RecipeNote Create(RecipeNote recipeNoteData)
  {
    string sql = @"
    INSERT INTO allspice_recipe_notes (recipe_id, account_id, body)
    SELECT @RecipeId, @AccountId, @Body
    WHERE NOT EXISTS (
      SELECT 1 FROM allspice_recipe_notes
      WHERE recipe_id = @RecipeId AND account_id = @AccountId);
      
    SELECT * FROM allspice_recipe_notes WHERE id = LAST_INSERT_ID();";

    return _db.Query<RecipeNote>(sql, recipeNoteData).SingleOrDefault();
  }

  public RecipeNote GetByRecipeIdAndAccountId(int recipeId, string accountId)
  {
    string sql = @"
    SELECT * FROM allspice_recipe_notes WHERE recipe_id = @RecipeId AND account_id = @AccountId;";

    return _db.Query<RecipeNote>(sql, new { recipeId, accountId }).SingleOrDefault();
  }
}