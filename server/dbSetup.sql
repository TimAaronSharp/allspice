CREATE TABLE IF NOT EXISTS accounts (
    id VARCHAR(255) NOT NULL PRIMARY KEY COMMENT 'primary key',
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
    name VARCHAR(255) COMMENT 'User Name',
    email VARCHAR(255) UNIQUE COMMENT 'User Email',
    picture VARCHAR(255) COMMENT 'User Picture'
) default charset utf8mb4 COMMENT '';

-- allspice_recipes START
CREATE TABLE allspice_recipes (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    title VARCHAR(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
    instructions VARCHAR(5000) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
    img VARCHAR(2000) NOT NULL,
    category ENUM(
        'breakfast',
        'lunch',
        'dinner',
        'snack',
        'dessert'
    ),
    created_from_recipe_id INT,
    creator_id VARCHAR(255) NOT NULL,
    FOREIGN KEY (creator_id) REFERENCES accounts (id) ON DELETE CASCADE
)

INSERT INTO
    allspice_recipes (
        title,
        instructions,
        img,
        category,
        creator_id
    )
VALUES (
        'Test title',
        'Test instructions',
        'https://upload.wikimedia.org/wikipedia/commons/thumb/6/6d/Good_Food_Display_-_NCI_Visuals_Online.jpg/1200px-Good_Food_Display_-_NCI_Visuals_Online.jpg',
        'dinner',
        '67e3273fee37d52171a8018c'
    )

DROP TABLE allspice_recipes
-- allspice_recipes END

-- allspice_ingredients START

CREATE TABLE allspice_ingredients (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    name VARCHAR(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
    quantity VARCHAR(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
    recipe_id INT NOT NULL,
    UNIQUE KEY uq_ingredient_name_quantity (name, quantity)
)

INSERT INTO
    allspice_ingredients (name, quantity, recipe_id)
VALUES ('Sugar', '1 Cup', 1)
ON DUPLICATE KEY UPDATE
    id = LAST_INSERT_ID(id);

SELECT LAST_INSERT_ID();

DROP TABLE allspice_ingredients

-- allspice_ingredients END

-- allspice_recipe_ingredients START
-- This is a join table that only will have an entry for every ingredient that a recipe has. Example: Recipe id:1 has 5 ingredients, so there will be 5 entries in this table for that recipe/ingredients. This table will be used to match ingredients to recipes. This allows the entries in the ingredients table to not need to be duplicated if more than one recipe uses that same ingredient ('sugar', '1 cup'), saving space and reducing redundancy.

-- id | recipe_di | ingredient_id
-- ___|____________|_____________
--  1   1            1
--  2   1            2
--  3   1            3
--  4   1            4
--  5   1            5

CREATE TABLE allspice_recipe_ingredients (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    recipe_id INT NOT NULL,
    ingredient_id INT NOT NULL,
    FOREIGN KEY (recipe_id) REFERENCES allspice_recipes (id) ON DELETE CASCADE,
    FOREIGN KEY (ingredient_id) REFERENCES allspice_ingredients (id) ON DELETE CASCADE
)

-- allspice_recipe_ingredients END

-- allspice_favorites START

CREATE TABLE allspice_favorites (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    account_id VARCHAR(255) NOT NULL,
    recipe_id INT NOT NULL,
    FOREIGN KEY (account_id) REFERENCES accounts (id) ON DELETE CASCADE,
    FOREIGN KEY (recipe_id) REFERENCES allspice_recipes (id) ON DELETE CASCADE
)

DROP TABLE allspice_favorites

-- allspice_favorites END

-- allspice_recipe_notes START

CREATE TABLE allspice_recipe_notes (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    recipe_id INT NOT NULL,
    body VARCHAR(5000) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
    creator_id VARCHAR(255) NOT NULL,
    FOREIGN KEY (recipe_id) REFERENCES allspice_recipes (id) ON DELETE CASCADE,
    FOREIGN KEY (creator_id) REFERENCES accounts (id) ON DELETE CASCADE
)

DROP TABLE allspice_recipe_notes

-- allspice_recipe_notes END

-- allspice_recipe_comments START

CREATE TABLE allspice_recipe_comments (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    body VARCHAR(5000) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
    creator_id VARCHAR(255) NOT NULL,
    FOREIGN KEY (creator_id) REFERENCES accounts (id) ON DELETE CASCADE
)

DROP TABLE allspice_recipe_comments

-- allspice_recipe_comments END

-- allspice_recipe_comments_likes START
CREATE TABLE allspice_recipe_comments_likes (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    comment_id INT NOT NULL,
    account_id VARCHAR(255) NOT NULL,
    FOREIGN KEY (comment_id) REFERENCES allspice_recipe_comments (id) ON DELETE CASCADE,
    FOREIGN KEY (account_id) REFERENCES accounts (id) ON DELETE CASCADE
)

DROP TABLE allspice_recipe_comments_likes

-- allspice_recipe_comments_likes END

-- allspice_recipe_tags START

CREATE TABLE allspice_recipe_tags (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    name VARCHAR(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
    UNIQUE KEY uq_recipe_tags_name (name)
)