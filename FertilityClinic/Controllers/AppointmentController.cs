using FertilityClinic.BLL.Services.Interfaces;
using FertilityClinic.DTO.Constants;
using FertilityClinic.DTO.Requests;
using Microsoft.AspNetCore.Mvc;

namespace FertilityClinic.Controllers
{
    [ApiController]
    public class AppointmentController : Controller
    {
        private readonly IAppoimentService _appointmentService;
        public AppointmentController(IAppoimentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpPost(APIEndPoints.Appointment.Create)]
        public async Task<IActionResult> CreateAppointment([FromBody] AppointmentRequest appointmentRequest, int userId, int doctorId, int treatmentMethodid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var appointment = await _appointmentService.CreateAppointmentAsync(appointmentRequest, userId, doctorId, treatmentMethodid);
                return Ok(appointment);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating appointment: {ex.Message}");
            }
        }

        [HttpDelete(APIEndPoints.Appointment.Delete)]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid appointment ID");
            }
            try
            {
                var result = await _appointmentService.DeleteAppointmentAsync(id);
                if (result)
                {
                    return Ok($"Appointment with ID {id} deleted successfully.");
                }
                else
                {
                    return NotFound($"Appointment with ID {id} not found.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error deleting appointment: {ex.Message}");
            }
        }

        [HttpGet(APIEndPoints.Appointment.GetAll)]
        public async Task<IActionResult> GetAllAppointments()
        {
            try
            {
                var appointments = await _appointmentService.GetAllAppointmentsAsync();
                return Ok(appointments);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving appointments: {ex.Message}");
            }
        }

        [HttpGet(APIEndPoints.Appointment.GetById)]
        public async Task<IActionResult> GetAppointmentById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid appointment ID");
            }
            try
            {
                var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
                if (appointment != null)
                {
                    return Ok(appointment);
                }
                else
                {
                    return NotFound($"Appointment with ID {id} not found.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving appointment: {ex.Message}");
            }
        }

        [HttpGet(APIEndPoints.Appointment.Update)]
        public async Task<IActionResult> UpdateAppointment(int id, [FromBody] UpdateAppointmentRequest appointmentRequest)
        {
            if (id <= 0 || !ModelState.IsValid)
            {
                return BadRequest("Invalid appointment ID or request data");
            }
            try
            {
                var updatedAppointment = await _appointmentService.UpdateAppointmentAsync(id, appointmentRequest);
                return Ok(updatedAppointment);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error updating appointment: {ex.Message}");
            }
        }
    }
}
