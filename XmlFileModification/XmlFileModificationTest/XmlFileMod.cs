using Microsoft.Xades;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Xml;

namespace XmlFileModificationTest
{
    public static class XmlFileMod
    {

        /// <summary>
        /// Sign xml file through XAdES
        /// </summary>
        /// <param name="fileName">Base file which would be signed </param>
        /// <param name="signedFileName">New created file with Xades signature</param>
        /// <param name="Key"></param>
        /// <param name="certificate">Testing certificate</param>
        public static void SignXmlFile([NotNull] X509Certificate2 certificate, XmlDocument xmlDocument)
        {
            var signedXml = new XadesSignedXml(xmlDocument);

            signedXml.SigningKey = certificate.PrivateKey;

            var reference = new Reference();
            reference.Uri = "";

            reference.AddTransform(new XmlDsigEnvelopedSignatureTransform());

            signedXml.AddReference(reference);

            // Create a new KeyInfo object.
            var keyInfo = new KeyInfo();
            keyInfo.AddClause(new KeyInfoX509Data(certificate));
            signedXml.KeyInfo = keyInfo;

            AddObjectSection(xmlDocument, certificate);

            signedXml.ComputeSignature();

            var xmlDigitalSignature = signedXml.GetXml();
        }

        public static void AddObjectSection(XmlDocument document, X509Certificate2 certificate)
        {
            string XadesPrefix = "xades";
            string SignaturePrefix = "ds";
            string SignatureNamespace = "http://www.w3.org/2000/09/xmldsig#";
            string xadesNamespaceUrl = "http://uri.etsi.org/01903/v1.3.2#";

            string SignatureId = "Signature";
            string SignaturePropertiesId = "SignedProperties";

            // <Object>
            var objectNode = document.CreateElement(SignaturePrefix, "Object", SignatureNamespace);

            // <Object><QualifyingProperties>
            var qualifyingPropertiesNode = document.CreateElement(XadesPrefix, "QualifyingProperties", xadesNamespaceUrl);
            qualifyingPropertiesNode.SetAttribute("Target", $"#{SignatureId}");
            objectNode.AppendChild(qualifyingPropertiesNode);

            // <Object><QualifyingProperties><SignedProperties>
            var signedPropertiesNode = document.CreateElement(XadesPrefix, "SignedProperties", xadesNamespaceUrl);
            signedPropertiesNode.SetAttribute("Id", SignaturePropertiesId);
            qualifyingPropertiesNode.AppendChild(signedPropertiesNode);

            var signedSignatureProperties = document.CreateElement(XadesPrefix, "SignedSignatureProperties", xadesNamespaceUrl);
            signedPropertiesNode.AppendChild(signedSignatureProperties);

            var signingCertificateNode = document.CreateElement(XadesPrefix, "SigningCertificate", xadesNamespaceUrl);
            signedSignatureProperties.AppendChild(signingCertificateNode);

            var cert = document.CreateElement(XadesPrefix, "Cert", xadesNamespaceUrl);
            signingCertificateNode.AppendChild(cert);

            var certDigest = document.CreateElement(XadesPrefix, "CertDigest", xadesNamespaceUrl);
            cert.AppendChild(certDigest);

            var digestMethod = document.CreateElement(SignaturePrefix, "DigestMethod", SignedXml.XmlDsigNamespaceUrl);
            var digestMethodAlgorithmAtribute = document.CreateAttribute("Algorithm");
            digestMethodAlgorithmAtribute.InnerText = SignedXml.XmlDsigSHA1Url;
            digestMethod.Attributes.Append(digestMethodAlgorithmAtribute);
            certDigest.AppendChild(digestMethod);

            var digestValue = document.CreateElement(SignaturePrefix, "DigestValue", SignedXml.XmlDsigNamespaceUrl);
            digestValue.InnerText = "";
            certDigest.AppendChild(digestValue);

            var issuerSerialNode = document.CreateElement(XadesPrefix, "IssuerSerial", xadesNamespaceUrl);
            cert.AppendChild(issuerSerialNode);

            var x509IssuerName = document.CreateElement(SignaturePrefix, "X509IssuerName", SignedXml.XmlDsigNamespaceUrl);
            x509IssuerName.InnerText = certificate.IssuerName.ToString();
            issuerSerialNode.AppendChild(x509IssuerName);

            var x509SerialNumber = document.CreateElement(SignaturePrefix, "X509SerialNumber", SignedXml.XmlDsigNamespaceUrl);
            x509SerialNumber.InnerText = certificate.Issuer;
            issuerSerialNode.AppendChild(x509SerialNumber);
        }
    }
}
