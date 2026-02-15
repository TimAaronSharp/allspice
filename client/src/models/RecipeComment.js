import { Profile } from "./Account.js";
import { DatabaseItem } from "./DatabaseItem.js";


export class RecipeComment extends DatabaseItem{
  /**
   * @typedef RecipeCommentData
   * @property {number} id
   * @property {Date} createdAt
   * @property {Date} updatedAt
   * @property {string} body
   * @property {number} recipeId
   * @property {string} creatorId
   * @property {Profile} creator
   * @param {RecipeCommentData} data 
   */
  constructor(data){
    super(data)
    this.body = data.body
    this.recipeId = data.recipeId
    this.creatorId = data.creatorId
    this.creator = data.creator
  }
}