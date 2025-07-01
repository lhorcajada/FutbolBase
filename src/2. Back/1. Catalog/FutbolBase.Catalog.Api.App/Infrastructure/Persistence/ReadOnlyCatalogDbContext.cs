using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace FutbolBase.Catalog.Api.App.Infrastructure.Persistence
{
    public sealed class ReadOnlyCatalogDbContext : CatalogDbContext
    {
        public ReadOnlyCatalogDbContext(DbConnection connection) : base(connection)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

    }
}
