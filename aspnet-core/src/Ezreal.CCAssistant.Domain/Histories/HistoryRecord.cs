using System.Text.Json.Serialization;

namespace Ezreal.CCAssistant.Histories;

[Keyless]
public class HistoryRecord
{
    [JsonInclude]
    public List<HistoryItem> Items { get; private set; } = new();
    public HistoryOperation Operation { get; private set; }
    public string Operator { get; private set; }
    public DateTime OperationTime { get; private set; }
    public string Remark { get; private set; }

    private HistoryRecord() { }

    public HistoryRecord(HistoryOperation operation, [NotNull] string @operator, DateTime operationTime, string remark = null)
    {
        Check.NotNullOrWhiteSpace(@operator, nameof(@operator));
        Operation = operation;
        Operator = @operator;
        OperationTime = operationTime;
        Remark = remark;
    }

    internal HistoryRecord SetItems(List<HistoryItem> items)
    {
        Items = items ?? new();
        return this;
    }
}
