namespace netCryptWebApi
{
    public class User
    {
        public string UserToken { get; set; }
        public string UserKey { get; set; }
        public string Active { get; set; }
    }


    public class TokenConfigurations
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int Seconds { get; set; }
    }
}