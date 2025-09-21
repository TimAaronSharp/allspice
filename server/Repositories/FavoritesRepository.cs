namespace allspice.Repositories;

public class FavoritesRepository
{
  private readonly IDbConnection _dbConnection;
  public FavoritesRepository(IDbConnection dbConnection)
  {
    _dbConnection = dbConnection;
  }
}