import { Ingredient } from "@/models/Ingredient.js"
import { api } from "./AxiosService.js"
import { AppState } from "@/AppState.js"
import { logger } from "@/utils/Logger.js"
/** @typedef {import('@/models/Ingredient.js').IngredientData} IngredientData */


class IngredientsService {
  // NOTE 🛠️ Create ingredient request to the server.
  async create(ingredientData) {
    const res = await api.post('api/ingredients', ingredientData)
    logger.log("IngredientsService.create() returned ", + res.data)
    const createdIngredient = new Ingredient(res.data)
    AppState.ingredients.push(createdIngredient)
    return res.data
  }
  // Playing around with @param/@returns. This was because originally makeIngredients() would behave differently based on if it was an array or not (like RecipesService.makeRecipes()), but upon further pondering I don't believe I actually will ever need an AppState.activeIngredient after all. Commenting for now in case I realize later on that I do.
  /**
   * 
   * @param {IngredientData[]} ingredients 
   * @returns {Ingredient[]}
   */
  // NOTE 🏭 Function to take ingredient data and create Ingredient class objects from it. Checks if data is an array (maps array to create multiple Ingredients) or single (creates new single Ingredient (Likely no longer needed)).
  makeIngredients(ingredients) {
    // if (Array.isArray(ingredients)) {
      return AppState.ingredients = ingredients.map(pojo => new Ingredient(pojo))
    // }
    // return AppState.activeIngredient = new Ingredient(ingredients)
  }
}

export const ingredientsService = new IngredientsService()