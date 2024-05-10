using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectService.Model
{
    /// <summary>
    /// POKO kojim definiram model
    /// POCO to define the model
    /// </summary>
    public class VehicleModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int MakeId { get; set; }


        [Required]
        [StringLength(80, ErrorMessage ="Maximum allowed number of characters = 80")]
        public string Name { get; set; }

        [Required]  
        [StringLength(20, ErrorMessage ="Maximum allowed number of characters = 20")]
        public string Abrv { get; set; }

        public virtual VehicleMake Make { get; set; }
    }
}
