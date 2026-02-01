import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { RecipeNote } from "@/models/RecipeNote.js"
import { AppState } from "@/AppState.js"


class RecipeNotesService {
  async create(recipeNoteData){
    const res = await api.post("api/recipeNotes", recipeNoteData)
    return this.makeRecipeNotes(res.data)
  }

  async getByRecipeIdAndAccountId(recipeId){
    AppState.activeRecipeNote = null
    const res = await api.get(`api/recipes/${recipeId}/recipe-notes`)
    logger.log("getByRecipeIdAndAccountId() returned ", res.data)
    const recipeNote = this.makeRecipeNotes(res.data)
    logger.log("recipeNote was created as ", recipeNote)
  }

  makeRecipeNotes(recipeNoteData){
    if (recipeNoteData == null || recipeNoteData == "") {
      return
    }
    return AppState.activeRecipeNote = new RecipeNote(recipeNoteData)
  }
}

export const recipeNotesService = new RecipeNotesService()