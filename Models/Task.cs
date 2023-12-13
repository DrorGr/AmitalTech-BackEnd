using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;


namespace AmitalBE.Models
{
    [Table("Tasks")]
    public class Task
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [NotNull]
        [Column(TypeName = "VARCHAR(100)")]
        public string Subject { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? TargetDate { get; set; }

        [Required]
        [ForeignKey("UserId")]
        public int UserId { get; set; }



    }
}
