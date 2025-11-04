import { DatabaseItem } from "./DatabaseItem.js";

/**
   * @typedef {Object} IngredientData
   * @property {number} id
   * @property {Date} createdAt
   * @property {Date} updatedAt
   * @property {string} name
   * @property {string} quantity
   * @property {number} recipeId
   * @param {IngredientData} data 
   */

export class Ingredient extends DatabaseItem{
  constructor(data){
    super(data)
    this.name = data.name
    this.quantity = data.quantity
    this.originRecipeId = data.recipeId
  }
}