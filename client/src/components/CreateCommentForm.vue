<script setup>
import { Pop } from '@/utils/Pop.js';
import { ref } from 'vue';
import { toggleCreateCommentForm } from '@/composables/useToggleCreateCommentForm.js'
import { logger } from '@/utils/Logger.js';
import { commentsService } from '@/services/CommentsService.js';
import { Recipe } from '@/models/Recipe.js';
import { validateForm } from '@/composables/useValidateForm.js';

const props = defineProps({
  recipeProp: { type: Recipe, required: true }
})

const editableCommentData = ref({
  body: "",
  recipeId: props.recipeProp.id
})

async function createComment() {
  try {
    debugger
    validateForm("create-comment-form")
    await commentsService.create(editableCommentData.value)
    editableCommentData.value.body = ""
    toggleCreateCommentForm()
  }
  catch (error) {
    Pop.error(error, "Could not create comment.");
    logger.error("Could not create comment.".toUpperCase(), error)
  }

}
</script>


<template>
  <form @submit.prevent="createComment()" id="create-comment-form">
    <label for="comment-body-field" class="form-label">Comment:</label>
    <textarea v-model="editableCommentData.body" name="comment-body" id="comment-body-field"></textarea>
    <div>
      <button type="submit" class="btn btn-outline-primary">Post</button>
      <button class="btn btn-outline-danger">Cancel</button>
    </div>
  </form>
</template>


<style lang="scss" scoped></style>