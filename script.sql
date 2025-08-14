select * from Persona
select * from rol
select * from Usuario

--drop table Usuario
--drop table rol

insert into Persona(Nombre,Apellido)values('William Teofilo','Astucuri Inca')
insert into rol(rolNombre) values('Admin')
insert into Usuario(UsuarioNombre,Contrasena,FechaCreacion,RolID) values('zerox','123456',GETDATE(),1)


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
,UsuarioNombre VARCHAR(100) NOT NULL
,Contrasena VARCHAR(255) NOT NULL
,FechaCreacion DATETIME DEFAULT GETDATE()
,RolID INT

,CONSTRAINT FK_Usuario_Rol FOREIGN KEY (RolID) REFERENCES Rol(RolID)
)

