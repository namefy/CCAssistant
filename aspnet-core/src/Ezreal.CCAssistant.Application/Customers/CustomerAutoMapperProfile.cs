namespace Ezreal.CCAssistant.Customers;

public class CustomerAutoMapperProfile : Profile
{
    public CustomerAutoMapperProfile()
    {
        CreateMap<Customer, CustomerDto>();
    }
}
