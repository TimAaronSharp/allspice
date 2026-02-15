import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { RecipeNote } from "@/models/RecipeNote.js"
import { AppState } from "@/AppState.js"


class RecipeNotesService {
  async create(recipeNoteData){
    const res = await api.post("api/recipeNotes", recipeNoteData)
    return this.makeRecipeNotes(res.data)
  }

  async delete(recipeNoteId){
    const res = await api.delete(`api/recipeNotes/${recipeNoteId}`)
    // this.makeRecipeNotes(res.data)
    AppState.activeRecipeNote = null
  }

  async edit(editedRecipeNoteData){
    const recipeId = editedRecipeNoteData.recipeId
    const res = await api.put(`api/recipeNotes/${recipeId}`, editedRecipeNoteData)
    // logger.log("RecipeNotesService.edit() returned ", res.data)
    this.makeRecipeNotes(res.data)
    // logger.log("makeRecipeNotes after edit() returned ", madeEditedRecipe)
  }

  async getByRecipeIdAndAccountId(recipeId){
    AppState.activeRecipeNote = null
    const res = await api.get(`api/recipes/${recipeId}/recipeNotes`)
    // logger.log("getByRecipeIdAndAccountId() returned ", res.data)
    this.makeRecipeNotes(res.data)
    // logger.log("recipeNote was created as ", recipeNote)
  }

  makeRecipeNotes(recipeNoteData){
    if (recipeNoteData == "") return
    else if (recipeNoteData == null ) return AppState.activeRecipeNote == null
    return AppState.activeRecipeNote = new RecipeNote(recipeNoteData)
  }
}

export const recipeNotesService = new RecipeNotesService()