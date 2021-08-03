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
    public class TrackHostedBackgroundService : IHostedService
    {
        public IServiceProvider Services { get; }
        private int number = 0; 
        private readonly ILogger<TrackHostedBackgroundService> _logger;
        public TrackHostedBackgroundService(IServiceProvider services,ILogger<TrackHostedBackgroundService> logger)
        {
            Services = services;
            _logger = logger;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                while (true)
                {
                    if (DateTime.Now.Hour == 23 || DateTime.Now.Hour == 12)
                    {

                        Interlocked.Increment(ref number);
                        _logger.LogInformation($"Print from timer number {number} : {DateTime.Now}");
                        using (var scope = Services.CreateScope())
                        {
                            var scopedProcessingService =
                                scope.ServiceProvider
                                    .GetRequiredService<ITrackService>();

                            scopedProcessingService.PROCESSLERIANALIZET(cancellationToken);
                        }
                        Interlocked.Decrement(ref number);
                    }
                    Thread.Sleep(TimeSpan.FromMinutes(20));
                }

            });

            
        }

        public Task StopAsync(CancellationToken cancellationToken)
        { 
            return Task.CompletedTask;
        }
         
    }
}
