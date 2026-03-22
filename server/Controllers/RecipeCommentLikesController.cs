namespace allspice.Controllers;

[ApiController]
[Route("api/[controller]")]

public class RecipeCommentLikesController : ControllerBase
{
  private readonly RecipeCommentLikesService _recipeCommentLikesService;
  private readonly Auth0Provider _auth0Provider;

  public RecipeCommentLikesController(RecipeCommentLikesService recipeCommentLikesService, Auth0Provider auth0Provider)
  {
    _recipeCommentLikesService = recipeCommentLikesService;
    _auth0Provider = auth0Provider;
  }

  [Authorize]
  [HttpPost]
  public async Task<ActionResult<string>> Create([FromBody] RecipeComment recipeCommentData)
  {
    try
    {
      Profile userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      return Ok(_recipeCommentLikesService.Create(recipeCommentData.Id, userInfo.Id));
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }
}