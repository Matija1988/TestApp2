namespace ProjectService.Model
{
    public record VehicleMakeDTORead(int Id, string Name, string Abrv);

    public record VehicleMakeDTOInsert(string Name, string Abrv);
}
