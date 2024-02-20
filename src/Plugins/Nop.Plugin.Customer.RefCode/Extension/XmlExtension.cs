using System.Xml;

namespace Nop.Plugin.Client.ReferanceCode.Extension
{
    public static class XmlExtension
    {

        public static string? XmlToValue(this string xmlString)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlString);

            var valueNode = xmlDoc.SelectSingleNode("//Value");

            return valueNode?.InnerText;
        }
    }
}
