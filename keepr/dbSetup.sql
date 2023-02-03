CREATE TABLE
    IF NOT EXISTS accounts(
        id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        name varchar(255) COMMENT 'User Name',
        email varchar(255) COMMENT 'User Email',
        picture varchar(255) COMMENT 'User Picture',
        coverImg VARCHAR(255) DEFAULT "https://images.unsplash.com/photo-1675223675329-da1d57fba45c?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1171&q=80"
    ) default charset utf8 COMMENT '';

CREATE TABLE
    keeps(
        id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
        creatorId VARCHAR(255) NOT NULL,
        name VARCHAR(50) NOT NULL,
        description TEXT NOT NULL,
        img VARCHAR(255) DEFAULT "https://gnpsvillager.org/wp-content/uploads/2022/04/DD5C48B2-38AB-4A6F-BCB4-DFA4ECADC545-475x356.jpeg",
        views INT DEFAULT 0,
        kept INT DEFAULT 0,
        FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

CREATE TABLE
    vaults(
        id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
        creatorId VARCHAR(255) NOT NULL,
        name VARCHAR(50) NOT NULL,
        description TEXT NOT NULL,
        img VARCHAR(255) DEFAULT "https://gnpsvillager.org/wp-content/uploads/2022/04/DD5C48B2-38AB-4A6F-BCB4-DFA4ECADC545-475x356.jpeg",
        isPrivate BOOLEAN DEFAULT false,
        FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

DROP TABLE vaults;

INSERT INTO
    vaults (
        name,
        description,
        img,
        `isPrivate`,
        `creatorId`
    )
VALUES (
        "joey",
        "from friends",
        "https://upload.wikimedia.org/wikipedia/en/d/da/Matt_LeBlanc_as_Joey_Tribbiani.jpg",
        FALSE,
        "638a4290b124aedd65c6ab55"
    );

INSERT INTO
    accounts (
        id,
        email,
        name,
        picture,
        `coverImg`
    )
VALUES (
        "638a4290b124aedd65c6ab55",
        "test@test.com",
        "test",
        "https://upload.wikimedia.org/wikipedia/en/d/da/Matt_LeBlanc_as_Joey_Tribbiani.jpg",
        "https://static.wikia.nocookie.net/disney/images/c/c5/Toy-story2-disneyscreencaps.com-8243.jpg/revision/latest?cb=20130508003153"
    );

DROP TABLE keeps;

INSERT INTO
    keeps (
        name,
        description,
        img,
        `creatorId`
    )
VALUES (
        "joey",
        "from friends",
        "https://upload.wikimedia.org/wikipedia/en/d/da/Matt_LeBlanc_as_Joey_Tribbiani.jpg",
        "638a4290b124aedd65c6ab55"
    );

DROP TABLE accounts;

DROP TABLE vaultkeeps;

INSERT INTO
    vaultkeeps (
        `keepId`,
        `vaultId`,
        `creatorId`
    )
VALUES (
        1,
        1,
        "638a4290b124aedd65c6ab55"
    );

CREATE TABLE
    vaultkeeps(
        id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
        creatorId VARCHAR(255) NOT NULL,
        vaultId INT NOT NULL,
        keepId INT NOT NULL,
        FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE,
        Foreign Key (vaultId) REFERENCES vaults(id) ON DELETE CASCADE,
        Foreign Key (keepId) REFERENCES keeps(id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

SELECT keeps.*, vaultkeeps.*
FROM vaultkeeps
    JOIN keeps ON keeps.id = vaultkeeps.`keepId`
WHERE vaultkeeps.keepId = 5;

SELECT * FROM accounts WHERE id = "638a4290b124aedd65c6ab55";

SELECT *
FROM keeps
    JOIN accounts ON accounts.id = keeps.creatorId
WHERE
    keeps.creatorId = "638a4290b124aedd65c6ab55";