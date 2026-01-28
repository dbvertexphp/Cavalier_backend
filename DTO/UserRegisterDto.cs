using Microsoft.AspNetCore.Http;
using System;

namespace Calavier_backend.DTO
{
    public class UserRegisterDto
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? EmpCode { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DOB { get; set; }
        public DateTime? DateOfJoining { get; set; }
        public string? CTC_Monthly { get; set; }
        public string? Designation { get; set; }
        public string? PAN_No { get; set; }
        public string? AadhaarNo { get; set; }
        public string? Mobile { get; set; }
        public int BranchId { get; set; }
        public IFormFile? Photo { get; set; }

        // --- Education & Personal (Pehle wali fields) ---
        public string? TenthYear { get; set; }
        public string? TwelfthYear { get; set; }
        public string? GraduationYear { get; set; }
        public string? PostGraduationYear { get; set; }
        public string? BloodGroup { get; set; }
        public string? SalaryAccountNo { get; set; }
        public string? MaritalStatus { get; set; }
        public string? EmergencyName { get; set; }
        public string? EmergencyRelation { get; set; }
        public string? EmergencyContactNo { get; set; }
        public string? SourceOfSelection { get; set; }

        // --- Nayi 16 Fields (Jo aapne add ki hain) ---
        public int? RoleId { get; set; }
        public string? LicenceType { get; set; }
        public string? Country { get; set; }
        public string? Department { get; set; }
        public string? Gender { get; set; }
        public string? UserType { get; set; }
        public int? ReportTo { get; set; }
        public bool MfaRegistration { get; set; }
        public string? IPAdress { get; set; }
        public string? ProfilePicture { get; set; }
        public string? Telephone { get; set; }
        public string? ProfileSelect { get; set; }
        public bool FieldVisit { get; set; }
        public string? Signature { get; set; }
        public bool AlwaysBccmyself { get; set; }
        public string? FunctionalArea { get; set; } // Agar ye miss ho rahi thi
    }
}