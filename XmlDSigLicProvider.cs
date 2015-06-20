using System;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Xml;

namespace BlueOnion
{
    public sealed class XmlDSigLicProvider : LicenseProvider
    {
        // ---------------------------------------------------------------------
        public override License GetLicense(LicenseContext context,
            Type type, object instance, bool allowExceptions)
        {
            var attribute = (XmlDSigLicAttribute)
                Attribute.GetCustomAttribute(type, typeof (XmlDSigLicAttribute));

            if (attribute == null)
            {
                if (allowExceptions)
                {
                    throw new LicenseException(type, instance, "Attribute not found");
                }

                return null;
            }

            var csp = new RSACryptoServiceProvider();

            var key = Encoding.UTF8.GetString
                (Convert.FromBase64String(attribute.Key));

            csp.FromXmlString(key);

            var licenseFile = Environment.GetFolderPath
                (Environment.SpecialFolder.CommonApplicationData) +
                attribute.File;

            if (System.IO.File.Exists(licenseFile) == false)
            {
                licenseFile = System.Windows.Forms.Application.StartupPath +
                    attribute.File;
            }

            var xmlDoc = new XmlDocument();

            try
            {
                xmlDoc.Load(licenseFile);
            }
            catch (XmlException e)
            {
                if (allowExceptions)
                {
                    throw new LicenseException(type, instance, e.Message);
                }

                return null;
            }
            catch (System.IO.FileNotFoundException e)
            {
                if (allowExceptions)
                {
                    throw new LicenseException(type, instance, e.Message);
                }

                return null;
            }

            var signedXml = new SignedXml(xmlDoc);

            try
            {
                var signature = xmlDoc.GetElementsByTagName("Signature",
                    SignedXml.XmlDsigNamespaceUrl)[0];

                signedXml.LoadXml((XmlElement) signature);
            }
            catch (XmlException)
            {
                if (allowExceptions)
                {
                    throw new LicenseException(type, instance,
                        "Signature not found");
                }

                return null;
            }

            if (signedXml.CheckSignature(csp) == false)
            {
                if (allowExceptions)
                {
                    throw new LicenseException(type, instance,
                        "Signature failed");
                }

                return null;
            }

            return new XmlDSigLic(xmlDoc.OuterXml);
        }
    }
}