namespace Ezreal.CCAssistant.Customers;

public class CustomerDto : EntityDto<Guid>
{
    /// <summary>
    /// 客户名称
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// 客户性别
    /// </summary>
    public Gender Gender { get; set; }
    /// <summary>
    /// 客户手机号
    /// </summary>
    public string Phone { get; set; }
}