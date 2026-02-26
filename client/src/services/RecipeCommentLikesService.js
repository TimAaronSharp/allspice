import { api } from "./AxiosService.js"

class RecipeCommentLikesService {

  async create(recipeCommentId){
    const res = await api.post("api/recipeCommentLikes", recipeCommentId)
    
  }
}

export const recipeCommentLikesService = new RecipeCommentLikesService()