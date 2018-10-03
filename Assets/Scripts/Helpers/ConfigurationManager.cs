using System.Linq;
using System.Xml.Linq;

namespace Assets.Scripts.Helpers
{
    public static class ConfigurationManager
    {
        private static XDocument xdocument;

        static ConfigurationManager()
        {
            xdocument = XDocument.Load("config.xml");
        }

        public static string GetComponent(string name)
        {
            return xdocument.Descendants(name).FirstOrDefault()?.Value;
        }
    }
}
