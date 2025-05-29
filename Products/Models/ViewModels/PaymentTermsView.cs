using Products.Entities;

namespace Products.DTO.ViewDTO {
    public class PaymentTermsView {
        public PaymentTermsView(int paymentTermsId, int days, string description) {
            PaymentTermsId = paymentTermsId;
            Days = days;
            Description = description;
        }
        public int PaymentTermsId { get; set; }
        public int Days { get; set; }
        public string Description { get; set; }

        public static PaymentTermsView FromEntity(PaymentTerms paymentTermsEntity) {
            return new PaymentTermsView(
                paymentTermsEntity.PaymentTermsId,
                paymentTermsEntity.Days,
                paymentTermsEntity.Description
            );
        }

        public static List<PaymentTermsView> FromEntity(List<PaymentTerms> paymentTermsEntities) {
            return paymentTermsEntities.Select(pt => FromEntity(pt)).ToList();
        }
    }
}
