<script setup>
import { Recipe } from '@/models/Recipe.js';


const props = defineProps({
  recipeProp: { type: Recipe, required: true }
})

</script>


<template>
  <div class="d-flex flex-column justify-content-between">
    <RouterLink :to="{ name: 'Recipe Page', params: { recipeId: recipeProp?.id } }">
      <img :src="recipeProp.img" alt="" class="w-100 img-fluid recipe-img">
    </RouterLink>
    <div class="h-100 w-100 mt-3 d-flex flex-column justify-content-between">
      <div>
        <RouterLink :to="{ name: 'Recipe Page', params: { recipeId: recipeProp?.id } }">
          <h5>{{ recipeProp?.title }}</h5>
        </RouterLink>
      </div>
      <div class="w-100 p-1 d-flex justify-content-between align-items-center">
        <p class="m-0">By: {{ recipeProp?.creator.name }}</p> <span><img :src="recipeProp.creator.picture" alt=""
            class="profile-thumbnail"></span>
      </div>
    </div>
  </div>
</template>


<style lang="scss" scoped>
.recipe-img {
  object-fit: cover;
  object-position: center;
  height: 40vh;
  border-radius: 5px;
}

.smoked-glass-bottom {
  border-bottom-left-radius: 5px;
  border-bottom-right-radius: 5px;
}

.smoked-glass-top {
  border-top-left-radius: 5px;
  border-top-right-radius: 5px;
}
</style>