using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Track.Service.Helpers
{
    public class LoggerProvider : ILoggerProvider
    {
        public IHostEnvironment _hostingEnvironment;
        public LoggerProvider(IHostEnvironment hostingEnvironment) => _hostingEnvironment = hostingEnvironment;
        public ILogger CreateLogger(string categoryName) => new Logger(_hostingEnvironment);
        public void Dispose() => throw new NotImplementedException();
    }
}
