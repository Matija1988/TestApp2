namespace ProjectService.Model
{
    /// <summary>
    /// Omotac za podatke koje saljem u API i frontend
    /// Data wrapper for info sent to API and frontend
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ServiceResponse<T>
    {
        public T? Data { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = null;
    }
}
