namespace Ezreal.CCAssistant.Histories;

[Keyless]
public class HistoryItem
{
    public string FieldName { get; private set; }
    public string? OldValue { get; private set; }
    public string? NewValue { get; private set; }

    private HistoryItem() { }

    public HistoryItem([NotNull] string fieldName, string? oldValue, string? newValue)
    {
        Check.NotNullOrWhiteSpace(fieldName, nameof(fieldName));
        FieldName = fieldName;
        OldValue = oldValue;
        NewValue = newValue;
    }
}
