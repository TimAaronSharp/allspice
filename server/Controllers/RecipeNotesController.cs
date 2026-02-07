namespace allspice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipeNotesController : ControllerBase
{
  private readonly RecipeNotesService _recipeNotesService;
  private readonly Auth0Provider _auth0Provider;

  public RecipeNotesController(RecipeNotesService recipeNotesService, Auth0Provider auth0Provider)
  {
    _recipeNotesService = recipeNotesService;
    _auth0Provider = auth0Provider;
  }

  [Authorize]
  [HttpPost]
  public async Task<ActionResult<RecipeNote>> Create([FromBody] RecipeNote recipeNoteData)
  {
    try
    {
      Profile userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      recipeNoteData.AccountId = userInfo.Id;
      return Ok(_recipeNotesService.Create(recipeNoteData));
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [Authorize]
  [HttpDelete("{recipeNoteId}")]
  public async Task<ActionResult<string>> Delete(int recipeNoteId)
  {
    try
    {
      Profile userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      return Ok(_recipeNotesService.Delete(recipeNoteId, userInfo));
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [Authorize]
  [HttpPut("{recipeNoteId}")]
  public async Task<ActionResult<RecipeNote>> Edit([FromBody] RecipeNote editedRecipeNoteData)
  {
    try
    {
      Profile userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      editedRecipeNoteData.AccountId = userInfo.Id;
      return Ok(_recipeNotesService.Edit(editedRecipeNoteData, userInfo));
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }
}