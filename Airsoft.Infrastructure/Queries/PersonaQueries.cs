using Microsoft.Identity.Client;

namespace Airsoft.Infrastructure.Queries
{
    public enum PersonaQueries
    {
        GetPersonas,
        GetPersonasById
    }
    
    public static class SqlQueryMapper
    {
        private static readonly Dictionary<PersonaQueries, string> queries = new()
            {
                { PersonaQueries.GetPersonasById, @"SELECT * FROM Persona WHERE PersonaID = @PersonaID" },
                { PersonaQueries.GetPersonas, @"SELECT * FROM Persona"}
            };
        public static string Get(PersonaQueries type) => queries[type];
    }


}
