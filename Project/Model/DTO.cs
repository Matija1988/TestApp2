using System.ComponentModel.DataAnnotations;

namespace ProjectService.Model
{
    public record VehicleMakeDTORead(int Id, string Name, string Abrv);

    public record VehicleMakeDTOInsert(
        
        [Required]
        [StringLength(80, ErrorMessage ="Maximum allowed number of characters = 80")]
        string Name,
        
        [Required]
        [StringLength(20, ErrorMessage ="Maximum allowed number of characters = 20")]
        string Abrv);
    
    public record VehicleModelDTORead(int Id, string Name, string Abrv, string Maker);

    public record VehicleModelDTOInsert(
        
        [Required]
        [StringLength(80, ErrorMessage ="Maximum allowed number of characters = 80")]
        string Name,

        [Required]
        [StringLength(20, ErrorMessage ="Maximum allowed number of characters = 20")]
        string Abrv);
}
