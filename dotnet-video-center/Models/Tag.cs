using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GMM7.Models
{
    [Index(nameof(Id), nameof(Name))]
    [Table("tags", Schema = "gmm7")]
    public class Tag
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(250)]
        public String Name { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreateTime { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime LastModifyTime { get; set; }
        [MaxLength(999)]
        public ICollection<MediaTag>? MediaTags { get; set; }
    }
}