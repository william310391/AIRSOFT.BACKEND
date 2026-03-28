namespace Airsoft.Infrastructure.Queries
{
    public class PersonaTelefonoQueries
    {
        public static readonly string GetByPersonaID = @"  
                                    SELECT PT.PersonaTelefonoID
                                          ,PT.TipoTelefonoID
                                          ,D.DatoNombre
                                          ,PT.PaisID
                                          ,P.Nombre
                                          ,PT.Telefono
                                          ,PT.FechaRegistro
                                          ,PT.FechaModificion 
                                    FROM Persona_Telefono PT
                                    INNER JOIN Pais P ON P.PaisID=PT.PaisID
                                    INNER JOIN Datos D ON D.TipoDato='TIPO_TELEFONO' AND D.DatoID=PT.TipoTelefonoID
                                    WHERE PT.PersonaID=@PersonaID
                                       AND PT.Activo=1
                                ";
        public static readonly string GetByPersonaTelefonoID = @"  
                                    SELECT PT.PersonaTelefonoID
                                          ,PT.TipoTelefonoID
                                          ,D.DatoNombre
                                          ,PT.PaisID
                                          ,P.Nombre
                                          ,PT.Telefono
                                          ,PT.FechaRegistro
                                          ,PT.FechaModificion 
                                    FROM Persona_Telefono PT
                                    INNER JOIN Pais P ON P.PaisID=PT.PaisID
                                    INNER JOIN Datos D ON D.TipoDato='TIPO_TELEFONO' AND D.DatoID=PT.TipoTelefonoID
                                    WHERE PT.PersonaTelefonoID=@PersonaTelefonoID
                                ";

        public static readonly string Save = @"  
                                    INSERT INTO Persona_Telefono(PersonaID,TipoTelefonoID,PaisID,Telefono,FechaRegistro,UsuarioRegistroID)
                                    VALUES(@PersonaID,@TipoTelefonoID,@PaisID,@Telefono,GETDATE(),@UsuarioRegistroID)
                                ";

        public static readonly string Update = @"  
                                    UPDATE Persona_Telefono SET 
                                         TipoTelefonoID=@TipoTelefonoID
                                        ,PaisID=@PaisID
                                        ,Telefono=@Telefono
                                        ,FechaModificion=GETDATE()
                                        ,UsuarioModeficionID=@UsuarioModeficionID
                                    WHERE PersonaTelefonoID=@PersonaTelefonoID
                                ";

        public static readonly string ChangeState = @"UPDATE Persona_Telefono SET Activo=@Activo WHERE PersonaTelefonoID=@PersonaTelefonoID";
    }
}
