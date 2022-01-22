using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Tasty.Models
{
    public static class MyExtensions
    {
        public static void Add<T>(this ICollection<T> list, params T[] additions)
        {
            foreach (T addition in additions)
            {
                list.Add(addition);
            }
        }

        public static Dictionary<string, string> WithRoute(this Dictionary<string, string> dict, string key, string value)
        {
            var result = new Dictionary<string, string>(dict);
            result[key] = value;
            return result;
        }

        public static string PathAndQuery(this HttpRequest request) =>
            request.QueryString.HasValue
                ? $"{request.Path}{request.QueryString}"
                : request.Path.ToString();

        public static void SetJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetJson<T>(this ISession session, string key)
        {
            var sessionData = session.GetString(key);
            return sessionData == null
                ? default(T) : JsonConvert.DeserializeObject<T>(sessionData);
        }
    }
}
