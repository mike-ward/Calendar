// Copyright 2005 Blue Onion Software, All rights reserved.
//
using System;
using System.Xml;
using System.Text;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;

namespace BlueOnion
{
	public sealed class XmlDSigLicProvider : LicenseProvider
	{
        // ---------------------------------------------------------------------
		public XmlDSigLicProvider()
		{
		}

        // ---------------------------------------------------------------------
        public override License GetLicense(LicenseContext context, 
            Type type, object instance, bool allowExceptions)
        {
            XmlDSigLicAttribute attribute = (XmlDSigLicAttribute)
                Attribute.GetCustomAttribute(type, typeof(XmlDSigLicAttribute));

            if (attribute == null)
            {
                if (allowExceptions == true)
                {
                    throw new LicenseException(type, instance, "Attribute not found");
                }

                return null;
            }

            RSACryptoServiceProvider csp = new RSACryptoServiceProvider();

            string key = Encoding.UTF8.GetString
                (Convert.FromBase64String(attribute.Key));

            csp.FromXmlString(key);

            string licenseFile = Environment.GetFolderPath
                (Environment.SpecialFolder.CommonApplicationData) +
                attribute.File;

            if (System.IO.File.Exists(licenseFile) == false)
            {
                licenseFile = System.Windows.Forms.Application.StartupPath +
                    attribute.File;
            }

            XmlDocument xmlDoc = new XmlDocument();
            
            try
            {
                xmlDoc.Load(licenseFile);
            }

            catch (XmlException e)
            {
                if (allowExceptions == true)
                {
                    throw new LicenseException(type, instance, e.Message);
                }

                return null;
            }

            catch (System.IO.FileNotFoundException e)
            {
                if (allowExceptions == true)
                {
                    throw new LicenseException(type, instance, e.Message);
                }

                return null;
            }

            SignedXml signedXml = new SignedXml(xmlDoc);

            try
            {
                XmlNode signature = xmlDoc.GetElementsByTagName("Signature",
                    SignedXml.XmlDsigNamespaceUrl)[0];

                signedXml.LoadXml((XmlElement)signature);
            }

            catch (XmlException)
            {
                if (allowExceptions == true)
                {
                    throw new LicenseException(type, instance, 
                        "Signature not found");
                }

                return null;
            }

            if (signedXml.CheckSignature(csp) == false)
            {
                if (allowExceptions == true)
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
