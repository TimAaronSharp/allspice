using System.Runtime.CompilerServices;

namespace allspice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipesController : ControllerBase
{

  private readonly RecipesService _recipesService;
  private readonly Auth0Provider _auth0Provider;
  private readonly RecipeIngredientLinksService _recipeIngredientLinksService;
  private readonly CommentsService _commentsService;

  public RecipesController(RecipesService recipesService, Auth0Provider auth0Provider, RecipeIngredientLinksService recipeIngredientLinksService, CommentsService commentsService)
  {
    _recipesService = recipesService;
    _auth0Provider = auth0Provider;
    _recipeIngredientLinksService = recipeIngredientLinksService;
    _commentsService = commentsService;
  }

  [Authorize]
  [HttpPost]

  public async Task<ActionResult<Recipe>> Create([FromBody] Recipe recipeData)
  {
    try
    {
      Profile userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      recipeData.CreatorId = userInfo.Id;
      return Ok(_recipesService.Create(recipeData));
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [Authorize]
  [HttpDelete("{recipeId}")]
  public async Task<ActionResult<string>> Delete(int recipeId)
  {
    try
    {
      Profile userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      return Ok(_recipesService.Delete(recipeId, userInfo));
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [Authorize]
  [HttpPut("{recipeId}")]
  public async Task<ActionResult<Recipe>> Edit([FromBody] Recipe updateRecipeData, int recipeId)
  {
    try
    {
      Profile userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      return Ok(_recipesService.Edit(userInfo, updateRecipeData, recipeId));
    }
    catch (Exception exception)
    {

      return BadRequest(exception.Message);
    }
  }

  [HttpGet]
  public ActionResult<List<Recipe>> GetAll()
  {
    try
    {
      return _recipesService.GetAll();
    }
    catch (Exception exception)
    {

      return BadRequest(exception.Message);
    }
  }

  [HttpGet("{recipeId}")]
  public ActionResult<Recipe> GetById(int recipeId)
  {
    try
    {
      return _recipesService.GetById(recipeId);
    }
    catch (Exception exception)
    {

      return BadRequest(exception.Message);
    }
  }

  // NOTE 🔍🧺 Get enum list of recipe categories from database (allspice_recipes table).
  [HttpGet("categories")]
  public ActionResult<List<string>> GetCategories()
  {
    try
    {
      return _recipesService.GetCategories();
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [HttpGet("{recipeId}/comments")]
  public ActionResult<List<Comment>> GetCommentsByRecipeId(int recipeId)
  {
    try
    {
      return Ok(_commentsService.GetByRecipeId(recipeId));
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [HttpGet("{recipeId}/ingredients")]

  public ActionResult<List<Ingredient>> GetIngredientsByRecipeId(int recipeId)
  {
    try
    {
      return Ok(_recipeIngredientLinksService.GetRecipeIngredientsByRecipeId(recipeId));
    }
    catch (Exception exception)
    {

      return BadRequest(exception.Message);
    }
  }
}