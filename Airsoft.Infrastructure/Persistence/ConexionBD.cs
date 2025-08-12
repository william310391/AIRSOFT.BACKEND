using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace Airsoft.Infrastructure.Persistence
{
    public class DapperContext
    {
        private readonly string _connectionString;

        // ✅ Ahora recibe directamente la cadena de conexión
        public DapperContext(string? connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("La cadena de conexión no puede estar vacía.", nameof(connectionString));

            _connectionString = connectionString;
        }

        public DbConnection CrearConexion()
        {
            DbProviderFactory factory = SqlClientFactory.Instance;
            DbConnection? connection = factory.CreateConnection();

            if (connection == null)
                throw new InvalidOperationException("No se pudo crear la conexión a la base de datos.");

            connection.ConnectionString = _connectionString;
            return connection;
        }

        public async Task<T> EjecutarAsync<T>(Func<DbConnection, Task<T>> accion)
        {
            using var connection = CrearConexion();
            await connection.OpenAsync();
            return await accion(connection);
        }
    }
}
