using Products.DTO.InputDTO;
using Products.DTO.ViewDTO;
using Products.Models.UpdateModels;

namespace Products.Services.PaymentTerms {
    public interface IPaymentTermsService {
        List<PaymentTermsView> GetAllPaymentTerms();
        PaymentTermsView GetPaymentTermsById(int paymentTermsId);
        List<PaymentTermsView> CreatePaymentTerms(PaymentTermsInput paymentTermsInput);
        PaymentTermsView AlterPaymentTerms(int paymentTermsId,PaymentTermsUpdate paymentTermsUpdate);
    }
}
