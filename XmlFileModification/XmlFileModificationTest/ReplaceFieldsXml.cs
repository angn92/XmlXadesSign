using System.Xml;

namespace XmlFileModificationTest
{
    public static class ReplaceFieldsXml
    {
        public static void ModifyXmlFile(XmlDocument xmlDocument, string pathToFile)
        {
            // Here we can modify many fields in given xml file
            var challenge = xmlDocument.GetElementsByTagName("Challenge").Item(0);
            challenge.FirstChild.Value = "test";

            xmlDocument.Save(pathToFile);
        }
    }
}
