import { Profile } from "./Account.js";
import { DatabaseItem } from "./DatabaseItem.js";


export class Recipe extends DatabaseItem{
  /**
   * @typedef RecipeData
   * @property {number} id
   * @property {Date} createdAt
   * @property {Date} updatedAt
   * @property {string} title
   * @property {string} instructions
   * @property {string} img
   * @property {string} category
   * @property {number} createdFromRecipeId
   * @property {string} creatorId
   * @property {Profile} creator
   * @param {RecipeData} data 
   */
  constructor(data){
    super(data)
    this.title = data.title
    this.instructions = data.instructions
    this.img = data.img
    this.category = data.category
    this.createdFromRecipeId = data.createdFromRecipeId
    this.creatorId = data.creatorId
    this.creator = data.creator
  }
}