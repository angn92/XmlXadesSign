// See https://aka.ms/new-console-template for more information
using XmlFileModificationTest;

Console.WriteLine("Hello, World!");


var pathFile = "C:\\app\\XmlFileReadWrite\\XmlFileModification\\XmlFileModificationTest\\XmlFile\\file.xml";

var xmlFileTest = new XmlFileMod();
xmlFileTest.ReadDataXmlFile(pathFile);