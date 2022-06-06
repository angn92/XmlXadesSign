//Read Xml file

using System.Xml;
using XmlFileModificationTest;

//Variable
var pathToFile = @"C:\app\XmlFileReadWrite\XmlFileModification\XmlFileModificationTest\XmlFile\file.xml";

// Load xml file
var xmlDocument = new XmlDocument();
xmlDocument.Load(pathToFile);

ReplaceFieldsXml.ModifyXmlFile(xmlDocument, pathToFile);