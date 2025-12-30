import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { AppState } from "@/AppState.js"
import { Favorite } from "@/models/Favorite.js"


class FavoritesService{
// NOTE 🛠️ Create Favorite request method to the server.
  async create(favoriteData) {
    AppState.activeFavorite = null
    // logger.log("AppState.activeFavorite starts out ", AppState.activeFavorite)
    const res = await api.post("api/favorites", favoriteData)
    // logger.log("favoritesService.create() returned ", res.data)
    this.makeFavorites(res.data)
    // logger.log("AppState.activeFavorite is now ", AppState.activeFavorite)
  }

  // NOTE 💣 Delete Favorite request method to the server.
  async delete(favoriteId){
    await api.delete(`api/favorites/${favoriteId}`)
    this.unMakeFavorites(favoriteId)
  }

  // NOTE 🔍📓🧑‍🦲 Get Favorite by recipe id and account id. This is used when loading a recipe page and a favorite id is not known yet.
  async getFavoriteByRecipeIdAndAccountId(recipeId){
    const res = await api.get(`api/favorites/recipes/${recipeId}`)
    // logger.log("getFavorite() returned ", res.data)
    this.makeFavorites(res.data)
  }

  // NOTE 🏭 Method to take favorite data retrieved from the server and create Favorite class objects from it. Null checks first, then checks if data is an array or not and creates Favorite class objects accordingly.
  makeFavorites(favoriteData){
    if (favoriteData == null || favoriteData == "") {
      return
    }
    if (Array.isArray(favoriteData)) {
      return AppState.favorites = favoriteData.map(pojo => new Favorite(pojo))
    }
    return AppState.activeFavorite = new Favorite(favoriteData)
  }
  // NOTE ☠️ Method to remove a favorite from AppState.favorites/activeFavorite when a recipe is unfavorited (deleted).
  unMakeFavorites(favoriteId){
    if (AppState.favorites != []) {
      const favoriteIndex = AppState.favorites.findIndex(favorite => favorite.id == favoriteId)
      AppState.favorites.splice(favoriteIndex, 1)
    }
    AppState.activeFavorite = null
  }
}

export const favoritesService = new FavoritesService()