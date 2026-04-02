namespace Airsoft.Infrastructure.Queries
{
    public class UbigeoQueries
    {
        public static readonly string GetUbigeoDepartamento = @"
                                SELECT 
                                 UbigeoID
                                ,UbigeoCodigo	
                                ,DepartamentoID	
                                ,ProvinciaID	
                                ,DistritoID	
                                ,DepartamentoNombre	
                                ,ProvinciaNombre	
                                ,DistritoNombre	
                                ,NombreCapital	
                                ,RegionNaturalID	
                                ,RegionNaturalNombre
                                FROM Ubigeo 
                                WHERE ProvinciaID=1 
                                  and DistritoID=1";

        public static readonly string GetUbigeoProvincia = @"
                                SELECT 
                                 UbigeoID
                                ,UbigeoCodigo	
                                ,DepartamentoID	
                                ,ProvinciaID	
                                ,DistritoID	
                                ,DepartamentoNombre	
                                ,ProvinciaNombre	
                                ,DistritoNombre	
                                ,NombreCapital	
                                ,RegionNaturalID	
                                ,RegionNaturalNombre
                                FROM Ubigeo 
                                WHERE DistritoID=1
                                  and DepartamentoID=@DepartamentoID";
        public static readonly string GetUbigeoDistrito = @"
                                SELECT 
                                 UbigeoID
                                ,UbigeoCodigo	
                                ,DepartamentoID	
                                ,ProvinciaID	
                                ,DistritoID	
                                ,DepartamentoNombre	
                                ,ProvinciaNombre	
                                ,DistritoNombre	
                                ,NombreCapital	
                                ,RegionNaturalID	
                                ,RegionNaturalNombre
                                FROM Ubigeo 
                                WHERE DepartamentoID=@DepartamentoID
                                  and ProvinciaID=@ProvinciaID";
    }

}
