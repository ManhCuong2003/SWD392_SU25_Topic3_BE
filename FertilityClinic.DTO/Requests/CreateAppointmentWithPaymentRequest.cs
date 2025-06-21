using System.ComponentModel.DataAnnotations;
using FertilityClinic.DTO.Requests;

namespace FertilityClinic.DTO.Requests
{
    public class CreateAppointmentWithPaymentRequest
    {
        [Required]
        public AppointmentRequest AppointmentRequest { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "UserId must be greater than 0")]
        public int UserId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "DoctorId must be greater than 0")]
        public int DoctorId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "TreatmentMethodId must be greater than 0")]
        public int TreatmentMethodId { get; set; }

        [Required]
        [Range(1000, 100000000, ErrorMessage = "Payment amount must be between 1,000 and 100,000,000 VND")]
        public int PaymentAmount { get; set; }
    }

    public class PaymentCallbackRequest
    {
        public string OrderCode { get; set; }
        public int AppointmentId { get; set; }
        public bool Success { get; set; }
        public string Status { get; set; }
        public int Amount { get; set; }
        public string TransactionId { get; set; }
        public DateTime PaymentTime { get; set; }
        public string Signature { get; set; } // For PayOS verification
    }

    public class CreatePaymentRequest
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "AppointmentId must be greater than 0")]
        public int AppointmentId { get; set; }

        [Required]
        [Range(1000, 100000000, ErrorMessage = "Amount must be between 1,000 and 100,000,000 VND")]
        public int Amount { get; set; }

        public string Description { get; set; }
    }
}