using EduLingual.Common.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Edulingual.Domain.Entities
{ 
    public class Payment : BaseEntity<Guid>
    {
        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;


        public string PaymentMethod { get; set; } = string.Empty;

        public double Fee { get; set; } = 0;

        public Guid CourseId { get; set; }
        public Course Course { get; set; } = null!;

        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
