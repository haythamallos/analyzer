using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Analyzer.Engine.Common
{
    public class Config
    {
        public string ConnectionString { get; set; }

        public Config(IOptions<ConfigSettings> settings)
        {
            try
            {
                try
                {
                    ConnectionString = settings.Value.ConnectionString;
                }
                catch (Exception e)
                {
                    ConnectionString = null;
                }

              
            }
            catch (Exception e)
            {
            }
        }   
    }

    public class ConfigSettings
    {
        public string ConnectionString { get; set; }
    }
}