﻿namespace Telerik_Reporting_Rest_Service
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;

    static class ConfigurationHelper
    {
        public static IConfiguration ResolveConfiguration(IWebHostEnvironment environment)
        {
            var reportingConfigFileName = System.IO.Path.Combine(environment.ContentRootPath, "appsettings.json");
            return new ConfigurationBuilder()
                .AddJsonFile(reportingConfigFileName, true)
                .Build();
        }
    }
}
