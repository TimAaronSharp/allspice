namespace allspice.Services;

public class RecipeCommentLikesService
{
  private readonly RecipeCommentLikesRepository _repo;

  public RecipeCommentLikesService(RecipeCommentLikesRepository repo)
  {
    _repo = repo;
  }

  public string Create(int recipeCommentId, string userId)
  {
    _repo.Create(recipeCommentId, userId);
    return "Comment has been liked!";
  }
}