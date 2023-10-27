using System.Text.Json.Serialization;

namespace Ezreal.CCAssistant.Histories;

public class History : Entity<Guid>
{
    [JsonInclude]
    public List<HistoryRecord> HistoryRecords { get; private set; } = new();

    private History() { }

    public History(Guid id)
    {
        Id = id;
    }

    internal History AddRecord(HistoryRecord record)
    {
        HistoryRecords.Add(record);
        return this;
    }
}
