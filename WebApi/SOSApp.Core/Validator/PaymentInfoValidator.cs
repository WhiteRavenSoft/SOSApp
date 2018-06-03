using FluentValidation;
using WhiteRaven.Core.Helper;
using WhiteRaven.Core.DataObject;

namespace WhiteRaven.Core.Validator
{
    public class PaymentInfoValidator : BaseValidator<PaymentInfo>
    {
        public PaymentInfoValidator()
        {
            RuleFor(x => x.CardHolderName).NotEmpty().WithMessage("WhiteRaven.Core.Validator.PaymentInfoValidator.CardHolderName.Required");
            RuleFor(x => x.CardNumber).IsCreditCard().WithMessage("WhiteRaven.Core.Validator.PaymentInfoValidator.CardNumber.Wrong");
            RuleFor(x => x.CardCode).Matches(@"^[0-9]{3,4}$").WithMessage("WhiteRaven.Core.Validator.PaymentInfoValidator.CardCode.Wrong");
            RuleFor(x => x.ExpireMonth).NotEmpty().WithMessage("WhiteRaven.Core.Validator.PaymentInfoValidator.ExpireMonth.Required");
            RuleFor(x => x.ExpireYear).NotEmpty().WithMessage("WhiteRaven.Core.Validator.PaymentInfoValidator.ExpireYear.Required");
        }
    }
}
