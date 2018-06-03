﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiteRaven.Core.Validator
{
    public static class ValidatorExtensions
    {
        public static IRuleBuilderOptions<T, string> IsCreditCard<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new CreditCardPropertyValidator());
        }
    }
}
