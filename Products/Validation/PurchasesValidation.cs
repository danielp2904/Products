using FluentValidation;
using Products.Models.Contracts;

namespace Products.Validation {
    public class PurchasesValidation<T> : AbstractValidator<T> where T : IPurchasesAmount {
        public PurchasesValidation() {
            RuleFor(e => e.Amount)
                .Must(HaveTwoDecimalPlaces)
                .WithMessage("The Amount must have exactly two decimal places.");
        }

        private bool HaveTwoDecimalPlaces(decimal value) {
            return (value * 100m) % 1 == 0;
        }
    }
}
