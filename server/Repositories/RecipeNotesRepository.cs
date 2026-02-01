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
    INSERT INTO allspice_recipe_notes (recipe_id, creator_id, body)
    SELECT @RecipeId, @CreatorId, @Body
    WHERE NOT EXISTS (
      SELECT 1 FROM allspice_recipe_notes
      WHERE recipe_id = @RecipeId AND creator_id = @CreatorId);
      
    SELECT * FROM allspice_recipe_notes WHERE id = LAST_INSERT_ID();";

    return _db.Query<RecipeNote>(sql, recipeNoteData).SingleOrDefault();
  }
}