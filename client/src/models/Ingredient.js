import { DatabaseItem } from "./DatabaseItem.js";


export class Ingredient extends DatabaseItem{
  /**
   * @typedef IngredientData
   * @property {number} id
   * @property {Date} createdAt
   * @property {Date} updatedAt
   * @property {string} name
   * @property {string} quantity
   * @property {number} originRecipeId
   * @param {IngredientData} data 
   */
  constructor(data){
    super(data)
    this.name = data.name
    this.quantity = data.quantity
    this.originRecipeId = data.originRecipeId
  }
}