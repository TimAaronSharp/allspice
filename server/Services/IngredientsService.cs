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
    Recipe recipe = _recipesService.GetById(ingredientData.OriginRecipeId);

    if (recipe.CreatorId != userInfo.Id)
    {
      throw new Exception($"You cannot create an ingredient on another user's recipe, {userInfo.Name}.".ToUpper());
    }

    Ingredient ingredientToCreate = _repo.Create(ingredientData);

    return ingredientToCreate;
  }

  // NOTE 💣 Delete Ingredient method. Gets ingredient by its id and sends ingredientToDelete.Id to repo for deletion from database. Does not verify creator because ingredients are only deleted from database if there are no recipeIngredientLinks associated with the ingredient id. Creator verification for recipeIngredientLink does occur. (Will research if there could be a verification check that could work with this/if it would be necessary.)

  public string Delete(int ingredientId)
  {
    Ingredient ingredientToDelete = GetById(ingredientId);
    // Recipe recipe = _recipesService.GetById(ingredientToDelete.OriginRecipeId);

    // if (recipe.CreatorId != userInfo.Id)
    // {
    //   throw new Exception($"You cannot delete another user's ingredient, {userInfo.Name}.".ToUpper());
    // }

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
}