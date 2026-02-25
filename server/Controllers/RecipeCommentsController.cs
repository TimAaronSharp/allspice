namespace allspice.Controllers;

[ApiController]
[Route("api/[controller]")]

public class RecipeCommentsController : ControllerBase
{
  private readonly RecipeCommentsService _recipeCommentsService;
  private readonly Auth0Provider _auth0Provider;

  public RecipeCommentsController(RecipeCommentsService recipeCommentsService, Auth0Provider auth0Provider)
  {
    _recipeCommentsService = recipeCommentsService;
    _auth0Provider = auth0Provider;
  }

  [Authorize]
  [HttpPost]
  public async Task<ActionResult<RecipeComment>> Create([FromBody] RecipeComment recipeCommentData)
  {
    try
    {
      Profile userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      recipeCommentData.CreatorId = userInfo.Id;
      return Ok(_recipeCommentsService.Create(recipeCommentData));
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [Authorize]
  [HttpDelete("{recipeCommentId}")]
  public async Task<ActionResult<string>> Delete(int recipeCommentId)
  {
    try
    {
      Profile userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      return Ok(_recipeCommentsService.Delete(recipeCommentId, userInfo));
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [Authorize]
  [HttpPut("{recipeCommentId}")]
  public async Task<ActionResult<RecipeComment>> Edit([FromBody] RecipeComment editedRecipeCommentData)
  {
    try
    {
      Profile userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      editedRecipeCommentData.CreatorId = userInfo.Id;
      return Ok(_recipeCommentsService.Edit(editedRecipeCommentData, userInfo));
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }
}