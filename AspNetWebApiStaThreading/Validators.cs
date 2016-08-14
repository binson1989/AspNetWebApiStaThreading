using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetWebApiStaThreading
{
    public class CompanyValidator : AbstractValidator<Company>
    {
        public CompanyValidator(IValidator<Address> addressValidator)
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Address).SetValidator(addressValidator);
        }
    }

    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            RuleFor(x => x.City).NotEmpty();
            RuleFor(x => x.State).NotEmpty();
        }
    }
}
