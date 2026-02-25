import { AppState } from "@/AppState.js";


export function toggleEditRecipeCommentForm() {
  AppState.editRecipeCommentFormToggle = !AppState.editRecipeCommentFormToggle
}