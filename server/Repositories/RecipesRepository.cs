


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
    allspice_recipes(name, instructions, img, category, creator_id)
    VALUES(@Name, @Instructions, @Img, @Category, @CreatorId);
    
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

  public void Delete(int recipeId)
  {
    string sql = "DELETE FROM allspice_recipes WHERE id = @recipeId LIMIT 1;";

    int rowsAffected = _db.Execute(sql, new { recipeId });

    if (rowsAffected != 1)
    {
      throw new Exception($"{rowsAffected} were deleted, which means your code is bad and you should feel bad. -Dr. Johnathan Alfred Zoidberg");
    }
  }

  public void Edit(Recipe recipeToUpdateData)
  {
    string sql = @"
    UPDATE allspice_recipes
    SET
    name = @Name,
    instructions = @Instructions,
    img = @Img,
    description = @Description,
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

  public List<Recipe> GetByProfileId(string profileId)
  {
    string sql = @"
    SELECT
    allspice_recipes.*,
    accounts.*
    FROM allspice_recipes
    INNER JOIN accounts ON accounts.id = allspice_recipes.creator_id
    WHERE allspice_recipes.creator_id = @profileId;";

    return _db.Query(sql, (Recipe recipe, Profile account) =>
    {
      recipe.Creator = account;
      return recipe;
    }, new { profileId }).ToList();
  }

  // NOTE 🔍🧺 Get recipe categories method. Queries for the datatype of the "category" column in the "allspice_recipes" table in the database and returns the enum values as a single string. The string is parsed to extract the individual items and combine them into a list.
  public List<string> GetCategories()
  {
    string sql = @"
    SELECT COLUMN_TYPE
    FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_SCHEMA = 'confident_yeti_fccf_db'
    AND TABLE_NAME = 'allspice_recipes'
    AND COLUMN_NAME = 'category';";

    string enumString = _db.QuerySingleOrDefault<string>(sql);

    return enumString
        .Replace("enum(", "")
        .Replace(")", "")
        .Replace("'", "")
        .Split(',')
        .ToList();
  }
}