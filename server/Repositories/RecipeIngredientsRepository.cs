namespace allspice.Repositories;

public class RecipeIngredientsRepository
{
  // NOTE 💉 Dependency injections.

  private readonly IDbConnection _db;

  // NOTE 🏗️ Class constructor.

  public RecipeIngredientsRepository(IDbConnection db)
  {
    _db = db;
  }
}