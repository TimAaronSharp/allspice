namespace allspice.Models;

public class Note : RepoItem<int>
{
  public int RecipeId { get; set; }
  public string Body { get; set; }
  public string CreatorId { get; set; }
}