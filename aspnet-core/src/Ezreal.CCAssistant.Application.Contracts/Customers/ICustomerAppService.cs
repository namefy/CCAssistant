namespace Ezreal.CCAssistant.Customers;

public interface ICustomerAppService
{
    Task<List<CustomerDto>> GetListAsync();
    Task<CustomerDto> GetAsync(Guid id);
    Task<CustomerDto> CreateAsync(CreateCustomerDto input);
    Task<CustomerDto> UpdateAsync(Guid id, UpdateCustomerDto input);
    Task DeleteAsync(Guid id);
}