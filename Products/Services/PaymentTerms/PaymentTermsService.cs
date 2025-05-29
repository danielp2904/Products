using Microsoft.EntityFrameworkCore;
using Products.Data;
using Products.DTO.InputDTO;
using Products.DTO.ViewDTO;
using Products.Entities;
using Products.Models.UpdateModels;

namespace Products.Services.PaymentTerms {
    public class PaymentTermsService : IPaymentTermsService {
        private readonly DataContext _dataContext;
        public PaymentTermsService(DataContext dataContext) {
            _dataContext = dataContext;
        }

        public PaymentTermsView AlterPaymentTerms(int paymentTermsId, PaymentTermsUpdate paymentTermsUpdate) {
            var paymentTerm = _dataContext.PaymentTerms.FirstOrDefault(e => e.PaymentTermsId == paymentTermsId);
            if (paymentTerm == null) {
                throw new KeyNotFoundException($"Payment terms with ID {paymentTermsId} not found.");
            }
            paymentTerm.Days = paymentTermsUpdate.Days;
            paymentTerm.Description = paymentTermsUpdate.Description;
            _dataContext.Update(paymentTerm);
            _dataContext.SaveChanges();
            return PaymentTermsView.FromEntity(paymentTerm);
        }

        public List<PaymentTermsView> CreatePaymentTerms(PaymentTermsInput paymentTermsInput) {
            var paymentTerms = new Entities.PaymentTerms {
                Days = paymentTermsInput.Days,
                Description = paymentTermsInput.Description,
                PurchasesId = paymentTermsInput.PurchasesId
            };
            _dataContext.PaymentTerms.Add(paymentTerms);
            _dataContext.SaveChanges();
            var paymentTermsList = _dataContext.PaymentTerms.ToList();
            return PaymentTermsView.FromEntity(paymentTermsList);
        }

        public List<PaymentTermsView> GetAllPaymentTerms() {
            var paymentTerms = _dataContext.PaymentTerms.ToList();
            return PaymentTermsView.FromEntity(paymentTerms);
        }

        public PaymentTermsView GetPaymentTermsById(int paymentTermsId) {
            var paymentTerm = _dataContext.PaymentTerms.FirstOrDefault(e => e.PaymentTermsId == paymentTermsId);
            if (paymentTerm == null) {
                throw new KeyNotFoundException($"Payment terms with ID {paymentTermsId} not found.");
            }
            return PaymentTermsView.FromEntity(paymentTerm);
        }
    }
}
