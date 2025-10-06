import { DatabaseItem } from "./DatabaseItem.js";


export class Tag extends DatabaseItem{
  /**
   * @typedef TagData
   * @property {number} id
   * @property {Date} createdAt
   * @property {Date} updatedAt
   * @property {string} name
   * @param {TagData} data 
   */
  constructor(data){
    super(data)
    this.name = data.name
  }
}

export class RecipeTag extends Tag{
  /**
   * @typedef RecipeTagData
   * @property {number} id
   * @property {Date} createdAt
   * @property {Date} updatedAt
   * @property {string} name
   * @property {number} recipeId
   * @property {number} tagId
   * @param {RecipeTagData} data 
   */
  constructor(data){
    super(data)
    this.recipeId = data.recipeId
    this.tagId = data.tagId
  }
}