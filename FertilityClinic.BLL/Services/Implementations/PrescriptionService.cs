using FertilityClinic.BLL.Services.Interfaces;
using FertilityClinic.DAL.Models;
using FertilityClinic.DAL.UnitOfWork;
using FertilityClinic.DTO.Requests;
using FertilityClinic.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.BLL.Services.Implementations
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PrescriptionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<PrescriptionResponse> CreatePrescriptionAsync(PrescriptionRequest request, int userId, int doctorId, int appointmentId, int treatmentMethodId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletePrescriptionAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<PrescriptionResponse>> GetAllPrescriptionsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PrescriptionResponse?> GetPrescriptionByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PrescriptionResponse> UpdatePrescriptionAsync(int id, PrescriptionRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
