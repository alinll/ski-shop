using Microsoft.EntityFrameworkCore;
using ski_shop.Data;

namespace ski_shop.Test
{
    internal class Connection : IDisposable
    {
        public static string connectionString = "Server=host.docker.internal;Port=5432;User Id=alina;Password=secret;Database=store";
        private bool disposedValue = false;

        public StoreContext CreateContext()
        {
            var option = new DbContextOptionsBuilder<StoreContext>().UseInMemoryDatabase(connectionString).Options;
            var context = new StoreContext(option);

            if (context != null)
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

            return context;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing) { }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
