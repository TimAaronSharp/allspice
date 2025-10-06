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

  // NOTE 🔍🧑‍🦲 Get profile by id method. Sends the profile id to ProfilesRepository to retrieve profile from database.

  public Profile GetById(string profileId)
  {
    Profile profile = _repo.GetById(profileId);

    if (profile == null)
    {
      throw new Exception($"Invalid profile id: {profileId}");
    }
    return profile;
  }
}