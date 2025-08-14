
namespace Airsoft.Infrastructure.Queries
{
    public static class SqlQueryMapper
    {
        private static readonly Dictionary<object, string> queries = new()
            {
                // Persona
                { PersonaQueries.GetPersonasById, @"SELECT * FROM Persona WHERE PersonaID = @PersonaID" },
                { PersonaQueries.GetPersonas, @"SELECT * FROM Persona"},

                // Usuario
                { UsuarioQueries.GetUsuariosByUsuarioNombre, @"SELECT * FROM Usuario WHERE UsuarioNombre = @UsuarioNombre" },



            };

        public static string Get<T>(T type) where T : Enum
        {
            return queries[type];
        }
    }

}
