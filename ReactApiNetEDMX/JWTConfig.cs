using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ReactApiNetEDMX
{
    public class JWTConfig
    {
        public string ISSUER { get; set; } // издатель токена
        public string AUDIENCE { get; set; } // потребитель токена
        public string KEY { get; set; }    // ключ для шифрации
        public int LIFETIME { get; set; }  // время жизни токена
    }
}
