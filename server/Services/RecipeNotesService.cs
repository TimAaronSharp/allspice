namespace allspice.Services;

public class RecipeNotesService
{
  private readonly RecipeNotesRepository _repo;

  public RecipeNotesService(RecipeNotesRepository repo)
  {
    _repo = repo;
  }

  public RecipeNote Create(RecipeNote recipeNoteData)
  {
    return _repo.Create(recipeNoteData);
  }
}