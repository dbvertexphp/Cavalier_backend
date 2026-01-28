namespace Calavier_backend.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? EmpCode { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DOB { get; set; }
        public DateTime? DateOfJoining { get; set; }
        public DateTime? DateOfLeft { get; set; }
        public string? CTC_Monthly { get; set; }
        public string? Designation { get; set; }
        public string? FunctionalArea { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? ContactPersonal { get; set; }
        public string? Location { get; set; }
        public string? PresentAddress { get; set; }
        public string? PermanentAddress { get; set; }
        public string? PAN_No { get; set; }
        public string? AadhaarNo { get; set; }
        public string? TenthYear { get; set; }
        public string? TwelfthYear { get; set; }
        public string? GraduationYear { get; set; }
        public string? PostGraduationYear { get; set; }
        public string? VaccinationCertificatePath { get; set; }
        public string? PhotoPath { get; set; }
        public string? BloodGroup { get; set; }
        public string? SalaryAccountNo { get; set; }
        public bool InvitationLetter { get; set; }
        public bool SimIssued { get; set; }
        public string? MaritalStatus { get; set; }
        public string? EmergencyName { get; set; }
        public string? EmergencyRelation { get; set; }
        public string? EmergencyContactNo { get; set; }
        public string? SourceOfSelection { get; set; }
        public bool Status { get; set; } = true;
        public int BranchId { get; set; }

        // Models/User.cs mein ye change karein
        public int? RoleId { get; set; }
        public string? LicenceType { get; set; }
        public string? Country { get; set; }
        public string? Department { get; set; }
        public string? Gender { get; set; }
        public string? UserType { get; set; }
        public int? ReportTo { get; set; }
        public bool? MfaRegistration { get; set; } // bool? hona bahut zaroori hai
        public string? IPAdress { get; set; }
        public string? ProfilePicture { get; set; }
        public string? Telephone { get; set; }
        public string? Mobile { get; set; }
        public string? ProfileSelect { get; set; }
        public bool? FieldVisit { get; set; } // bool? hona zaroori hai
        public string? Signature { get; set; }
        public bool? AlwaysBccmyself { get; set; } // bool? hona zaroori hai
    }
}