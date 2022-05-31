using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Xml;

namespace XmlFileModificationTest
{
    public class XmlFileMod
    {
        public void SignXmlFile(string FileName, string SignedFileName, RSA Key, string Certificate)
        {
            // Create a new XML document.
            XmlDocument doc = new XmlDocument();

            // Format the document to ignore white spaces.
            doc.PreserveWhitespace = false;

            // Load the passed XML file using it's name.
            doc.Load(new XmlTextReader(FileName));

            // Create a SignedXml object.
            var signedXml = new SignedXml(doc);

            // Add the key to the SignedXml document. 
            signedXml.SigningKey = Key;

            // Create a reference to be signed.
            var reference = new Reference();
            reference.Uri = "";

            // Add an enveloped transformation to the reference.
            XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
            reference.AddTransform(env);

            // Add the reference to the SignedXml object.
            signedXml.AddReference(reference);

            // Create a new KeyInfo object.
            KeyInfo keyInfo = new KeyInfo();

            // Load the X509 certificate.
            //X509Certificate MSCert = X509Certificate.CreateFromCertFile(Certificate);

            // Load the certificate into a KeyInfoX509Data object
            // and add it to the KeyInfo object.
            //keyInfo.AddClause(new KeyInfoX509Data(MSCert));

            // Add the KeyInfo object to the SignedXml object.
            signedXml.KeyInfo = keyInfo;

            // Compute the signature.
            signedXml.ComputeSignature();

            // Get the XML representation of the signature and save
            // it to an XmlElement object.
            XmlElement xmlDigitalSignature = signedXml.GetXml();

            AddObject(doc, xmlDigitalSignature, null);

            // Append the element to the XML document.
            doc.DocumentElement.AppendChild(doc.ImportNode(xmlDigitalSignature, true));

            if (doc.FirstChild is XmlDeclaration)
            {
                doc.RemoveChild(doc.FirstChild);
            }

            // Save the signed XML document to a file specified
            // using the passed string.
            XmlTextWriter xmltw = new XmlTextWriter(SignedFileName, new UTF8Encoding(false));
            doc.WriteTo(xmltw);
            xmltw.Close();
        }

        public void AddObject(XmlDocument document, XmlElement signatureNode, X509Certificate2 signingCertificate)
        {
            string XmlDsigSignatureProperties = "http://uri.etsi.org/01903#SignedProperties";
            string XadesProofOfApproval = "http://uri.etsi.org/01903/v1.2.2#ProofOfApproval";
            string XadesPrefix = "xades";
            string SignaturePrefix = "ds";
            string SignatureNamespace = "http://www.w3.org/2000/09/xmldsig#";
            string XadesNamespaceUrl = "http://uri.etsi.org/01903/v1.3.2#";

            string SignatureId = "Signature";
            string SignaturePropertiesId = "SignedProperties";

            // <Object>
            var objectNode = document.CreateElement(SignaturePrefix, "Object", SignatureNamespace);
            signatureNode.AppendChild(objectNode);

            // <Object><QualifyingProperties>
            var qualifyingPropertiesNode = document.CreateElement(XadesPrefix, "QualifyingProperties", XadesNamespaceUrl);
            qualifyingPropertiesNode.SetAttribute("Target", $"#{SignatureId}");
            objectNode.AppendChild(qualifyingPropertiesNode);

            // <Object><QualifyingProperties><SignedProperties>
            var signedPropertiesNode = document.CreateElement(XadesPrefix, "SignedProperties", XadesNamespaceUrl);
            signedPropertiesNode.SetAttribute("Id", SignaturePropertiesId);
            qualifyingPropertiesNode.AppendChild(signedPropertiesNode);

            var signedSignatureProperties = document.CreateElement(XadesPrefix, "SignedSignatureProperties", XadesNamespaceUrl);
            signedPropertiesNode.AppendChild(signedSignatureProperties);

            var signingCertificateNode = document.CreateElement(XadesPrefix, "SigningCertificate", XadesNamespaceUrl);
            signedSignatureProperties.AppendChild(signingCertificateNode);

            var cert = document.CreateElement(XadesPrefix, "Cert", XadesNamespaceUrl);
            signingCertificateNode.AppendChild(cert);

            var certDigest = document.CreateElement(XadesPrefix, "CertDigest", XadesNamespaceUrl);
            cert.AppendChild(certDigest);

            var digestMethod = document.CreateElement("ds", "DigestMethod", SignedXml.XmlDsigNamespaceUrl);
            var digestMethodAlgorithmAtribute = document.CreateAttribute("Algorithm");
            digestMethodAlgorithmAtribute.InnerText = SignedXml.XmlDsigSHA1Url;
            digestMethod.Attributes.Append(digestMethodAlgorithmAtribute);
            certDigest.AppendChild(digestMethod);

           
            var digestValue = document.CreateElement("ds", "DigestValue", SignedXml.XmlDsigNamespaceUrl);
            digestValue.InnerText = "";
            certDigest.AppendChild(digestValue);

            var issuerSerialNode = document.CreateElement(XadesPrefix, "IssuerSerial", XadesNamespaceUrl);
            cert.AppendChild(issuerSerialNode);

           
            var x509IssuerName = document.CreateElement("ds", "X509IssuerName", SignedXml.XmlDsigNamespaceUrl);
            x509IssuerName.InnerText = "asasa";
            issuerSerialNode.AppendChild(x509IssuerName);

           
            var x509SerialNumber = document.CreateElement("ds", "X509SerialNumber", SignedXml.XmlDsigNamespaceUrl);
            x509SerialNumber.InnerText = "232323";
            issuerSerialNode.AppendChild(x509SerialNumber);

        }
    }
}
