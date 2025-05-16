using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace HRMS_FinalProject.Extensions
{
    public static class SessionExtensions
    {
        // Method to set an object in session
        public static void SetObject<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        // Method to get an object from session
        public static T GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
