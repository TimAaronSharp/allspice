


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

  // public Ingredient Edit(Ingredient updateIngredientData, int ingredientId, Profile userInfo)
  // {
  //   Ingredient ingredientToUpdate = GetById(ingredientId);

  //   if (ingredientToUpdate.CreatorId != userInfo.Id)
  //   {
  //     throw new Exception($"You cannot edit another user's ingredient, {userInfo.Name}.".ToUpper());
  //   }

  //   ingredientToUpdate.Name = updateIngredientData.Name ?? ingredientToUpdate.Name;
  //   ingredientToUpdate.Quantity = updateIngredientData.Quantity ?? ingredientToUpdate.Quantity;

  //   _repo.Edit(ingredientToUpdate);
  //   return ingredientToUpdate;
  // }

  private Ingredient GetById(int ingredientId)
  {
    Ingredient ingredient = _repo.GetById(ingredientId);

    if (ingredient == null)
    {
      throw new Exception($"Invalid ingredient id: {ingredientId}");
    }

    return ingredient;
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

  // public string Delete(int ingredientId, Profile userInfo)
  // {
  //   Ingredient ingredient = GetById(ingredientId);

  //   if (ingredient.CreatorId != userInfo.Id)
  //   {
  //     throw new Exception($"You cannot delete another user's ingredient, {userInfo.Name}.".ToUpper());
  //   }

  //   _repo.Delete(ingredient.Id);
  //   return $"Ingredient {ingredient.Quantity} {ingredient.Name} has been deleted. You monster.";
  // }
}