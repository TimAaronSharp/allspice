import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  /**@type {import('@bcwdev/auth0provider-client').Identity} */
  identity: null,
  /** @type {import('./models/Account.js').Account} user info from the database*/
  account: null,
  /** @type {import('./models/Account.js').Profile} user info from the database*/
  profile: null,
  /** @type {import('./models/Recipe.js').Recipe[]} recipes from the database*/
  recipes:[],
  /** @type {import('./models/Recipe.js').Recipe} recipe from the database*/
  activeRecipe:null,
  /** @type {import('./models/Ingredient.js').Ingredient[]} ingredients from the database*/
  ingredients:[],
  /** @type {import('./models/Ingredient.js').Ingredient} recipe from the database*/
  activeIngredient: null,
  /** @type {import('./models/Favorite.js').Favorite[]} favorites from the database*/
  favorites:[],
  /** @type {import('./models/Favorite.js').Favorite} favorite from the database*/
  activeFavorite: null,
  ingredientsToCreate:[],
  newIngredient: false,
  categories:[],
  /** @type {import('./models/Comment.js').Comment[]} comments from the database*/
  comments:[],
  /** @type {import('./models/Comment.js').Comment} comments from the database*/
  activeComment: null
})

