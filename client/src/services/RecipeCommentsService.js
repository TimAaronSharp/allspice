import { AppState } from "@/AppState.js"
import { api } from "./AxiosService.js"
import { logger } from "@/utils/Logger.js"
import { RecipeComment } from "@/models/RecipeComment.js"


class RecipeCommentsService{
  
  async create(recipeCommentData){
    AppState.activeRecipeComment = null
    const res = await api.post("api/recipeComments", recipeCommentData)
    // logger.log ("RecipeCommentsService.create() returned ", res.data)
    // debugger
    const createdRecipeComment = this.makeRecipeComments(res.data);
    // NOTE
    // @ts-ignore - makeRecipeComments() can return either a single RecipeComment or a RecipeComment[]. Functionally will work but has a type error/warning. Ignoring for the time being and will investigate ways to properly remove the error (such as TypeScript overloading).
    AppState.recipeComments.push(createdRecipeComment)
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
  const foundRecipeComments = this.makeRecipeComments(res.data)
  // NOTE
  // @ts-ignore - makeRecipeComments() can return either a single RecipeComment or a RecipeComment[]. Functionally will work but has a type error/warning. Ignoring for the time being and will investigate ways to properly remove the error (such as TypeScript overloading).
  AppState.recipeComments = foundRecipeComments
    }

  makeRecipeComments(recipeCommentData){
    // FIXME Commenting out this null check for now as it would return undefined. Can't remember exactly why I put that in, must have been to fix a problem I was having (but may not have anymore now that the code is more fleshed out?). Will keep an eye on it.
    // if (recipeCommentData == null || recipeCommentData == "") {
    //   return
    // }
    if (Array.isArray(recipeCommentData)) {
      return recipeCommentData.map(pojo => new RecipeComment(pojo));
    }
    return new RecipeComment(recipeCommentData);
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