
namespace ECommerce.BalanceManagement.Domain.Entities.BaseModels;

public class EntityChangeLogModel
{
    public string ShemaName { get; set; }
    public string TableName { get; set; }
    public CrudOperationType CrudOperationType { get; set; }
    public string Token { get; set; }
    public Dictionary<string, string> KeyValues { get; set; } = [];
    public Dictionary<string, string> OldValues { get; set; } = [];
    public Dictionary<string, string> NewValues { get; set; } = [];
    public List<string> AffectedColumns { get; set; } = [];
}