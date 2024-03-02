namespace BlazorWebProject.Service
{
    public class TokenService
    {
        private string _token = string.Empty;

        public string Token
        {
            get => _token; 
            set => _token = value;
        }
    }
}
