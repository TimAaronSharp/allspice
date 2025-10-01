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

  // NOTE 🛠️ Create ingredient method. Creates a new ingredient in the database. Name + Quantity create a unique key in the allspice_ingredients table to prevent ingredient duplication to save database storage space. 
  // When ingredient is being created it checks if the unique key (Name + Quantity) already exists and if it does it updates and returns the id. 
  // A join table will be used to connect recipes with ingredients by recipe_id/ingredient_id.

  public Ingredient Create(Ingredient ingredientData)
  {
    string sql = @"
    INSERT INTO allspice_ingredients (name, quantity, recipe_id)
    VALUES (@Name, @quantity, @RecipeId)
    ON DUPLICATE KEY UPDATE id = LAST_INSERT_ID(id);

    SELECT * FROM allspice_ingredients where id = LAST_INSERT_ID();";

    return _db.Query<Ingredient>(sql, ingredientData).SingleOrDefault();
  }

  public void Delete(int ingredientId)
  {
    string sql = "DELETE FROM allspice_ingredients WHERE id = @ingredientId LIMIT 1;";

    int rowsAffected = _db.Execute(sql, new { ingredientId });

    if (rowsAffected != 1)
    {
      throw new Exception($"{rowsAffected} ingredients were deleted, which means your code is bad and you should feel bad. -Dr. Johnathan Alfred Zoidberg");
    }
  }

  public void Edit(Ingredient ingredientToUpdate)
  {
    string sql = @"
    UPDATE allspice_ingredients
    SET
    name = @Name,
    quantity = @Quantity
    WHERE id = @id LIMIT 1;";

    int rowsAffected = _db.Execute(sql, ingredientToUpdate);

    if (rowsAffected != 1)
    {
      throw new Exception($"{rowsAffected} ingredients were update, which means your code is bad and you should feel bad. -Dr. Johnathan Alfred Zoidberg");
    }
  }

  public Ingredient GetById(int ingredientId)
  {
    string sql = "SELECT * FROM allspice_ingredients WHERE allspice_ingredients.id = @ingredientId;";

    return _db.Query<Ingredient>(sql, new { ingredientId }).SingleOrDefault();
  }

  public List<Ingredient> GetByRecipeId(int recipeId)
  {
    string sql = @"
    SELECT
    allspice_ingredients.*,
    allspice_recipes.*
    FROM allspice_ingredients
    INNER JOIN allspice_recipes ON allspice_recipes.id = allspice_ingredients.recipe_id
    WHERE allspice_ingredients.recipe_id = @recipeId;";

    return _db.Query(sql, (Ingredient ingredient, Recipe recipe) =>
    {
      ingredient.RecipeId = recipe.Id;
      return ingredient;
    }, new { recipeId }).ToList();
  }
}