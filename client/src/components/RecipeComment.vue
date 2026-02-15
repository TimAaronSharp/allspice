<script setup>
import { RecipeComment } from '@/models/RecipeComment.js';
import { recipeCommentsService } from '@/services/RecipeCommentsService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';


const props = defineProps({
  recipeCommentProp: { type: RecipeComment, required: true }
})

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

</script>


<template>
  <div class="d-flex">
    <img :src="recipeCommentProp.creator.picture" :alt="recipeCommentProp.creator.name + `'s profile picture.'`">
    <div class="d-flex flex-column">
      <div>
        <button @click="deleteRecipeComment()" class="mdi mdi-close-circle text-red transparent-btn-style"></button>
      </div>
      <p>{{ recipeCommentProp.creator.name }}</p>
      <p>{{ recipeCommentProp.body }}</p>
    </div>
  </div>
</template>


<style lang="scss" scoped></style>