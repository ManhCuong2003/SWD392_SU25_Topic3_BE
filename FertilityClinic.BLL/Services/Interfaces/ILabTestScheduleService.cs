using FertilityClinic.DTO.Requests;
using FertilityClinic.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.BLL.Services.Interfaces
{
    public interface ILabTestScheduleService
    {
        Task<LabTestScheduleResponse> CreateLabTestScheduleAsync(LabTestScheduleRequest labTestSchedule, int doctorId, int treatmentProcessId);
        Task<bool> DeleteLabTestScheduleAsync(int id);
        Task<List<LabTestScheduleResponse>> GetAllLabTestSchedulesAsync();
        Task<LabTestScheduleResponse> GetLabTestScheduleByIdAsync(int labTestScheduleId);
        Task<LabTestScheduleResponse> UpdateLabTestScheduleAsync(int id, UpdateLabTestScheduleRequest labTestSchedule);
    }
}
