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

  public void Edit(RecipeNote editedRecipeNote)
  {
    string sql = @"
    UPDATE allspice_recipe_notes
    SET
    body = @Body
    WHERE id = @Id LIMIT 1;";

    int rowsAffected = _db.Execute(sql, editedRecipeNote);

    if (rowsAffected != 1)
    {
      throw new Exception($"{rowsAffected} rows were updated, which means your code is bad and you should feel bad. -Dr. Johnathan Alfred Zoidberg.");
    }
  }

  public RecipeNote GetById(int recipeId)
  {
    string sql = "SELECT * FROM allspice_recipe_notes WHERE allspice_recipe_notes.recipe_id = @recipeId;";

    return _db.Query<RecipeNote>(sql, new { recipeId }).SingleOrDefault();
  }

  public RecipeNote GetByRecipeIdAndAccountId(int recipeId, string accountId)
  {
    string sql = @"
    SELECT * FROM allspice_recipe_notes WHERE recipe_id = @RecipeId AND account_id = @AccountId;";

    return _db.Query<RecipeNote>(sql, new { recipeId, accountId }).SingleOrDefault();
  }
}