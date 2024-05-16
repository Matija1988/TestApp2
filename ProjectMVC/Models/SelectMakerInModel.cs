using ProjectService.Model;

namespace ProjectMVC.Models
{
    public class SelectMakerInModel
    {
        public List<VehicleMakeDTORead> VehicleMakeRead { get; set; }

        public List<VehicleMakeDTOInsert> VehicleMakeInsert { get; set; }
    }
}
