using System.Collections;
using System.Resources.NetStandard;

namespace OvdVsBotWeb.ResourceManagement
{
    public class MessageTextManager
    {
        private readonly IList<ResXResourceReader> resourceManagers = new List<ResXResourceReader>(5);

        public MessageTextManager()
        {
            resourceManagers.Add(ResXResourceReader.FromFileContents("ResourceManagement/Messages.Ru.resx"));
            resourceManagers.Add(ResXResourceReader.FromFileContents("ResourceManagement/Messages.En.resx"));
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
                        rm = resourceManagers.FirstOrDefault(rm => rm.BasePath.Contains(".Ru"));
                        break;
                    case SupportedLangs.EN:
                    default:
                        rm = resourceManagers.FirstOrDefault(rm => rm.BasePath.Contains(".En"));
                        break;
                }

                if (rm == default)
                    throw new InvalidOperationException($"Can't find a resource manager for lang: {clientLanguage}!");

                foreach (DictionaryEntry entry in rm )
                {
                    if (entry.Key.Equals(key))
                    {
                        result = entry.Value?.ToString();
                        break;
                    }
                }

                return result ?? key;
            } catch (Exception ex)
            {

            }

            return key;
        }
    }
}
