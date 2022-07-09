using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GMM7.Models
{
    [Table("MediaTags", Schema = "gmm7")]
    public class MediaTag
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime PublicationDate { get; set; }

        [Required]
        public Guid TagId { get; set; }
        public Tag Tag { get; set; }

        [Required]
        public Guid MediaId { get; set; }
        public Media Media { get; set; }
    }
}