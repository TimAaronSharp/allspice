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
}