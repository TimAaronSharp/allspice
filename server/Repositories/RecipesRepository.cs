namespace allspice.Repositories;

public class RecipesRepository
{

  public RecipesRepository(IDbConnection db)
  {
    _db = db;
  }
  private readonly IDbConnection _db;
}