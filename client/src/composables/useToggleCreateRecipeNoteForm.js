import { AppState } from "@/AppState.js";
import { logger } from "@/utils/Logger.js";

// NOTE 🔁 Toggles AppState.createRecipeNotesFormToggle for CreateRecipeNoteForm.vue rendering.
export function toggleCreateRecipeNoteForm(){
  AppState.createRecipeNoteFormToggle = !AppState.createRecipeNoteFormToggle
  logger.log("createRecipeNoteFormToggle is now ", AppState.createRecipeNoteFormToggle)
}