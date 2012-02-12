// Copyright 2005 Blue Onion Software, All rights reserved.
//
using System;
using System.Xml;
using System.Text;
using System.Reflection;
using System.Globalization;
using System.ComponentModel;

namespace BlueOnion
{
	public sealed class XmlDSigLic : License
	{
        private string licenseKey;
        private string productID;
        private string name;
        private string email;
        private DateTime expirationDate = DateTime.MinValue;
        private bool keyParseAttempted;

        // ---------------------------------------------------------------------
		public XmlDSigLic(string licenseKey)
		{
            this.licenseKey = licenseKey;
		}

        // ---------------------------------------------------------------------
        public override string LicenseKey
        {
            get
            {
                return this.licenseKey;
            }
        }

        // ---------------------------------------------------------------------
        public bool IsValid()
        {
            ParseKey();

            Assembly assembly = Assembly.GetExecutingAssembly();

            ProductIdAttribute productId = assembly.GetCustomAttributes
                (typeof(ProductIdAttribute), false)[0] as ProductIdAttribute;

            if (this.productID != productId.ProductId)
            {
                return false;
            }
            
            // Once an expiration is detected, never allow it to be true
            if (DateTime.Today >= this.expirationDate)
            {
                this.expirationDate = DateTime.MinValue;
                return false;
            }

            return true;
        }

        // ---------------------------------------------------------------------
        public string Name
        {
            get 
            { 
                ParseKey();
                return this.name; 
            }
        }

        // ---------------------------------------------------------------------
        public string Email
        {
            get 
            { 
                ParseKey();
                return this.email; 
            }
        }

        // ---------------------------------------------------------------------
        public DateTime Expires
        {
            get 
            { 
                ParseKey();
                return this.expirationDate; 
            }
        }

        // ---------------------------------------------------------------------
        private void ParseKey()
        {
            if (this.keyParseAttempted == true)
            {
                return;
            }

            this.keyParseAttempted = true;

            if (this.licenseKey.Length == 0)
            {
                return;
            }

            XmlDocument doc = new XmlDocument();

            try
            {
                doc.LoadXml(LicenseKey);
                XmlElement licenseElement = doc["license"];

                this.productID = licenseElement["product_id"].InnerText;

                this.name = licenseElement["first_name"].InnerText + " " +
                    licenseElement["last_name"].InnerText;

                this.email = licenseElement["email"].InnerText;

                byte[] expireb = Convert.FromBase64String
                    (licenseElement["d1"].InnerText);

                string expires = Encoding.UTF8.GetString(expireb);

                this.expirationDate = DateTime.ParseExact
                    (expires, "s", CultureInfo.InvariantCulture, 
                    DateTimeStyles.NoCurrentDateDefault |
                    DateTimeStyles.AllowLeadingWhite |
                    DateTimeStyles.AllowTrailingWhite);
            }

            catch (XmlException e)
            {
                Log.Error(e.Message);
                return;
            }

            catch (NullReferenceException e)
            {
                Log.Error(e.Message);
                return;
            }

            catch (FormatException e)
            {
                Log.Error(e.Message);
                return;
            }
        }

        // ---------------------------------------------------------------------
        public override void Dispose()
        {
            return;
        }
	}
}
