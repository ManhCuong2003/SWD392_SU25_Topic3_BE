using FertilityClinic.DAL.Models;
using FertilityClinic.DAL.Repositories.Interfaces;
using FertilityClinic.DTO.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DAL.Repositories.Implementations
{
    public class LabTestResultRepository : GenericRepository<LabTestResult> ,ILabTestResultRepository
    {
        private readonly FertilityClinicDbContext _context;
        public LabTestResultRepository(FertilityClinicDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> CreateLabTestResultAsync(LabTestResult labTestResult)
        {
            await _context.LabTestResults.AddAsync(labTestResult);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteLabTestResultAsync(int labTestResultId)
        {
            var labTestResult = await _context.LabTestResults.FindAsync(labTestResultId);
            if (labTestResult == null)
            {
                return false;
            }
            Remove(labTestResult);
            return await _context.SaveChangesAsync() > 0;
        }

        public Task<IEnumerable<LabTestResult>> GetAllLabTestResultsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<LabTestResult> GetLabTestResultByIdAsync(int labTestResultId)
        {
            throw new NotImplementedException();
        }
    }
}
