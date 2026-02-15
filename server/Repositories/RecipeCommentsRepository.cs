namespace allspice.Repositories;

public class RecipeCommentsRepository
{
  private readonly IDbConnection _db;

  public RecipeCommentsRepository(IDbConnection db)
  {
    _db = db;
  }

  public RecipeComment Create(RecipeComment recipeCommentData)
  {
    string sql = @"
    INSERT INTO
    allspice_recipe_comments(body, recipe_id, creator_id)
    VALUES(@Body, @RecipeId, @CreatorId);
    
    SELECT * FROM allspice_recipe_comments WHERE id = LAST_INSERT_ID();";

    return _db.Query<RecipeComment>(sql, recipeCommentData).SingleOrDefault();
  }

  public void Delete(int recipeCommentId)
  {
    string sql = "DELETE FROM allspice_recipe_comments WHERE id = @recipeCommentId LIMIT 1;";

    int rowsAffected = _db.Execute(sql, new { recipeCommentId });

    if (rowsAffected != 1)
    {
      throw new Exception($"{rowsAffected} rows were deleted, which means your code is bad and you should feel bad. -Dr. Johnathan Alfred Zoidberg.");
    }
  }

  public RecipeComment GetById(int recipeCommentId)
  {
    string sql = "SELECT * FROM allspice_recipe_comments WHERE id = @recipeCommentId;";

    return _db.Query<RecipeComment>(sql, new { recipeCommentId }).SingleOrDefault();
  }

  public List<RecipeComment> GetByRecipeId(int recipeId)
  {
    string sql = @"
    SELECT
    allspice_recipe_comments.*,
    accounts.*
    FROM allspice_recipe_comments
    INNER JOIN accounts ON allspice_recipe_comments.creator_id = accounts.id
    WHERE allspice_recipe_comments.recipe_id = @recipeId;";

    return _db.Query(sql, (RecipeComment recipeComment, Profile account) =>
    {
      recipeComment.Creator = account;
      return recipeComment;
    }, new { recipeId }).ToList();
  }
}