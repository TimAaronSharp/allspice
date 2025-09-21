CREATE TABLE IF NOT EXISTS accounts (
    id VARCHAR(255) NOT NULL PRIMARY KEY COMMENT 'primary key',
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
    name VARCHAR(255) COMMENT 'User Name',
    email VARCHAR(255) UNIQUE COMMENT 'User Email',
    picture VARCHAR(255) COMMENT 'User Picture'
) default charset utf8mb4 COMMENT '';

-- allspice_recipes BEGIN
CREATE TABLE allspice_recipes (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    title VARCHAR(255) NOT NULL,
    instructions VARCHAR(5000) NOT NULL,
    img VARCHAR(2000) NOT NULL,
    category ENUM(
        'breakfast',
        'lunch',
        'dinner',
        'snack',
        'dessert'
    ),
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

-- allspice_recipes END

-- allspice_altered_recipes BEGIN

CREATE TABLE allspice_altered_recipes (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    recipe_id INT NOT NULL,
    FOREIGN KEY (recipe_id) REFERENCES allspice_recipes (id) ON DELETE CASCADE
)

-- allspice_altered_recipes END

-- allspice_ingredients BEGIN

CREATE TABLE allspice_ingredients (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    name VARCHAR(255) NOT NULL,
    quantity VARCHAR(255) NOT NULL,
    recipe_id INT NOT NULL,
    creator_id VARCHAR(255) NOT NULL,
    FOREIGN KEY (recipe_id) REFERENCES allspice_recipes (id) ON DELETE CASCADE,
    FOREIGN KEY (creator_id) REFERENCES accounts (id) ON DELETE CASCADE
)

-- allspice_ingredients END

-- allspice_favorites BEGIN

CREATE TABLE allspice_favorites (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    account_id VARCHAR(255) NOT NULL,
    recipe_id INT NOT NULL,
    FOREIGN KEY (account_id) REFERENCES accounts (id) ON DELETE CASCADE,
    FOREIGN KEY (recipe_id) REFERENCES allspice_recipes (id) ON DELETE CASCADE
)

-- allspice_favorites END

-- allspice_recipe_notes BEGIN

CREATE TABLE allspice_recipe_notes (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    recipe_id INT NOT NULL,
    note_body VARCHAR(5000) NOT NULL,
    creator_id VARCHAR(255) NOT NULL,
    FOREIGN KEY (recipe_id) REFERENCES allspice_recipes (id) ON DELETE CASCADE,
    FOREIGN KEY (creator_id) REFERENCES accounts (id) ON DELETE CASCADE
)

-- allspice_recipe_notes END

-- allspice_recipe_comments BEGIN

CREATE TABLE allspice_recipe_comments (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    comment_body VARCHAR(5000) NOT NULL,
    creator_id VARCHAR(255) NOT NULL
)

-- allspice_recipe_comments END

-- allspice_recipe_comments_likes BEGIN
CREATE TABLE allspice_recipe_comments_likes (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    comment_id INT NOT NULL,
    account_id VARCHAR(255) NOT NULL,
    FOREIGN KEY (comment_id) REFERENCES allspice_recipe_comments (id) ON DELETE CASCADE,
    FOREIGN KEY (account_id) REFERENCES accounts (id) ON DELETE CASCADE
)

-- allspice_recipe_comments_likes END