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

  public string Delete(int recipeNoteId, Profile userInfo)
  {
    RecipeNote recipeNoteToDelete = GetById(recipeNoteId);

    if (recipeNoteToDelete.AccountId != userInfo.Id)
    {
      throw new Exception($"You cannot delete another user's recipe note, {userInfo.Name}".ToUpper());
    }

    _repo.Delete(recipeNoteId);
    return $"Recipe note id :{recipeNoteId} has been deleted. You monster.";
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

  private RecipeNote GetById(int recipeNoteId)
  {
    RecipeNote foundRecipeNote = _repo.GetById(recipeNoteId);

    if (foundRecipeNote == null)
    {
      throw new Exception($"Invalid recipe note id: {recipeNoteId}");
    }

    return foundRecipeNote;
  }

  public RecipeNote GetByRecipeIdAndAccountId(int recipeId, string accountId)
  {
    RecipeNote recipeNote = _repo.GetByRecipeIdAndAccountId(recipeId, accountId);
    return recipeNote;
  }
}