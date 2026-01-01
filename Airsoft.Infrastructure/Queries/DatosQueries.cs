
namespace Airsoft.Infrastructure.Queries
{
    public static class DatosQueries
    {
        public static readonly string FindAll = @"
                                SELECT 
                                     D.TipoDato
                                    ,D.DatoID
                                    ,D.DatoNombre
                                    ,D.DatoValor
                                    ,D.Activo
                                    ,D.UsuarioRegistro
                                    ,D.FechaRegistro
                                    ,D.UsuarioModificacion
                                    ,D.FechaModificion
                                FROM Datos D";

        public static readonly string FindByTipoDato = @"
                                SELECT 
                                     D.TipoDato
                                    ,D.DatoID
                                    ,D.DatoNombre
                                    ,D.DatoValor
                                    ,D.Activo
                                    ,D.UsuarioRegistroID
                                    ,D.FechaRegistro
                                    ,D.UsuarioModificacionID
                                    ,D.FechaModificacion
                                FROM Datos D
                                WHERE TipoDato=@TipoDato
                                    AND Activo=1";


        public static readonly string FindBuscarDato = @"
                                SELECT 
                                     D.TipoDato
                                    ,D.DatoID
                                    ,D.DatoNombre
                                    ,D.DatoValor
                                    ,D.Activo
                                    ,D.UsuarioRegistroID
                                    ,D.FechaRegistro
                                    ,D.UsuarioModificacionID
                                    ,D.FechaModificacion
                                FROM Datos D
                                WHERE (@Buscar IS NULL OR D.TipoDato LIKE '%' + @Buscar + '%' 
                                                       OR D.DatoNombre LIKE '%' + @Buscar + '%')
                               
                                ORDER BY D.TipoDato
                                OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY;

                                SELECT COUNT(*)
                                FROM Datos D
                                WHERE (@Buscar IS NULL OR D.TipoDato LIKE '%' + @Buscar + '%' 
                                                       OR D.DatoNombre LIKE '%' + @Buscar + '%')";


        public static readonly string Save = @"
                               INSERT INTO Datos(TipoDato
                                                ,DatoNombre
                                                ,DatoValor
                                                ,Activo
                                                ,UsuarioRegistroID
                                                ,FechaModificacion)
                               VALUES (@TipoDato
                                      ,@DatoNombre
                                      ,@DatoValor
                                      ,1
                                      ,@UsuarioRegistroID
                                      ,GETDATE())";




        public static readonly string Update = @"
                              UPDATE Datos
                                 SET DatoNombre            = @DatoNombre
                                    ,DatoValor             = @DatoValor
                                    ,UsuarioModificacionID = @UsuarioModificacionID
                                    ,FechaModificacion     = GETDATE()
                               WHERE DatoID   = @DatoID
                              ";



        public static readonly string ExistsDato = @"                             
                            SELECT CASE 
                                        WHEN EXISTS ( SELECT 1 FROM Datos WHERE TipoDato = @TipoDato AND DatoNombre= @DatoNombre) 
                                        THEN 1 
                                        ELSE 0 
                                    END AS ExisteDato;
                              ";


        public static readonly string findByDatoID = @"
                            SELECT 
                                 D.TipoDato
                                ,D.DatoID
                                ,D.DatoNombre
                                ,D.DatoValor
                                ,D.Activo
                                ,D.UsuarioRegistroID
                                ,D.FechaRegistro
                                ,D.UsuarioModificacionID
                                ,D.FechaModificacion
                            FROM Datos D
                            WHERE DatoID=@DatoID";

        public static readonly string ChangeState = @"
                            UPDATE DATOS SET Activo=@Activo WHERE DatoID=@DatoID";

    }
}
