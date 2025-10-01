namespace allspice.Services;

public class IngredientsService
{

  // NOTE 💉 Dependency injections.

  private readonly IngredientsRepository _repo;
  private readonly RecipesService _recipesService;

  // NOTE 🏗️ Class constructor.

  public IngredientsService(IngredientsRepository repo, RecipesService recipesService)
  {
    _repo = repo;
    _recipesService = recipesService;
  }

  // NOTE 🛠️ Create ingredient method. Gets recipe from recipe id to check if user is recipe creator to prevent users from creating an ingredient on another user's recipe. If true, passes ingredientData to repo.

  public Ingredient Create(Ingredient ingredientData, Profile userInfo)
  {
    Recipe recipe = _recipesService.GetById(ingredientData.RecipeId);

    if (recipe.CreatorId != userInfo.Id)
    {
      throw new Exception($"You cannot create an ingredient on another user's recipe, {userInfo.Name}.".ToUpper());
    }
    return _repo.Create(ingredientData);
  }

  // NOTE 💣 Delete Ingredient method. Gets ingredient and recipe by their ids, verifies user is the ingredient creator by checking the recipe.CreatorId (ingredients don't have creator info, but ingredients cannot be created on a different user's recipe) (if not, throws exception), and sends ingredientToDelete.Id to repo for deletion from database.

  public string Delete(int ingredientId, Profile userInfo)
  {
    Ingredient ingredientToDelete = GetById(ingredientId);
    Recipe recipe = _recipesService.GetById(ingredientToDelete.RecipeId);

    if (recipe.CreatorId != userInfo.Id)
    {
      throw new Exception($"You cannot delete another user's ingredient, {userInfo.Name}.".ToUpper());
    }

    _repo.Delete(ingredientToDelete.Id);
    return $"Ingredient {ingredientToDelete.Quantity} {ingredientToDelete.Name} has been deleted. You monster.";
  }

  // NOTE 🔍🧩 Get ingredient by id method. Sends ingredientId to repo to get ingredient, checks if what was returned was null (if it exists in the database), and then returns the ingredient.

  private Ingredient GetById(int ingredientId)
  {
    Ingredient ingredient = _repo.GetById(ingredientId);

    if (ingredient == null)
    {
      throw new Exception($"Invalid ingredient id: {ingredientId}");
    }

    return ingredient;
  }

  // NOTE 🔍🧩📓 Get ingredients by recipe id. Sends recipeId to repo to retrieve all ingredients for the given recipe.

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