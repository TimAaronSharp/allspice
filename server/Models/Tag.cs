namespace allspice.Models;

public class Tag : RepoItem<int>
{
  public string Name { get; set; }
}

public class RecipeTag : Tag
{
  public int RecipeId { get; set; }
  public int TagId { get; set; }
}