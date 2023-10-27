namespace Ezreal.CCAssistant.Customers;

/// <summary>
/// 客户信息
/// </summary>
public class Customer : FullAuditedAggregateRoot<Guid>
{
    /// <summary>
    /// 客户名称
    /// </summary>
    public string Name { get; private set; }
    /// <summary>
    /// 客户性别
    /// </summary>
    public Gender Gender { get; set; }
    /// <summary>
    /// 客户手机号码
    /// </summary>
    public string Phone { get; private set; }
    /// <summary>
    /// 账户余额
    /// </summary>
    public decimal Amount { get; private set; }
    /// <summary>
    /// 最近消费时间
    /// </summary>
    public DateTime? LastExpenditureTime { get; private set; }

    public Customer ShallowCopy() => (Customer)MemberwiseClone();

    /// <summary>
    /// 客户信息
    /// </summary>
    private Customer() { }

    /// <summary>
    /// 客户信息
    /// </summary>
    public Customer(Guid id, [NotNull] string name, Gender gender, [NotNull] string phone) : base(id)
    {
        ChangeName(name);
        Gender = gender;
        ChangePhone(phone);
        Amount = 0;
    }

    /// <summary>
    /// 变更姓名
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    internal Customer ChangeName([NotNull] string name)
    {
        Name = Check.NotNullOrWhiteSpace(name, nameof(name), CustomerConsts.NameMaxLength);
        return this;
    }

    /// <summary>
    /// 变更手机号码
    /// </summary>
    /// <param name="phone"></param>
    /// <returns></returns>
    internal Customer ChangePhone([NotNull] string phone)
    {
        Phone = Check.NotNullOrWhiteSpace(phone, nameof(phone), CustomerConsts.PhoneLength, CustomerConsts.PhoneLength);
        return this;
    }
}
