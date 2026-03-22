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
    name VARCHAR(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
    instructions VARCHAR(5000) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
    img VARCHAR(2000) NOT NULL,
    category ENUM(
        'breakfast',
        'lunch',
        'dinner',
        'snack',
        'dessert'
    ),
    description VARCHAR(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci,
    created_from_recipe_id INT,
    creator_id VARCHAR(255) NOT NULL,
    FOREIGN KEY (creator_id) REFERENCES accounts (id) ON DELETE CASCADE
)

ALTER TABLE allspice_recipes RENAME COLUMN title TO name;

ALTER TABLE allspice_recipe_comments_likes
RENAME allspice_recipe_comment_likes

ALTER TABLE allspice_recipes
MODIFY COLUMN updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Latest Update'

DROP TABLE allspice_recipes
-- allspice_recipes END

-- allspice_ingredients START

CREATE TABLE allspice_ingredients (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    name VARCHAR(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
    quantity VARCHAR(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
    origin_recipe_id INT NOT NULL,
    UNIQUE KEY uq_ingredient_name_quantity (name, quantity)
)

ALTER TABLE allspice_ingredients
MODIFY COLUMN updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Latest Update'

DROP TABLE allspice_ingredients

-- allspice_ingredients END

-- allspice_recipe_ingredient_links START
-- This is a join table that only will have an entry for every ingredient that a recipe has. Example: Recipe id:1 has 5 ingredients, so there will be 5 entries in this table for that recipe/ingredients. This table will be used to match ingredients to recipes. This allows the entries in the ingredients table to not need to be duplicated if more than one recipe uses that same ingredient ('sugar', '1 cup'), saving space and reducing redundancy.

-- id | recipe_di | ingredient_id
-- ___|____________|_____________
--  1   1            1
--  2   1            2
--  3   1            3
--  4   1            4
--  5   1            5

CREATE TABLE allspice_recipe_ingredient_links (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    recipe_id INT NOT NULL,
    ingredient_id INT NOT NULL,
    creator_id VARCHAR(255) NOT NULL,
    FOREIGN KEY (recipe_id) REFERENCES allspice_recipes (id) ON DELETE CASCADE,
    FOREIGN KEY (ingredient_id) REFERENCES allspice_ingredients (id) ON DELETE CASCADE,
    UNIQUE KEY uq_recipe_ingredient_ids (recipe_id, ingredient_id)
)

ALTER TABLE allspice_recipe_ingredient_links
ADD creator_id VARCHAR(255) NOT NULL

ALTER TABLE allspice_recipe_ingredient_links
MODIFY COLUMN updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Latest Update'

DROP TABLE allspice_recipe_ingredient_links;
-- allspice_recipe_ingredient_links END

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

ALTER TABLE allspice_favorites
ADD UNIQUE KEY uq_recipe_account_ids (recipe_id, account_id)

ALTER TABLE allspice_favorites
MODIFY COLUMN updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Latest Update'

DROP TABLE allspice_favorites

-- allspice_favorites END

-- allspice_recipe_notes START

CREATE TABLE allspice_recipe_notes (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    recipe_id INT NOT NULL,
    body VARCHAR(5000) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
    account_id VARCHAR(255) NOT NULL,
    FOREIGN KEY (recipe_id) REFERENCES allspice_recipes (id) ON DELETE CASCADE,
    FOREIGN KEY (account_id) REFERENCES accounts (id) ON DELETE CASCADE,
    UNIQUE KEY uq_recipe_creator_ids (recipe_id, account_id)
)

ALTER TABLE allspice_recipe_notes
MODIFY COLUMN updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Latest Update'

DROP TABLE allspice_recipe_notes

-- allspice_recipe_notes END

-- allspice_recipe_comments START

CREATE TABLE allspice_recipe_comments (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    body VARCHAR(5000) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
    recipe_id INT NOT NULL,
    creator_id VARCHAR(255) NOT NULL,
    FOREIGN KEY (recipe_id) REFERENCES allspice_recipes (id) ON DELETE CASCADE,
    FOREIGN KEY (creator_id) REFERENCES accounts (id) ON DELETE CASCADE
)

ALTER TABLE allspice_recipe_comments
MODIFY COLUMN updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Latest Update'

DROP TABLE allspice_recipe_comments

-- allspice_recipe_comments END

-- allspice_recipe_comments_likes START
CREATE TABLE allspice_recipe_comments_likes (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    recipe_comment_id INT NOT NULL,
    account_id VARCHAR(255) NOT NULL,
    FOREIGN KEY (recipe_comment_id) REFERENCES allspice_recipe_comments (id) ON DELETE CASCADE,
    FOREIGN KEY (account_id) REFERENCES accounts (id) ON DELETE CASCADE
)

ALTER TABLE allspice_recipe_comments_likes
MODIFY COLUMN updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Latest Update'

DROP TABLE allspice_recipe_comments_likes

ALTER TABLE allspice_recipe_comments_likes
RENAME COLUMN comment_id TO recipe_comment_id

ALTER TABLE allspice_recipe_comments_likes
ADD UNIQUE KEY uq_recipe_comment_account_ids (recipe_comment_id, account_id)

-- allspice_recipe_comments_likes END

-- allspice_recipe_tags START

CREATE TABLE allspice_recipe_tags (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    name VARCHAR(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
    UNIQUE KEY uq_recipe_tags_name (name)
)

ALTER TABLE allspice_recipe_tags
MODIFY COLUMN updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Latest Update'

DROP TABLE allspice_recipe_tags;

SELECT COLUMN_TYPE
FROM INFORMATION_SCHEMA.COLUMNS
WHERE
    TABLE_SCHEMA = 'confident_yeti_fccf_db'
    AND TABLE_NAME = 'allspice_recipes'
    AND COLUMN_NAME = 'category';