using System.Data.Common;

namespace Airsoft.Infrastructure.Persistence
{
    public class DapperTransaction : IAsyncDisposable
    {
        public DbConnection Connection { get; }
        public DbTransaction Transaction { get; }

        public DapperTransaction(
            DbConnection connection,
            DbTransaction transaction)
        {
            Connection = connection;
            Transaction = transaction;
        }

        public async Task CommitAsync()
        {
            await Transaction.CommitAsync();
        }

        public async Task RollbackAsync()
        {
            await Transaction.RollbackAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await Transaction.DisposeAsync();
            await Connection.DisposeAsync();
        }
    }
}
