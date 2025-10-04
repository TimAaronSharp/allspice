namespace allspice.Controllers;

[ApiController]
[Route("api/[controller]")]

public class ProfilesController : ControllerBase
{
  // NOTE 💉 Dependency injections.

  private readonly ProfilesService _profilesService;
  private readonly Auth0Provider _auth0Provider;
  private readonly RecipesService _recipesService;

  // NOTE 🏗️ Class constructor.

  public ProfilesController(ProfilesService profilesService, Auth0Provider auth0Provider, RecipesService recipesService)
  {
    _profilesService = profilesService;
    _auth0Provider = auth0Provider;
    _recipesService = recipesService;
  }

  // NOTE 🔍🧑‍🦲 Get profile by id method. Sends the profile id to ProfilesService.

  [HttpGet("{profileId}")]
  public ActionResult<Profile> GetById(string profileId)
  {
    try
    {
      return Ok(_profilesService.GetById(profileId));
    }
    catch (Exception exception)
    {

      return BadRequest(exception.Message);
    }
  }

  [HttpGet("{profileId}/recipes")]
  public ActionResult<List<Recipe>> GetRecipesByProfileId(string profileId)
  {
    try
    {
      return Ok(_recipesService.GetByProfileId(profileId));
    }
    catch (Exception exception)
    {

      return BadRequest(exception.Message);
    }
  }
}