namespace Airsoft.Infrastructure.Queries
{
    public static class PersonaQueries 
    {
        public static readonly string GetPersonasById = @"
                                SELECT P.PersonaID
                                      ,P.TipoDocumentoID
                                      ,DTD.DatoNombre AS TipoDocumento
                                      ,P.NumeroDocumento
                                      ,P.Nombre
                                      ,P.ApellidoPaterno
                                      ,P.ApellidoMaterno
                                      ,P.FechaNacimiento
                                      ,P.PaisID
                                      ,PA.Nombre AS Pais
                                      ,P.UsuarioRegistroID
                                      ,P.FechaRegistro
                                      ,P.UsuarioModeficionID
                                      ,P.FechaModificion
                                FROM Persona P
                                INNER JOIN Datos DTD ON DTP.DatoID= P.TipoDocumentoID
                                INNER JOIN Datos DTS ON DTS.DatoID= P.SexoID
                                INNER JOIN Pais PA ON PA.PaisID=P.PaisID
                                WHERE P.PersonaID = @PersonaID";
        public static readonly string GetPersonas = @"
                                SELECT P.PersonaID
                                      ,P.TipoDocumentoID
                                      ,DTD.DatoNombre AS TipoDocumento 
                                      ,P.NumeroDocumento
                                      ,P.Nombre
                                      ,P.ApellidoPaterno
                                      ,P.ApellidoMaterno
                                      ,P.FechaNacimiento
                                      ,P.PaisID
                                      ,PA.Nombre AS Pais
                                      ,P.UsuarioRegistroID
                                      ,P.FechaRegistro
                                      ,P.UsuarioModeficionID
                                      ,P.FechaModificion
                                FROM Persona P
                                INNER JOIN Datos DTD ON DTP.DatoID= P.TipoDocumentoID
                                INNER JOIN Datos DTS ON DTS.DatoID= P.SexoID
                                INNER JOIN Pais PA ON PA.PaisID=P.PaisID";

        public static readonly string Save = @"
                                INSERT INTO Persona(TipoDocumentoID
                                                   ,NumeroDocumento
                                                   ,Nombre
                                                   ,ApellidoPaterno
                                                   ,ApellidoMaterno
                                                   ,FechaNacimiento
                                                   ,PaisID
                                                   ,UsuarioRegistroID
                                                   ,FechaRegistro)
                                             VALUES(@TipoDocumentoID
                                                   ,@NumeroDocumento
                                                   ,@Nombre
                                                   ,@ApellidoPaterno
                                                   ,@ApellidoMaterno
                                                   ,@FechaNacimiento
                                                   ,@PaisID
                                                   ,@UsuarioRegistroID
                                                   ,GETDATE())";

        public static readonly string Update = @"
                                UPDATE Persona
                                        SET TipoDocumentoID    =@TipoDocumentoID
                                           ,NumeroDocumento    =@NumeroDocumento
                                           ,Nombre             =@Nombre
                                           ,ApellidoPaterno    =@ApellidoPaterno
                                           ,ApellidoMaterno    =@ApellidoMaterno
                                           ,FechaNacimiento    =@FechaNacimiento
                                           ,PaisID             =@PaisID
                                           ,UsuarioModeficionID=@UsuarioModeficionID
                                           ,FechaModificion    =GETDATE()
                                WHERE PersonaID = @PersonaID";
    }
}
