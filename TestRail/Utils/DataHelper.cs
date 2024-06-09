using Newtonsoft.Json.Linq;
using RestSharp;
using Sprache;
using System.ComponentModel;
using TestRail.Models;

namespace TestRail.Utils
{
    public static class DataHelper
    {
        public static string CreateStringByLength(int length)
        {
            Random random = new Random();
            const string allowedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz0123456789!@$?_-";
            char[] chars = new char[length];

            for (int i = 0; i < length; i++)
            {
                chars[i] = allowedChars[random.Next(0, allowedChars.Length)];
            }

            return new string(chars);
        }

        public static string FormatDate(DateTime date)
        {
            return date.Month.ToString() + "/" + date.Day.ToString() + "/" + date.Year.ToString();
        }

        public static List<MilestoneModel> GetListFromContent(string content)
        {
            var jsonParsed = JObject.Parse(content);
            var jsonResult = jsonParsed["milestones"];
            List<MilestoneModel> result = new List<MilestoneModel>();
            if (jsonResult != null)
                result = jsonResult.ToObject<List<MilestoneModel>>();
            return result;
        }
    }
}
