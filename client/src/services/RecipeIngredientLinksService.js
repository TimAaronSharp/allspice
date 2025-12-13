import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"


class RecipeIngredientLinksService {

  async create(recipeIngredientLinkData){
    const res = await api.post('api/recipeIngredientLinks', recipeIngredientLinkData)
    logger.log("RecipeIngredientLinksService.create() returned ", res.data)
  }
}

export const recipeIngredientLinksService = new RecipeIngredientLinksService()