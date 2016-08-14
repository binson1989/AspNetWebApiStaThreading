using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetWebApiStaThreading
{
    /// <summary>
    /// Error code can be directly used as message in fluent validation
    /// can add description attribute to enum, and description can be given string values.
    /// as mentioned in sergey's page, in validation action filter we can return our custom object having validation errors.
    /// </summary>
    public class CompanyValidator : AbstractValidator<Company>
    {
        public CompanyValidator(IValidator<Address> addressValidator)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("E003");
            RuleFor(x => x.Address).SetValidator(addressValidator);
        }
    }

    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            RuleFor(x => x.City).NotEmpty().WithMessage("Name is empty....fix it!!!");
            RuleFor(x => x.State).NotEmpty().WithMessage("Name is empty....fix it!!!");
        }
    }
}
