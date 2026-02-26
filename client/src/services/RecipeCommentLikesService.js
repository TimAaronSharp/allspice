import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"

class RecipeCommentLikesService {

  async create(recipeComment){
    const res = await api.post("api/recipeCommentLikes", recipeComment)
    logger.log(res.data)
  }
}

export const recipeCommentLikesService = new RecipeCommentLikesService()