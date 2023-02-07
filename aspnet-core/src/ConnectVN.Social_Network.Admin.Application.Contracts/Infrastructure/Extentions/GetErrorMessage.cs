using Newtonsoft.Json;
using Slugify;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConnectVN.Social_Network.Admin.Infrastructure.Extentions
{
    public static class ManageMyFunction
    {
        private static readonly string _fileName = "Resources\\ErrorMessages-en.json";
        public static string GetMessage(this string codeMsg)
        {
            Dictionary<string, string> keyValueMsg = new();
            string pathFile = Environment.CurrentDirectory;
            string fullpath = Path.Combine(pathFile, _fileName);
            string content = File.ReadAllText(fullpath);
            keyValueMsg = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);
            string msg = keyValueMsg.ToList().FirstOrDefault(x => x.Key.Equals(codeMsg)).Value;
            return msg ?? "message is null";
        }
        public static string ToSlug(this string name)
        {
            SlugHelper slugify = new();
            name = name.Replace('Đ', 'D');
            name = name.Replace('đ', 'd');
            return slugify.GenerateSlug(name);
        }
    }
}
