USE [Prueba]
GO
INSERT INTO dbo.Rols (Descripcion, SnActivo)
VALUES ('Admin', 1);
INSERT INTO dbo.Rols (Descripcion, SnActivo)
VALUES ('Cliente', 1);

INSERT INTO dbo.Usuarios VALUES ('Admin1', 'A123', 'Administrador2@mail.com', 'Jose', ' Garcia ', '1234321', 1, -1)
INSERT INTO dbo.Usuarios VALUES ('UserTest1', 'Test1', 'UserTest1@mail.com', 'Juan', ' Gonzales ', '1234322', 2, -1)
INSERT INTO dbo.Usuarios VALUES ('UserTest2', 'Test2', 'Administrador@mail.com', 'Bernardo', ' Alta ', '1234323', 2, -1)
INSERT INTO dbo.Usuarios VALUES ('UserTest3', 'Test3', 'Administrador1@mail.com', 'Rodrigo', ' Mendoza ', '1234324', 2, -1)

INSERT INTO dbo.Peliculas VALUES ('Duro de matar III', 3, 0,1.5,5.0)
INSERT INTO dbo.Peliculas VALUES ('Todo Poderoso', 2,1,1.5,7.0)
INSERT INTO dbo.Peliculas VALUES ('Stranger than fiction', 1,1,1.5,8.0)
INSERT INTO dbo.Peliculas VALUES ('OUIJA', 0,2,2.0,20.50)

INSERT INTO Generos VALUES('Acción')
INSERT INTO Generos VALUES('Comedia')
INSERT INTO Generos VALUES('Drama')
INSERT INTO Generos VALUES('Terror')

INSERT INTO GenerosPeliculas VALUES(1,1)
INSERT INTO GenerosPeliculas VALUES(2,2)
INSERT INTO GenerosPeliculas VALUES(3,2)
INSERT INTO GenerosPeliculas VALUES(3,3)
INSERT INTO GenerosPeliculas VALUES(4,4)

insert into AlquilerVenta (UsuariosId, alq_com, PeliculasId, precio, devolucion) Values (
                        1 ,
                        1,
                        1 , 
                        5.3,
                        -1 )

/****** Objeto: Table [dbo].[Usuarios] Fecha del script: 22/03/2022 03:59:48 p.m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Usuarios] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [UserName]     NVARCHAR (MAX) NOT NULL,
    [Password]     NVARCHAR (MAX) NOT NULL,
    [Email]        NVARCHAR (MAX) NOT NULL,
    [Nombre]       NVARCHAR (MAX) NOT NULL,
    [Apellido]     NVARCHAR (MAX) NOT NULL,
    [NroDocumento] NVARCHAR (MAX) NOT NULL,
    [RolsId]       INT            NOT NULL,
    [RolId]        INT            NOT NULL,
    [SnActivo]     INT            NOT NULL
);


GO
CREATE NONCLUSTERED INDEX [IX_Usuarios_RolsId]
    ON [dbo].[Usuarios]([RolsId] ASC);


GO
ALTER TABLE [dbo].[Usuarios]
    ADD CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED ([Id] ASC);


GO
ALTER TABLE [dbo].[Usuarios]
    ADD CONSTRAINT [FK_Usuarios_Rols_RolsId] FOREIGN KEY ([RolsId]) REFERENCES [dbo].[Rols] ([Id]) ON DELETE CASCADE;


