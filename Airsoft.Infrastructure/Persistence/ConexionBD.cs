using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
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

        public async Task<DapperTransaction> GetDapperTransaction()
        {
            var connection = CrearConexion();
            await connection.OpenAsync();

            var transaction = await connection.BeginTransactionAsync();

            return new DapperTransaction(connection, transaction);
        }

        // ✅ Devuelve directamente la conexión y la transacción
        public async Task<(DbConnection Connection, DbTransaction Transaction)>
            BeginTransactionAsync()
        {
            var connection = CrearConexion();

            await connection.OpenAsync();

            var transaction = await connection.BeginTransactionAsync();

            return (connection, transaction);
        }

        public async Task<T> EjecutarAsync<T>(Func<DbConnection, Task<T>> accion)
        {
            using var connection = CrearConexion();
            await connection.OpenAsync();
            return await accion(connection);
        }

        public async Task<bool> EjecutarQueryAsync(string sql, object? parametros = null, IDbTransaction? transaction = null)
        {
            var connection = transaction?.Connection as DbConnection ?? CrearConexion();

            var debeCerrarConexion = transaction == null;

            if (connection.State != ConnectionState.Open)
                await connection.OpenAsync();

            try
            {
                var filasAfectadas = await connection.ExecuteAsync(sql, parametros, transaction);
                return filasAfectadas > 0;
            }
            finally
            {
                if (debeCerrarConexion)
                    await connection.DisposeAsync();
            }
        }


        //public async Task<bool> EjecutarQueryAsync(string sql, object? parametros = null, IDbTransaction? transaction = null)
        //{
        //    await using var connection = CrearConexion();
        //    await connection.OpenAsync();

        //    var filasAfectadas = await connection.ExecuteAsync(sql, parametros, transaction);
        //    return filasAfectadas > 0;
        //}
    }
}
