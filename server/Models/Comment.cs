using System.ComponentModel.DataAnnotations;

namespace allspice.Models;

public class Comment : RepoItem<int>
{
  [MaxLength(5000)] public string Body { get; set; }
  [MaxLength(255)] public string CreatorId { get; set; }
  public Profile Creator { get; set; }
  public List<string> LikeIds { get; set; }
}