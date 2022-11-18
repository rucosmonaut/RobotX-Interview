using System.ComponentModel.DataAnnotations;

namespace RobotX_Interview.Entities
{
    public class Client
    {
        [Key]
        public int Id { get; set; }

        public long CardCode { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? FinishDate { get; set; }

        public string? LastName { get; set; }

        public string? FirstName { get; set; }

        public string? SurName { get; set; }

        public string? Gender { get; set; } = default!;

        public DateTime? BirthDay { get; set; }

        public string? PhoneHome { get; set; } = default!;
                     
        public string? PhoneMobil { get; set; } = default!;

        public string? Email { get; set; } = default!;

        public string? City { get; set; } = default!;
 
        public string? Street { get; set; } = default!;

        public string? House { get; set; } = default!;

        public string? Apartment { get; set; } = default!;
    }
}