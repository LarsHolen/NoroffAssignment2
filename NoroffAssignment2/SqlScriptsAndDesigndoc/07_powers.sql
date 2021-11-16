INSERT INTO Superpower (nameSuperpower, descriptionSuperpower)
VALUES ('Intimidation','frighten or overawe enemies, in order to make them do what one wants.')

INSERT INTO Superpower (nameSuperpower, descriptionSuperpower)
VALUES ('Flying','levitate or fly through air at unimaginable speeds.')

INSERT INTO Superpower (nameSuperpower, descriptionSuperpower)
VALUES ('Superstrength','are able to lift 200 quintillion tons… with one hand')

INSERT INTO Superpower (nameSuperpower, descriptionSuperpower)
VALUES ('Programming','can learn any computer language')

INSERT INTO LinkSuperheroAndSuperpower (idSuperhero, idSuperpower)
VALUES (1, 1)
INSERT INTO LinkSuperheroAndSuperpower (idSuperhero, idSuperpower)
VALUES (2,2)
INSERT INTO LinkSuperheroAndSuperpower (idSuperhero, idSuperpower)
VALUES (3, 3)
GO

/* Adding ekstra powers to superhero with id 3 */
/* One superhero have multiple powers, and one power is shared between multiple superheroes*/

INSERT INTO LinkSuperheroAndSuperpower (idSuperhero, idSuperpower)
VALUES (3, 2)
INSERT INTO LinkSuperheroAndSuperpower (idSuperhero, idSuperpower)
VALUES (3, 1)

SELECT * FROM LinkSuperheroAndSuperpower