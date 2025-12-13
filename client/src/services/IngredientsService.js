import { Ingredient } from "@/models/Ingredient.js"
import { api } from "./AxiosService.js"
import { AppState } from "@/AppState.js"
import { logger } from "@/utils/Logger.js"
import { Pop } from "@/utils/Pop.js"
/** @typedef {import('@/models/Ingredient.js').IngredientData} IngredientData */


class IngredientsService {
  // NOTE 🛠️ Create ingredient request to the server. After a new Ingredient class object is created with res.data, AppState.ingredients is iterated over to see if the ingredient already exists in the array, and if it does then it does not push, preventing duplication on the client (duplication already prevented in database).
  async create(ingredientData) {
    const res = await api.post('api/ingredients', ingredientData)
    logger.log("IngredientsService.create() returned ", + res.data)


    const createdIngredients = this.makeIngredients(res.data)
    // const createdIngredient = new Ingredient(res.data)
    
    createdIngredients.forEach(createdIngredient => {

    // if(AppState.ingredients.find(ingredient => ingredient.id == createdIngredient.id)){
    //   Pop.toast(`${createdIngredient.name} already exists in recipe.`)
    //   return
    // }
      AppState.ingredients.push(createdIngredient)
    })
    return res.data
  }
  
  // NOTE 🏭 Function to take ingredient data and create Ingredient class objects from it. Checks if data is an array (maps array to create multiple Ingredients) or single (creates new single Ingredient, but returns as an array to prevent type definition errors. Really should only be in the unrealistic scenario when a recipe has only one ingredient).

  // NOTE Do I need this to set AppState.ingredients or can I just have it return the new Ingredients? Look into whether this is needed/if it would be worth changing.
  makeIngredients(ingredients) {
    if (Array.isArray(ingredients)) {
      return AppState.ingredients = ingredients.map(pojo => new Ingredient(pojo))
    }
    return AppState.ingredients = [new Ingredient(ingredients)]
  }
}

export const ingredientsService = new IngredientsService()