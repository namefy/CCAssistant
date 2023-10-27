namespace Ezreal.CCAssistant.Customers;

public class CustomerManager : DomainService
{
    private readonly IRepository<Customer, Guid> _repository;

    public CustomerManager(IRepository<Customer, Guid> repository)
    {
        _repository = repository;
    }

    public async Task<Customer> CreateCusomterAsync(string name, Gender gender, string phone)
    {
        Customer customer = new(GuidGenerator.Create(), name, gender, phone);
        await ChangeNameAsync(customer, name);
        await ChangePhoneAsync(customer, phone);
        return customer;
    }

    public async Task ChangeNameAsync([NotNull] Customer customer, [NotNull] string name)
    {
        Check.NotNull(customer, nameof(customer));
        bool existName = await _repository.AnyAsync(p => p.Id != customer.Id && p.Name == name && !p.IsDeleted);
        if (existName)
        {
            throw new CustomerNameExistException(name);
        }
        customer.ChangeName(name);
    }

    public async Task ChangePhoneAsync([NotNull] Customer customer, [NotNull] string phone)
    {
        Check.NotNull(customer, nameof(customer));
        bool existPhone = await _repository.AnyAsync(p => p.Id != customer.Id && p.Phone == phone && !p.IsDeleted);
        if (existPhone)
        {
            throw new CustomerPhoneExistException(phone);
        }
        customer.ChangePhone(phone);
    }
}
