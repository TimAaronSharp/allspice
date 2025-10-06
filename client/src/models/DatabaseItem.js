// NOTE This is meant to act as an abstract class inherited by other classes that all need these same properties.

export class DatabaseItem{
  constructor(data){
    this.id = data.id
    this.createdAt = data?.createdAt
    this.updatedAt = data?.updatedAt
  }
}