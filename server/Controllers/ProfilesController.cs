namespace allspice.Controllers;

[ApiController]
[Route("api/[controller]")]

public class ProfilesController : ControllerBase
{
  // NOTE 💉 Dependency injections.

  private readonly ProfilesService _profilesService;
  private readonly Auth0Provider _auth0Provider;

  // NOTE 🏗️ Class constructor.

  public ProfilesController(ProfilesService profilesService, Auth0Provider auth0Provider)
  {
    _profilesService = profilesService;
    _auth0Provider = auth0Provider;
  }
}