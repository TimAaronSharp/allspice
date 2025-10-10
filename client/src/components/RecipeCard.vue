<script setup>
import { Recipe } from '@/models/Recipe.js';


const props = defineProps({
  recipeProp: { type: Recipe, required: true }
})

</script>


<template>
  <div class="d-flex flex-column justify-content-between position-relative">
    <img :src="recipeProp.img" alt="" class="img-fluid recipe-img">
    <div class="position-absolute h-100 w-100 d-flex flex-column justify-content-between">
      <div class="smoked-glass smoked-glass-top">
        <h2>{{ recipeProp?.title }}</h2>
      </div>
      <div class="w-100 p-1 d-flex justify-content-between smoked-glass smoked-glass-bottom align-items-center">
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