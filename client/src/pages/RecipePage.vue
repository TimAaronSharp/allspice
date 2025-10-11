<script setup>
import { AppState } from '@/AppState.js';
import { Recipe } from '@/models/Recipe.js';
import { recipesService } from '@/services/RecipesService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed, onMounted } from 'vue';
import { useRoute, useRouter } from 'vue-router';

const account = computed(() => AppState.account)
const recipe = computed(() => AppState.activeRecipe)
const route = useRoute()
const router = useRouter()

onMounted(() => {
  // debugger
  getRecipeById()
})

async function getRecipeById() {
  try {
    await recipesService.getById(route.params.recipeId)
  }
  catch (error) {
    Pop.error(error, "Could not get recipe by id");
    logger.error("Could not get recipe by id".toUpperCase(), error)
  }
}

</script>


<template>
  <section>
    <h1>{{ recipe?.title }}</h1>
    <p>By: {{ recipe?.creator?.name }}</p>
    <img :src="recipe?.img" alt="">
  </section>
</template>


<style lang="scss" scoped></style>