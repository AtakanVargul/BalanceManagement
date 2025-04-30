using ECommerce.BalanceManagement.Domain.Entities.BaseModels;
using ECommerce.BalanceManagement.Domain.Enums;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore;
using ECommerce.BalanceManagement.Application.Common.Interfaces;
using ECommerce.BalanceManagement.Domain.Entities;

namespace ECommerce.BalanceManagement.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public DbSet<Balance> Balance { get; set; }
    public DbSet<Order> Order { get; set; }
    public DbSet<Product> Product { get; set; }

    private readonly ICurrentClientService _currentClientService;

    public ApplicationDbContext(ICurrentClientService currentClientService, DbContextOptions options) : base(options)
    {
        _currentClientService = currentClientService;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    if (string.IsNullOrWhiteSpace(entry.Entity.CreatedBy))
                    {
                        entry.Entity.CreatedBy = _currentClientService?.ClientToken ?? "MissingClientInfo";
                    }
                    entry.Entity.CreateDate = DateTimeOffset.UtcNow;
                    entry.Entity.RecordStatus = RecordStatus.Active;
                    break;

                case EntityState.Modified:
                    entry.Entity.LastModifiedBy = !string.IsNullOrWhiteSpace(entry.Entity.LastModifiedBy)
                        ? entry.Entity.LastModifiedBy
                        : _currentClientService?.ClientToken ?? "MissingClientInfo";
                    entry.Entity.UpdateDate = DateTimeOffset.UtcNow;
                    break;
            }
        }

        //var entityChangeLog = PrepareChangeTrackLogs(_currentClientService?.ClientToken ?? "MissingClientInfo");

        var result = await base.SaveChangesAsync(cancellationToken);

        //await DispatchChangeTrackLogsAsync(entityChangeLog);

        return result;
    }

    private List<EntityChangeLogModel> PrepareChangeTrackLogs(string clientToken)
    {
        var entries = new List<EntityChangeLogModel>();

        foreach (var entry in ChangeTracker.Entries<ITrackChange>())
        {
            var shemaName = GetSchema(entry);

            var entityChangeLog = new EntityChangeLogModel
            {
                ShemaName = shemaName,
                TableName = entry.Entity.GetType().Name,
                Token = clientToken
            };

            if (entry.State == EntityState.Added || entry.State == EntityState.Modified || entry.State == EntityState.Deleted)
            {
                entries.Add(entityChangeLog);

                var databaseValues = entry.GetDatabaseValues();

                foreach (var property in entry.Properties)
                {
                    string propertyName = property.Metadata.Name;

                    if (property.Metadata.IsPrimaryKey())
                    {
                        entityChangeLog.KeyValues[propertyName] = property.CurrentValue?.ToString();
                        continue;
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entityChangeLog.CrudOperationType = CrudOperationType.Create;
                            entityChangeLog.NewValues[propertyName] = property.CurrentValue?.ToString();
                            break;

                        case EntityState.Deleted:
                            entityChangeLog.CrudOperationType = CrudOperationType.Delete;
                            entityChangeLog.OldValues[propertyName] = property.OriginalValue?.ToString();
                            break;

                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                entityChangeLog.AffectedColumns.Add(propertyName);
                                entityChangeLog.CrudOperationType = CrudOperationType.Update;
                                entityChangeLog.OldValues[propertyName] = databaseValues[propertyName]?.ToString();
                                entityChangeLog.NewValues[propertyName] = property.CurrentValue?.ToString();
                            }
                            break;
                    }
                }
            }
        }

        return entries;
    }

    private async Task DispatchChangeTrackLogsAsync(List<EntityChangeLogModel> entityChangeLogList)
    {
        // Send the item to the message bus or any other service
        /*
        foreach (var item in entityChangeLogList)
        {
            
             var cancellationToken = new CancellationTokenSource(TimeSpan.FromSeconds(10));
             var busEndpoint = await _bus.GetSendEndpoint(new Uri("exchange:Log.ChangeTracker"));
             await busEndpoint.Send(item, cancellationToken.Token);
        }
        */

        await Task.CompletedTask;
    }

    private string GetSchema(EntityEntry entry)
    {
        var entity = entry.Entity;
        var schemaAnnotation = base.Model.FindEntityType(entity.GetType()).GetAnnotations()
        .FirstOrDefault(a => a.Name == "Relational:Schema");

        return schemaAnnotation == null ? "dbo" : schemaAnnotation.Value.ToString();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        ConvertEnumColumnsToString(builder);
    }

    private static void ConvertEnumColumnsToString(ModelBuilder builder)
    {
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                if (property.ClrType.IsEnum)
                {
                    var type = typeof(EnumToStringConverter<>).MakeGenericType(property.ClrType);
                    var converter = Activator.CreateInstance(type, new ConverterMappingHints()) as ValueConverter;

                    property.SetValueConverter(converter);
                    property.SetMaxLength(50);
                }
            }
        }
    }
}