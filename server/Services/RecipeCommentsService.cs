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

  public string Delete(int recipeCommentId, Profile userInfo)
  {
    RecipeComment recipeComment = GetById(recipeCommentId);

    if (recipeComment.CreatorId != userInfo.Id)
    {
      throw new Exception($"You cannot delete another user's recipe comment, {userInfo.Name}".ToUpper());
    }
    _repo.Delete(recipeCommentId);
    return $"Recipe Comment id :{recipeCommentId} has been deleted. You monster.";
  }

  private RecipeComment GetById(int recipeCommentId)
  {
    RecipeComment foundRecipeComment = _repo.GetById(recipeCommentId);

    if (foundRecipeComment == null)
    {
      throw new Exception($"Invalid recipe comment id: {recipeCommentId}");
    }

    return foundRecipeComment;
  }

  public List<RecipeComment> GetByRecipeId(int recipeId)
  {
    return _repo.GetByRecipeId(recipeId);
  }
}