using System.Collections;
using System.Reflection;
using System.Resources.NetStandard;

namespace OvdVsBotWeb.ResourceManagement
{
    public class MessageTextManager
    {
        private readonly Dictionary<string, ResXResourceReader> resourceManagers = new(5);

        public MessageTextManager()
        {
            resourceManagers["ru"] = new ResXResourceReader(@".\ResourceManagement\Messages.Ru.resx");
            resourceManagers["en"] = new ResXResourceReader(@".\ResourceManagement\Messages.En.resx");
        }

        public string GetText(string key, SupportedLangs clientLanguage)
        {
            try
            {
                ResXResourceReader rm;
                string result = string.Empty;

                switch (clientLanguage)
                {
                    case SupportedLangs.RU:
                        rm = resourceManagers["ru"];
                        break;
                    case SupportedLangs.EN:
                    default:
                        rm = resourceManagers["en"];
                        break;
                }

                if (rm == default)
                    throw new InvalidOperationException($"Can't find a resource manager for lang: {clientLanguage}!");

                foreach (DictionaryEntry entry in rm )
                {
                    if (entry.Key.Equals(key))
                    {
                        result = Process(entry.Value?.ToString());
                        break;
                    }
                }

                return result ?? key;
            } catch (Exception ex)
            {

            }

            return key;
        }

        private static string Process(string strg)
            => strg.Replace(@"\n", Environment.NewLine)
                   .Replace(@"\ver", Assembly.GetExecutingAssembly().GetName().Version.ToString());
    }
}
