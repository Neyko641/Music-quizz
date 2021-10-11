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
        private readonly AnimeService _animeService;
        private readonly ILogger<AnimeHostedService> _logger;
        private Timer _timer;

        public AnimeHostedService(IHostEnvironment env, AnimeService animeService, ILogger<AnimeHostedService> logger)
        {
            FileHelper.SetRootPath(env.ContentRootPath);

            _animeService = animeService;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Every hour it updates the data of all available animes and the top animes
            _timer = new Timer(
                (options) => {
                    _logger.LogInformation("Updating 'animes.json'!");
                    Settings.AreControllersAvailable = false;

                    // While updating, all controllers are unavailable
                    _animeService.Update();

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