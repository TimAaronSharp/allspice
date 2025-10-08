<script setup>
import { AppState } from '@/AppState.js';
import Example from '@/components/Example.vue';
import { recipesService } from '@/services/RecipesService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed, onMounted } from 'vue';

const recipes = computed(() => AppState.recipes)

onMounted(() => {
  getAllRecipes()
})

// NOTE 🧺 Get all recipes function.
async function getAllRecipes() {
  try {
    await recipesService.getAll()
  }
  catch (error) {
    Pop.error(error, "Could not get all recipes.");
    logger.error("Could not get all recipes.".toUpperCase(), error)
  }
}

</script>

<template>
  <Example />
</template>

<style scoped lang="scss"></style>
