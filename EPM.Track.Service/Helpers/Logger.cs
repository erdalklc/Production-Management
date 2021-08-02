using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EPM.Track.Service.Helpers
{
    public class Logger:ILogger
    {
        IHostEnvironment _hostingEnvironment;
        public Logger(IHostEnvironment hostingEnvironment) => _hostingEnvironment = hostingEnvironment;
        public IDisposable BeginScope<TState>(TState state) => null;
        public bool IsEnabled(LogLevel logLevel) => true;
        public async void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            using (StreamWriter streamWriter = new StreamWriter("C:/log.txt", true))
            {
                await streamWriter.WriteLineAsync($"Log Level : {logLevel.ToString()} | Event ID : {eventId.Id} | Event Name : {eventId.Name} | Formatter : {formatter(state, exception)}");
                streamWriter.Close();
                await streamWriter.DisposeAsync();
            }
        }
    }
}
