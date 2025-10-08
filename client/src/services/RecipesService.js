import { AppState } from "@/AppState.js";
import { logger } from "@/utils/Logger.js";
import { api } from "./AxiosService.js";
import { Recipe } from "@/models/Recipe.js";


class RecipesService{
  
  // NOTE 🧺 Get all keeps request to the server
  async getAll(){
    AppState.recipes = []
    logger.log("AppState.recipes starts as ", AppState.recipes)
    const res = await api.get('api/recipes')
    logger.log("getAll returned ", res.data)
    this.makeRecipes(res.data)
    logger.log("AppState.recipes is now ", AppState.recipes)
  }

  // NOTE 🏭 Function to take recipe data and create Recipe class objects from it. Checks if data is an array (maps array to create multiple Recipes) or single (creates new single Recipe).
  makeRecipes(recipes){
    if (Array.isArray(recipes)) {
      const createdRecipes = recipes.map(pojo => new Recipe(pojo))
      return AppState.recipes = createdRecipes
    }
    const createdRecipe = new Recipe(recipes)
    return AppState.activeRecipe = createdRecipe
  }
}

export const recipesService = new RecipesService()