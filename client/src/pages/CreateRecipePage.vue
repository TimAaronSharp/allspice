<script setup>
import { AppState } from '@/AppState.js'
import IngredientInput from '@/components/IngredientInput.vue'
import { Recipe } from '@/models/Recipe.js'
import { RecipeIngredientLink } from '@/models/RecipeIngredientLink.js'
import { ingredientsService } from '@/services/IngredientsService.js'
import { recipeIngredientLinksService } from '@/services/RecipeIngredientLinksService.js'
import { recipesService } from '@/services/RecipesService.js'
import { logger } from '@/utils/Logger.js'
import { Pop } from '@/utils/Pop.js'
import { computed, onMounted, ref } from 'vue'

const categories = computed(() => AppState.categories)
const ingredientsToCreate = computed(() => AppState.ingredientsToCreate)
const createdRecipe = computed(() => AppState.activeRecipe)
let newIngredient = computed(() => AppState.newIngredient)

const editableRecipeData = ref({
  name: "",
  instructions: "",
  img: "",
  category: "",
})

onMounted(() => {
  getCategories()
  AppState.ingredientsToCreate = []
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

async function createIngredients() {
  try {
    // debugger
    AppState.ingredientsToCreate.forEach(ingredientToCreate => {
      ingredientToCreate.originRecipeId = AppState.activeRecipe.id
    })

    const createdIngredients = await ingredientsService.create(AppState.ingredientsToCreate)

    await createRecipeIngredientLinks(createdIngredients)

  }
  catch (error) {
    Pop.error(error, "Could not create ingredients");
    logger.error("Could not create ingredients".toUpperCase(), error)
  }
}

async function createRecipe() {
  try {
    await recipesService.create(editableRecipeData.value)
    await createIngredients()
    resetEditableRecipeData()
  }
  catch (error) {
    Pop.error(error, `Could not create recipe `);
    logger.error(`Could not create recipe ${editableRecipeData.value.name}`.toUpperCase(), error)
  }
}

async function createRecipeIngredientLinks(createdIngredients) {
  try {
    debugger
    const recipeIngredientLinkData = []
    // NOTE REMEMBER TO CHANGE THE RECIPE CHECK IN SERVER | INGREDIENTSSERVICE.CREATE() TO USE RECIPEID INSTEAD OF ORIGINRECIPEID (OR ATLEAST THINK ABOUT IF IT'S NECESSARY?)
    createdIngredients.forEach(createdIngredient => {
      let recipeIngredientLink = {}

      recipeIngredientLink.recipeId = createdRecipe.value.id,
        recipeIngredientLink.ingredientId = createdIngredient.id

      recipeIngredientLinkData.push(recipeIngredientLink)
    })
    await recipeIngredientLinksService.create(recipeIngredientLinkData)
  }
  catch (error) {
    Pop.error(error, "Could not create recipe ingredient links");
    logger.error("Could not create recipe ingredient links", error)
  }
}

function newIngredientToggle() {
  AppState.newIngredient = !AppState.newIngredient

  logger.log("newIngredientToggle is now ", newIngredient)
}

function removeIngredientFromCreateList(ingredientToRemove) {
  const ingredientIndex = AppState.ingredientsToCreate.findIndex(ingredient => ingredient.name == ingredientToRemove.name)
  AppState.ingredientsToCreate.splice(ingredientIndex, 1)
}

function resetEditableRecipeData() {
  editableRecipeData.value = {
    name: "",
    instructions: "",
    img: "",
    category: "",
  }
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
    <button @click="newIngredientToggle()" v-if="!newIngredient">Add
      Ingredient</button>
    <IngredientInput v-if="newIngredient" :recipeProp="createdRecipe">
    </IngredientInput>
  </section>
</template>


<style lang="scss" scoped></style>