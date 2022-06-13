USE [Prueba]
GO
INSERT INTO dbo.Rols (Descripcion, SnActivo)
VALUES ('Cliente', 1);



/****** Objeto: Table [dbo].[Rols] Fecha del script: 22/03/2022 03:52:49 p.m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Rols] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Descripcion] NVARCHAR (MAX) NOT NULL,
    [SnActivo]    INT            NOT NULL
);


