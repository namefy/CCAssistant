namespace Ezreal.CCAssistant.Histories;

public static class HistoryConsts
{
    /// <summary>
    /// 无需记录字段
    /// </summary>
    public static readonly string[] IgnoreRecordFields = new string[]
    {
        "Id","ExtraProperties", "ConcurrencyStamp", "CreationTime", "CreatorId",
        "LastModificationTime", "LastModifierId","IsDeleted","DeleterId","DeletionTime"
    };
}
