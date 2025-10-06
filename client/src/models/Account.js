import { DatabaseItem } from "./DatabaseItem.js"

export class Profile extends DatabaseItem{
  /**
   * @typedef ProfileData
   * @property {string} id
   * @property {Date} createdAt
   * @property {Date} updatedAt
   * @property {string} name
   * @property {string} picture
   * @property {string} coverImg
   * @param {ProfileData} data 
   */
  constructor(data){
    super(data)
    this.name = data.name
    this.picture = data.picture
    this.coverImg = data.coverImg
  }

}


export class Account extends Profile{
  /**
   * @typedef AccountData
   * @property {string} id
   * @property {Date} createdAt
   * @property {Date} updatedAt
   * @property {string} name
   * @property {string} picture
   * @property {string} email
   * @property {string} coverImg
   * @param {AccountData} data
   */
  constructor(data) {
    super(data)
    this.email = data.email
    // TODO add additional properties if needed
  }
}
