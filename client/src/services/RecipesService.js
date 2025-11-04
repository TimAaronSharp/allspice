import { AppState } from "@/AppState.js";
import { logger } from "@/utils/Logger.js";
import { api } from "./AxiosService.js";
import { Recipe } from "@/models/Recipe.js";
import { ingredientsService } from "./IngredientsService.js";


class RecipesService{
  // NOTE 🧺 Get all recipes request to the server.
  async getAll(){
    AppState.recipes = []
    const res = await api.get('api/recipes')
    logger.log("getAll returned ", res.data)
    this.makeRecipes(res.data)
    logger.log("AppState.recipes is now ", AppState.recipes)
  }

  // NOTE 🔍 Get recipe by id request to the server.
  async getById(recipeId) {
    AppState.activeRecipe = null
    const res = await api.get(`api/recipes/${recipeId}`)
    logger.log("recipesService.getById() returned ", res.data)
    this.makeRecipes(res.data)
    logger.log("AppState.activeRecipe is now ", AppState.activeRecipe)
  }

  // NOTE 🧺🔍 Get ingredients by recipe id request to the server.
  async getIngredientsByRecipeId(recipeId) {
    const res = await api.get(`api/recipes/${recipeId}/ingredients`)
    logger.log("recipesService.getIngredientsByRecipeId() returned ", res.data)
    ingredientsService.makeIngredients(res.data)
  }

  // NOTE 🏭 Function to take recipe data and create Recipe class objects from it. Checks if data is an array (maps array to create multiple Recipes) or single (creates new single Recipe).
  makeRecipes(recipes){
    if (Array.isArray(recipes)) {
      logger.log("AppState.recipes starts as ", AppState.recipes)
      return AppState.recipes = recipes.map(pojo => new Recipe(pojo))
    }
    logger.log("AppState.activeRecipe starts as ", AppState.activeRecipe)
    return AppState.activeRecipe = new Recipe(recipes)
    
  }
}

export const recipesService = new RecipesService()