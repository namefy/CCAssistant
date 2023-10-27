namespace Ezreal.CCAssistant.Customers;

public class CustomerNameExistException : BusinessException
{
    public CustomerNameExistException(string name) : base(CCAssistantDomainErrorCodes.CustomerNameExist)
    {
        WithData(nameof(name), name);
    }
}
