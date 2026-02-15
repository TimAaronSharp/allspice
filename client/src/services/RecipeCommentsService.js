import { AppState } from "@/AppState.js"
import { api } from "./AxiosService.js"
import { logger } from "@/utils/Logger.js"
import { RecipeComment } from "@/models/RecipeComment.js"


class RecipeCommentsService{
  async create(recipeCommentData){
    AppState.activeRecipeComment = null
    const res = await api.post("api/recipeComments", recipeCommentData)
    // logger.log ("RecipeCommentsService.create() returned ", res.data)
    this.makeRecipeComments(recipeCommentData);
    }

  async getByRecipeId(recipeId){
  AppState.recipeComments = []
  const res = await api.get(`api/recipes/${recipeId}/recipeComments`)
  // logger.log("RecipeCommentsService.getByRecipeId() returned ", res.data)
  this.makeRecipeComments(res.data)
    }

  makeRecipeComments(recipeCommentData){
    if (recipeCommentData == null || recipeCommentData == "") {
      return
    }
    if (Array.isArray(recipeCommentData)) {
      return AppState.recipeComments = recipeCommentData.map(pojo => new RecipeComment(pojo));
    }
    return AppState.activeRecipeComment = new RecipeComment(recipeCommentData);
  }
}

export const recipeCommentsService = new RecipeCommentsService()