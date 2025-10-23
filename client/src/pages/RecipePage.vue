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
            <h1>{{ recipe?.title }}</h1>
            <p>{{ recipe?.category[0].toUpperCase() + recipe?.category.slice(1) }}</p>
            <p>By: {{ recipe?.creator?.name }}</p>
            <!-- NOTE Recipe description here? -->
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