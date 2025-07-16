using FertilityClinic.DTO.Requests;
using FertilityClinic.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.BLL.Services.Interfaces
{
    public interface ILabTestResultService
    {
        Task<List<LabTestResultResponse>> GetAllLabTestResultsAsync();
        Task<LabTestResultResponse> GetLabTestResultByIdAsync(int id);
        Task<LabTestResultResponse> CreateLabTestResultAsync(LabTestResultRequest labTestResult, int userId);
       
        Task<bool> DeleteLabTestResultAsync(int id);
    }
}
