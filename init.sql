DROP DATABASE IF EXISTS paranabanco;

CREATE DATABASE paranabanco;
USE paranabanco;

CREATE TABLE customer(
  id varchar(100) primary key,
  name varchar(150),
  email varchar(200) unique,
  password varchar(255),
  document varchar(20),
  salary float,
  amountAll float,
    city varchar(100),
    country varchar(100),
    state varchar(100),
    zipCode varchar(10),
  dateOfBirth datetime,
  cellNumber varchar(20)
);

CREATE TABLE creditCards(
    id varchar(100) primary key,
    customerId varchar(100),
    name varchar(150),
    email varchar(200),
    salary float,
    limit float,
    cardNumber varchar(20),
    password varchar(4),
    expirationDate datetime,
    createdAt datetime
);

create table creditProposals(
    id varchar(100) primary key,
    userId varchar(100),
    name varchar(150),
    email varchar(200),
    proposalValue float,
    createdAt datetime
);