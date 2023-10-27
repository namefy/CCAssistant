using System.Text.RegularExpressions;

namespace Ezreal.CCAssistant.Customers;

public class CreateCustomerDtoValidator : AbstractValidator<CreateCustomerDto>
{
    private readonly Regex _phoneRegex = new(@$"^\d{{{CustomerConsts.PhoneLength}}}$");

    public CreateCustomerDtoValidator()
    {
        RuleFor(p => p.Phone).Must(p =>
        {
            return _phoneRegex.Match(p).Success;
        }).WithMessage("手机号码必须为11位数字");
    }
}
