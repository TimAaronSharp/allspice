
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

  public string Delete(int recipeId, Profile userInfo)
  {
    Recipe recipeToDelete = GetById(recipeId);

    if (recipeToDelete.CreatorId != userInfo.Id)
    {
      throw new Exception($"You cannot delete another user's recipe, {userInfo.Name}.".ToUpper());
    }
    _repo.Delete(recipeId);
    return $"Recipe {recipeToDelete.Title} has been deleted. You monster.";
  }

  public Recipe Edit(Profile userInfo, Recipe updateRecipeData, int recipeId)
  {
    Recipe recipeToUpdate = GetById(recipeId);

    if (recipeToUpdate.CreatorId != userInfo.Id)
    {
      throw new Exception($"You cannot edit another user's recipe, {userInfo.Name}.".ToUpper());
    }

    recipeToUpdate.Title = updateRecipeData.Title ?? recipeToUpdate.Title;
    recipeToUpdate.Instructions = updateRecipeData.Instructions ?? recipeToUpdate.Instructions;
    recipeToUpdate.Img = updateRecipeData.Img ?? recipeToUpdate.Img;
    recipeToUpdate.Category = updateRecipeData.Category ?? recipeToUpdate.Category;

    _repo.Edit(recipeToUpdate);
    return recipeToUpdate;
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