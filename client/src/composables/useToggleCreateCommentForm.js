import { AppState } from "@/AppState.js"
import { logger } from "@/utils/Logger.js"

// NOTE 🔁 Toggles AppState.createCommentFormToggle for createCommentForm.vue rendering.
export function toggleCreateCommentForm() {
  AppState.createCommentFormToggle = !AppState.createCommentFormToggle
  logger.log("createCommentFormToggle is now ", AppState.createCommentFormToggle)
}

