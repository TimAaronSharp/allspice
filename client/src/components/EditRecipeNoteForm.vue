<script setup>
import { AppState } from '@/AppState.js';
import { toggleEditRecipeNoteForm } from '@/composables/useToggleEditRecipeNoteForm.js';
import { validateWhiteSpace } from '@/composables/useValidateWhiteSpace.js';
import { Recipe } from '@/models/Recipe.js';
import { recipeNotesService } from '@/services/RecipeNotesService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed, ref } from 'vue';

const recipeNote = computed(() => AppState.activeRecipeNote)

const props = defineProps({
  recipeProp: { type: Recipe, required: true }
})

const editableRecipeNoteData = ref({
  body: recipeNote.value.body
})

async function editRecipeNote(event) {
  try {
    const validatedForm = validateWhiteSpace(event)
    if (validatedForm === "") return
    await recipeNotesService.edit(editableRecipeNoteData.value, recipeNote.value.id)
    editableRecipeNoteData.value.body = ""
    toggleEditRecipeNoteForm()
  }
  catch (error) {
    Pop.error(error, "Could not edit recipe note.");
    logger.error("Could not edit recipe note.".toUpperCase(), error)
  }
}

</script>


<template>
  <form @submit.prevent="editRecipeNote($event)">
    <textarea v-model="editableRecipeNoteData.body" name="recipe-note-body-edit"
      id="recipe-note-body-edit-field"></textarea>
    <div>
      <button type="submit" class="btn btn-outline-primary">Edit Note</button>
      <button type="button" @click="toggleEditRecipeNoteForm()">Cancel</button>
    </div>
  </form>
</template>


<style lang="scss" scoped></style>