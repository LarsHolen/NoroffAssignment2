 /* Targeting the SuperheroDb */
USE SuperheroesDb
GO

/* Creating the three Tables */
CREATE TABLE Superhero (
	idSuperhero int NOT NULL PRIMARY KEY IDENTITY(1,1),
	nameSuperhero nVarChar(50) NOT NULL,
	aliasSuperhero nVarChar(50) NOT NULL,
	originSuperhero nVarChar(50) NOT NULL
);
GO

CREATE TABLE Assistant (
	idAssistant int NOT NULL PRIMARY KEY IDENTITY(1,1),
	nameAssistant nVarChar(50) NOT NULL
);
GO

CREATE TABLE Superpower (
	idSuperpower int NOT NULL PRIMARY KEY IDENTITY(1,1),
	nameSuperpower nVarChar(50) NOT NULL,
	descriptionSuperpower nVarChar(150) NOT NULL
);