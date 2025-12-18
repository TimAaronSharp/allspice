import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { AppState } from "@/AppState.js"
import { Favorite } from "@/models/Favorite.js"


class FavoritesService{
  async create(favoriteData) {
    const res = await api.post("api/favorites", favoriteData)
    logger.log("favoritesService.create() returned ", res.data)
    this.makeFavorites(res.data)
  }

  makeFavorites(favoriteData){
    if (Array.isArray(favoriteData)) {
      return AppState.favorites = favoriteData.map(pojo => new Favorite(pojo))
    }
    return AppState.activeFavorite = new Favorite(favoriteData)
  }
}

export const favoritesService = new FavoritesService()