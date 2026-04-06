namespace Airsoft.Infrastructure.Queries
{
    public class PersonaCorreoQueries
    {
        public static readonly string GetByPersonaID = @"
                                SELECT PC.PersonaCorreoID,
                                       PC.PersonaID,
                                       PC.TipoCorreoD,
                                       D.DatoNombre,
                                       PC.Correo,
                                       PC.FechaRegistro,
                                       PC.FechaModificion
                                FROM Persona_Correo PC
                                INNER JOIN DATOS D ON D.TipoDato='TIPO_CORREO' D.DatosID=PC.TipoCorreoID
                                WHERE PC.PersonaID=@PersonaID
                                  AND PC.Activo=1
                                ";

        public static readonly string GetByPersonaCorreoID = @"
                                SELECT PC.PersonaCorreoID,
                                       PC.PersonaID,
                                       PC.TipoCorreoD,
                                       D.DatoNombre,
                                       PC.Correo,
                                       PC.FechaRegistro,
                                       PC.FechaModificion
                                FROM Persona_Correo PC
                                INNER JOIN DATOS D ON D.TipoDato='TIPO_CORREO' D.DatosID=PC.TipoCorreoID
                                WHERE PC.PersonaCorreoID=@PersonaCorreoID
                                ";

        public static readonly string Save = @"
                                INSERT INTO Persona_Correo(PersonaID,TipoCorreoD,Correo,FechaRegistro,UsuarioRegistroID)
                                VALUES(@PersonaID,@TipoCorreoD,@Correo,GETDATE(),@UsuarioRegistroID)";

        public static readonly string Update = @"
                                UPDATE Persona_Correo SET 
                                     TipoCorreoD=@TipoCorreoD
                                    ,Correo=@Correo
                                    ,FechaModificion=GETDATE()
                                    ,UsuarioModeficionID=@UsuarioModeficionID
                                WHERE PersonaCorreoID=@PersonaCorreoID";

        public static readonly string ChangeState = @"UPDATE Persona_Correo 
                                                        SET Activo=@Activo 
                                                        ,FechaModificion=GETDATE()
                                                        ,UsuarioModeficionID=@UsuarioModeficionID
                                                        WHERE PersonaCorreoID=@PersonaCorreoID";
    }
}
