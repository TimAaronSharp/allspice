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

  // NOTE 🧺🔍🧩📓 Get RecipeIngredients by recipe id. Will return a list of ingredient ids that will be sent to IngredientsService to get all ingredients for the recipe. 

  public List<int> GetIngredientIdsByRecipeId(int recipeId)
  {
    return _repo.GetIngredientIdsByRecipeId(recipeId);
  }

}