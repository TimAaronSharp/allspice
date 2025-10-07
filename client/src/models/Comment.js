import { Profile } from "./Account.js";
import { DatabaseItem } from "./DatabaseItem.js";


export class Comment extends DatabaseItem{
  /**
   * @typedef CommentData
   * @property {number} id
   * @property {Date} createdAt
   * @property {Date} updatedAt
   * @property {string} body
   * @property {string} creatorId
   * @property {Profile} creator
   * @param {CommentData} data 
   */
  constructor(data){
    super(data)
    this.body = data.body
    this.creatorId = data.creatorId
    this.creator = data.creator
  }
}