using System.Text.Json;
using System.Text.Json.Serialization;

namespace FMFT.Web.Client.Models.API.Accounts
{
    public class APIError
    {
        public string Title { get; set; }
        public Dictionary<string, string[]> Errors { get; set; }
        
        public Dictionary<string, string[]> Data => Errors;
        public string Code => Title.Substring(0, 6);
        
        
    }
}
