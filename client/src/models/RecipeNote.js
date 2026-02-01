import { DatabaseItem } from "./DatabaseItem.js";


export class RecipeNote extends DatabaseItem{
  /**
   * @typedef RecipeNoteData
   * @property {number} id
   * @property {Date} createdAt
   * @property {Date} updatedAt
   * @property {number} recipeId
   * @property {string} body
   * @property {string} accountId
   * @param {RecipeNoteData} data 
   */
  constructor(data){
    super(data)
    this.recipeId = data.recipeId
    this.body = data.body
    this.accountId = data.accountId
  }
}