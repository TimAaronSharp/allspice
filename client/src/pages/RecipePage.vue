<script setup>
import { AppState } from '@/AppState.js';
import { favoritesService } from '@/services/FavoritesService.js';
import { recipesService } from '@/services/RecipesService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed, onMounted, watch } from 'vue';
import { useRoute, useRouter } from 'vue-router';

const account = computed(() => AppState.account)
const recipe = computed(() => AppState.activeRecipe)
const ingredients = computed(() => AppState.ingredients)
const route = useRoute()
const router = useRouter()
const favorite = computed(() => AppState.activeFavorite)
const recipeId = Number(route.params.recipeId)

// NOTE The recipe category is stored lowercase in the database. This makes the first letter uppercase for display.
const recipeCategory = computed(() => recipe.value?.category[0].toUpperCase() + recipe.value?.category.slice(1))

// NOTE getFavorite() is called in onMounted and watch(account) because if the page is refreshed onMounted happens before the account info is able to be retrieved. Likewise, if account is already defined then getFavorite() will never be called in watch(account). Calling in both ensures the recipe is checked to see if it is favorited by the logged in user.
onMounted(() => {
  clearRecipeInfo()
  getRecipeById()
  getFavorite()
})

watch(account, getFavorite)

function clearRecipeInfo() {
  AppState.activeRecipe = null
  AppState.activeFavorite = null
  AppState.ingredients = []
}

// NOTE 🛠️ Create favorite function. Creating object with only the recipe id so that when sent to FavoritesController ASP.NET can parse as a JSON object (primitives like ints will give a 415 Unsupported Media Type error). AccountId will be assigned in controller.
async function createFavorite() {
  try {
    const favoriteData = {
      recipeId: recipeId
    }
    await favoritesService.create(favoriteData)
    // logger.log("AppState.activeFavorite is after create", AppState.activeFavorite)
  }
  catch (error) {
    Pop.error(error, "Could not favorite recipe.");
    logger.error("Could not favorite the recipe (create new favorite).".toUpperCase())
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
// NOTE Try handling the account stuff only on the server side (Also maybe do an if() for whether or not userInfo is found?)
async function getFavorite() {
  try {
    if (account.value) {
      await favoritesService.getFavorite(route.params.recipeId)
    }
    logger.log("AppState.activeFavorite is ", AppState.activeFavorite)
  }
  catch (error) {
    Pop.error(error, "Could not get favorite recipe information");
    logger.error("Could not check for and get favorite".toUpperCase())
  }
}

async function getRecipeById() {
  try {
    // debugger
    await recipesService.getById(route.params.recipeId)
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
            <button v-else-if="account && favorite?.recipeId != recipeId" @click="createFavorite()"
              class="mdi mdi-heart-outline transparent-btn-style"></button>
            <p>{{ recipeCategory }}</p>
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