namespace allspice.Services;

public class RecipeIngredientsService
{
  // NOTE 💉 Dependency injections.

  private readonly RecipeIngredientsRepository _repo;
  private readonly IngredientsService _ingredientsService;

  // NOTE 🏗️ Class constructor.

  public RecipeIngredientsService(RecipeIngredientsRepository repo, IngredientsService ingredientsService)
  {
    _repo = repo;
    _ingredientsService = ingredientsService;
  }

  // NOTE 🔍 Method to check if any recipeIngredients exist that have a given ingredientId. If there is, then nothing happens. If there aren't any, then the ingredient will be deleted from the allspice_ingredients table through the IngredientsService. This is only called from RecipeIngredientsService.Delete().
  public void CheckForIngredientId(int ingredientId)
  {
    int rowsReturned = _repo.CheckForIngredientId(ingredientId);
    if (rowsReturned == 0)
    {
      // _ingredientsService.Delete(ingredientId);
    }
  }

  // NOTE 🛠️ Create RecipeIngredient method. Receives the ingredient that was just created in the allspice_ingredients table and sends it to the RecipeIngredientsRepository to create the entry in the allspice_recipe_ingredient_links table (which is just a linking table that links recipes and ingredients by their ids). This allows multiple recipes can use the same ingredient from the ingredients table (if 2 recipes happen to write it the exact way. This saves storage space in the database by referencing ingredients that already exist instead of creating duplicates of the same recipe).

  public void Create(Ingredient ingredient)
  {
    _repo.Create(ingredient);
  }

  // NOTE 💣 Delete RecipeIngredient method. This is used if a user deletes a recipeIngredient from their recipe, or if they are going to edit a recipeIngredient (behind the scenes the editing will just be deleting and creating a new recipeIngredient).

  public string Delete(int recipeId, int ingredientId)
  {
    _repo.Delete(recipeId, ingredientId);
    CheckForIngredientId(ingredientId);
    return $"RecipeIngredient for recipe id: {recipeId}, ingredient id:{ingredientId} has been deleted. You monster.";
  }

  // NOTE 🧺🔍🧩📓 Get RecipeIngredients by recipe id. Will return a list of ingredient ids that will be sent to IngredientsService to get all ingredients for the recipe. 

  public List<int> GetIngredientIdsByRecipeId(int recipeId)
  {
    return _repo.GetIngredientIdsByRecipeId(recipeId);
  }

}