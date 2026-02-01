<script setup>
import { toggleCreateRecipeNoteForm } from '@/composables/useToggleCreateRecipeNoteForm.js';


</script>


<template>
  <form @submit.prevent="" id="create-recipe-note-form">
    <label for="recipe-note-body-field" class="form-label">Recipe Note:</label>
    <h6>Only you will see this.</h6>
    <textarea name="recipe-note-body" id="recipe-note-body-field"></textarea>
    <div>
      <button type="submit" class="btn btn-outline-primary">Create Note</button>
      <button type="button" @click="toggleCreateRecipeNoteForm" class="btn btn-outline-danger">Cancel</button>
    </div>
  </form>
</template>


<style lang="scss" scoped></style>