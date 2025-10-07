import { DatabaseItem } from "./DatabaseItem.js";


export class Favorite extends DatabaseItem{
  /**
   * @typedef FavoriteData
   * @property {number} id
   * @property {Date} createdAt
   * @property {Date} updatedAt
   * @property {number} recipeId
   * @property {string} accountId
   * @param {FavoriteData} data 
   */
  constructor(data){
    super(data)
    this.recipeId = data.recipeId
    this.accountId = data.accountId
  }
}