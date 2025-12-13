namespace allspice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipeIngredientLinksController : ControllerBase
{
  private readonly RecipeIngredientLinksService _RecipeIngredientLinksService;
  private readonly Auth0Provider _auth0Provider;

  public RecipeIngredientLinksController(RecipeIngredientLinksService RecipeIngredientLinksService, Auth0Provider auth0Provider)
  {
    _RecipeIngredientLinksService = RecipeIngredientLinksService;
    _auth0Provider = auth0Provider;
  }

  [Authorize]
  [HttpPost]
  public async Task<ActionResult<List<RecipeIngredientLink>>> Create([FromBody] List<RecipeIngredientLink> recipeIngredientLinkData)
  {
    try
    {
      Profile userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      recipeIngredientLinkData.ForEach(recipeIngredientLink => recipeIngredientLink.CreatorId = userInfo.Id);
      return Ok(_RecipeIngredientLinksService.Create(recipeIngredientLinkData, userInfo));
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [Authorize]
  [HttpDelete("{recipeIngredientLinkId}")]
  public async Task<ActionResult<string>> Delete(int recipeIngredientLinkId)
  {
    try
    {
      Profile userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      return Ok(_RecipeIngredientLinksService.Delete(recipeIngredientLinkId, userInfo));
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }
}