import { AppState } from "@/AppState.js"
import { logger } from "@/utils/Logger.js"

// NOTE 🔁 Toggles AppState.createRecipeCommentFormToggle for CreateRecipeCommentForm.vue rendering.
export function toggleCreateRecipeCommentForm() {
  AppState.createRecipeCommentFormToggle = !AppState.createRecipeCommentFormToggle
  logger.log("createRecipeCommentFormToggle is now ", AppState.createRecipeCommentFormToggle)
}

