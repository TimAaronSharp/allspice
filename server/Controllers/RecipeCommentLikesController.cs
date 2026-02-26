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
}