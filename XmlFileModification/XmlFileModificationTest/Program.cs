// See https://aka.ms/new-console-template for more information

using System.Security.Cryptography.X509Certificates;
using XmlFileModificationTest;

var replaceFieldXml = new ReplaceFieldsXml();

//Get test certificate
var cert = new X509Certificate2("C:\\app\\cert\\domain.pfx", "123456789");

replaceFieldXml.ModifyXmlFile("C:\\app\\XmlFileReadWrite\\XmlFileModification\\XmlFileModificationTest\\XmlFile\\test.xml", cert);