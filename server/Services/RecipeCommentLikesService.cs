namespace allspice.Services;

public class RecipeCommentLikesService
{
  private readonly RecipeCommentLikesRepository _repo;

  public RecipeCommentLikesService(RecipeCommentLikesRepository repo)
  {
    _repo = repo;
  }
}