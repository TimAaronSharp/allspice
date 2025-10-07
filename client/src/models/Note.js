import { DatabaseItem } from "./DatabaseItem.js";


export class Note extends DatabaseItem{
  /**
   * @typedef NoteData
   * @property {number} id
   * @property {Date} createdAt
   * @property {Date} updatedAt
   * @property {number} recipeId
   * @property {string} body
   * @property {string} creatorId
   * @param {NoteData} data 
   */
  constructor(data){
    super(data)
    this.recipeId = data.recipeId
    this.body = data.body
    this.creatorId = data.creatorId
  }
}