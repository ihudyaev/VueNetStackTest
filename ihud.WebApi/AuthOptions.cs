using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;


namespace ihud.WebApi
{
    public class AuthOptions
    {
        public const string ISSUER = "MyAuthServer"; // издатель токена
        public const string AUDIENCE = "MyAuthClient"; // потребитель токена
        const string KEY = "dasdsaf32qrf3qfbg2q4g423r312415r252";   // ключ для шифрации
        public const int LIFETIME = 180; // время жизни токена - 3 часа
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
