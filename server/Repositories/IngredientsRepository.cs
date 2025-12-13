using System.Text;

namespace allspice.Repositories;

public class IngredientsRepository
{
  // NOTE 💉 Dependency injections.

  private readonly IDbConnection _db;

  // NOTE 🏗️ Class constructor.

  public IngredientsRepository(IDbConnection db)
  {
    _db = db;
  }

  // NOTE 🛠️ Create ingredient method. Creates a new ingredient in the database. Name + Quantity create a unique key in the allspice_ingredients table to prevent ingredient duplication (WHERE NOT EXISTS checks for the unique key, if it does not exist it will create the new ingredient). 
  // A join table will be used to connect recipes with ingredients by recipe_id/ingredient_id.

  public List<Ingredient> Create(List<Ingredient> ingredientData)
  {
    // NOTE Returns an empty list instead of erroring out.
    if (ingredientData == null || ingredientData.Count == 0)
    {
      return new List<Ingredient>();
    }

    var sql = new StringBuilder();
    var valueParams = new DynamicParameters();

    for (int i = 0; i < ingredientData.Count; i++)
    {
      sql.AppendLine($@"
        INSERT INTO allspice_ingredients (name, quantity, origin_recipe_id)
        SELECT @Name{i}, @Quantity{i}, @OriginRecipeId{i}
        WHERE NOT EXISTS (
          SELECT 1 FROM allspice_ingredients
          WHERE name = @Name{i} AND quantity = @Quantity{i});");

      valueParams.Add($"@Name{i}", ingredientData[i].Name);
      valueParams.Add($"@Quantity{i}", ingredientData[i].Quantity);
      valueParams.Add($"@OriginRecipeId{i}", ingredientData[i].OriginRecipeId);
    }

    sql.AppendLine("SELECT * FROM allspice_ingredients WHERE ");

    for (int i = 0; i < ingredientData.Count; i++)
    {
      if (i > 0)
      {
        sql.Append(" OR ");
      }
      sql.Append($"(name = @Name{i} AND quantity = @Quantity{i})");
    }

    return _db.Query<Ingredient>(sql.ToString(), valueParams).ToList();

    // string sql = @"
    // INSERT INTO allspice_ingredients (name, quantity, origin_recipe_id)
    // SELECT @Name, @Quantity, @OriginRecipeId
    // WHERE NOT EXISTS (
    //   SELECT 1 FROM allspice_ingredients
    //   WHERE name = @Name AND quantity = @Quantity);

    // SELECT * FROM allspice_ingredients where id = LAST_INSERT_ID();";

    // return _db.Query<Ingredient>(sql, ingredientData).ToList();
  }

  // NOTE 💣 Delete Ingredient method. Deletes ingredient from database.

  public void Delete(int ingredientId)
  {
    string sql = "DELETE FROM allspice_ingredients WHERE id = @ingredientId LIMIT 1;";

    int rowsAffected = _db.Execute(sql, new { ingredientId });

    if (rowsAffected != 1)
    {
      throw new Exception($"{rowsAffected} ingredients were deleted, which means your code is bad and you should feel bad. -Dr. Johnathan Alfred Zoidberg");
    }
  }

  // NOTE 🔍🧩 Get ingredient by id method. Queries database for ingredient by its id.

  public Ingredient GetById(int ingredientId)
  {
    string sql = "SELECT * FROM allspice_ingredients WHERE allspice_ingredients.id = @ingredientId;";

    return _db.Query<Ingredient>(sql, new { ingredientId }).SingleOrDefault();
  }

  // NOTE 🔍🧩📓 Get ingredients by recipe id. Queries database for all ingredients that have the recipeId.

  // public List<Ingredient> GetByRecipeId(List<int> ingredientIds)
  // {
  //   // NOTE Shouldn't ever happen organically, but just in case.
  //   if (ingredientIds == null || ingredientIds.Count == 0)
  //   {
  //     return new List<Ingredient>();
  //   }

  //   string sql = @"
  //   SELECT *
  //   FROM allspice_ingredients 
  //   WHERE allspice_ingredients.id IN @ingredientIds;";

  //   return _db.Query<Ingredient>(sql, new { ingredientIds }).ToList();
  // }
}

// SELECT
//     allspice_ingredients.*,
//     allspice_recipes.*
//     FROM allspice_ingredients
//     INNER JOIN allspice_recipes ON allspice_recipes.id = allspice_ingredients.recipe_id
//     WHERE allspice_ingredients.recipe_id = @recipeId;