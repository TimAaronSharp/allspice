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

    async delete(recipeCommentId){
      const res = await api.delete(`api/recipeComments/${recipeCommentId}`)
      logger.log("RecipeCommentsService.delete() returned ", res.data)
      this.unMakeRecipeComments(recipeCommentId)
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

  unMakeRecipeComments(recipeCommentId){
    if (AppState.recipeComments.length > 0) {
      const recipeCommentIndex = AppState.recipeComments.findIndex(recipeComment => recipeComment.id == recipeCommentId)
      AppState.recipeComments.splice(recipeCommentIndex, 1)
    }
    AppState.activeRecipeComment = null
  }
}

export const recipeCommentsService = new RecipeCommentsService()