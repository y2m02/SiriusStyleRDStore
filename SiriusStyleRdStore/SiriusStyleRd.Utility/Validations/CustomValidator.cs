using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using SiriusStyleRd.Utility.Extensions;

namespace SiriusStyleRd.Utility.Validations
{
    public class CustomValidator : ValidationAttribute
    {
        private readonly ValidationType _validationType;

        public CustomValidator(ValidationType validationType)
        {
            _validationType = validationType;
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            if (value == null || value.ToString().IsEmpty())
                return ValidationResult.Success;

            var isValid = _validationType switch
            {
                ValidationType.IdentificationCode => ValidateIdentificationCode(value.ToString()),
                ValidationType.PhoneNumber => ValidatePhoneNumber(value.ToString()),
                _ => throw new ArgumentOutOfRangeException()
            };

            return isValid
                ? ValidationResult.Success
                : new ValidationResult(FormatErrorMessage(context.DisplayName));
        }

        private static bool ValidatePhoneNumber(string phoneNumber)
        {
            return phoneNumber.Length == 10
                   && phoneNumber.Where(w => w != '0').IsNotEmpty();
        }

        private static bool ValidateIdentificationCode(string identificationCode)
        {
            if (identificationCode.Length != 11 
                || identificationCode.Where(w => w != '0').IsEmpty()) 
                return false;

            var pairDigitsSum = 0;
            var oddDigitsSum = 0;

            for (var digitPosition = 1; digitPosition <= 10; digitPosition++)
            {
                var digit = identificationCode.Substring(digitPosition - 1, 1).ToInt();

                if (digitPosition % 2 == 0)
                {
                    var pairDigit = digit * 2;

                    if (pairDigit > 9)
                        pairDigit -= 9;

                    pairDigitsSum += pairDigit;
                }
                else
                {
                    oddDigitsSum += digit;
                }
            }

            var verifierValidator = 10 - (pairDigitsSum + oddDigitsSum) % 10;
            var verifierNumber = Convert.ToInt32(identificationCode.Substring(10, 1));

            return verifierValidator == 10 && verifierNumber == 0 || verifierValidator == verifierNumber;
        }
    }
}