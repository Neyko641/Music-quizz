using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MusicQuizAPI.Helpers;

namespace MusicQuizAPI.Services
{
    public class AnimeHostedService : IHostedService
    {
        private readonly IHostEnvironment _env;
        private readonly AnimeService _animeService;
        private readonly ILogger<AnimeHostedService> _logger;
        private Timer _timer;

        public AnimeHostedService(IHostEnvironment env, AnimeService animeService, ILogger<AnimeHostedService> logger)
        {
            _env = env;
            FileHelper.SetRootPath(_env.ContentRootPath);

            _animeService = animeService;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Every hour it updates the data of all available animes and the top animes
            _timer = new Timer(
                (o) => {
                    _logger.LogInformation("Updating 'animes.json'!");
                    Settings.AreControllersAvailable = false;

                    var animes = APIHelper.GetAnimes().Result;
                    var topAnimes = APIHelper.GetTopAnimes();

                    FileHelper.WriteToAnimes(animes);
                    FileHelper.WriteToTopAnimes(topAnimes);

                    _animeService.Update(animes, topAnimes);

                    _logger.LogInformation("'animes.json' updated successfully!");
                    Settings.AreControllersAvailable = true;
                }, null, TimeSpan.Zero, TimeSpan.FromHours(1));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}