namespace allspice.Repositories;

public class RecipeCommentLikesRepository
{
  private readonly IDbConnection _db;

  public RecipeCommentLikesRepository(IDbConnection db)
  {
    _db = db;
  }

  public void Create(int recipeCommentId, string userId)
  {
    string sql = @"
    INSERT INTO
    allspice_recipe_comment_likes(recipe_comment_id, account_id)
    VALUES(@recipeCommentId, @userId)
    ON DUPLICATE KEY UPDATE
    recipe_comment_id = recipe_comment_id;";

    int rowsAffected = _db.Execute(sql, new { recipeCommentId, userId });

    if (rowsAffected != 1)
    {
      throw new Exception($"{rowsAffected} rows were created(liked), which means your code is bad and you should feel bad. -Dr. Johnathan Alfred Zoidberg.");
    }
  }
}