import { Ingredient } from "@/models/Ingredient.js"
import { api } from "./AxiosService.js"
import { AppState } from "@/AppState.js"
import { logger } from "@/utils/Logger.js"


class IngredientsService {
  async create(ingredientData) {
    const res = await api.post('api/ingredients', ingredientData)
    logger.log("create() returned ", + res.data)
    this.makeIngredients(res.data)
  }
  makeIngredients(ingredients) {
    if (Array.isArray(ingredients)) {
      AppState.ingredients = ingredients.map(pojo => new Ingredient(pojo))
      logger.log("makeIngredients() returned ", AppState.ingredients)
      return
    }
    AppState.activeIngredient = new Ingredient(ingredients)
    logger.log("makeIngredients() returned ", AppState.activeIngredient)
  }
}

export const ingredientsService = new IngredientsService()