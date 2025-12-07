<script setup>
import { AppState } from '@/AppState.js'
import IngredientInput from '@/components/IngredientInput.vue'
import { ingredientsService } from '@/services/IngredientsService.js'
import { recipeIngredientLinksService } from '@/services/RecipeIngredientLinksService.js'
import { recipesService } from '@/services/RecipesService.js'
import { logger } from '@/utils/Logger.js'
import { Pop } from '@/utils/Pop.js'
import { computed, onMounted, ref } from 'vue'

const categories = computed(() => AppState.categories)
const ingredientsToCreate = computed(() => AppState.ingredientsToCreate)
let newIngredientToggle = true

const editableRecipeData = ref({
  name: "",
  instructions: "",
  img: "",
  category: "",
})

let newIngredientInput = true

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
    await recipesService.create(editableRecipeData.value)
  }
  catch (error) {
    Pop.error(error, `Could not create recipe `);
  }
}

function removeIngredientFromCreateList(ingredientToRemove) {
  const ingredientIndex = AppState.ingredientsToCreate.findIndex(ingredient => ingredient.name == ingredientToRemove.name)
  AppState.ingredientsToCreate.splice(ingredientIndex, 1)
}

</script>


<template>
  <section>
    <h2>Recipe:</h2>
    <form @submit.prevent="createRecipe()">
      <label for="recipe-name">Recipe Name:</label>
      <input v-model="editableRecipeData.name" class="w-100" id="recipe-name" type="text" required>
      <label for="recipe-img">Recipe Image:</label>
      <input v-model="editableRecipeData.img" class="w-100" id="recipe-img" type="text" required>
      <label for="recipe-instructions">Cooking Instructions:</label>
      <textarea v-model="editableRecipeData.instructions" class="w-100" id="recipe-instructions" required></textarea>
      <label for="recipe-category">Category:</label>
      <select v-model="editableRecipeData.category" id="recipe-category" required>
        <option value="" disabled>Select Category</option>
        <option v-for="category in categories" :key="category" :value="category"> {{ category }}
        </option>
      </select>
      <hr>
      <button type="submit">Create Recipe</button>
      <hr>
    </form>
    <h2>Ingredients:</h2>
    <div v-for="ingredientToCreate in ingredientsToCreate" :key="ingredientToCreate + ' key'" class="d-flex">
      <p> {{ ingredientToCreate.quantity }} {{ ingredientToCreate.name }}
      </p> <button @click="removeIngredientFromCreateList(ingredientToCreate)"
        class="mdi mdi-close-circle text-red transparent-btn-style"></button>
    </div>
    <!-- NOTE Check with Cameron to see if he thinks there would be a problem with having input fields without a form (best practices, ADA, etc.) -->
    <IngredientInput v-if="newIngredientInput == true"></IngredientInput>
  </section>
</template>


<style lang="scss" scoped></style>