namespace ProjectService.Model
{
    /// <summary>
    /// Omotac za podatke koje saljem u API, sluzi za debuging i slanje poruka 
    /// koje priblizavaju gdje je sto poslo krivo
    /// Data wrapper for info sent to API, used for debinging and passing along 
    /// error messages explaining where things might have gone wrong
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ServiceResponse<T>
    {
        public T? Data { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = null;
    }
}
