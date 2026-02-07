namespace allspice.Services;

public class RecipeNotesService
{
  private readonly RecipeNotesRepository _repo;

  public RecipeNotesService(RecipeNotesRepository repo)
  {
    _repo = repo;
  }

  public RecipeNote Create(RecipeNote recipeNoteData)
  {
    return _repo.Create(recipeNoteData);
  }
  public RecipeNote Edit(RecipeNote editedRecipeNoteData, Profile userInfo)
  {
    RecipeNote recipeNoteToEdit = GetByRecipeIdAndAccountId(editedRecipeNoteData.RecipeId, editedRecipeNoteData.AccountId);

    if (recipeNoteToEdit.AccountId != userInfo.Id)
    {
      throw new Exception($"You cannot edit another user's recipe note, {userInfo.Name}.".ToUpper());
    }

    recipeNoteToEdit.Body = editedRecipeNoteData.Body ?? recipeNoteToEdit.Body;

    _repo.Edit(recipeNoteToEdit);
    return recipeNoteToEdit;
  }

  private RecipeNote GetById(int recipeId)
  {
    RecipeNote foundRecipeNote = _repo.GetById(recipeId);

    if (foundRecipeNote == null)
    {
      throw new Exception($"Invalid recipe note id: {recipeId}");
    }

    return foundRecipeNote;
  }

  public RecipeNote GetByRecipeIdAndAccountId(int recipeId, string accountId)
  {
    RecipeNote recipeNote = _repo.GetByRecipeIdAndAccountId(recipeId, accountId);
    return recipeNote;
  }
}