namespace allspice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipeNotesController : ControllerBase
{
  private readonly RecipeNotesService _recipeNotesService;
  private readonly Auth0Provider _auth0Provider;

  public RecipeNotesController(RecipeNotesService recipeNotesService, Auth0Provider auth0Provider)
  {
    _recipeNotesService = recipeNotesService;
    _auth0Provider = auth0Provider;
  }
}