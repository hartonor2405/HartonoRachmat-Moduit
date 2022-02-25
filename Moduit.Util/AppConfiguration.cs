using Microsoft.Extensions.Configuration;
using Moduit.Constant;

namespace Moduit.Util
{
    public static class AppConfiguration
    {
        /// <summary>
        /// For get appsettings.json data
        /// How to set json setting: at .AddJsonFiles section, then add/edit JsonName in Config
        /// How to use: After calling AppConfiguration.AppSetting then add [GrandParent_Key:Parent_Key:Child_Key] which data already configured in appsettings.json file.
        /// Example: AppConfiguration.AppSetting["AppCommonSettings:EnableMigration"];
        /// </summary>
        public static IConfiguration AppSetting { get; }
        static AppConfiguration()
        {
            try
            {
                AppSetting = new ConfigurationBuilder()
                        .SetBasePath(AppContext.BaseDirectory)
                        .AddJsonFile(ConfigConstant.JsonName)
                        .Build();
            }
            catch
            {
                throw new Exception("AppSetting Not Found");
            }
        }
    }
}