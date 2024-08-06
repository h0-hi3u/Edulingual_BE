using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edulingual.Domain.Entities
{
    [Table("user_exam")]
    public class UserExam
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Column("user_id")]
        public Guid UserId { get; set; }
        public virtual User User { get; set; } = null!;

        [Column("exam_id")]
        public Guid ExamId { get; set; }
        public virtual Exam Exam { get; set; } = null!;

        public double Score { get; set; } = 0;
        [Column("total_question_right")]
        public int TotalQuestionRight { get; set; } = 0;
        [Column("total_question_wrong")]
        public int TotalQuestionWrong { get; set; } = 0;
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow.AddHours(7);
    }
}
