<script setup>
import { toggleCreateRecipeNoteForm } from '@/composables/useToggleCreateRecipeNoteForm.js';
import { validateWhiteSpace } from '@/composables/useValidateWhiteSpace.js';
import { Recipe } from '@/models/Recipe.js';
import { recipeNotesService } from '@/services/RecipeNotesService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { ref } from 'vue';

const props = defineProps({
  recipeProp: { type: Recipe, required: true }
})

const editableRecipeNoteData = ref({
  body: "",
  recipeId: props.recipeProp.id
})

async function createRecipeNote(event) {
  try {
    const validatedForm = validateWhiteSpace(event)
    if (validatedForm === "") return
    await recipeNotesService.create(editableRecipeNoteData.value)
    editableRecipeNoteData.value.body = ""
    toggleCreateRecipeNoteForm()
  }
  catch (error) {
    Pop.error(error, "Could not create recipe note.");
    logger.error("Could not create recipe note.".toUpperCase(), error)
  }
}

</script>


<template>
  <form @submit.prevent="createRecipeNote($event)" id="create-recipe-note-form">
    <label for="recipe-note-body-field" class="form-label">Recipe Note:</label>
    <h6>Only you will see this.</h6>
    <textarea v-model="editableRecipeNoteData.body" name="recipe-note-body" id="recipe-note-body-field"></textarea>
    <div>
      <button type="submit" class="btn btn-outline-primary">Create Note</button>
      <button type="button" @click="toggleCreateRecipeNoteForm" class="btn btn-outline-danger">Cancel</button>
    </div>
  </form>
</template>


<style lang="scss" scoped></style>