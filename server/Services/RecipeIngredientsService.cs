namespace allspice.Services;

public class RecipeIngredientsService
{
  // NOTE 💉 Dependency injections.

  private readonly RecipeIngredientsRepository _repo;
  // private readonly IngredientsService _ingredientsService;

  // NOTE 🏗️ Class constructor.

  public RecipeIngredientsService(RecipeIngredientsRepository repo)
  {
    _repo = repo;
    // _ingredientsService = ingredientsService;
  }

  // NOTE 🛠️ Create RecipeIngredient method. Receives the ingredient that was just created in the allspice_ingredients table and sends it to the RecipeIngredientsRepository to create the entry in the allspice_recipe_ingredients table.

  public void Create(Ingredient ingredient)
  {
    _repo.Create(ingredient);
  }

  // NOTE 💣 Delete RecipeIngredient method. This is only used for if a user is changing an ingredient in their recipe. The RecipeIngredient will be deleted and another will be created with the new ingredient (through the respective methods). RecipeIngredients are cascade deleted when a the recipe or ingredient(shouldn't be actively deleting ingredients with how I'm setting it up, though) is deleted .

  public string Delete(int recipeId, int ingredientId)
  {
    _repo.Delete(recipeId, ingredientId);
    return $"RecipeIngredient for recipe id: {recipeId}, ingredient id:{ingredientId} has been deleted. You monster.";
  }

  // NOTE 🧺🔍🧩📓 Get RecipeIngredients by recipe id. Will return a list of ingredient ids that will be sent to IngredientsService to get all ingredients for the recipe. 

  public List<int> GetIngredientIdsByRecipeId(int recipeId)
  {
    return _repo.GetIngredientIdsByRecipeId(recipeId);
  }

}