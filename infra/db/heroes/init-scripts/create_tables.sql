-- create_tables.sql

CREATE TABLE IF NOT EXISTS heroes(
    Id SERIAL PRIMARY KEY,
    Name VARCHAR(50),
    Class VARCHAR(20),
    Level INT,
    HitPoints INT,
    Damage INT,
    Attack INT,
    ArmorClass INT
);