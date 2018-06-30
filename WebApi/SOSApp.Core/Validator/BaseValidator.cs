using FluentValidation;

namespace SOSApp.Core.Validator
{
    public abstract class BaseValidator<T> : AbstractValidator<T> where T : class
    {

    }
}
