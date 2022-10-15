using System.Text;
using System.Text.Json;

namespace FMFT.Web.Client.Brokers.SecurityTokens
{
    public class SecurityTokenBroker : ISecurityTokenBroker
    {
        public T DeserializeJWT<T>(string jwt)
        {
            string[] chunks = jwt.Split('.');
            string data = DecodeFromBase64(chunks.ElementAtOrDefault(1));

            return JsonSerializer.Deserialize<T>(data);
        }

        private string DecodeFromBase64(string payload)
        {
            payload = payload.Replace('_', '/').Replace('-', '+');
            switch (payload.Length % 4)
            {
                case 2:
                    payload += "==";
                    break;
                case 3:
                    payload += "=";
                    break;
            }
            return Encoding.UTF8.GetString(Convert.FromBase64String(payload));
        }
    }
}
