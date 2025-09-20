namespace allspice.Services;

public class RecipesService
{

  public RecipesService(RecipesRepository repo)
  {
    _repo = repo;
  }
  private readonly RecipesRepository _repo;

  public Recipe Create(Recipe recipeData)
  {
    return _repo.Create(recipeData);
  }
}