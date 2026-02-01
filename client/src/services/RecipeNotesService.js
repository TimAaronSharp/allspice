import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { RecipeNote } from "@/models/RecipeNote.js"
import { AppState } from "@/AppState.js"


class RecipeNotesService {
  async create(recipeNoteData){
    AppState.activeRecipeNote = null
    const res = await api.post("api/recipeNotes", recipeNoteData)
    return this.makeRecipeNotes(res.data)
  }

  makeRecipeNotes(recipeNoteData){
    return AppState.activeRecipeNote = new RecipeNote(recipeNoteData)
  }
}

export const recipeNotesService = new RecipeNotesService()