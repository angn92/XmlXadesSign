using System.Security.Cryptography.X509Certificates;
using System.Xml;

namespace XmlFileModificationTest
{
    public class ReplaceFieldsXml
    {
        public void ModifyXmlFile(string pathToFile, X509Certificate2 certificate)
        {
            var doc = new XmlDocument();
            doc.Load(pathToFile);

            //var challenge = doc.GetElementsByTagName("Challenge").Item(0);
            //challenge.FirstChild.Value = "aaaa";

            var cert = doc.GetElementsByTagName("ds:X509Certificate").Item(0);
            cert.FirstChild.Value = Convert.ToBase64String(certificate.GetRawCertData());


            var X509IssuerName = doc.GetElementsByTagName("ds:X509IssuerName").Item(0);
            X509IssuerName.FirstChild.Value = certificate.IssuerName.ToString();

            var X509SerialNumber = doc.GetElementsByTagName("ds:X509SerialNumber").Item(0);
            X509SerialNumber.FirstChild.Value = certificate.Issuer.ToString();

            var X509SubjectName = doc.GetElementsByTagName("ds:X509SubjectName").Item(0);
            X509SubjectName.FirstChild.Value = certificate.Issuer.ToString();

        


            doc.Save(pathToFile);
        }
    }
}
