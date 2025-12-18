<script setup>
import { AppState } from '@/AppState.js';
import { Recipe } from '@/models/Recipe.js';
import { favoritesService } from '@/services/FavoritesService.js';
import { ingredientsService } from '@/services/IngredientsService.js';
import { recipeIngredientLinksService } from '@/services/RecipeIngredientLinksService.js';
import { recipesService } from '@/services/RecipesService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed, onMounted, ref } from 'vue';
import { useRoute, useRouter } from 'vue-router';

const account = computed(() => AppState.account)
const recipe = computed(() => AppState.activeRecipe)
const ingredients = computed(() => AppState.ingredients)
const route = useRoute()
const router = useRouter()
const favorite = computed(() => AppState.activeFavorite)

const recipeId = Number(route.params.recipeId)

onMounted(() => {
  getRecipeById()
})

async function createFavorite() {
  try {
    debugger
    const favoriteData = {
      recipeId: recipeId,
      accountId: null
    }
    // logger.log("recipeId is ", recipeId)
    await favoritesService.create(favoriteData)
  }
  catch (error) {
    Pop.error(error,);
  }
}

async function deleteFavorite() {
  try {
    // temporarily empty
    // await favoritesService.delete(favorite.value.id)
  }
  catch (error) {
    Pop.error(error);
  }
}

async function getRecipeById() {
  try {
    await recipesService.getById(route.params.recipeId)
    logger.log("recipe is ", recipe.value)
    getRecipeIngredientsByRecipeId(route.params.recipeId)
  }
  catch (error) {
    Pop.error(error, "Could not get recipe by id");
    logger.error("Could not get recipe by id".toUpperCase(), error)
  }
}

async function getRecipeIngredientsByRecipeId(recipeId) {
  try {
    await recipesService.getRecipeIngredientsByRecipeId(recipeId)
  }
  catch (error) {
    Pop.error(error, "Could not get ingredients by recipe id.");
    logger.error("Could not get ingredients by recipe id.".toUpperCase(), error)
  }
}

</script>

<!-- NOTE Add a description to recipe models/database table. -->

<template>
  <section class="container-fluid">
    <div class="row">
      <div class="col-12">
        <div class="d-flex">
          <div>
            <img :src="recipe?.img" alt="" class="recipe-img">
          </div>
          <div class="recipe-data">
            <h1>{{ recipe?.name }}</h1>
            <button v-if="account && favorite?.recipeId == recipeId" @click="deleteFavorite()"
              class="mdi mdi-heart text-red transparent-btn-style"></button>
            <button v-if="account" @click="createFavorite()"
              class="mdi mdi-heart-outline transparent-btn-style"></button>
            <p>{{ recipe?.category[0].toUpperCase() + recipe?.category.slice(1) }}</p>
            <p>By: {{ recipe?.creator?.name }}</p>
            <!-- NOTE Recipe description here? -->


            <p v-for="ingredient in ingredients" :key="'ingredient' + ingredient.id">
              {{ ingredient.quantity }} {{ ingredient.name }}
            </p>
          </div>
        </div>
      </div>
      <div>
        <hr>
        <h4>Instructions:</h4>
        <p>{{ recipe?.instructions }}</p>
      </div>
    </div>
  </section>
</template>


<style lang="scss" scoped>
.recipe-img {
  object-fit: cover;
  object-position: center;
  width: 40vw;
  aspect-ratio: 1/1;
  border-radius: 5px;
}

.recipe-data {
  margin-left: 10vw;
}
</style>