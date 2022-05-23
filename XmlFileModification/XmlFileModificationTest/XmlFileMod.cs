using System.Xml;

namespace XmlFileModificationTest
{
    public class XmlFileMod
    {
        public void ReadDataXmlFile(string pathToFile)
        {

            XmlTextReader textReader = new XmlTextReader("C:\\app\\XmlFileReadWrite\\XmlFileModification\\XmlFileModificationTest\\XmlFile\\file.xml");
            //string test = "aaa";
            //string test1 = "";
            //while (textReader.Read())
            //{
            //    //Console.WriteLine(textReader.Name);
            //    //textReader.MoveToNextAttribute();
            //    //Console.WriteLine(textReader.Value);

            //    if (test1 == "Challenge")
            //    {
            //        test = textReader.Value;
            //        test1 = null;
            //    }


            //    if (textReader.Name == "Challenge" && test1 != null)
            //    {

            //        test1 = textReader.Name;

            //    }


            //}

            //Console.WriteLine(test);

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load("C:\\app\\XmlFileReadWrite\\XmlFileModification\\XmlFileModificationTest\\XmlFile\\file.xml");

            //XmlNode xmlNode = xmlDocument.DocumentElement.GetElementsByTagName("Challenge").Item(0);
            
            //Console.WriteLine(xmlNode.FirstChild.Value);

            var a = xmlDocument.GetElementsByTagName("Challenge").Item(0);
            //if(a?.FirstChild != null)
                Console.WriteLine(a?.FirstChild?.Value);

            a.FirstChild.Value = "asasasass";
            xmlDocument.Save("C:\\app\\XmlFileReadWrite\\XmlFileModification\\XmlFileModificationTest\\XmlFile\\file.xml");


            var aa = xmlDocument.GetElementsByTagName("Identifier").Item(0);

            Console.WriteLine(aa.FirstChild.FirstChild.Value);
        }
    }
}
