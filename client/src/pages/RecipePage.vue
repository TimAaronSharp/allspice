<script setup>
import { AppState } from '@/AppState.js';
import { favoritesService } from '@/services/FavoritesService.js';
import { recipesService } from '@/services/RecipesService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed, onMounted, watch } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { toggleCreateCommentForm } from '@/composables/useToggleCreateCommentForm.js'
import CreateCommentForm from '@/components/CreateCommentForm.vue';
import { commentsService } from '@/services/CommentsService.js';
import CreateRecipeNoteForm from '@/components/CreateRecipeNoteForm.vue';
import { toggleCreateRecipeNoteForm } from '@/composables/useToggleCreateRecipeNoteForm.js';
import { recipeNotesService } from '@/services/RecipeNotesService.js';
import EditRecipeNoteForm from '@/components/EditRecipeNoteForm.vue';
import { toggleEditRecipeNoteForm } from '@/composables/useToggleEditRecipeNoteForm.js';

const account = computed(() => AppState.account)
const recipe = computed(() => AppState.activeRecipe)
const ingredients = computed(() => AppState.ingredients)
const route = useRoute()
const router = useRouter()
const favorite = computed(() => AppState.activeFavorite)
const recipeId = Number(route.params.recipeId)
const comments = computed(() => AppState.comments)
const recipeNote = computed(() => AppState.activeRecipeNote)
const activeRecipeNote = computed(() => AppState.activeRecipeNote)

const createCommentFormToggle = computed(() => AppState.createCommentFormToggle)
const createRecipeNoteFormToggle = computed(() => AppState.createRecipeNoteFormToggle)
const editRecipeNoteFormToggle = computed(() => AppState.editRecipeNoteFormToggle)


// NOTE The recipe category is stored lowercase in the database. This makes the first letter uppercase for display.
const recipeCategory = computed(() => recipe.value?.category[0].toUpperCase() + recipe.value?.category.slice(1))

// NOTE getFavoriteByRecipeIdAndAccountId() and getRecipeNote() are called in onMounted and watch(account) because if the page is refreshed onMounted happens before the account info is able to be retrieved. Likewise, if account is already defined then they will never be called in watch(account). Calling in both ensures the recipe is checked to see if it is favorited by the logged in user.
onMounted(() => {
  clearRecipeInfo()
  resetToggles()
  getRecipeById()
  getRecipeNote()
  getFavoriteByRecipeIdAndAccountId()
})

watch(account, () => {
  getFavoriteByRecipeIdAndAccountId()
  getRecipeNote()
})


function clearRecipeInfo() {
  // debugger
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
    // const confirmed = await Pop.confirm(`Remove "${recipe.value.name}" from your favorites?`, "No", "Yes")
    // if (confirmed) {
    // debugger
    await favoritesService.delete(favorite.value.id)
    Pop.toast(`${recipe.value.name} removed from favorites.`)
    // }
  }
  catch (error) {
    Pop.error(error);
  }
}

async function getCommentsByRecipeId() {
  try {
    await commentsService.getByRecipeId(route.params.recipeId);
    if (comments.value) {
      // logger.log("Computed comments are ", comments.value)
    }
  }
  catch (error) {
    Pop.error(error, "Could not get comments for recipe.");
    logger.error("Could not get comments for recipe.".toUpperCase(), error);
  }
}
// NOTE Try handling the account stuff only on the server side (Also maybe do an if() for whether or not userInfo is found?)
async function getFavoriteByRecipeIdAndAccountId() {
  try {
    // debugger
    if (account.value) {
      await favoritesService.getFavoriteByRecipeIdAndAccountId(route.params.recipeId)
    }
    // logger.log("AppState.activeFavorite is ", AppState.activeFavorite)
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
    getCommentsByRecipeId()
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

async function getRecipeNote() {
  try {
    // debugger
    if (account.value) {
      // debugger
      await recipeNotesService.getByRecipeIdAndAccountId(recipeId)
    }
  }
  catch (error) {
    Pop.error(error, "Could not get recipe note.");
    logger.error("Could not get recipe note.".toUpperCase(), error)
  }
}

function resetToggles() {
  AppState.createCommentFormToggle = false
  AppState.createRecipeNoteFormToggle = false
  AppState.editRecipeNoteFormToggle = false
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
            <div v-if="account">
              <div v-if="!createRecipeNoteFormToggle && activeRecipeNote == null">
                <button @click="toggleCreateRecipeNoteForm()"
                  class="mdi mdi-plus-circle btn btn-outline-secondary">Create Note</button>
              </div>
              <CreateRecipeNoteForm v-if="createRecipeNoteFormToggle" :recipeProp="recipe" />
              <div v-if="recipeNote != null">
                <h5>Recipe Notes:</h5>
                <p v-if="!editRecipeNoteFormToggle">{{ recipeNote.body }}</p>
                <EditRecipeNoteForm v-if="editRecipeNoteFormToggle" :recipeProp="recipe" />
                <button v-if="!editRecipeNoteFormToggle" @click="toggleEditRecipeNoteForm()"
                  class="mdi mdi-pencil btn btn-outline-secondary">Edit
                  Note</button>
              </div>
            </div>
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
    <div>
      <h4>Comments:</h4>
      <div v-if="account">
        <button @click="toggleCreateCommentForm()" v-if="!createCommentFormToggle"
          class="mdi mdi-plus-circle btn btn-outline-secondary">
          Comment</button>
        <CreateCommentForm v-if="createCommentFormToggle" :recipeProp="recipe" />
      </div>
      <div>
        <div v-for="comment in comments" class="d-flex" :key="'Comment ' + comment?.id">
          <img :src="comment?.creator.picture" :alt="comment?.creator?.name + `'s profile picture.'`">
          <p>{{ comment?.creator?.name }}</p>
          <p> {{ comment?.body }}</p>
        </div>
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