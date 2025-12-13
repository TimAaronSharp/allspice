import { DatabaseItem } from "./DatabaseItem.js";

/**
   * @typedef {Object} RecipeIngredientLinkData
   * @property {number} id
   * @property {Date} createdAt
   * @property {Date} updatedAt
   * @property {number} recipeId
   * @property {number} ingredientId
   * @property {string} creatorId
   * @param {RecipeIngredientLinkData} data 
   */


export class RecipeIngredientLink extends DatabaseItem{
  constructor(data){
    super(data)
    this.recipeId = data.recipeId
    this.ingredientId = data.ingredientId
    this.creatorId = data.creatorId
  }
}