namespace allspice.Services;

public class CommentsService
{
  private readonly CommentsRepository _repo;

  public CommentsService(CommentsRepository repo)
  {
    _repo = repo;
  }

  public Comment Create(Comment commentData)
  {
    return _repo.Create(commentData);
  }
}