using FertilityClinic.DAL.Models;
using FertilityClinic.DTO.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DAL.Repositories.Interfaces
{
    public interface ILabTestResultRepository : IGenericRepository<LabTestResult>
    {
         Task<bool> CreateLabTestResultAsync(LabTestResult labTestResult);
         Task<IEnumerable<LabTestResult>> GetAllLabTestResultsAsync();
         Task<LabTestResult> GetLabTestResultByIdAsync(int labTestResultId);
            //Task<LabTestResultRequest> UpdateLabTestResultAsync(LabTestResultRequest labTestResultRequest);
         Task<bool> DeleteLabTestResultAsync(int labTestResultId);
        Task<IEnumerable<LabTestResult>> GetLabTestResultsByUserIdAsync(int userId);

        Task<LabTestResult?> GetByUserIdAsync(int userId);

    }
}
