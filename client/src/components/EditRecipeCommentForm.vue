<script setup>
import { AppState } from '@/AppState.js';
import { computed, ref } from 'vue';
import { useRoute } from 'vue-router';
import RecipeCommentCard from './RecipeCommentCard.vue';
import { RecipeComment } from '@/models/RecipeComment.js';
import { toggleEditRecipeCommentForm } from '@/composables/useToggleEditRecipeCommentForm.js';
import { Pop } from '@/utils/Pop.js';
import { logger } from '@/utils/Logger.js';
import { validateWhiteSpace } from '@/composables/useValidateWhiteSpace.js';
import { recipeCommentsService } from '@/services/RecipeCommentsService.js';


// const recipeComment = computed(() => AppState.activeRecipeComment)
const route = useRoute()
// const recipeId = Number(route.params.recipeId)

const props = defineProps({
  editRecipeCommentProp: { type: RecipeComment, required: true }
})

const editableRecipeCommentData = ref({
  body: props.editRecipeCommentProp.body,
  id: props.editRecipeCommentProp.id
})

async function editRecipeComment(event) {
  try {
    if (editableRecipeCommentData.value.body === props.editRecipeCommentProp.body) {
      Pop.toast("Edit was not different than original comment. No change made.")
      return
    }
    const validatedForm = validateWhiteSpace(event)
    if (validatedForm === "") return
    await recipeCommentsService.edit(editableRecipeCommentData.value)
    editableRecipeCommentData.value.body = ""
    toggleEditRecipeCommentForm()
    AppState.activeRecipeComment = null
  }
  catch (error) {
    Pop.error(error, "Could not edit recipe comment.");
    logger.error("Could not edit recipe comment.".toUpperCase(), error)
  }
}


</script>


<template>
  <form @submit.prevent="editRecipeComment($event)">
    <textarea v-model="editableRecipeCommentData.body" name="recipe-comment-body-edit"
      id="recipe-comment-body-edit-field"></textarea>
    <div>
      <button type="submit" class="btn btn-outline-primary">Edit Comment</button>
      <button type="button" @click="toggleEditRecipeCommentForm()">Cancel</button>
    </div>
  </form>
</template>


<style lang="scss" scoped></style>