using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MusicQuizAPI.Models;
using MusicQuizAPI.Services;
using Newtonsoft.Json;

namespace MusicQuizAPI.Helpers
{
    public static class FileHelper
    {
        private static string _rootPath;

        public static void SetRootPath(string path)
        {
            _rootPath = path;
        }

        private static void Write(string file, object data)
        {
            CheckDataFolder();
            File.WriteAllText($@"{_rootPath}/data/{file}", JsonConvert.SerializeObject(data));
        }

        public static void WriteToAnimes(List<AnimeModel> animes)
        {
            Write("animes.json", animes);
        }

        public static void WriteToTopAnimes(List<AnimeModel> topAnimes)
        {
            Write("top_animes.json", topAnimes);
        }

        private static void CheckDataFolder()
        {
            Directory.CreateDirectory($@"{_rootPath}/data/");
        }
    }
}