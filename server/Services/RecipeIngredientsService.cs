namespace allspice.Services;

public class RecipeIngredientsService
{
  // NOTE 💉 Dependency injections.

  private readonly RecipeIngredientsRepository _repo;

  // NOTE 🏗️ Class constructor.

  public RecipeIngredientsService(RecipeIngredientsRepository repo)
  {
    _repo = repo;
  }
}