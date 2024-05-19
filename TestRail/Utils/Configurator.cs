using Newtonsoft.Json;

namespace TestRail.Utils
{
    public class Configurator
    {
        public static AppSettings ReadConfiguration()
        {
            var appSettingPath = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName,
                "appsettings.json");
            var appSettingsText = File.ReadAllText(appSettingPath);
            return JsonConvert.DeserializeObject<AppSettings>(appSettingsText) ?? throw new FileNotFoundException();
        }
    }
}
