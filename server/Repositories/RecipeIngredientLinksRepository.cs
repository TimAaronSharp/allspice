using Microsoft.VisualBasic;

namespace allspice.Repositories;

public class RecipeIngredientLinksRepository
{
  // NOTE 💉 Dependency injections.

  private readonly IDbConnection _db;

  // NOTE 🏗️ Class constructor.

  public RecipeIngredientLinksRepository(IDbConnection db)
  {
    _db = db;
  }

  public int CheckForIngredientId(int ingredientId)
  {
    string sql = "SELECT * FROM allspice_recipe_ingredient_links WHERE allspice_recipe_ingredient_links.ingredient_id = @ingredientId;";

    return _db.Execute(sql, new { ingredientId });

  }

  // NOTE 🛠️ Create RecipeIngredient method. Creates an entry in the allspice_recipe_ingredient_links table that is the relationship between the recipe and ingredient. Having this join table allows the ingredients to be re-used between different recipes instead of duplication (say if 2 different recipes call for 'sugar' '1 cup' it won't duplicate that ingredient. It will instead create a relationship between the recipe and the already existing ingredient).

  public RecipeIngredientLink Create(RecipeIngredientLink recipeIngredientLinkData)
  {
    string sql = @"
    INSERT INTO allspice_recipe_ingredient_links (recipe_id, ingredient_id, creator_id)
    VALUES (@RecipeId, @IngredientId, @CreatorId)
    ON DUPLICATE KEY UPDATE id = LAST_INSERT_ID(id);
    
    SELECT * FROM allspice_recipe_ingredient_links WHERE id = LAST_INSERT_ID();";

    return _db.Query(sql, recipeIngredientLinkData).SingleOrDefault();
  }

  // NOTE 💣 Delete RecipeIngredient method. Finds the RecipeIngredient that matches the recipeId and ingredientId and deletes from the database. This is only used for if a user is changing an ingredient in their recipe.

  public void Delete(int recipeIngredientId)
  {
    string sql = "DELETE from allspice_recipe_ingredient_links WHERE allspice_recipe_ingredient_links.id = @recipeIngredientId;";

    int rowsAffected = _db.Execute(sql, new { recipeIngredientId });

    if (rowsAffected != 1)
    {
      throw new Exception($"{rowsAffected} RecipeIngredients were deleted, which means your code is bad and you should feel bad. -Dr. Johnathan Alfred Zoidberg");
    }
  }

  public RecipeIngredientLink GetById(int recipeIngredientLinkId)
  {
    string sql = "SELECT * FROM allspice_recipe_ingredient_links WHERE allspice_recipe_ingredient_links.id = @ recipeIngredientLinkId;";

    return _db.Query<RecipeIngredientLink>(sql, new { recipeIngredientLinkId }).SingleOrDefault();
  }

  // NOTE 🧺🔍🧩📓 Get RecipeIngredients by recipe id. Only SELECTS the ingredient_ids that match the recipeId and returns them to be sent to the IngredientsService/Repository to query for those ingredients.

  public List<RecipeIngredient> GetRecipeIngredientsByRecipeId(int recipeId)
  {
    string sql = @"
    SELECT
    allspice_recipe_ingredient_links.*,
    allspice_ingredients.*
    FROM allspice_recipe_ingredient_links
    INNER JOIN allspice_ingredients ON allspice_ingredients.id = allspice_recipe_ingredient_links.ingredient_id
    WHERE allspice_recipe_ingredient_links.recipe_id = @recipeId;";

    return _db.Query(sql, (RecipeIngredientLink recipeIngredientLink, RecipeIngredient recipeIngredient) =>
    {
      recipeIngredient.RecipeIngredientLinkId = recipeIngredientLink.Id;
      recipeIngredient.RecipeId = recipeIngredientLink.RecipeId;
      return recipeIngredient;
    }, new { recipeId }).ToList();

  }
}


// SELECT ingredient_id FROM allspice_recipe_ingredient_links
//     WHERE allspice_recipe_ingredient_links.recipe_id = @recipeId;