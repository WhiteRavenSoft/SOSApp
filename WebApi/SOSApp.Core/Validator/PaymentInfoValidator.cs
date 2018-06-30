using FluentValidation;
using SOSApp.Core.Helper;
using SOSApp.Core.DataObject;

namespace SOSApp.Core.Validator
{
    public class PaymentInfoValidator : BaseValidator<PaymentInfo>
    {
        public PaymentInfoValidator()
        {
            RuleFor(x => x.CardHolderName).NotEmpty().WithMessage("SOSApp.Core.Validator.PaymentInfoValidator.CardHolderName.Required");
            RuleFor(x => x.CardNumber).IsCreditCard().WithMessage("SOSApp.Core.Validator.PaymentInfoValidator.CardNumber.Wrong");
            RuleFor(x => x.CardCode).Matches(@"^[0-9]{3,4}$").WithMessage("SOSApp.Core.Validator.PaymentInfoValidator.CardCode.Wrong");
            RuleFor(x => x.ExpireMonth).NotEmpty().WithMessage("SOSApp.Core.Validator.PaymentInfoValidator.ExpireMonth.Required");
            RuleFor(x => x.ExpireYear).NotEmpty().WithMessage("SOSApp.Core.Validator.PaymentInfoValidator.ExpireYear.Required");
        }
    }
}
