using Microsoft.VisualBasic;

namespace allspice.Repositories;

public class RecipeIngredientsRepository
{
  // NOTE 💉 Dependency injections.

  private readonly IDbConnection _db;

  // NOTE 🏗️ Class constructor.

  public RecipeIngredientsRepository(IDbConnection db)
  {
    _db = db;
  }

  // NOTE 🛠️ Create RecipeIngredient method. Creates an entry in the allspice_recipe_ingredients table that is the relationship between the recipe and ingredient. Having this join table allows the ingredients to be re-used between different recipes instead of duplication (say if 2 different recipes call for 'sugar' '1 cup' it won't duplicate that ingredient. It will instead create a relationship between the recipe and the already existing ingredient).

  public void Create(Ingredient ingredient)
  {
    string sql = @"
    INSERT INTO allspice_recipe_ingredients (recipe_id, ingredient_id)
    VALUES (@RecipeId, @Id);
    
    SELECT * FROM allspice_recipe_ingredients WHERE allspice_recipe_ingredients.id = LAST_INSERT_ID();";

    int rowsAffected = _db.Execute(sql, ingredient);

    if (rowsAffected != 1)
    {
      throw new Exception($"{rowsAffected} RecipeIngredients were created, which means your code is bad and you should feel bad. -Dr. Johnathan Alfred Zoidberg");
    }
  }

  // NOTE 🧺🔍🧩📓 Get RecipeIngredients by recipe id. Only SELECTS the ingredient_ids that match the recipeId and returns them to be sent to the IngredientsService/Repository to query for those ingredients.

  public List<int> GetIngredientIdsByRecipeId(int recipeId)
  {
    string sql = @"
    SELECT ingredient_id FROM allspice_recipe_ingredients
    WHERE allspice_recipe_ingredients.recipe_id = @recipeId;";

    return _db.Query<int>(sql, new { recipeId }).ToList();
  }

}