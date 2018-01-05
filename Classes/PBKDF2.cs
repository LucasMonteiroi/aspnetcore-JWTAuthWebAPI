namespace netCryptWebApi.Classes
{
    public class PBKDF2
    {
        public string Password { get; set; }
        public string CryptPassword { get; set; }
        public string PasswordSalt { get; set; }
        public string Message { get; set; }
    }
}