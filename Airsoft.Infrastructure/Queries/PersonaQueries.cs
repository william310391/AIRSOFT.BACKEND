namespace Airsoft.Infrastructure.Queries
{
    public static class PersonaQueries 
    {
        public static readonly string GetPersonas = @"
                                SELECT * 
                                FROM Persona 
                                WHERE PersonaID = @PersonaID";
        public static readonly string GetPersonasById = @"
                                SELECT * 
                                FROM Persona";
    }
}
