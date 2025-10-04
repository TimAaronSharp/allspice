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

  // NOTE 🔍🧑‍🦲 Get profile by id method. Queries database (accounts table) for profile by id.

  public Profile GetById(string profileId)
  {
    string sql = "SELECT * FROM accounts WHERE id = @profileId LIMIT 1;";

    return _db.Query<Profile>(sql, new { profileId }).SingleOrDefault();
  }
}