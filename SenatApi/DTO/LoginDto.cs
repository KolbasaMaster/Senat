
namespace SenatApi
{
    public class LoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }

        public LoginDto()
        {
        Username = "badmin";
        Password = "badmin";
        RememberMe = true;
        }
    public LoginDto(string Name, string Pass, bool Remember)
    {
        Username = Name;
        Password = Pass;
        Remember = RememberMe;
    }
}
    
}
