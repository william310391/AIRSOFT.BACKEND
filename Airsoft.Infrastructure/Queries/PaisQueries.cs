namespace Airsoft.Infrastructure.Queries
{
    public class PaisQueries
    {
        public static readonly string GetPaisAll = @"
                                SELECT 
                                     P.PaisID
                                    ,P.CodigoIso2
                                    ,P.CodigoIso3
                                    ,P.Nombre
                                    ,P.NombreOficial
                                    ,P.Continente
                                    ,P.Activo
                                FROM Pais P
                                WHERE P.Activo= 1";
    }
}
