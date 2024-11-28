-- create_tables.sql

CREATE TABLE IF NOT EXISTS users (
    Id SERIAL PRIMARY KEY,
    FirstName VARCHAR(50),
    Surname VARCHAR(50),
    Email VARCHAR(100) UNIQUE,
    Password VARCHAR(100),
    Role VARCHAR(20)
    );

CREATE TABLE IF NOT EXISTS heroes(
    Id SERIAL PRIMARY KEY,
    Name VARCHAR(50),
    Class VARCHAR(20),
    Level INT,
    HitPoints INT,
    Damage INT,
    Attack INT,
    ArmorClass INT
    UserId INT,  -- Foreign key to users table
    FOREIGN KEY (UserId) REFERENCES users(Id)
);