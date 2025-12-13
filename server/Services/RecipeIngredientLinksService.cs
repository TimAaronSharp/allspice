namespace allspice.Services;

public class RecipeIngredientLinksService
{
  // NOTE 💉 Dependency injections.

  private readonly RecipeIngredientLinksRepository _repo;
  private readonly IngredientsService _ingredientsService;
  private readonly RecipesService _recipesService;

  // NOTE 🏗️ Class constructor.

  public RecipeIngredientLinksService(RecipeIngredientLinksRepository repo, IngredientsService ingredientsService, RecipesService recipesService)
  {
    _repo = repo;
    _ingredientsService = ingredientsService;
    _recipesService = recipesService;
  }

  // NOTE 🔍 Method to check if any recipeIngredients exist that have a given ingredientId. If there is, then nothing happens. If there aren't any, then the ingredient will be deleted from the allspice_ingredients table through the IngredientsService. This is only called from RecipeIngredientLinksService.Delete().
  public void CheckForIngredientId(int ingredientId)
  {
    int rowsReturned = _repo.CheckForIngredientId(ingredientId);
    if (rowsReturned == 0)
    {
      _ingredientsService.Delete(ingredientId);
    }
  }

  // NOTE 🛠️ Create RecipeIngredient method. Receives the ingredient that was just created in the allspice_ingredients table and sends it to the RecipeIngredientLinksRepository to create the entry in the allspice_recipe_ingredient_links table (which is just a linking table that links recipes and ingredients by their ids). This allows multiple recipes can use the same ingredient from the ingredients table (if 2 recipes happen to write it the exact way. This saves storage space in the database by referencing ingredients that already exist instead of creating duplicates of the same recipe).

  public List<RecipeIngredientLink> Create(List<RecipeIngredientLink> recipeIngredientLinkData, Profile userInfo)
  {
    // Shouldn't ever be a problem with a user creating a recipeIngredientLink for a recipe they didn't create, but just in case.
    Recipe recipe = _recipesService.GetById(recipeIngredientLinkData[0].RecipeId);

    if (recipe.CreatorId != userInfo.Id)
    {
      throw new Exception($"You cannot create a recipe ingredient link on another user's recipe, {userInfo.Name}.".ToUpper());
    }

    return _repo.Create(recipeIngredientLinkData);
  }

  // NOTE 💣 Delete RecipeIngredient method. This is used if a user deletes a recipeIngredient from their recipe, or if they are going to edit a recipeIngredient (behind the scenes the editing will just be deleting and creating a new recipeIngredient).

  public string Delete(int recipeIngredientLinkId, Profile userInfo)
  {
    RecipeIngredientLink recipeIngredientLink = GetById(recipeIngredientLinkId);

    if (recipeIngredientLink.CreatorId != userInfo.Id)
    {
      throw new Exception($"You cannot delete another user's recipeIngredientLink, {userInfo.Name}.".ToUpper());
    }

    _repo.Delete(recipeIngredientLink.Id);
    CheckForIngredientId(recipeIngredientLink.IngredientId);
    return $"RecipeIngredient id: {recipeIngredientLink.Id} has been deleted. You monster.";
  }

  public RecipeIngredientLink GetById(int recipeIngredientLinkId)
  {
    RecipeIngredientLink recipeIngredientLink = _repo.GetById(recipeIngredientLinkId);

    if (recipeIngredientLink == null)
    {
      throw new Exception($"Invalid recipeIngredientLink id: {recipeIngredientLinkId}");
    }

    return recipeIngredientLink;
  }

  // NOTE 🧺🔍🧩📓 Get RecipeIngredients by recipe id. Will return a list of ingredient ids that will be sent to IngredientsService to get all ingredients for the recipe. 

  public List<RecipeIngredient> GetRecipeIngredientsByRecipeId(int recipeId)
  {
    List<RecipeIngredient> recipeIngredients = _repo.GetRecipeIngredientsByRecipeId(recipeId);

    // NOTE Shouldn't ever happen organically, but just in case.
    if (recipeIngredients == null || recipeIngredients.Count == 0)
    {
      return new List<RecipeIngredient>();
    }
    return recipeIngredients;
  }

}