import { AppState } from "@/AppState.js"
import { logger } from "@/utils/Logger.js"

export function toggleCreateCommentForm() {
  AppState.createCommentFormToggle = !AppState.createCommentFormToggle
  logger.log("createCommentFormToggle is now ", AppState.createCommentFormToggle)
}

