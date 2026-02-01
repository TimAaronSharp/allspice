namespace allspice.Controllers;

[ApiController]
[Route("api/[controller]")]

public class CommentsController : ControllerBase
{
  private readonly CommentsService _commentsService;
  private readonly Auth0Provider _auth0Provider;

  public CommentsController(CommentsService commentsService, Auth0Provider auth0Provider)
  {
    _commentsService = commentsService;
    _auth0Provider = auth0Provider;
  }

  [Authorize]
  [HttpPost]
  public async Task<ActionResult<Comment>> Create([FromBody] Comment commentData)
  {
    try
    {
      Profile userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      commentData.CreatorId = userInfo.Id;
      return Ok(_commentsService.Create(commentData));
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }
}