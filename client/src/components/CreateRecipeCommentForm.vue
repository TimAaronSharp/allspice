<script setup>
import { Pop } from '@/utils/Pop.js';
import { onMounted, ref } from 'vue';
import { toggleCreateRecipeCommentForm } from '@/composables/useToggleCreateRecipeCommentForm.js'
import { logger } from '@/utils/Logger.js';
import { recipeCommentsService } from '@/services/RecipeCommentsService.js';
import { Recipe } from '@/models/Recipe.js';
import { validateWhiteSpace } from '@/composables/useValidateWhiteSpace.js';

const props = defineProps({
  recipeProp: { type: Recipe, required: true }
})

const editableRecipeCommentData = ref({
  body: "",
  recipeId: props.recipeProp.id
})

async function createRecipeComment(event) {
  try {
    // debugger
    const validatedForm = validateWhiteSpace(event)
    // debugger
    if (validatedForm === "") return
    await recipeCommentsService.create(editableRecipeCommentData.value)
    editableRecipeCommentData.value.body = ""
    toggleCreateRecipeCommentForm()
  }
  catch (error) {
    Pop.error(error, "Could not create recipe comment.");
    logger.error("Could not create recipe comment.".toUpperCase(), error)
  }

}
</script>


<template>
  <form @submit.prevent="createRecipeComment($event)" id="create-recipe-comment-form">
    <label for="recipe-comment-body-field" class="form-label">Comment:</label>
    <textarea v-model="editableRecipeCommentData.body" name="recipe-comment-body"
      id="recipe-comment-body-field"></textarea>
    <div>
      <button type="submit" class="btn btn-outline-primary">Post</button>
      <button type="button" @click="toggleCreateRecipeCommentForm()" class="btn btn-outline-danger">Cancel</button>
    </div>
  </form>
</template>


<style lang="scss" scoped></style>