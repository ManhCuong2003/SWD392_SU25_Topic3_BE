// FertilityClinic.DTO/Responses/PaymentResponse.cs
namespace FertilityClinic.DTO.Responses
{
    public class PaymentResponse
    {
        public string PaymentUrl { get; set; }
        public string OrderId { get; set; }
        public string Status { get; set; }
    }
}
