namespace Ezreal.CCAssistant.Customers;

public class CustomerAppService : CCAssistantAppService, ICustomerAppService
{
    private readonly IAsyncQueryableExecuter _asyncExecuter;
    private readonly IRepository<Customer, Guid> _repository;
    private readonly CustomerManager _customerManager;
    private readonly IRepository<History> _historyRepository;
    private readonly HistoryManager _historyManager;

    public CustomerAppService(IAsyncQueryableExecuter asyncExecuter, CustomerManager customerManager, IRepository<Customer, Guid> repository, IRepository<History> historyRepository, HistoryManager historyManager)
    {
        _asyncExecuter = asyncExecuter;
        _customerManager = customerManager;
        _repository = repository;
        _historyRepository = historyRepository;
        _historyManager = historyManager;
    }

    public async Task<CustomerDto> GetAsync(Guid id)
    {
        var customer = await _repository.GetAsync(p => p.Id == id && !p.IsDeleted);

        return ObjectMapper.Map<Customer, CustomerDto>(customer);
    }

    public async Task<List<CustomerDto>> GetListAsync()
    {
        var queryable = await _repository.GetQueryableAsync();
        var query = queryable.Where(p => !p.IsDeleted);
        var customers = await _asyncExecuter.ToListAsync(query);

        return ObjectMapper.Map<List<Customer>, List<CustomerDto>>(customers);
    }

    public virtual async Task<CustomerDto> CreateAsync(CreateCustomerDto input)
    {
        var customer = await _customerManager.CreateCusomterAsync(input.Name, input.Gender, input.Phone);

        var record = _historyManager.CreateRecord(null, customer, CurrentUser.UserName);
        var history = await _historyManager.CreateHistoryAsync(customer.Id, record);

        await _historyRepository.InsertAsync(history);
        await _repository.InsertAsync(customer);

        return ObjectMapper.Map<Customer, CustomerDto>(customer);
    }

    public virtual async Task<CustomerDto> UpdateAsync(Guid id, UpdateCustomerDto input)
    {
        var customer = await _repository.GetAsync(p => p.Id == id && !p.IsDeleted);
        var old = customer.ShallowCopy();

        customer.Gender = input.Gender;
        await _customerManager.ChangeNameAsync(customer, input.Name);
        await _customerManager.ChangePhoneAsync(customer, input.Phone);

        var record = _historyManager.CreateRecord(old, customer, CurrentUser.UserName);
        var history = await _historyManager.CreateHistoryAsync(customer.Id, record);

        await _historyRepository.UpdateAsync(history);
        await _repository.UpdateAsync(customer);

        return ObjectMapper.Map<Customer, CustomerDto>(customer);
    }

    public async Task DeleteAsync(Guid id)
    {
        var customer = await _repository.GetAsync(p => p.Id == id && !p.IsDeleted);
        await _repository.DeleteAsync(customer);
    }
}
