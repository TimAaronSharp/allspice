

namespace allspice.Repositories;

public class IngredientsRepository
{
  public IngredientsRepository(IDbConnection db)
  {
    _db = db;
  }
  private readonly IDbConnection _db;

  public Ingredient Create(Ingredient ingredientData)
  {
    string sql = @"
    INSERT INTO
    allspice_ingredients(name, quantity, recipe_id, creator_id)
    VALUES(@Name, @Quantity, @RecipeId, @CreatorId);
    
    SELECT
    allspice_ingredients.*,
    accounts.*
    FROM allspice_ingredients
    INNER JOIN accounts ON accounts.id = allspice_ingredients.creator_id
    WHERE allspice_ingredients.id = LAST_INSERT_ID();";

    return _db.Query(sql, (Ingredient ingredient, Profile account) =>
    {
      ingredient.Creator = account;
      return ingredient;
    }, ingredientData).SingleOrDefault();
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