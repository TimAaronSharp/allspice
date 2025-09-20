


namespace allspice.Repositories;

public class RecipesRepository
{

  public RecipesRepository(IDbConnection db)
  {
    _db = db;
  }
  private readonly IDbConnection _db;

  public Recipe Create(Recipe recipeData)
  {
    string sql = @"
    INSERT INTO
    allspice_recipes(title, instructions, img, category, creator_id)
    VALUES(@Title, @Instructions, @Img, @Category, @CreatorId);
    
    SELECT
    allspice_recipes.*,
    accounts.*
    FROM allspice_recipes
    INNER JOIN accounts ON accounts.id = allspice_recipes.creator_id
    WHERE allspice_recipes.id = LAST_INSERT_ID();";

    return _db.Query(sql, (Recipe recipe, Profile account) =>
    {
      recipe.Creator = account;
      return recipe;
    }, recipeData).SingleOrDefault();
  }

  public void Edit(Recipe recipeToUpdateData)
  {
    string sql = @"
    UPDATE allspice_recipes
    SET
    title = @Title,
    instructions = @Instructions,
    img = @Img,
    category = @Category
    WHERE id = @id LIMIT 1;";

    int rowsAffected = _db.Execute(sql, recipeToUpdateData);

    if (rowsAffected != 1)
    {
      throw new Exception($"{rowsAffected} were updated, which means your code is bad and you should feel bad. -Dr. Johnathan Alfred Zoidberg");
    }
  }

  public List<Recipe> GetAll()
  {
    string sql = @"
    SELECT
    allspice_recipes.*,
    accounts.*
    FROM allspice_recipes
    INNER JOIN accounts ON accounts.id = allspice_recipes.creator_id;";

    return _db.Query(sql, (Recipe recipe, Profile account) =>
    {
      recipe.Creator = account;
      return recipe;
    }).ToList();
  }

  public Recipe GetById(int recipeId)
  {
    string sql = @"
    SELECT
    allspice_recipes.*,
    accounts.*
    FROM allspice_recipes
    INNER JOIN accounts ON accounts.id = allspice_recipes.creator_id
    WHERE allspice_recipes.id = @recipeId;";

    return _db.Query(sql, (Recipe recipe, Profile account) =>
    {
      recipe.Creator = account;
      return recipe;
    }, new { recipeId }).SingleOrDefault();
  }
}