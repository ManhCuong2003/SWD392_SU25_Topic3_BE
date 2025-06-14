using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityClinic.DTO.Constants
{
    public static class APIEndPoints
    {
        public const string Base = "api";

        public static class Auth
        {
            public const string Login = $"{Base}/auth/login";
            public const string Register = $"{Base}/auth/register";
        }

        public static class Debug
        {
            public const string CheckDb = $"{Base}/debug/db-check";
        }
        public static class Users
        {
            public const string BaseUser = $"{Base}/user";
            public const string Create = $"{Base}/create";
            public const string GetAll = $"{BaseUser}/all";
            public const string GetById = $"{BaseUser}/{{id}}";
            public const string Update = $"{BaseUser}/update";
            public const string Delete = $"{BaseUser}/delete/{{id}}";
            public const string HardDelete = $"{BaseUser}/delete-hard/{{userId}}";
        }

        public static class Doctor
        {
            public const string BaseUser = $"{Base}/doctor";
            public const string Create = $"{Base}/create/{{id}}";
            public const string GetAll = $"{BaseUser}/all";
            public const string GetById = $"{BaseUser}/{{id}}";
            public const string Update = $"{BaseUser}/update";
            public const string Delete = $"{BaseUser}/delete/{{id}}";
        }

        public static class Appointment
        {
            public const string BaseAppointment = $"{Base}/appointment";
            public const string Create = $"{BaseAppointment}/create";
            public const string GetAll = $"{BaseAppointment}/all";
            public const string GetById = $"{BaseAppointment}/{{id}}";
            public const string Update = $"{BaseAppointment}/update";
            public const string Delete = $"{BaseAppointment}/delete/{{id}}";
        }

        public static class AppointmentHistory
        {
            public const string BaseAppointment = $"{Base}/appointmenthistory";
            public const string GetById = $"{BaseAppointment}/{{id}}";
            public const string GetByUserId = $"{BaseAppointment}/{{userId}}";

        }

        public static class Partner
        {
            public const string BasePartner = $"{Base}/partner";
            public const string Create = $"{BasePartner}/create/{{id}}";
            public const string GetAll = $"{BasePartner}/all";
            public const string GetById = $"{BasePartner}/{{id}}";
            public const string Update = $"{BasePartner}/update";
            public const string Delete = $"{BasePartner}/delete/{{id}}";
        }
        public static class TreatmentMethod
        {
            public const string BaseTreatmentMethod = $"{Base}/treatment-method";
            public const string Create = $"{BaseTreatmentMethod}/create";
            public const string GetAll = $"{BaseTreatmentMethod}/all";
            public const string GetById = $"{BaseTreatmentMethod}/{{id}}";
            public const string Update = $"{BaseTreatmentMethod}/update";
            public const string Delete = $"{BaseTreatmentMethod}/delete/{{id}}";
        }
        public static class TreatmentProcess
        {
            public const string BaseTreatmentProcess = $"{Base}/treatment-process";
            public const string Create = $"{BaseTreatmentProcess}/create";
            public const string GetAll = $"{BaseTreatmentProcess}/all";
            public const string GetById = $"{BaseTreatmentProcess}/{{id}}";
            public const string Update = $"{BaseTreatmentProcess}/update";
            public const string Delete = $"{BaseTreatmentProcess}/delete/{{id}}";
        }
        
        
        public static class Payment
        {
            public const string BaseTreatmentMethod = $"{Base}/payment";
            public const string Create = $"{BaseTreatmentMethod}/create";
            public const string GetAll = $"{BaseTreatmentMethod}/all";
            public const string GetById = $"{BaseTreatmentMethod}/{{id}}";
            public const string Update = $"{BaseTreatmentMethod}/update";
            public const string Delete = $"{BaseTreatmentMethod}/delete/{{id}}";
        }
        public static class LabTestSchedule
        {
            public const string BaseLabTestSchedule = $"{Base}/lab-test-schedule";
            public const string Create = $"{BaseLabTestSchedule}/create/{{doctorId}}";
            public const string GetAll = $"{BaseLabTestSchedule}/all";
            public const string GetById = $"{BaseLabTestSchedule}/{{labTestScheduleId}}";
            public const string Update = $"{BaseLabTestSchedule}/update/{{id}}";
            public const string Delete = $"{BaseLabTestSchedule}/delete/{{id}}";
        }
        public static class LabTestResult
        {
            public const string BaseLabTestResult = $"{Base}/lab-test-result";
            public const string Create = $"{BaseLabTestResult}/create/{{labTestScheduleId}}";
            public const string GetAll = $"{BaseLabTestResult}/all";
            public const string GetById = $"{BaseLabTestResult}/{{labTestResultId}}";
            public const string Update = $"{BaseLabTestResult}/update/{{id}}";
            public const string Delete = $"{BaseLabTestResult}/delete/{{id}}";
        }
        public static class InseminationSchedule
        {
            public const string BaseInseminationSchedule = $"{Base}/insemination-schedule";
            public const string Create = $"{BaseInseminationSchedule}/create/{{doctorId}}";
            public const string GetAll = $"{BaseInseminationSchedule}/all";
            public const string GetById = $"{BaseInseminationSchedule}/{{inseminationScheduleId}}";
            public const string Update = $"{BaseInseminationSchedule}/update/{{id}}";
            public const string Delete = $"{BaseInseminationSchedule}/delete/{{id}}";
        }
    }
}
