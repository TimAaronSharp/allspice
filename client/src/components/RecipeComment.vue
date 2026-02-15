<script setup>
import { RecipeComment } from '@/models/RecipeComment.js';


const props = defineProps({
  recipeCommentProp: { type: RecipeComment, required: true }
})


</script>


<template>
  <div class="d-flex">
    <img :src="recipeCommentProp.creator.picture" :alt="recipeCommentProp.creator.name + `'s profile picture.'`">
    <div class="d-flex flex-column">
      <p>{{ recipeCommentProp.creator.name }}</p>
      <p>{{ recipeCommentProp.body }}</p>
    </div>
  </div>
</template>


<style lang="scss" scoped></style>