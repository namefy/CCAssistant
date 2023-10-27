namespace Ezreal.CCAssistant.Customers;

public class CustomerPhoneExistException : BusinessException
{
    public CustomerPhoneExistException(string phone) : base(CCAssistantDomainErrorCodes.CustomerPhoneExist)
    {
        WithData(nameof(phone), phone);
    }
}
