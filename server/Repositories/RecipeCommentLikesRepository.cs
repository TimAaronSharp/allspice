namespace allspice.Repositories;

public class RecipeCommentLikesRepository
{
  private readonly IDbConnection _db;

  public RecipeCommentLikesRepository(IDbConnection db)
  {
    _db = db;
  }
}