using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FertilityClinic.DTO.Requests;
using FertilityClinic.DTO.Responses;

namespace FertilityClinic.BLL.Services.Interfaces
{
    public interface IPayOSService
    {
        Task<PaymentResponse> CreatePaymentLinkAsync(PaymentRequest request);
        Task<PaymentStatus> CheckPaymentStatusAsync(string orderId);
    }

}
