using Microsoft.AspNetCore.Http.Features;

namespace allspice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IngredientsController : ControllerBase
{
  // NOTE 💉 Dependency injections.

  private readonly IngredientsService _ingredientsService;
  private readonly Auth0Provider _auth0Provider;

  // NOTE 🏗️ Class constructor.

  public IngredientsController(IngredientsService ingredientsService, Auth0Provider auth0Provider)
  {
    _ingredientsService = ingredientsService;
    _auth0Provider = auth0Provider;
  }

  // NOTE 🛠️ Create ingredient method. Gets user info for authentication in service.

  [Authorize]
  [HttpPost]
  public async Task<ActionResult<Ingredient>> Create([FromBody] Ingredient ingredientData, int recipeId)
  {
    try
    {
      Profile userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      return Ok(_ingredientsService.Create(ingredientData, recipeId, userInfo));
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  // NOTE 💣 Delete Ingredient method. Gets user info for authentication.

  // [Authorize]
  // [HttpDelete("{ingredientId}")]
  // public async Task<ActionResult<string>> Delete(int ingredientId)
  // {
  //   try
  //   {
  //     Profile userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
  //     return Ok(_ingredientsService.Delete(ingredientId, userInfo));
  //   }
  //   catch (Exception exception)
  //   {
  //     return BadRequest(exception.Message);
  //   }
  // }
}