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
            var newPrescription = new Prescription
            {
                DoctorId = doctorId,
                AppointmentId = appointmentId,
                UserId = userId,
                PrescribedDate = DateTime.Now,
                TrackingMode = request.TrackingMode,
                TreatmentMethodId = treatmentMethodId,
            };
            var treatmentMethod = await _unitOfWork.TreatmentMethods.GetByIdAsync(treatmentMethodId);
            await _unitOfWork.Prescriptions.AddAsync(newPrescription);
            await _unitOfWork.SaveAsync();
            return new PrescriptionResponse
            {
                MethodName = treatmentMethod?.MethodName,
                TrackingMode = request.TrackingMode,
            };
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
    }
}
