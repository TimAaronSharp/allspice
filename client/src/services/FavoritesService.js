import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { AppState } from "@/AppState.js"
import { Favorite } from "@/models/Favorite.js"


class FavoritesService{
  async create(favoriteData) {
    AppState.activeFavorite = null
    logger.log("AppState.activeFavorite starts out ", AppState.activeFavorite)
    const res = await api.post("api/favorites", favoriteData)
    logger.log("favoritesService.create() returned ", res.data)
    this.makeFavorites(res.data)
    logger.log("AppState.activeFavorite is now ", AppState.activeFavorite)
  }

  async getFavorite(recipeId){
    const res = await api.get(`api/favorites/recipes/${recipeId}`)
    logger.log("getFavorite() returned ", res.data)
    this.makeFavorites(res.data)
  }

  // NOTE I believe this may not be creating Favorites correctly. Check out later.
  makeFavorites(favoriteData){
    if (favoriteData == null || favoriteData == "") {
      return
    }
    if (Array.isArray(favoriteData)) {
      return AppState.favorites = favoriteData.map(pojo => new Favorite(pojo))
    }
    return AppState.activeFavorite = new Favorite(favoriteData)
  }
}

export const favoritesService = new FavoritesService()