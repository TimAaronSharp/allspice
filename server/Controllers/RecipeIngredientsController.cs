namespace allspice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipeIngredientsController : ControllerBase
{
  private readonly RecipeIngredientsService _recipeIngredientsService;
  private readonly Auth0Provider _auth0Provider;

  public RecipeIngredientsController(RecipeIngredientsService recipeIngredientsService, Auth0Provider auth0Provider)
  {
    _recipeIngredientsService = recipeIngredientsService;
    _auth0Provider = auth0Provider;
  }

}