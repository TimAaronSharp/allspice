USER STORIES/BUSINESS RULES

- Only recipe owner can modify the ingredients and recipe content (but maybe find a way to allow them to make notes in their copy of the recipe or something, so they can effectively alter their own version, say if they like to add carrots to the recipe or something, but not have it affect the actual recipe).

- Comments for recipes that everyone can see.

- Notes for recipes that only the note creator can see.

// NOTE When creating an altered recipe have it add the new altered recipe id to AlternateRecipeIds. When an altered recipe is being viewed have it check if the RecipeId == CreatedFromRecipeId. If not, have it check if the (alternate) recipe id is in the list of AlternateRecipeIds. This MIGHT be a viable path to not needing to make extra copies of unaltered ingredients.