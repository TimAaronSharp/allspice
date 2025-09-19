namespace allspice.Models;

public class Comment : RepoItem<int>
{
  public string Body { get; set; }
  public string CreatorId { get; set; }
  public Profile Creator { get; set; }
  public List<string> LikeIds { get; set; }
}