namespace allspice.Repositories;

public class CommentsRepository
{
  private readonly IDbConnection _db;

  public CommentsRepository(IDbConnection db)
  {
    _db = db;
  }

  public Comment Create(Comment commentData)
  {
    string sql = @"
    INSERT INTO
    allspice_recipe_comments(body, recipe_id, creator_id)
    VALUES(@Body, @RecipeId, @CreatorId);
    
    SELECT * FROM allspice_recipe_comments WHERE id = LAST_INSERT_ID();";

    return _db.Query<Comment>(sql, commentData).SingleOrDefault();
  }

  public List<Comment> GetByRecipeId(int recipeId)
  {
    string sql = @"
    SELECT
    allspice_recipe_comments.*,
    accounts.*
    FROM allspice_recipe_comments
    INNER JOIN accounts ON allspice_recipe_comments.creator_id = accounts.id
    WHERE allspice_recipe_comments.recipe_id = @recipeId;";

    return _db.Query(sql, (Comment comment, Profile account) =>
    {
      comment.Creator = account;
      return comment;
    }, new { recipeId }).ToList();
  }
}