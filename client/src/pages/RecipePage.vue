<script setup>
import { AppState } from '@/AppState.js';
import { Recipe } from '@/models/Recipe.js';
import { ingredientsService } from '@/services/IngredientsService.js';
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

const editableIngredientData = ref({
  name: "",
  quantity: "",
  recipeId: 0
})

onMounted(() => {
  getRecipeById()
})

async function createIngredient() {
  try {
    // debugger
    // NOTE Make it so that if a recipe already has an ingredient that a new recipeIngredient can't be created. Check how you did it in the ingredients table
    editableIngredientData.value.recipeId = recipe.value?.id
    await ingredientsService.create(editableIngredientData.value)
    editableIngredientData.value = {
      name: "",
      quantity: "",
      recipeId: 0
    }
  }
  catch (error) {
    Pop.error(error, `Could not create ingredient: ${editableIngredientData.value.quantity} ${editableIngredientData.value.name}`);
    logger.error(`Could not create ingredient: ${editableIngredientData.value.quantity} ${editableIngredientData.value.name}`.toUpperCase(), error)
  }
}

async function getRecipeById() {
  try {
    await recipesService.getById(route.params.recipeId)
    getIngredientsByRecipeId(route.params.recipeId)
  }
  catch (error) {
    Pop.error(error, "Could not get recipe by id");
    logger.error("Could not get recipe by id".toUpperCase(), error)
  }
}

async function getIngredientsByRecipeId(recipeId) {
  try {
    await recipesService.getIngredientsByRecipeId(recipeId)
  }
  catch (error) {
    Pop.error(error, "Could not get ingredients by recipe id.");
    logger.error("Could not get ingredients by recipe id.".toUpperCase(), error)
  }
}

</script>

<!-- NOTE Add a description to recipe models/database table. -->

<template>
  <div>
    <form @submit.prevent="createIngredient()">
      <label for="ingredient-quantity" class="form-label">Quantity:</label>
      <input v-model="editableIngredientData.quantity" class="w-100" id="ingredient-quantity" type="text">
      <label for="ingredient-name" class="form-label">Ingredient:</label>
      <input v-model="editableIngredientData.name" class="w-100" id="ingredient-name" type="text">
      <button type="submit">Add Ingredient</button>
    </form>
  </div>
  <section class="container-fluid">
    <div class="row">
      <div class="col-12">
        <div class="d-flex">
          <div>
            <img :src="recipe?.img" alt="" class="recipe-img">
          </div>
          <div class="recipe-data">
            <h1>{{ recipe?.title }}</h1>
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