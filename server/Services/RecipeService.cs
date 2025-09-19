namespace allspice.Services;

public class RecipesService
{

  public RecipesService(RecipesRepository repo)
  {
    _repo = repo;
  }
  private readonly RecipesRepository _repo;
}