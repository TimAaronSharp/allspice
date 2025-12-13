using System.Text;
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

  public List<RecipeIngredientLink> Create(List<RecipeIngredientLink> recipeIngredientLinkData)
  {
    if (recipeIngredientLinkData == null || recipeIngredientLinkData.Count == 0)
    {
      return new List<RecipeIngredientLink>();
    }

    var sql = new StringBuilder();
    var valueParams = new DynamicParameters();

    for (int i = 0; i < recipeIngredientLinkData.Count; i++)
    {
      sql.AppendLine($@"
      INSERT INTO allspice_recipe_ingredient_links (recipe_id, ingredient_id, creator_id)
      SELECT @RecipeId{i}, @IngredientId{i}, @CreatorId{i}
      WHERE NOT EXISTS (
        SELECT 1 FROM allspice_recipe_ingredient_links
        WHERE recipe_id = @RecipeId{i} AND ingredient_id = @IngredientId{i});");

      valueParams.Add($"@RecipeId{i}", recipeIngredientLinkData[i].RecipeId);
      valueParams.Add($"@IngredientId{i}", recipeIngredientLinkData[i].IngredientId);
      valueParams.Add($"@CreatorId{i}", recipeIngredientLinkData[i].CreatorId);
    }

    sql.AppendLine("SELECT * FROM allspice_recipe_ingredient_links WHERE ");

    for (int i = 0; i < recipeIngredientLinkData.Count; i++)
    {
      if (i > 0)
      {
        sql.Append(" OR ");
      }
      sql.Append($"(recipe_id = @RecipeId{i} AND ingredient_id = @IngredientId{i})");
    }

    return _db.Query<RecipeIngredientLink>(sql.ToString(), valueParams).ToList();

    // string sql = @"
    // INSERT INTO allspice_recipe_ingredient_links (recipe_id, ingredient_id, creator_id)
    // SELECT @RecipeId, @IngredientId, @CreatorId
    // WHERE NOT EXISTS (
    //   SELECT 1 FROM allspice_recipe_ingredient_links
    //   WHERE recipe_id = @RecipeId AND ingredient_id = @IngredientId);

    // SELECT * FROM allspice_recipe_ingredient_links WHERE id = LAST_INSERT_ID();";

    // return _db.Query<RecipeIngredientLink>(sql, recipeIngredientLinkData).ToList();
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