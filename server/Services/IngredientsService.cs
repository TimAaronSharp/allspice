namespace allspice.Services;

public class IngredientsService
{
  public IngredientsService(IngredientsRepository repo)
  {
    _repo = repo;
  }
  private readonly IngredientsRepository _repo;
}