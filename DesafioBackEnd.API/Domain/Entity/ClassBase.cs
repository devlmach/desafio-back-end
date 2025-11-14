using System.ComponentModel.DataAnnotations;

namespace DesafioBackEnd.API.Domain.Entity
{
    public abstract class ClassBase
    {
        [Key]
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
    }
}
