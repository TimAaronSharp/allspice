import { AppState } from "@/AppState.js";


export function toggleEditRecipeNoteForm() {
  AppState.editRecipeNoteFormToggle = !AppState.editRecipeNoteFormToggle
}