using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.Shared.Constants
{
    public static class RouteConstants
    {
        public const string Login = "/";
        public const string Signup = "/Signup";
        public const string DoctorSignup = "/Signup/Doctor";
        public const string Doctors = "/Doctors";
        public const string ScheduleAppointment = "Schedule/Appointment/Patient";
        public const string PatientAppointments = "/Appointments/Patient";
        public const string DoctorAppointments = "/Appointments/Doctor";
        public const string DoctorSettings = "/Doctor/Settings";
    }
}
