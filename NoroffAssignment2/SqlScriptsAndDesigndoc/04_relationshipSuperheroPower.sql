CREATE TABLE LinkSuperheroAndSuperpower(
	idSuperhero int,
	idSuperpower int
	PRIMARY KEY(idSuperhero, idSuperpower)
);

ALTER TABLE LinkSuperheroAndSuperpower
	ADD CONSTRAINT FK_Superhero_Superpower
	FOREIGN KEY (idSuperhero) REFERENCES Superhero(idSuperhero)
GO
ALTER TABLE LinkSuperheroAndSuperpower
	ADD CONSTRAINT FK_Superpower_Superhero
	FOREIGN KEY (idSuperpower) REFERENCES Superpower(idSuperpower)
