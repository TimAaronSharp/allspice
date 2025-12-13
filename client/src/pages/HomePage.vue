<script setup>
import { AppState } from '@/AppState.js';
import Example from '@/components/Example.vue';
import RecipeCard from '@/components/RecipeCard.vue';
import { recipesService } from '@/services/RecipesService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed, onMounted } from 'vue';

const account = computed(() => AppState.account)
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
  <section v-if="account">
    <RouterLink :to="{ name: 'Create Recipe Page' }">
      <button>Create Recipe</button>
    </RouterLink>
  </section>
  <section class="container-fluid">
    <div class="row row-cols-2 row-cols-md-3 row-cols-lg-4 g-5 mt-4 px-5">
      <div v-for="recipe in recipes" :key="'recipe ' + recipe.id">
        <RecipeCard :recipeProp="recipe" />
      </div>
    </div>
  </section>
</template>

<style scoped lang="scss">
.recipe-card {
  border-radius: 5px;
  height: 30vh;
}
</style>
