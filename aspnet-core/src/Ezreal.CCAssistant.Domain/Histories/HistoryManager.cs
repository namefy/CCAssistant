using System.Reflection;
using System.Text.Json.Serialization;

namespace Ezreal.CCAssistant.Histories;

public class HistoryManager : DomainService
{
    private readonly IRepository<History> _repository;

    public HistoryManager(IRepository<History> repository)
    {
        _repository = repository;
    }

    public HistoryRecord CreateRecord<T>(T? oldObj, T? newObj, string @operator, string? remark = null) where T : class
    {
        if (oldObj is null && newObj is null)
        {
            throw new ArgumentNullException();
        }        

        HistoryOperation operation = HistoryOperation.Edit;
        if (oldObj is null)
        {
            operation = HistoryOperation.Create;
        }
        if (newObj is null)
        {
            operation = HistoryOperation.Delete;
        }

        HistoryRecord record = new(operation, @operator, DateTime.Now, remark);
        PropertyInfo[] properties = typeof(T).GetProperties();

        List<HistoryItem> items = new();
        foreach (PropertyInfo propertyInfo in properties)
        {
            if (HistoryConsts.IgnoreRecordFields.Contains(propertyInfo.Name)) continue;
            if (propertyInfo.GetCustomAttribute<JsonIgnoreAttribute>() is not null) continue;
            string? oldValue = oldObj is null ? null : propertyInfo.GetValue(oldObj)?.ToString();
            string? newValue = newObj is null ? null : propertyInfo.GetValue(newObj)?.ToString();
            if (oldValue != newValue)
            {
                items.Add(new HistoryItem(propertyInfo.Name, oldValue, newValue));
            }
        }
        record.SetItems(items);

        return record;
    }

    public async Task<History> CreateHistoryAsync(Guid id, HistoryRecord record)
    {
        History history = await _repository.FirstOrDefaultAsync(x => x.Id == id);
        history ??= new(id);
        history.AddRecord(record);
        return history;
    }
}
