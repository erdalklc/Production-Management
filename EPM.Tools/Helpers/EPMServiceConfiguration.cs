using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Tools.Helpers
{
    public class EPMServiceConfiguration
    {
        public static IConfiguration GetConfig()
        {
            var builder = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return builder.Build();
        }
        public static bool IsDevelopment()
        {
            var settings = GetConfig();

            return (settings["Environment"] != null && settings["Environment"] == "Development") ? true : false;
        }
    }
}
