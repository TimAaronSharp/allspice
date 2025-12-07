<script setup>
import { AppState } from '@/AppState.js';
import CreateRecipePage from '@/pages/CreateRecipePage.vue';
import { logger } from '@/utils/Logger.js';
import { computed, ref } from 'vue';
import { useRoute } from 'vue-router';
import { Pop } from "@/utils/Pop.js"

const ingredientsToCreate = computed(() => AppState.ingredientsToCreate)

const editableIngredientData = ref({
  name: "",
  quantity: "",
  originRecipeId: 0
})

function addIngredientToCreateList() {
  if (AppState.ingredientsToCreate.find(ingredient => ingredient.name == editableIngredientData.value.name)) {

    Pop.toast(`${editableIngredientData.value.name} already exists in recipe.`)
    resetEditableIngredientData()
    return
  }
  ingredientsToCreate.value.push(editableIngredientData.value)
  resetEditableIngredientData()
}

function resetEditableIngredientData() {
  editableIngredientData.value = {
    name: "",
    quantity: "",
    originRecipeId: 0
  }
}

</script>


<template>
  <label for="ingredient-quantity" class="form-label">Quantity:</label>
  <input v-model="editableIngredientData.quantity" class="w-100" id="ingredient-quantity" type="text">
  <label for="ingredient-name" class="form-label">Ingredient:</label>
  <input v-model="editableIngredientData.name" class="w-100" id="ingredient-name" type="text">
  <button @click="addIngredientToCreateList()">Add Ingredient</button>
</template>


<style lang="scss" scoped></style>