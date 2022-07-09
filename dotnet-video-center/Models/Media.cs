using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GMM7.Models
{

    [Index(nameof(Id), nameof(Name), nameof(MediaType), nameof(Description))]
    [Table("medias", Schema = "gmm7")]
    public class Media
    {

        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }
        [Required]
        [MaxLength(300)]
        public string Path { get; set; }
        [Required]
        [MaxLength(30)]
        public string MediaType { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreateTime { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime LastModifyTime { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }

        [MaxLength(10)]
        public ICollection<MediaTag>? MediaTags { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string IconUrl { get; set; }

        [MaxLength(2083)]
        public string Url { get; set; }

    }
}