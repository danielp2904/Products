using FluentValidation;
using Products.Models.Contracts;

namespace Products.Validation {
    public class ProductValidation<T> : AbstractValidator<T> where T : IProductValue {
        public ProductValidation() {
            RuleFor(e => e.Value)
                .Must(HaveTwoDecimalPlaces)
                .WithMessage("The value must have exactly two decimal places.");
        }

        private bool HaveTwoDecimalPlaces(decimal value) {
            return (value * 100m) % 1 == 0;
        }
    }
}
