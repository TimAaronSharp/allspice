
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

  public List<Recipe> GetAll()
  {
    return _repo.GetAll();
  }

  public Recipe GetById(int recipeId)
  {
    Recipe recipe = _repo.GetById(recipeId);

    if (recipe == null)
    {
      throw new Exception($"Invalid recipe id: {recipeId}");
    }

    return recipe;

  }
}