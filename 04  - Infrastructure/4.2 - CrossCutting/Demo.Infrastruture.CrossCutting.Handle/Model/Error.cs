namespace Demo.Infrastruture.CrossCutting.Handle.Model
{
    public class Error
    {
        public Error(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; set; }
    }
}
