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
            public const string TestDoctor = $"{BaseUser}/doctorAll";
            public const string Delete = $"{BaseUser}/delete/{{id}}";
            public const string HardDelete = $"{BaseUser}/delete-hard/{{userId}}";
            public const string GetByEmail = $"{BaseUser}/by-email/{{email}}";
            public const string GetAllPatients = $"{BaseUser}/patients/all";

        }

        public static class Doctor
        {
            public const string BaseDoctor = $"{Base}/doctor";
            public const string Create = $"{Base}/create/{{id}}";
            public const string GetAll = $"{BaseDoctor}/all";
            public const string GetById = $"{BaseDoctor}/{{id}}";
            public const string Update = $"{BaseDoctor}/update";
            public const string Delete = $"{BaseDoctor}/delete/{{id}}";
        }


        public static class Appointment
        {
            public const string Create = "api/appointments/create";
            public const string CreateWithPayment = "api/appointments/create-with-payment";
            public const string CreatePayment = "api/appointments/{appointmentId}/create-payment";
            public const string PaymentCallback = "api/appointments/payment-callback";
            public const string Delete = "api/appointments/{id}";
            public const string GetAll = "api/appointments";
            public const string GetById = "api/appointments/{id}";
            public const string Update = "api/appointments/{id}";
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
        public static class InjectionSchedule
        {
            public const string BaseInjectionSchedule = $"{Base}/injectionSchedule";
            public const string Create = $"{BaseInjectionSchedule}/create/{{doctorId}}";
            public const string GetAll = $"{BaseInjectionSchedule}/all";
            public const string GetById = $"{BaseInjectionSchedule}/{{injectionScheduleId}}";
            public const string Update = $"{BaseInjectionSchedule}/update/{{id}}";
            public const string Delete = $"{BaseInjectionSchedule}/delete/{{id}}";
        }
        public static class LabTestResult
        {
            public const string BaseLabTestResult = $"{Base}/lab-test-result";
            public const string Create = $"{BaseLabTestResult}/create/{{labTestScheduleId}}";
            public const string GetAll = $"{BaseLabTestResult}/all";
            public const string GetById = $"{BaseLabTestResult}/{{labTestResultId}}";
            public const string Update = $"{BaseLabTestResult}/update/{{id}}";
            public const string Delete = $"{BaseLabTestResult}/delete/{{id}}";
            public const string GetByUserId = $"{BaseLabTestResult}/user/{{userId}}";

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

        public static class InseminationResult
        {
            public const string BaseInseminationResult = $"{Base}/insemination-result";
            public const string Create = $"{BaseInseminationResult}/create/{{inseminationScheduleId}}";
            public const string GetAll = $"{BaseInseminationResult}/all";
            public const string GetById = $"{BaseInseminationResult}/{{inseminationResultId}}";
        }

        public static class Pills
        {
            public const string BasePills = $"{Base}/pills";
            public const string Create = $"{BasePills}/create";
            public const string GetAll = $"{BasePills}/all";
            public const string GetById = $"{BasePills}/{{id}}";
            public const string Update = $"{BasePills}/update";
            public const string Delete = $"{BasePills}/delete/{{id}}";
        }
        public static class Prescription
        {
            public const string BasePrescription = $"{Base}/prescription";
            public const string Create = $"{BasePrescription}/create";
            public const string GetAll = $"{BasePrescription}/all";
            public const string GetById = $"{BasePrescription}/{{id}}";
            public const string Update = $"{BasePrescription}/update";
            public const string Delete = $"{BasePrescription}/delete/{{id}}";
            public const string GetByUserId = $"{BasePrescription}/user/{{userId}}";

        }

        public static class Review
        {
            public const string BaseReview = $"{Base}/review";
            public const string Create = $"{BaseReview}/create";
            public const string GetAll = $"{BaseReview}/all";
            public const string GetById = $"{BaseReview}/{{id}}";
            public const string Update = $"{BaseReview}/update";
            public const string Delete = $"{BaseReview}/delete/{{id}}";
        }

        public static class Notification
        {
            public const string BaseNotification = $"{Base}/notification";
            public const string Create = $"{BaseNotification}/create";
            public const string GetAll = $"{BaseNotification}/all";
            public const string GetById = $"{BaseNotification}/{{id}}";
            public const string Update = $"{BaseNotification}/update";
            public const string Delete = $"{BaseNotification}/delete/{{id}}";
        }
        public static class Blog 
        {
            public const string BaseBlog = $"{Base}/blog";
            public const string Create = $"{BaseBlog}/create";
            public const string GetAll = $"{BaseBlog}/all";
            public const string GetById = $"{BaseBlog}/{{id}}";
            public const string Update = $"{BaseBlog}/update";
            public const string Delete = $"{BaseBlog}/delete/{{id}}";
        }
        public static class Injection
        {
            public const string BaseInjection = $"{Base}/injection";
            public const string Create = $"{BaseInjection}/create";
            public const string GetAll = $"{BaseInjection}/all";
            public const string GetById = $"{BaseInjection}/{{id}}";
            public const string Update = $"{BaseInjection}/update";
            public const string Delete = $"{BaseInjection}/delete/{{id}}";
        }
    }
}