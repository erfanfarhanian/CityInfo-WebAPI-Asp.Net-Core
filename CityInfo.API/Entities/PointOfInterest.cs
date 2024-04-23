using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityInfo.API.Entities
{
    public class PointOfInterest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        // Relation
        [ForeignKey("CityID")]
        public City? City { get; set; }

        public int CityID { get; set; }

        public PointOfInterest(string name)
        {
            this.Name = name;
        }
    }
}
