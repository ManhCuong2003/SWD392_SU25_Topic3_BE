// FertilityClinic.DTO/Requests/AppointmentPaymentRequest.cs
namespace FertilityClinic.DTO.Requests
{
    public class PaymentStatus
    {
        public string OrderId { get; set; }
        public string Status { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionTime { get; set; }
    }
}
