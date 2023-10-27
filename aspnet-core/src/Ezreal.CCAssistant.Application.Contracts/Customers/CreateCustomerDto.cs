using Volo.Abp.Validation;

namespace Ezreal.CCAssistant.Customers;

public class CreateCustomerDto : IValidationEnabled
{
    /// <summary>
    /// 客户名称
    /// </summary>
    [Required]
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
