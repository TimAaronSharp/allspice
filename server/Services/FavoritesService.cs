namespace allspice.Services;

public class FavoritesService
{
  private readonly FavoritesRepository _repo;
  private readonly RecipesService _recipesService;
  private readonly IngredientsService _ingredientsService;

  public FavoritesService(FavoritesRepository repo, RecipesService recipesService, IngredientsService ingredientsService)
  {
    _repo = repo;
    _recipesService = recipesService;
    _ingredientsService = ingredientsService;
  }
}