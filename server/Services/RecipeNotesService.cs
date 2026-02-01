namespace allspice.Services;

public class RecipeNotesService
{
  private readonly RecipeNotesRepository _repo;

  public RecipeNotesService(RecipeNotesRepository repo)
  {
    _repo = repo;
  }
}