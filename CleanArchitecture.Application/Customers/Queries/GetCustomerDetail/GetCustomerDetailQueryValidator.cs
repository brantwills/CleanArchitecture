using CachingFramework.Redis.Contracts;
using FluentValidation;

namespace CleanArchitecture.Application.Customers.Queries.GetCustomerDetail
{
    public class GetCustomerDetailQueryValidator : AbstractValidator<GetCustomerDetailQuery>
    {
        public GetCustomerDetailQueryValidator(IContext redis)
        {
            RuleFor(v => v.Id).GreaterThan(0).WithMessage("Id must be greater than zero");
        }
    }
}
