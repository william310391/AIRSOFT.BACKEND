select * from Persona
select * from rol
select * from Usuario
select * from menu
select * from pagina
SELECT * FROM Pagina_Rol


--drop table Usuario
--drop table rol

--DROP TABLE Menu
--DROP TABLE Pagina
--DROP TABLE Pagina_Rol

insert into Persona(Nombre,Apellido)values('William Teofilo','Astucuri Inca')
insert into rol(rolNombre) values('Admin')
insert into rol(rolNombre) values('Usuario')
insert into Usuario(,UsuarioCuenta,UsuarioNombre,Contrasena,FechaCreacion,RolID) values('zerox','usaurio de prueba','123456',GETDATE(),1)


CREATE TABLE Persona(
PersonaID INT IDENTITY(1,1) PRIMARY KEY
,Nombre varchar(100)
,Apellido varchar(150)
)


CREATE TABLE Rol(
 RolID INT PRIMARY KEY 
,RolNombre VARCHAR(100) NOT NULL

)

create table Usuario(
 UsuarioID INT IDENTITY(1,1) PRIMARY KEY
,UsuarioCuenta VARCHAR(250) NOT NULL
,UsuarioNombre VARCHAR(100) NOT NULL
,Contrasena VARCHAR(255) NOT NULL
,FechaCreacion DATETIME DEFAULT GETDATE()
,RolID INT
,Activo bit DEFAULT 1

,CONSTRAINT FK_Usuario_Rol FOREIGN KEY (RolID) REFERENCES Rol(RolID)
)


CREATE TABLE Menu(
   MenuID INT IDENTITY(1,1) PRIMARY KEY,
   MenuNombre VARCHAR(150),
   MenuIcono VARCHAR(100),
   MenuUrlLink VARCHAR(500),
   Activo BIT DEFAULT 1,
   UsuarioIDCreacion INT DEFAULT 1,
   FechaCreacion DATETIME DEFAULT GETDATE(),
   UsuarioIDModificacion INT DEFAULT 1,
   FechaModificacion DATETIME DEFAULT GETDATE()
)

INSERT INTO Menu(MenuNombre,MenuIcono,MenuUrlLink)VALUES('ADMINISTRACION','','#')
INSERT INTO Menu(MenuNombre,MenuIcono,MenuUrlLink)VALUES('REPORTE','','#')

CREATE TABLE Pagina(
   PaginaID INT IDENTITY(1,1) PRIMARY KEY,
   MenuID INT,
   PaginaNombre VARCHAR(150),
   PaginaIcono VARCHAR(100),
   PaginaUrlLink VARCHAR(500),
   Activo BIT DEFAULT 1,
   UsuarioIDCreacion INT DEFAULT 1,
   FechaCreacion DATETIME DEFAULT GETDATE(),
   UsuarioIDModificacion INT DEFAULT 1,
   FechaModificacion DATETIME DEFAULT GETDATE(),
   
  CONSTRAINT FK_Pagina_Menu FOREIGN KEY (MenuID) REFERENCES Menu(MenuID)
)


INSERT INTO Pagina(MenuID,PaginaNombre,PaginaIcono,PaginaUrlLink) VALUES(1,'USUARIOS','','#')
INSERT INTO Pagina(MenuID,PaginaNombre,PaginaIcono,PaginaUrlLink) VALUES(2,'USUARIOS REGISTRADOS','','#')


CREATE TABLE Pagina_Rol(
	PaginaRolID INT IDENTITY(1,1) PRIMARY KEY,
	PaginaID INT,
	RolID INT,
	Activo BIT DEFAULT 1,
	UsuarioIDCreacion INT DEFAULT 1,
	FechaCreacion DATETIME DEFAULT GETDATE(),
	UsuarioIDModificacion INT DEFAULT 1,
	FechaModificacion DATETIME DEFAULT GETDATE(),

	CONSTRAINT FK_PaginaRol_Pagina FOREIGN KEY (PaginaID) REFERENCES Pagina(PaginaID),
	CONSTRAINT FK_PaginaRol_Rol FOREIGN KEY (RolID) REFERENCES Rol(RolID)
)

INSERT INTO Pagina_Rol(PaginaID,RolID)VALUES(1,1)
INSERT INTO Pagina_Rol(PaginaID,RolID)VALUES(2,2)