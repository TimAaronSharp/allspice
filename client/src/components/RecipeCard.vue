<script setup>
import { Recipe } from '@/models/Recipe.js';


const props = defineProps({
  recipeProp: { type: Recipe, required: true }
})

</script>


<template>
  <div>
    <h2>{{ recipeProp?.title }}</h2>
    <p>By: {{ recipeProp?.creator.name }}</p> <span><img :src="recipeProp.creator.picture" alt=""></span>
  </div>
</template>


<style lang="scss" scoped>
</style>