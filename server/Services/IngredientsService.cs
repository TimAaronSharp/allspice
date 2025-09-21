

namespace allspice.Services;

public class IngredientsService
{
  public IngredientsService(IngredientsRepository repo, RecipesService recipesService)
  {
    _repo = repo;
    _recipesService = recipesService;
  }
  private readonly IngredientsRepository _repo;
  private readonly RecipesService _recipesService;

  public Ingredient Create(Ingredient ingredientData)
  {
    return _repo.Create(ingredientData);
  }

  public List<Ingredient> GetByRecipeId(int recipeId)
  {
    Recipe recipe = GetRecipeById(recipeId);
    return _repo.GetByRecipeId(recipe.Id);
  }

  private Recipe GetRecipeById(int recipeId)
  {
    return _recipesService.GetById(recipeId);
  }
}