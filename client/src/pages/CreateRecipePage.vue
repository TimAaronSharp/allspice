<script setup>
import { AppState } from '@/AppState.js'
import { ingredientsService } from '@/services/IngredientsService.js'
import { recipeIngredientLinksService } from '@/services/RecipeIngredientLinksService.js'
import { recipesService } from '@/services/RecipesService.js'
import { logger } from '@/utils/Logger.js'
import { Pop } from '@/utils/Pop.js'
import { computed, onMounted, ref } from 'vue'

const categories = computed(() => AppState.categories)

const editableRecipeData = ref({
  name: "",
  instructions: "",
  img: "",
  category: "",
})

const editableIngredientData = ref({
  name: "",
  quantity: "",
  originRecipeId: 0
})

onMounted(() => {
  getCategories()
})

async function getCategories() {
  try {
    await recipesService.getCategories()
  }
  catch (error) {
    Pop.error(error, "Could not get categories.");
    logger.error("Could not get categories.".toUpperCase(), error)
  }
}


// async function createIngredient() {
//   try {
//     // debugger
//     editableIngredientData.value.originRecipeId = recipe.value?.id
//     const ingredient = await ingredientsService.create(editableIngredientData.value)

//     editableIngredientData.value = {
//       name: "",
//       quantity: "",
//       originRecipeId: 0
//     }

//     const recipeIngredientLinkData = { recipeId: ingredient.originRecipeId, ingredientId: ingredient.id, creatorId: "" }
//     // debugger
//     await recipeIngredientLinksService.create(recipeIngredientLinkData)
//   }
//   catch (error) {
//     Pop.error(error, `Could not create ingredient: ${editableIngredientData.value.quantity} ${editableIngredientData.value.name}`);
//     logger.error(`Could not create ingredient: ${editableIngredientData.value.quantity} ${editableIngredientData.value.name}`.toUpperCase(), error)
//   }
// }

async function createRecipe() {
  try {
    debugger
    await recipesService.create(editableRecipeData.value)
  }
  catch (error) {
    Pop.error(error, `Could not create recipe `);
  }
}

</script>


<template>
  <section>
    <form @submit.prevent="createRecipe()">
      <label for="recipe-name">Recipe Name:</label>
      <input v-model="editableRecipeData.name" class="w-100" id="recipe-name" type="text">
      <label for="recipe-img">Recipe Image:</label>
      <input v-model="editableRecipeData.img" class="w-100" id="recipe-img" type="text">
      <label for="recipe-instructions">Cooking Instructions:</label>
      <textarea v-model="editableRecipeData.instructions" class="w-100" id="recipe-instructions"></textarea>
      <label for="recipe-category">Category:</label>
      <select v-model="editableRecipeData.category" id="recipe-category">
        <option value="" disabled>Select Category</option>
        <option v-for="category in categories" :key="category" :value="category"> {{ category }}
        </option>
      </select>
      <button type="submit">Create Recipe</button>
    </form>
  </section>
  <!-- <div>
    <form @submit.prevent="createIngredient()">
      <label for="ingredient-quantity" class="form-label">Quantity:</label>
      <input v-model="editableIngredientData.quantity" class="w-100" id="ingredient-quantity" type="text">
      <label for="ingredient-name" class="form-label">Ingredient:</label>
      <input v-model="editableIngredientData.name" class="w-100" id="ingredient-name" type="text">
      <button type="submit">Add Ingredient</button>
    </form>
  </div> -->
</template>


<style lang="scss" scoped></style>