using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EPM.Track.Service.Services
{
    public class TrackBackgroundService : BackgroundService
    { 
        
        private readonly ILogger<TrackBackgroundService> _logger;

        public TrackBackgroundService(IServiceProvider services,
            ILogger<TrackBackgroundService> logger)
        {
            Services = services;
            _logger = logger; 
        }

        public IServiceProvider Services { get; }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                "Consume Scoped Service Hosted Service running.");
               while (!stoppingToken.IsCancellationRequested)
                {
                    if (DateTime.Now.Hour == 23 || DateTime.Now.Hour == 17)
                    {
                        await DoWork(stoppingToken);
                        await Task.Delay(TimeSpan.FromHours(3), stoppingToken);
                    }
                    await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken);
                } 
            
        }

        private async Task DoWork(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                "Consume Scoped Service Hosted Service is working.");

            using (var scope = Services.CreateScope())
            {
                var scopedProcessingService =
                    scope.ServiceProvider
                        .GetRequiredService<ITrackService>();

                await scopedProcessingService.PROCESSLERIANALIZET(stoppingToken);
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                "Consume Scoped Service Hosted Service is stopping.");

            await base.StopAsync(stoppingToken);
        }
    }
}
