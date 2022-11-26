using OvdVsBotWeb.ResourceManagement;

namespace OvdVsBotWeb.Utils
{
    public static class LangHelper
    {
        public static SupportedLangs GetLang(string lang)
            => lang.ToLowerInvariant() switch
            {
                "ru" => SupportedLangs.RU,
                _ => SupportedLangs.EN,
            };
    }
}
