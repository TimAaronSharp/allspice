<script setup>
import { AppState } from '@/AppState.js';
import { toggleEditRecipeCommentForm } from '@/composables/useToggleEditRecipeCommentForm.js';
import { RecipeComment } from '@/models/RecipeComment.js';
import { recipeCommentsService } from '@/services/RecipeCommentsService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed } from 'vue';
import EditRecipeCommentForm from './EditRecipeCommentForm.vue';

const account = computed(() => AppState.account)
const activeRecipeComment = computed(() => AppState.activeRecipeComment)
const editRecipeCommentFormToggle = computed(() => AppState.editRecipeCommentFormToggle)

const props = defineProps({
  recipeCommentProp: { type: RecipeComment, required: true }
})

const wasEdited = new Date(props.recipeCommentProp.updatedAt) > new Date(props.recipeCommentProp.createdAt)

async function deleteRecipeComment() {
  try {
    const confirmed = await Pop.confirm("Are you sure you want to delete this comment?", "This action cannot be undone.", "Yes", "No")
    if (confirmed) await recipeCommentsService.delete(props.recipeCommentProp.id)
  }
  catch (error) {
    Pop.error(error, "Could not delete recipe comment.");
    logger.error("Could not delete recipe comment.".toUpperCase(), error)
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
      <div
        v-if="account?.id == recipeCommentProp.creatorId && editRecipeCommentFormToggle && activeRecipeComment.id == recipeCommentProp.id">
        <EditRecipeCommentForm :editRecipeCommentProp="recipeCommentProp" />
      </div>
      <div v-if="wasEdited" class="d-flex">
        <p>{{ recipeCommentProp.creator.name }}</p>
        <p>*Edited*</p>
      </div>
      <p>{{ recipeCommentProp.body }}</p>
    </div>
  </div>
</template>


<style lang="scss" scoped></style>