namespace allspice.Repositories;

public class ProfilesRepository
{
  // NOTE 💉 Dependency injections.

  private readonly IDbConnection _db;

  // NOTE 🏗️ Class constructor.

  public ProfilesRepository(IDbConnection db)
  {
    _db = db;
  }
}