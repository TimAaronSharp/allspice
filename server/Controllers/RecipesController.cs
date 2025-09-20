using System.Runtime.CompilerServices;

namespace allspice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipesController : ControllerBase
{
  public RecipesController(RecipesService recipesService, Auth0Provider auth0Provider)
  {
    _recipesService = recipesService;
    _auth0Provider = auth0Provider;
  }
  private readonly RecipesService _recipesService;
  private readonly Auth0Provider _auth0Provider;

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
}