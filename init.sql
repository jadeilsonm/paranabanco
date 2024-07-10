-- Criação do banco de dados
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'paranabanco')
BEGIN
    CREATE DATABASE paranabanco;
END;
GO

-- Seleção do banco de dados
USE paranabanco;
GO

-- Criação da tabela customer se não existir
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'customer')
BEGIN
    CREATE TABLE customer (
        id NVARCHAR(100) PRIMARY KEY,
        name NVARCHAR(150),
        email NVARCHAR(200) UNIQUE,
        password NVARCHAR(255),
        document NVARCHAR(20),
        salary FLOAT,
        amountAll FLOAT,
        city NVARCHAR(100),
        country NVARCHAR(100),
        state NVARCHAR(100),
        zipCode NVARCHAR(10),
        dateOfBirth DATETIME,
        cellNumber NVARCHAR(20)
    );
END;
GO

-- Criação da tabela creditCards se não existir
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'creditCards')
BEGIN
    CREATE TABLE creditCards (
        id NVARCHAR(100) PRIMARY KEY,
        customerId NVARCHAR(100),
        name NVARCHAR(150),
        email NVARCHAR(200),
        salary FLOAT,
        limit FLOAT,
        cardNumber NVARCHAR(20),
        password NVARCHAR(4),
        expirationDate DATETIME,
        createdAt DATETIME
    );
END;
GO

-- Criação da tabela creditProposals se não existir
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'creditProposals')
BEGIN
    CREATE TABLE creditProposals (
        id NVARCHAR(100) PRIMARY KEY,
        userId NVARCHAR(100),
        name NVARCHAR(150),
        email NVARCHAR(200),
        proposalValue FLOAT,
        createdAt DATETIME
    );
END;
GO