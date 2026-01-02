namespace allspice.Controllers;

[ApiController]
[Route("api/[controller]")]

public class CommentsController : ControllerBase
{
  private readonly CommentsService _commentsService;

  public CommentsController(CommentsService commentsService)
  {
    _commentsService = commentsService;
  }
}