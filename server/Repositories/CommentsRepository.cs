namespace allspice.Repositories;

public class CommentsRepository
{
  private readonly IDbConnection _db;

  public CommentsRepository(IDbConnection db)
  {
    _db = db;
  }

  public Comment Create(Comment commentData)
  {
    string sql = @"
    INSERT INTO
    allspice_comments(body, creator_id)
    VALUES(@Body, @CreatorId);
    
    SELECT * FROM allspice_comments WHERE id = LAST_INSERT_ID();";

    return _db.Query<Comment>(sql, commentData).SingleOrDefault();
  }
}