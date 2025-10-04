namespace allspice.Services;

public class ProfilesService
{
  // NOTE 💉 Dependency injections.

  private readonly ProfilesRepository _repo;

  // NOTE 🏗️ Class constructor.

  public ProfilesService(ProfilesRepository repo)
  {
    _repo = repo;
  }
}