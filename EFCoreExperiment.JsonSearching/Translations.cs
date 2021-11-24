using System.Collections.Generic;

namespace EFCoreExperiment.JsonSearching
{
    public class Translations : Dictionary<string, Dictionary<string, string>>
    {
        public Translations Add(string language, string key, string value)
        {
            if (!ContainsKey(language))
            {
                this[language] = new Dictionary<string, string>();
            }

            this[language][key] = value;
            return this;
        }
    }
}
