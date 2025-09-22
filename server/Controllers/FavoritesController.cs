namespace allspice.Controllers;

[ApiController]
[Route("api/[controller]")]

public class FavoritesController : ControllerBase
{
  // NOTE 💉 Dependency injections.

  private readonly FavoritesService _favoritesService;
  private readonly Auth0Provider _auth0Provider;

  // NOTE 🏗️ Class constructor.

  public FavoritesController(FavoritesService favoritesService, Auth0Provider auth0Provider)
  {
    _favoritesService = favoritesService;
    _auth0Provider = auth0Provider;
  }
  // NOTE 🛠️ Create favorite method. Gets user info for authentication and sets favoriteData.AccountId = userInfo.Id to prevent user's from creating a favorite with another user's id.

  [Authorize]
  [HttpPost]
  public async Task<ActionResult<Favorite>> Create([FromBody] Favorite favoriteData)
  {
    try
    {
      Profile userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      favoriteData.AccountId = userInfo.Id;
      return Ok(_favoritesService.Create(favoriteData));
    }
    catch (Exception exception)
    {

      return BadRequest(exception.Message);
    }
  }

  // NOTE 💣 Delete favorite method. Gets user info for authentication.

  [Authorize]
  [HttpDelete("{favoriteId}")]
  public async Task<ActionResult<string>> Delete(int favoriteId)
  {
    try
    {
      Profile userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      return Ok(_favoritesService.Delete(favoriteId, userInfo));
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }
}