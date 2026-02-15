namespace allspice.Services;

public class RecipeCommentsService
{
  private readonly RecipeCommentsRepository _repo;

  public RecipeCommentsService(RecipeCommentsRepository repo)
  {
    _repo = repo;
  }

  public RecipeComment Create(RecipeComment recipeCommentData)
  {
    return _repo.Create(recipeCommentData);
  }

  public List<RecipeComment> GetByRecipeId(int recipeId)
  {
    return _repo.GetByRecipeId(recipeId);
  }
}