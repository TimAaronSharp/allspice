<script setup>
import { AppState } from '@/AppState.js';
import { toggleEditRecipeCommentForm } from '@/composables/useToggleEditRecipeCommentForm.js';
import { RecipeComment } from '@/models/RecipeComment.js';
import { recipeCommentsService } from '@/services/RecipeCommentsService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed } from 'vue';
import EditRecipeCommentForm from './EditRecipeCommentForm.vue';
import { recipeCommentLikesService } from '@/services/RecipeCommentLikesService.js';


const props = defineProps({
  recipeCommentProp: { type: RecipeComment, required: true }
})

const account = computed(() => AppState.account)
const activeRecipeComment = computed(() => AppState.activeRecipeComment)
const editRecipeCommentFormToggle = computed(() => AppState.editRecipeCommentFormToggle)
const recipeComments = computed(() => AppState.recipeComments)
const recipeCommentIndex = AppState.recipeComments.findIndex(recipeComment => recipeComment.id == props.recipeCommentProp.id)

// This is a createdAt vs updatedAt time comparison to render whether a comment has been edited. The logic is down in the html and works there but is erroring out when I declare it here because it can't read the unassigned properties. Will look into seeing if I can make this work to make the html easier to digest but it is currently working.
// const wasEdited = computed(() => new Date(recipeComments[recipeCommentIndex].updatedAt) > new Date(recipeComments[recipeCommentIndex].createdAt))

async function deleteRecipeComment() {
  try {
    const confirmed = await Pop.confirm("Are you sure you want to delete this comment?", "This action cannot be undone.", "Yes", "No")
    if (confirmed) await recipeCommentsService.delete(props.recipeCommentProp.id)
    Pop.toast("Comment has been deleted.")
  }
  catch (error) {
    Pop.error(error, "Could not delete recipe comment.");
    logger.error("Could not delete recipe comment.".toUpperCase(), error)
  }
}

async function likeRecipeComment() {
  try {
    await recipeCommentLikesService.create(props.recipeCommentProp.id)
  }
  catch (error) {
    Pop.error(error, "Could not like recipe comment.");
    logger.error("Could not like recipe comment.".toUpperCase(), error)
  }
}

function setActiveRecipeCommentToEdit() {
  AppState.activeRecipeComment = props.recipeCommentProp
  toggleEditRecipeCommentForm()
}

</script>


<template>
  <div class="d-flex">
    <img :src="recipeCommentProp.creator.picture" :alt="recipeCommentProp.creator.name + `'s profile picture.'`">
    <div class="d-flex flex-column">
      <div v-if="account?.id == recipeCommentProp.creatorId && !editRecipeCommentFormToggle">
        <button @click="deleteRecipeComment()" class="mdi mdi-close-circle text-red transparent-btn-style"></button>
        <button @click="setActiveRecipeCommentToEdit()" class="mdi mdi-pencil transparent-btn-style"></button>
      </div>
      <p>{{ recipeCommentProp.creator.name }}</p>
      <div
        v-if="new Date(recipeComments[recipeCommentIndex].updatedAt) > new Date(recipeComments[recipeCommentIndex].createdAt)"
        class="d-flex">
        <p>*Edited*</p>
      </div>
      <div
        v-if="account?.id == recipeCommentProp.creatorId && editRecipeCommentFormToggle && activeRecipeComment.id == recipeCommentProp.id">
        <EditRecipeCommentForm :editRecipeCommentProp="recipeCommentProp" />
      </div>
      <p v-else>{{ recipeCommentProp.body }}</p>
      <div>
        <button @click="likeRecipeComment()" type="button"
          class="mdi mdi-thumb-up-outline transparent-btn-style"></button>
      </div>
    </div>
  </div>
</template>


<style lang="scss" scoped></style>