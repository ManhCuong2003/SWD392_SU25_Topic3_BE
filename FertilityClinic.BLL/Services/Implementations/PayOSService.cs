using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using FertilityClinic.BLL.Services.Interfaces;
using FertilityClinic.DTO.Config;
using FertilityClinic.DTO.Requests;
using FertilityClinic.DTO.Responses;
using Microsoft.Extensions.Options;

namespace FertilityClinic.BLL.Services.Implementations
{
    public class PayOSService : IPayOSService
    {
        private readonly HttpClient _httpClient;
        private readonly PayOSSettings _settings;

        public PayOSService(HttpClient httpClient, IOptions<PayOSSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings.Value;
            _httpClient.BaseAddress = new Uri(_settings.BaseUrl);
            _httpClient.DefaultRequestHeaders.Add("x-client-id", _settings.ClientId);
            _httpClient.DefaultRequestHeaders.Add("x-api-key", _settings.ApiKey);
        }

        public async Task<PaymentResponse> CreatePaymentLinkAsync(PaymentRequest request)
        {
            var payload = new
            {
                orderCode = request.OrderId,
                amount = request.Amount,
                description = request.Description,
                cancelUrl = request.CancelUrl,
                returnUrl = request.ReturnUrl,
                signature = GenerateSignature(request)
            };

            var response = await _httpClient.PostAsJsonAsync("/v1/payment-requests", payload);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<PaymentResponse>();
        }

        public async Task<PaymentStatus> CheckPaymentStatusAsync(string orderId)
        {
            var response = await _httpClient.GetAsync($"/v1/payment-requests/{orderId}");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<PaymentStatus>();
        }

        private string GenerateSignature(PaymentRequest request)
        {
            var dataStr = $"{request.OrderId}{request.Amount}{_settings.ChecksumKey}";
            using var sha256 = SHA256.Create();
            var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(dataStr));
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }
    }

}
