// FertilityClinic.DTO/Responses/PaymentResponse.cs
namespace FertilityClinic.DTO.Responses
{
    public class AppointmentWithPaymentResponse: AppointmentResponse
    {
        // Payment related properties
        public string PaymentUrl { get; set; }
        public float PaymentAmount { get; set; }
        public bool PaymentRequired { get; set; }
        public string PaymentError { get; set; }
        public string PaymentStatus { get; set; } = "Pending";
    }
}
