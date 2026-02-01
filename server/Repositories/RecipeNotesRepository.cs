namespace allspice.Repositories;

public class RecipeNotesRepository
{
  private readonly IDbConnection _db;

  public RecipeNotesRepository(IDbConnection db)
  {
    _db = db;
  }
}