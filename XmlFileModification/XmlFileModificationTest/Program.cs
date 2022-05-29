// See https://aka.ms/new-console-template for more information
using System.Security.Cryptography;
using XmlFileModificationTest;

Console.WriteLine("Hello, World!");


var pathFile = "C:\\app\\XmlFileReadWrite\\XmlFileModification\\XmlFileModificationTest\\XmlFile\\file.xml";
var signedFathFile = "C:\\app\\XmlFileReadWrite\\XmlFileModification\\XmlFileModificationTest\\XmlFile\\file1.xml";

var xmlFileTest = new XmlFileMod();
RSA Key = RSA.Create();
string Certificate = "microsoft.cer";
xmlFileTest.SignXmlFile(pathFile, signedFathFile, Key, Certificate);